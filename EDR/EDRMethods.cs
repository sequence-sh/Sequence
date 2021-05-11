using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Errors;
using Reductech.EDR.Core.Internal.Parser;
using Reductech.EDR.Core.Internal.Serialization;
using TypeReference = Reductech.EDR.Core.Internal.TypeReference;

namespace Reductech.EDR
{

/// <summary>
/// EDR methods to be run in the console.
/// </summary>
[Command(Description = "Executes EDR Sequences")]
public class EDRMethods
{
    private readonly ILogger<EDRMethods> _logger;
    private readonly IFileSystem _fileSystem;
    private readonly IConnectorManager _connectorManager;

    private readonly SCLSettings _settings;

    /// <summary>
    /// External context without injected contexts
    /// </summary>
    private readonly IExternalContext _baseExternalContext;

    private const int Success = 0;
    private const int Failure = 1;

    /// <summary>
    /// Instantiate EDRMethods using the default IExternalContext provider.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="fileSystem">The file system</param>
    /// <param name="connectorManager"></param>
    public EDRMethods(
        ILogger<EDRMethods> logger,
        SCLSettings settings,
        IFileSystem fileSystem,
        IConnectorManager connectorManager)
        : this(
            logger,
            settings,
            new ExternalContext(
                ExternalContext.Default.ExternalProcessRunner,
                ExternalContext.Default.Console
            ),
            fileSystem,
            connectorManager
        ) { }

    /// <summary>
    /// Instantiate EDRMethods using the specified IExternalContext provider.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="baseExternalContext">The external context. Can be mocked for testing.</param>
    /// <param name="fileSystem">The file system</param>
    /// <param name="connectorManager"></param>
    public EDRMethods(
        ILogger<EDRMethods> logger,
        SCLSettings settings,
        IExternalContext baseExternalContext,
        IFileSystem fileSystem,
        IConnectorManager connectorManager)
    {
        _logger              = logger;
        _baseExternalContext = baseExternalContext;
        _fileSystem          = fileSystem;
        _settings            = settings;
        _connectorManager    = connectorManager;
    }

    /// <summary>
    /// Run a Sequence of Steps defined using the Sequence Configuration Language (SCL) either from file or from a string.
    /// </summary>
    [DefaultMethod]
    [Command(
        Name = "run",
        Description =
            "Run a Sequence of Steps defined using the Sequence Configuration Language (SCL)"
    )]
    public Task<int> Execute(
        CancellationToken cancellationToken,
        [Option(
            LongName    = "sequence",
            ShortName   = "s",
            Description = "Run a Sequence from a string."
        )]
        string? scl = null,
        [Option(
            LongName    = "path",
            ShortName   = "p",
            Description = "Run a Sequence defined in a file."
        )]
        string? path = null,
        [Option(
            LongName    = "build",
            ShortName   = "b",
            Description = "If set, build the SCL but do not execute it."
        )]
        bool validate = false) => ExecuteAbstractAsync(scl, path, validate, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    [SubCommand]
    public ConnectorCommand Connector { get; set; }

    /// <summary>
    /// Executes SCL from either a file or a string.
    /// </summary>
    private async Task<int> ExecuteAbstractAsync(
        string? scl,
        string? path,
        bool validate,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(scl))
            return await ExecuteFromStringAsync(scl, validate, cancellationToken);
        else if (!string.IsNullOrWhiteSpace(path))
            return await ExecuteFromPathAsync(path, validate, cancellationToken);
        else
            throw new ArgumentException("Please provide a Sequence string (-s) or path (-p).");
    }

    private async Task<ConnectorData[]> GetConnectors()
    {
        if (!await _connectorManager.Verify())
            throw new InvalidConfigurationException("Could not validate installed connectors.");

        var connectors = _connectorManager.List()
            .Select(c => c.data)
            .Where(c => c.ConnectorSettings.Enable)
            .ToArray();

        if (connectors.GroupBy(c => c.ConnectorSettings.Id).Any(g => g.Count() > 1))
            throw new InvalidConfigurationException(
                "More than one connector configuration with the same id."
            );

        return connectors;
    }

    private async Task<int> ExecuteFromPathAsync(
        string path,
        bool validate,
        CancellationToken cancellationToken)
    {
        var text = await _fileSystem.File.ReadAllTextAsync(path, cancellationToken);

        var connectors       = await GetConnectors();
        var stepFactoryStore = StepFactoryStore.Create(connectors);

        if (validate)
            return ValidateSCL(text, stepFactoryStore);

        var externalContext = GetInjectedExternalContext(stepFactoryStore, _baseExternalContext);

        if (externalContext.IsFailure)
        {
            SCLRunner.LogError(
                _logger,
                externalContext.Error.WithLocation(ErrorLocation.EmptyLocation)
            );

            return Failure;
        }

        var runner = new SCLRunner(
            _settings,
            _logger,
            stepFactoryStore,
            externalContext.Value
        );

        var r = await runner.RunSequence(
            text,
            new Dictionary<string, object>()
            {
                { SCLRunner.SequenceIdName, Guid.NewGuid() }, { SCLRunner.SCLPathName, path }
            },
            cancellationToken
        );

        if (!r.IsFailure)
            return Success;

        SCLRunner.LogError(_logger, r.Error);
        return Failure;
    }

    private Result<IExternalContext, IErrorBuilder> GetInjectedExternalContext(
        StepFactoryStore sfs,
        IExternalContext baseExternalContext)
    {
        var injectedContextsResult = sfs.TryGetInjectedContexts(_settings);

        if (injectedContextsResult.IsFailure)
            return injectedContextsResult.ConvertFailure<IExternalContext>();

        var externalContext = new ExternalContext(
            baseExternalContext.ExternalProcessRunner,
            baseExternalContext.Console,
            injectedContextsResult.Value
        );

        return externalContext;
    }

    private async Task<int> ExecuteFromStringAsync(
        string scl,
        bool validate,
        CancellationToken cancellationToken)
    {
        var connectors       = await GetConnectors();
        var stepFactoryStore = StepFactoryStore.Create(connectors);

        if (validate)
            return ValidateSCL(scl, stepFactoryStore);

        var externalContext = GetInjectedExternalContext(
            stepFactoryStore,
            _baseExternalContext
        );

        if (externalContext.IsFailure)
        {
            SCLRunner.LogError(
                _logger,
                externalContext.Error.WithLocation(ErrorLocation.EmptyLocation)
            );

            return Failure;
        }

        var runner = new SCLRunner(
            _settings,
            _logger,
            stepFactoryStore,
            externalContext.Value
        );

        var r = await runner.RunSequenceFromTextAsync(
            scl,
            new Dictionary<string, object>() { { SCLRunner.SequenceIdName, Guid.NewGuid() } },
            cancellationToken
        );

        if (!r.IsFailure)
            return 0;

        SCLRunner.LogError(_logger, r.Error);
        return 1;
    }

    private int ValidateSCL(string scl, StepFactoryStore stepFactoryStore)
    {
        var stepResult = SCLParsing.TryParseStep(scl)
            .Bind(x => x.TryFreeze(TypeReference.Any.Instance, stepFactoryStore))
            .Map(SCLRunner.ConvertToUnitStep);

        if (stepResult.IsFailure)
        {
            SCLRunner.LogError(_logger, stepResult.Error);
            return Failure; //indicate failure
        }

        _logger.LogInformation("Build Successful");
        return Success;
    }
}

}
