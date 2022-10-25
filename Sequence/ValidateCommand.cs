using System.IO.Abstractions;
using CommandDotNet;
using Microsoft.Extensions.Logging;
using Reductech.Sequence.ConnectorManagement.Base;
using Reductech.Sequence.Core.Abstractions;
using Reductech.Sequence.Core.Connectors;
using Reductech.Sequence.Core.Internal;
using Reductech.Sequence.Core.Internal.Parser;
using Reductech.Sequence.Core.Internal.Serialization;
using static Reductech.Sequence.Result;

namespace Reductech.Sequence;

/// <summary>
/// Check if a Sequence Configuration Language file or string is valid
/// </summary>
[Command(
    "validate",
    Description = "Check if a Sequence Configuration Language file or string is valid"
)]
public class ValidateCommand
{
    private readonly ILogger<ValidateCommand> _logger;
    private readonly IFileSystem _fileSystem;
    private readonly IConnectorManager _connectorManager;

    /// <summary>
    /// Instantiate using the provided logger, file system and connector manager.
    /// </summary>
    public ValidateCommand(
        ILogger<ValidateCommand> logger,
        IFileSystem fileSystem,
        IConnectorManager connectorManager)
    {
        _logger           = logger;
        _fileSystem       = fileSystem;
        _connectorManager = connectorManager;
    }

    /// <summary>
    /// Shorthand for the path command.
    /// </summary>
    [DefaultCommand]
    public async Task<int> ValidateDefault(
        CancellationToken cancellationToken,
        [Operand(Description = "Path to the SCL file (Shorthand for using the path command)")]
        string sclPath) => await ValidatePath(cancellationToken, sclPath);

    /// <summary>
    /// Validate a Sequence in a file
    /// </summary>
    [Command(
        "path",
        Description = "Validate a Sequence in a file"
    )]
    public async Task<int> ValidatePath(
        CancellationToken cancellationToken,
        [Operand(Description = "Path to the SCL file")]
        string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !_fileSystem.File.Exists(path))
            throw new CommandLineArgumentException("Please provide a path to a valid SCL file.");

        var text = await _fileSystem.File.ReadAllTextAsync(path, cancellationToken);

        return await ValidateSCL(cancellationToken, text);
    }

    /// <summary>
    /// Validate an in-line SCL string
    /// </summary>
    [Command(
        "scl",
        Description = "Validate an in-line SCL string"
    )]
    public async Task<int> ValidateSCL(
        CancellationToken cancellationToken,
        [Operand(Description = "SCL string")] string scl)
    {
        if (string.IsNullOrWhiteSpace(scl))
            throw new CommandLineArgumentException("Please provide a valid SCL string.");

        var externalContextResult = await _connectorManager.GetExternalContextAsync(
            ExternalContext.Default.ExternalProcessRunner,
            ExternalContext.Default.RestClientFactory,
            ExternalContext.Default.Console,
            cancellationToken
        );

        if (externalContextResult.IsFailure)
        {
            _logger.LogError(externalContextResult.Error.AsString);
            return Failure;
        }

        var stepFactoryStoreResult =
            await _connectorManager.GetStepFactoryStoreAsync(
                externalContextResult.Value,
                cancellationToken
            );

        if (stepFactoryStoreResult.IsFailure)
        {
            _logger.LogError(stepFactoryStoreResult.Error.AsString);
            return Failure;
        }

        var stepResult = SCLParsing.TryParseStep(scl)
            .Bind(
                x => x.TryFreeze(
                    SCLRunner.RootCallerMetadata,
                    stepFactoryStoreResult.Value,
                    new OptimizationSettings(
                        true,
                        true,
                        new Dictionary<VariableName, InjectedVariable>()
                    )
                )
            )
            .Map(SCLRunner.ConvertToUnitStep);

        if (stepResult.IsSuccess)
        {
            _logger.LogInformation("Successfully validated SCL");

            return Success;
        }

        ErrorLogger.LogError(_logger, stepResult.Error);

        return Failure;
    }
}
