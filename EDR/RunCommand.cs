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
using static Reductech.EDR.Helpers;

namespace Reductech.EDR
{

/// <summary>
/// 
/// </summary>
[Command(
    Name        = "run",
    Usage       = "run <path>",
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
    /// <param name="logger">The logger.</param>
    /// <param name="fileSystem">The file system</param>
    /// <param name="connectorManager"></param>
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
    /// <param name="logger">The logger.</param>
    /// <param name="baseExternalContext">The external context. Can be mocked for testing.</param>
    /// <param name="fileSystem">The file system</param>
    /// <param name="connectorManager"></param>
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
    /// Run a Sequence of Steps defined using the Sequence Configuration Language (SCL) from file.
    /// </summary>
    [DefaultMethod]
    public async Task<int> ExecuteAsync(
        CancellationToken cancellationToken,
        [Operand(Description = "A path to an SCL file.")]
        string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !_fileSystem.File.Exists(path))
            throw new CommandLineArgumentException("Please provide a path to a valid SCL file.");

        return await RunSequenceAsync(path, cancellationToken);
    }

    internal static async Task<Result<SCLRunner, IErrorBuilder>> GetSCLRunner(
        IConnectorManager connectorManager,
        ILogger logger,
        IExternalContext baseExternalContext,
        SCLSettings? sclSettings,
        CancellationToken ct = default)
    {
        var stepFactoryStore = await connectorManager.GetStepFactoryStoreAsync(ct);

        var injectedContextsResult =
            stepFactoryStore.TryGetInjectedContexts(SCLSettings.EmptySettings);

        if (injectedContextsResult.IsFailure)
            return injectedContextsResult.ConvertFailure<SCLRunner>();

        var externalContext = new ExternalContext(
            baseExternalContext.ExternalProcessRunner,
            baseExternalContext.Console,
            injectedContextsResult.Value
        );

        var runner = new SCLRunner(
            sclSettings ?? SCLSettings.EmptySettings,
            logger,
            stepFactoryStore,
            externalContext
        );

        return runner;
    }

    private async Task<int> RunSequenceAsync(
        string path,
        CancellationToken ct = default)
    {
        var runner = await GetSCLRunner(_connectorManager, _logger, _baseExternalContext, null, ct);

        if (runner.IsFailure)
        {
            SCLRunner.LogError(_logger, runner.Error.WithLocation(ErrorLocation.EmptyLocation));

            return Failure;
        }

        var r = await runner.Value.RunSequenceFromPathAsync(
            path,
            new Dictionary<string, object>()
            {
                { SCLRunner.SequenceIdName, Guid.NewGuid() }, { SCLRunner.SCLPathName, path }
            },
            ct
        );

        if (r.IsSuccess)
            return Success;

        SCLRunner.LogError(_logger, r.Error);
        return Failure;
    }
}

}
