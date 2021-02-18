using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Serialization;

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
        : this(logger, settings, ExternalContext.Default) { }

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
        string? path = null) => ExecuteAbstractAsync(scl, path, cancellationToken);

    /// <summary>
    /// Executes SCL from either a file or a string.
    /// </summary>
    private async Task ExecuteAbstractAsync(
        string? scl,
        string? path,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(scl))
            await ExecuteFromStringAsync(scl, cancellationToken);
        else if (!string.IsNullOrWhiteSpace(path))
            await ExecuteFromPathAsync(path, cancellationToken);
        else
            throw new ArgumentException("Please provide a Sequence string (-s) or path (-p).");
    }

    private async Task ExecuteFromPathAsync(string path, CancellationToken cancellationToken)
    {
        var text =
            await _externalContext.FileSystemHelper.File.ReadAllTextAsync(path, cancellationToken);

        await ExecuteFromStringAsync(text, cancellationToken);
    }

    private async Task ExecuteFromStringAsync(string scl, CancellationToken cancellationToken)
    {
        var stepFactoryStore = StepFactoryStore.CreateUsingReflection(
            ConnectorTypes.Append(typeof(IStep)).ToArray()
        );

        var runner = new SCLRunner(_settings, _logger, stepFactoryStore, _externalContext);

        var r = await runner.RunSequenceFromTextAsync(scl, cancellationToken);

        if (r.IsFailure)
            SCLRunner.LogError(_logger, r.Error);
    }

    /// <summary>
    /// One type for each connector.
    /// </summary>
    private IEnumerable<Type> ConnectorTypes { get; } = new List<Type> { typeof(IRubyScriptStep) };
}

}
