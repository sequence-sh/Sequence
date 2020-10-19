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
using Reductech.EDR.Core.Serialization;
using Reductech.EDR.Core.Util;

namespace Reductech.EDR
{
    [Command(Description = "Executes Nuix Sequences")]
    internal class EDRMethods
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
                .BindCast<IStep, IStep<Unit>>();

            if (freezeResult.IsFailure)
                Logger.LogError(freezeResult.Error);
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
                        foreach (var runError in runResult.Error.AllErrors)
                            Logger.LogError(runError.Message);
                }
            }
        }


        private Result<ISettings> TryGetSettings()
        {
            var settingsResult = NuixSettings
                .TryCreate(sn => ConfigurationManager.AppSettings[sn])
                .Map(x => x as ISettings);

            return settingsResult;
        }

        /// <summary>
        /// One type for each connector.
        /// </summary>
        private IEnumerable<Type> ConnectorTypes { get; } = new List<Type> {typeof(IRubyScriptStep)};


        private ILogger Logger => Program.Logger!;
    }
}