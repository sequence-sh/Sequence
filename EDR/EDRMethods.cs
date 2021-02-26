using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Connectors.Sql.Steps;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Parser;
using Reductech.EDR.Core.Internal.Serialization;

#if INCLUDE_PWSH
using Reductech.EDR.Connectors.Pwsh;
#endif

namespace Reductech.EDR
{

/// <summary>
/// EDR methods to be run in the console.
/// </summary>
[Command(Description = "Executes EDR Sequences")]
public class EDRMethods
{
    private readonly ILogger<EDRMethods> _logger;

    private readonly SCLSettings _settings;
    private readonly IExternalContext _externalContext;

    /// <summary>
    /// Instantiate EDRMethods using the default IFileSystem provider.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="settings">Configuration for connectors.</param>
    public EDRMethods(ILogger<EDRMethods> logger, SCLSettings settings)
        : this(
            logger,
            settings,
            new ExternalContext(
                ExternalContext.Default.FileSystemHelper,
                ExternalContext.Default.ExternalProcessRunner,
                ExternalContext.Default.Console,
                (Connectors.Sql.DbConnectionFactory.DbConnectionName,
                 Connectors.Sql.DbConnectionFactory.Instance) //SQL database stuff
            )
        ) { }

    /// <summary>
    /// Instantiate EDRMethods using the specified IFileSystem provider.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="settings">Configuration</param>
    /// <param name="externalContext">The external context. Can be mocked for testing.</param>
    public EDRMethods(
        ILogger<EDRMethods> logger,
        SCLSettings settings,
        IExternalContext externalContext)
    {
        _logger          = logger;
        _externalContext = externalContext;
        _settings        = settings;
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
    public Task Execute(
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
    /// Executes SCL from either a file or a string.
    /// </summary>
    private async Task ExecuteAbstractAsync(
        string? scl,
        string? path,
        bool validate,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(scl))
            await ExecuteFromStringAsync(scl, validate, cancellationToken);
        else if (!string.IsNullOrWhiteSpace(path))
            await ExecuteFromPathAsync(path, validate, cancellationToken);
        else
            throw new ArgumentException("Please provide a Sequence string (-s) or path (-p).");
    }

    private async Task ExecuteFromPathAsync(
        string path,
        bool validate,
        CancellationToken cancellationToken)
    {
        var text =
            await _externalContext.FileSystemHelper.File.ReadAllTextAsync(path, cancellationToken);

        var stepFactoryStore = StepFactoryStore.CreateUsingReflection(
            ConnectorTypes.Append(typeof(IStep)).ToArray()
        );

        var runner = new SCLRunner(_settings, _logger, stepFactoryStore, _externalContext);

        var r = await runner.RunSequence(
            text,
            new Dictionary<string, object>()
            {
                { SCLRunner.SequenceIdName, Guid.NewGuid() }, { SCLRunner.SCLPathName, path }
            },
            cancellationToken
        );

        if (r.IsFailure)
            SCLRunner.LogError(_logger, r.Error);
    }

    private async Task ExecuteFromStringAsync(
        string scl,
        bool validate,
        CancellationToken cancellationToken)
    {
        var stepFactoryStore = StepFactoryStore.CreateUsingReflection(
            ConnectorTypes.Append(typeof(IStep)).ToArray()
        );

        if (validate)
        {
            ValidateSCL(scl, stepFactoryStore);
        }
        else
        {
            var runner = new SCLRunner(_settings, _logger, stepFactoryStore, _externalContext);

            var r = await runner.RunSequenceFromTextAsync(
                scl,
                new Dictionary<string, object>() { },
                cancellationToken
            );

            if (r.IsFailure)
                SCLRunner.LogError(_logger, r.Error);
        }
    }

    private void ValidateSCL(string scl, StepFactoryStore stepFactoryStore)
    {
        var stepResult = SCLParsing.ParseSequence(scl)
            .Bind(x => x.TryFreeze(stepFactoryStore))
            .Map(SCLRunner.ConvertToUnitStep);

        if (stepResult.IsFailure)
            SCLRunner.LogError(_logger, stepResult.Error);
        else
            _logger.LogInformation("Build Successful");
    }

    /// <summary>
    /// One type for each connector.
    /// </summary>
    private IEnumerable<Type> ConnectorTypes { get; } =
        new List<Type>
        {
            typeof(IRubyScriptStep), typeof(SqlInsert)
#if INCLUDE_PWSH
,typeof(PwshRunScript)
#endif
        };
}

}
