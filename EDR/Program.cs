using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Internal;
using NLog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Reductech.EDR
{
    [Command(Description = "Executes Nuix Sequences")]
    internal class EDRMethods : ConsoleMethods
    {

        [DefaultMethod]
        [Command(Name = "execute", Description = "Execute a step defined in yaml")]
        public Task Execute(
            CancellationToken cancellationToken,
            [Option(LongName = "command", ShortName = "c", Description = "The command to execute")]
            string? yaml = null,
            [Option(LongName = "path", ShortName = "p", Description = "The path to the yaml to execute")]
            string? path = null) => ExecuteAbstractAsync(yaml, path, cancellationToken);

        [Command(Name = "documentation", Description = "Generate Documentation in Markdown format")]
        public void Documentation(
            [Option(LongName = "path", ShortName = "p", Description = "The path to the documentation file to write")]
            string path)
            => GenerateDocumentationAbstract(path);

        /// <inheritdoc />
        protected override Result<ISettings> TryGetSettings()
        {
            var settingsResult = NuixSettings
                .TryCreate(sn => ConfigurationManager.AppSettings[sn])
                .Map(x => x as ISettings);

            return settingsResult;
        }

        /// <inheritdoc />
        protected override IEnumerable<Type> ConnectorTypes { get; } = new List<Type>() { typeof(IRubyScriptStep) };

        /// <inheritdoc />
        protected override ILogger Logger => Program.Logger!;
    }


    internal class Program
    {
        public static ILogger? Logger;

        public static async Task Main(string[] args)
        {
            var provider = new NLogLoggerProvider();
            Logger = provider.CreateLogger("Console Logger");

            try
            {
                var appRunner = new AppRunner<EDRMethods>().UseDefaultMiddleware();

                await appRunner.RunAsync(args);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }

            await Task.Delay(1);
        }
    }
}
