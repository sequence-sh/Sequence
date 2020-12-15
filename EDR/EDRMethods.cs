using System;
using System.Collections.Generic;
using System.IO;
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
using Reductech.EDR.Core.Serialization;

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
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="nuixConfig"></param>
        public EDRMethods(ILogger<EDRMethods> logger, IOptions<NuixConfig> nuixConfig)
            : this(logger, nuixConfig, new FileSystem()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="nuixConfig"></param>
        /// <param name="fileSystem"></param>
        public EDRMethods(ILogger<EDRMethods> logger, IOptions<NuixConfig> nuixConfig, IFileSystem fileSystem)
        {
            _logger = logger;
            _nuixConfig = nuixConfig.Value;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Execute yaml from a path or directly from a command.
        /// </summary>
        [DefaultMethod]
        [Command(Name = "execute", Description = "Execute a step defined in yaml")]
        public Task Execute(
            CancellationToken cancellationToken,
            [Option(LongName = "command", ShortName = "c", Description = "The command to execute")]
            string? yaml = null,
            [Option(LongName = "path", ShortName = "p", Description = "The path to the yaml to execute")]
            string? path = null) => ExecuteAbstractAsync(yaml, path, cancellationToken);
        
        /// <summary>
        /// Executes yaml
        /// </summary>
        private async Task ExecuteAbstractAsync(string? yaml, string? path, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(yaml))
                await ExecuteYamlStringAsync(yaml, cancellationToken);
            else if (!string.IsNullOrWhiteSpace(path))
                await ExecuteYamlFromPathAsync(path, cancellationToken);
            else
                throw new ArgumentException($"Please provide either {nameof(yaml)} or {nameof(path)}");
        }
        
        private async Task ExecuteYamlFromPathAsync(string path, CancellationToken cancellationToken)
        {
            var text = await _fileSystem.File.ReadAllTextAsync(path, cancellationToken);
            await ExecuteYamlStringAsync(text, cancellationToken);
        }

        /// <summary>
        /// Runs a step defined in a yaml string
        /// </summary>
        private async Task ExecuteYamlStringAsync(string yaml, CancellationToken cancellationToken)
        {
            var stepFactoryStore =
                StepFactoryStore.CreateUsingReflection(ConnectorTypes.Append(typeof(IStep)).ToArray());

            var freezeResult = YamlMethods.DeserializeFromYaml(yaml, stepFactoryStore)
                .Bind(x => x.TryFreeze())
                .Bind(YamlRunner.ConvertToUnitStep);

            var nuixFeatures = new HashSet<NuixFeature>();
            foreach (var feature in _nuixConfig.Features)
                if (Enum.TryParse(typeof(NuixFeature), feature, true, out var nf) && nf is NuixFeature nuixFeature)
                    nuixFeatures.Add(nuixFeature);

            var nuixSettings = new NuixSettings(_nuixConfig.UseDongle, _nuixConfig.ExeConsolePath,
                _nuixConfig.Version, nuixFeatures);

            if (freezeResult.IsFailure)
                LogError(_logger, freezeResult.Error);
            else
            {
                var stateMonad = new StateMonad(_logger, nuixSettings, ExternalProcessRunner.Instance,
                    FileSystemHelper.Instance, stepFactoryStore);

                var runResult = await freezeResult.Value.Run(stateMonad, cancellationToken);

                if (runResult.IsFailure)
                    LogError(_logger, runResult.Error);
            }
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
