using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Core;
using Reductech.EDR.Core.ExternalProcesses;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Errors;
using Reductech.EDR.Core.Parser;
using Reductech.EDR.Core.Util;

namespace Reductech.EDR
{
    /// <summary>
    /// EDR methods to be run in the console.
    /// </summary>
    [Command(Description = "Executes EDR Sequences")]
    public class EDRMethods
    {
        private readonly ILogger<EDRMethods> _logger;
        private readonly NuixConfig _nuixConfig;
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Instantiate EDRMethods using the default IFileSystem provider.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="nuixConfig">Configuration for the Nuix connector.</param>
        public EDRMethods(ILogger<EDRMethods> logger, IOptions<NuixConfig> nuixConfig)
            : this(logger, nuixConfig, new FileSystem()) { }

        /// <summary>
        /// Instantiate EDRMethods using the specified IFileSystem provider.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="nuixConfig">Configuration for the Nuix connector.</param>
        /// <param name="fileSystem">An instance of the FileSystem helper. Used for testing.</param>
        public EDRMethods(ILogger<EDRMethods> logger, IOptions<NuixConfig> nuixConfig, IFileSystem fileSystem)
        {
            _logger = logger;
            _nuixConfig = nuixConfig.Value;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Run a Sequence of Steps defined using the Sequence Configuration Language (SCL) either from file or from a string.
        /// </summary>
        [DefaultMethod]
        [Command(Name = "run", Description = "Run a Sequence of Steps defined using the Sequence Configuration Language (SCL)")]
        public Task Execute(
            CancellationToken cancellationToken,
            [Option(LongName = "sequence", ShortName = "s", Description = "Run a Sequence from a string.")]
            string? scl = null,
            [Option(LongName = "path", ShortName = "p", Description = "Run a Sequence defined in a file.")]
            string? path = null) => ExecuteAbstractAsync(scl, path, cancellationToken);
        
        /// <summary>
        /// Executes SCL from either a file or a string.
        /// </summary>
        private async Task ExecuteAbstractAsync(string? scl, string? path, CancellationToken cancellationToken)
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
            var text = await _fileSystem.File.ReadAllTextAsync(path, cancellationToken);
            await ExecuteFromStringAsync(text, cancellationToken);
        }
        
        private async Task ExecuteFromStringAsync(string scl, CancellationToken cancellationToken)
        {
            var stepFactoryStore = StepFactoryStore.CreateUsingReflection(
                ConnectorTypes.Append(typeof(IStep)).ToArray());
            
            var freezeResult = SequenceParsing.ParseSequence(scl).Bind(x => x.TryFreeze(stepFactoryStore));
            
            if (freezeResult.IsFailure)
                LogError(_logger, freezeResult.Error);
            else
            {
                var nuixSettings = CreateNuixSettings();
                
                var stateMonad = new StateMonad(_logger, nuixSettings, ExternalProcessRunner.Instance,
                    FileSystemHelper.Instance, stepFactoryStore);

                var runResult = await freezeResult.Value.Run<Unit>(stateMonad, cancellationToken);

                if (runResult.IsFailure)
                    LogError(_logger, runResult.Error);
            }
        }

        private NuixSettings CreateNuixSettings()
        {
            var nuixFeatures = new HashSet<NuixFeature>();
            foreach (var feature in _nuixConfig.Features)
                if (Enum.TryParse(typeof(NuixFeature), feature, true, out var nf) && nf is NuixFeature nuixFeature)
                    nuixFeatures.Add(nuixFeature);

            var nuixSettings = new NuixSettings(_nuixConfig.UseDongle, _nuixConfig.ExeConsolePath,
                _nuixConfig.Version, nuixFeatures);

            return nuixSettings;
        }

        private static void LogError(ILogger logger, IError error)
        {
            foreach (var singleError in error.GetAllErrors())
            {
                if (singleError.Exception != null)
                    logger.LogError(singleError.Exception, "{Error} - {Location}", singleError.Message, singleError.Location.AsString);
                else
                    logger.LogError("{Error} - {Location}", singleError.Message, singleError.Location.AsString);
            }
        }

        /// <summary>
        /// One type for each connector.
        /// </summary>
        private IEnumerable<Type> ConnectorTypes { get; } = new List<Type> { typeof(IRubyScriptStep) };
    }
}
