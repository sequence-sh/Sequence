using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Errors;
using Reductech.EDR.Core.Serialization;

namespace Reductech.EDR
{
    /// <summary>
    /// EDR methods to be run in the console.
    /// </summary>
    [Command(Description = "Executes Nuix Sequences")]
    public class EDRMethods
    {

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
            if (string.IsNullOrWhiteSpace(yaml))
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentException($"Please provide either {nameof(yaml)} or {nameof(path)}");
                }

                await ExecuteYamlFromPathAsync(path, cancellationToken);
            }

            else
            {
                if (string.IsNullOrWhiteSpace(path))
                    await ExecuteYamlStringAsync(yaml, cancellationToken);
                else
                    throw new ArgumentException($"Please provide only one of {nameof(yaml)} or {nameof(path)}");
            }
        }



        private async Task ExecuteYamlFromPathAsync(string path, CancellationToken cancellationToken)
        {
            var text = await File.ReadAllTextAsync(path, cancellationToken);

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
                .Bind(YamlRunner.ConvertToUnitStep)
                ;

            if (freezeResult.IsFailure)
                LogError(Logger, freezeResult.Error);
            else
            {
                var settingsResult = TryGetSettings();

                if (settingsResult.IsFailure)
                    Logger.LogError(settingsResult.Error);
                else
                {
                    var stateMonad = new StateMonad(Logger, settingsResult.Value, ExternalProcessRunner.Instance,
                        stepFactoryStore);

                    var runResult = await freezeResult.Value.Run(stateMonad, cancellationToken);

                    if (runResult.IsFailure)
                        LogError(Logger, runResult.Error);
                }
            }
        }

        public static void LogError(ILogger logger, IError error)
        {
            foreach (var singleError in error.GetAllErrors())
            {
                if(singleError.Exception != null)
                    logger.LogError(singleError.Exception, "{Error} - {Location}",  singleError.Message, singleError.Location.AsString);
                else
                    logger.LogError("{Error} - {Location}",  singleError.Message, singleError.Location.AsString);
            }
        }

        private Result<ISettings> TryGetSettings()
        {
            if (StaticSettings.HasValue)
                return Result.Success(StaticSettings.Value);

            var settingsResult = NuixSettings
                .TryCreate(sn => ConfigurationManager.AppSettings[sn])
                .Map(x => x as ISettings);

            return settingsResult;
        }

        /// <summary>
        /// One type for each connector.
        /// </summary>
        private IEnumerable<Type> ConnectorTypes { get; } = new List<Type> {typeof(IRubyScriptStep)};

        /// <summary>
        /// The logger - needs to be set externally.
        /// </summary>
        public static ILogger? StaticLogger { get; set; }

        /// <summary>
        /// The settings - needs to be set externally
        /// </summary>
        public static Maybe<ISettings> StaticSettings { get; set; }


        private ILogger Logger => StaticLogger;
    }
}