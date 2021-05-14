using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.Internal.Errors;
using Reductech.EDR.Core.Internal.Serialization;
using static Reductech.EDR.Result;

namespace Reductech.EDR
{

/// <summary>
/// Run a Sequence of Steps defined using the Sequence Configuration Language (SCL)
/// </summary>
[Command(
    Name = "run",
    //Usage       = "run <path>",
    Description = "Run a Sequence of Steps defined using the Sequence Configuration Language (SCL)"
)]
public class RunCommand
{
    private readonly ILogger<RunCommand> _logger;
    private readonly IFileSystem _fileSystem;
    private readonly IConnectorManager _connectorManager;

    /// <summary>
    /// External context without injected contexts
    /// </summary>
    private readonly IExternalContext _baseExternalContext;

    /// <summary>
    /// Instantiate EDRMethods using the default IExternalContext provider.
    /// </summary>
    public RunCommand(
        ILogger<RunCommand> logger,
        IFileSystem fileSystem,
        IConnectorManager connectorManager)
        : this(
            logger,
            fileSystem,
            connectorManager,
            new ExternalContext(
                ExternalContext.Default.ExternalProcessRunner,
                ExternalContext.Default.Console
            )
        ) { }

    /// <summary>
    /// Instantiate EDRMethods using the specified IExternalContext provider.
    /// </summary>
    public RunCommand(
        ILogger<RunCommand> logger,
        IFileSystem fileSystem,
        IConnectorManager connectorManager,
        IExternalContext baseExternalContext)
    {
        _logger              = logger;
        _baseExternalContext = baseExternalContext;
        _fileSystem          = fileSystem;
        _connectorManager    = connectorManager;
    }

    /// <summary>
    /// Shorthand for the path command.
    /// </summary>
    [DefaultMethod]
    public async Task<int> RunDefault(
        CancellationToken cancellationToken,
        [Operand(Description = "Path to the SCL file (Shorthand for using the path command)")]
        string pathToSCLFile) => await RunPath(cancellationToken, pathToSCLFile);

    /// <summary>
    /// Run a Sequence from a file
    /// </summary>
    [Command(
        Name        = "path",
        Description = "Run a Sequence from a file"
    )]
    public async Task<int> RunPath(
        CancellationToken cancellationToken,
        [Operand(Description = "Path to the SCL file")]
        string pathToSCLFile)
    {
        if (string.IsNullOrWhiteSpace(pathToSCLFile) || !_fileSystem.File.Exists(pathToSCLFile))
            throw new CommandLineArgumentException("Please provide a path to a valid SCL file.");

        var runner = await GetSCLRunner(null, cancellationToken);

        if (runner.IsFailure)
        {
            SCLRunner.LogError(_logger, runner.Error.WithLocation(ErrorLocation.EmptyLocation));

            return Failure;
        }

        var r = await runner.Value.RunSequenceFromPathAsync(
            pathToSCLFile,
            new Dictionary<string, object>()
            {
                { SCLRunner.SequenceIdName, Guid.NewGuid() },
                { SCLRunner.SCLPathName, pathToSCLFile }
            },
            cancellationToken
        );

        if (r.IsSuccess)
            return Success;

        SCLRunner.LogError(_logger, r.Error);
        return Failure;
    }

    /// <summary>
    /// Execute an in-line SCL string
    /// </summary>
    [Command(
        Name        = "scl",
        Description = "Execute an in-line SCL string"
    )]
    public async Task<int> RunSCL(
        CancellationToken cancellationToken,
        [Operand(Description = "SCL string")] string scl)
    {
        if (string.IsNullOrWhiteSpace(scl))
            throw new CommandLineArgumentException("Please provide a valid SCL string.");

        var runner = await GetSCLRunner(null, cancellationToken);

        if (runner.IsFailure)
        {
            SCLRunner.LogError(_logger, runner.Error.WithLocation(ErrorLocation.EmptyLocation));

            return Failure;
        }

        var r = await runner.Value.RunSequenceFromTextAsync(
            scl,
            new Dictionary<string, object>() { { SCLRunner.SequenceIdName, Guid.NewGuid() } },
            cancellationToken
        );

        if (r.IsSuccess)
            return Success;

        SCLRunner.LogError(_logger, r.Error);
        return Failure;
    }

    private async Task<Result<SCLRunner, IErrorBuilder>> GetSCLRunner(
        SCLSettings? sclSettings,
        CancellationToken ct = default)
    {
        var settings = sclSettings ?? SCLSettings.EmptySettings;

        var stepFactoryStore = await _connectorManager.GetStepFactoryStoreAsync(ct);

        var injectedContextsResult =
            stepFactoryStore.TryGetInjectedContexts(settings);

        if (injectedContextsResult.IsFailure)
            return injectedContextsResult.ConvertFailure<SCLRunner>();

        var externalContext = new ExternalContext(
            _baseExternalContext.ExternalProcessRunner,
            _baseExternalContext.Console,
            injectedContextsResult.Value
        );

        var runner = new SCLRunner(settings, _logger, stepFactoryStore, externalContext);

        return runner;
    }
}

}
