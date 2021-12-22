using System.IO.Abstractions;
using CommandDotNet;
using Microsoft.Extensions.Logging;
using Reductech.Sequence.ConnectorManagement.Base;
using Reductech.Sequence.Core.Abstractions;
using Reductech.Sequence.Core.Connectors;
using Reductech.Sequence.Core.Internal.Analytics;
using Reductech.Sequence.Core.Internal.Errors;
using Reductech.Sequence.Core.Internal.Serialization;
using Reductech.Sequence.Core.Util;
using static Reductech.Sequence.Result;

namespace Reductech.Sequence;

/// <summary>
/// Run a Sequence of Steps defined using the Sequence Configuration Language (SCL)
/// </summary>
[Command(
    Name = "run",
    Description = "Run a Sequence of Steps defined using the Sequence Configuration Language (SCL)"
)]
public class RunCommand
{
    private readonly ILogger<RunCommand> _logger;
    private readonly IFileSystem _fileSystem;
    private readonly IConnectorManager _connectorManager;
    private readonly IAnalyticsWriter _analyticsWriter;

    /// <summary>
    /// External context without injected contexts
    /// </summary>
    private readonly IExternalContext _baseExternalContext;

    /// <summary>
    /// Instantiate ConsoleMethods using the default IExternalContext provider.
    /// </summary>
    public RunCommand(
        ILogger<RunCommand> logger,
        IFileSystem fileSystem,
        IConnectorManager connectorManager,
        IAnalyticsWriter analyticsWriter)
        : this(
            logger,
            fileSystem,
            connectorManager,
            new ExternalContext(
                ExternalContext.Default.ExternalProcessRunner,
                ExternalContext.Default.RestClientFactory,
                ExternalContext.Default.Console
            ),
            analyticsWriter
        ) { }

    /// <summary>
    /// Instantiate ConsoleMethods using the specified IExternalContext provider.
    /// </summary>
    public RunCommand(
        ILogger<RunCommand> logger,
        IFileSystem fileSystem,
        IConnectorManager connectorManager,
        IExternalContext baseExternalContext,
        IAnalyticsWriter analyticsWriter)
    {
        _logger              = logger;
        _baseExternalContext = baseExternalContext;
        _analyticsWriter     = analyticsWriter;
        _fileSystem          = fileSystem;
        _connectorManager    = connectorManager;
    }

    /// <summary>
    /// Shorthand for the path command.
    /// </summary>
    [DefaultCommand]
    public async Task<int> RunDefault(
        CancellationToken cancellationToken,
        [Operand(Description = "Path to the SCL file (Shorthand for using the path command)")]
        string sclPath) => await RunPath(cancellationToken, sclPath);

    /// <summary>
    /// Execute a Sequence from an SCL file
    /// </summary>
    [Command(
        Name = "path",
        Description = "Execute a Sequence from an SCL file"
    )]
    public async Task<int> RunPath(
        CancellationToken cancellationToken,
        [Operand(Description = "Path to the SCL file")]
        string pathToSCLFile)
    {
        if (string.IsNullOrWhiteSpace(pathToSCLFile) || !_fileSystem.File.Exists(pathToSCLFile))
            throw new CommandLineArgumentException("Please provide a path to a valid SCL file.");

        var text = await _fileSystem.File.ReadAllTextAsync(pathToSCLFile, cancellationToken);

        var meta = new Dictionary<string, object>
        {
            { SCLRunner.RunIdName, Guid.NewGuid() }, { SCLRunner.SCLPathName, pathToSCLFile }
        };

        return await RunSCLFromTextAsync(text, meta, cancellationToken);
    }

    /// <summary>
    /// Execute a Sequence from SCL in standard input
    /// </summary>
    [Command(
        Name = "scl",
        Description = "Execute a Sequence from an in-line SCL string"
    )]
    public async Task<int> RunSCL(
        CancellationToken cancellationToken,
        [Operand(Description = "SCL string")] string scl)
    {
        if (string.IsNullOrWhiteSpace(scl))
            throw new CommandLineArgumentException("Please provide a valid SCL string.");

        var meta = new Dictionary<string, object> { { SCLRunner.RunIdName, Guid.NewGuid() } };

        return await RunSCLFromTextAsync(scl, meta, cancellationToken);
    }

    private async Task<int> RunSCLFromTextAsync(
        string scl,
        Dictionary<string, object> metadata,
        CancellationToken cancellationToken = default)
    {
        var pm = PerformanceMonitor.Start(
            TimeSpan.FromMilliseconds(10),
            ("Process", "% Processor Time"),
            ("Process", "Working Set")
        );

        var externalContextResult = await
            _connectorManager.GetExternalContextAsync(
                _baseExternalContext.ExternalProcessRunner,
                _baseExternalContext.RestClientFactory,
                _baseExternalContext.Console,
                cancellationToken
            );

        if (externalContextResult.IsFailure)
        {
            ErrorLogger.LogError(
                _logger,
                externalContextResult.Error.WithLocation(ErrorLocation.EmptyLocation)
            );

            return Failure;
        }

        var stepFactoryStoreResult =
            await _connectorManager.GetStepFactoryStoreAsync(
                externalContextResult.Value,
                cancellationToken
            );

        if (stepFactoryStoreResult.IsFailure)
        {
            ErrorLogger.LogError(
                _logger,
                stepFactoryStoreResult.Error.WithLocation(ErrorLocation.EmptyLocation)
            );

            return Failure;
        }

        var analyticsLogger = new AnalyticsLogger();
        var multiLogger     = new MultiLogger(analyticsLogger, _logger);

        var runner = new SCLRunner(
            multiLogger,
            stepFactoryStoreResult.Value,
            externalContextResult.Value
        );

        var r = await runner.RunSequenceFromTextAsync(scl, metadata, cancellationToken);

        _analyticsWriter.LogAnalytics(analyticsLogger.SequenceAnalytics);

        pm.Dispose();

        foreach (var result in pm.Results)
        {
            _logger.LogInformation(
                $"{result.CategoryName} {result.CounterName}:  Max: {result.Max} Mean: {result.Mean}"
            );
        }

        if (r.IsSuccess)
            return Success;

        ErrorLogger.LogError(_logger, r.Error);

        return Failure;
    }
}
