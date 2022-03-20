using System.Collections.Immutable;
using System.IO.Abstractions;
using System.Text.Json;
using System.Text.RegularExpressions;
using CommandDotNet;
using Microsoft.Extensions.Logging;
using Reductech.Sequence.ConnectorManagement.Base;
using Reductech.Sequence.Core.Abstractions;
using Reductech.Sequence.Core.Connectors;
using Reductech.Sequence.Core.Internal;
using Reductech.Sequence.Core.Internal.Analytics;
using Reductech.Sequence.Core.Internal.Errors;
using Reductech.Sequence.Core.Internal.Serialization;
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
        string sclPath,
        [Option('v', Description = "Additional variable to inject e.g. <myVar> = \"abc\"")]
        string[]? variable) => await RunPath(cancellationToken, sclPath, variable);

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
        string pathToSCLFile,
        [Option('v', Description = "Additional variable to inject e.g. <myVar> = \"abc\"")]
        string[]? variable)
    {
        if (string.IsNullOrWhiteSpace(pathToSCLFile) || !_fileSystem.File.Exists(pathToSCLFile))
            throw new CommandLineArgumentException("Please provide a path to a valid SCL file.");

        var text = await _fileSystem.File.ReadAllTextAsync(pathToSCLFile, cancellationToken);

        var meta = new Dictionary<string, object>
        {
            { SCLRunner.RunIdName, Guid.NewGuid() }, { SCLRunner.SCLPathName, pathToSCLFile }
        };

        return await RunSCLFromTextAsync(text, meta, variable, cancellationToken);
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
        [Operand(Description = "SCL string")] string scl,
        [Option('v', Description = "Additional variable to inject e.g. <myVar> = \"abc\"")]
        string[]? variable)
    {
        if (string.IsNullOrWhiteSpace(scl))
            throw new CommandLineArgumentException("Please provide a valid SCL string.");

        var meta = new Dictionary<string, object> { { SCLRunner.RunIdName, Guid.NewGuid() } };

        return await RunSCLFromTextAsync(scl, meta, variable, cancellationToken);
    }

    private IReadOnlyDictionary<VariableName, ISCLObject>? DeserializeInjectedVariables(
        string[]? injectedVariables)
    {
        if (injectedVariables is null)
            return ImmutableDictionary<VariableName, ISCLObject>.Empty;

        Regex regex = new(@"\A\s*\<(?<name>[\w-_]+)\>\s*=\s*(?<value>.+)\Z");

        var result = new Dictionary<VariableName, ISCLObject>();

        foreach (var injectedVariable in injectedVariables)
        {
            var match = regex.Match(injectedVariable);

            if (match.Success)
            {
                var val = match.Groups["value"].Value;

                JsonElement jsonElement;

                try
                {
                    jsonElement = JsonSerializer.Deserialize<JsonElement>(val);
                }
                catch (JsonException)
                {
                    jsonElement = JsonSerializer.Deserialize<JsonElement>($"\"{val}\"");
                }

                var sclObject = jsonElement.ConvertToSCLObject();

                result.Add(new VariableName(match.Groups["name"].Value), sclObject);
            }
        }

        return result;
    }

    private async Task<int> RunSCLFromTextAsync(
        string scl,
        Dictionary<string, object> metadata,
        string[]? injectedVariables,
        CancellationToken cancellationToken = default)
    {
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

        var variablesToInject = DeserializeInjectedVariables(injectedVariables);

        var r = await runner.RunSequenceFromTextAsync(
            scl,
            metadata,
            cancellationToken,
            variablesToInject
        );

        _analyticsWriter.LogAnalytics(analyticsLogger.SequenceAnalytics);

        if (r.IsSuccess)
            return Success;

        ErrorLogger.LogError(_logger, r.Error);

        return Failure;
    }
}
