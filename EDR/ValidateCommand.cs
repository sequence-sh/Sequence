using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Parser;
using Reductech.EDR.Core.Internal.Serialization;
using static Reductech.EDR.Result;

namespace Reductech.EDR
{

/// <summary>
/// 
/// </summary>
[Command(
    Name        = "validate",
    Usage       = "validate [command] [path or scl]",
    Description = "Check if a Sequence Configuration Language file or string is valid"
)]
public class ValidateCommand
{
    private readonly ILogger<ValidateCommand> _logger;
    private readonly IFileSystem _fileSystem;
    private readonly IConnectorManager _connectorManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="fileSystem">The file system</param>
    /// <param name="connectorManager"></param>
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
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="CommandLineArgumentException"></exception>
    [DefaultMethod]
    public async Task<int> ValidateDefault(
        CancellationToken cancellationToken,
        [Operand(Description = "A path to an SCL file, or in-line SCL.")]
        string sclPath) => await ValidatePath(cancellationToken, sclPath);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="CommandLineArgumentException"></exception>
    [Command(
        Name        = "path",
        Description = "Run from path"
    )]
    public async Task<int> ValidatePath(
        CancellationToken cancellationToken,
        [Operand(Description = "A path to an SCL file, or in-line SCL.")]
        string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !_fileSystem.File.Exists(path))
            throw new CommandLineArgumentException("Please provide a path to a valid SCL file.");

        var text = await _fileSystem.File.ReadAllTextAsync(path, cancellationToken);

        return await ValidateSCL(cancellationToken, text);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="scl"></param>
    /// <returns></returns>
    [Command(
        Name        = "scl",
        Description = "Run from "
    )]
    public async Task<int> ValidateSCL(
        CancellationToken cancellationToken,
        [Operand(Description = "A path to an SCL file, or in-line SCL.")]
        string scl)
    {
        if (string.IsNullOrWhiteSpace(scl))
            throw new CommandLineArgumentException("Need an SCL string.");

        var stepFactoryStore =
            await _connectorManager.GetStepFactoryStoreAsync(cancellationToken);

        var stepResult = SCLParsing.TryParseStep(scl)
            .Bind(x => x.TryFreeze(TypeReference.Any.Instance, stepFactoryStore))
            .Map(SCLRunner.ConvertToUnitStep);

        if (stepResult.IsSuccess)
        {
            _logger.LogInformation("SCL is valid");

            return Success;
        }

        SCLRunner.LogError(_logger, stepResult.Error);

        return Failure;
    }
}

}
