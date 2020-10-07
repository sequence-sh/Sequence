using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using CommandDotNet;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Internal;

namespace Reductech.EDR
{
    [Command(Description = "Executes Nuix Sequences")]
    internal class EDRMethods : ConsoleMethods
    {
        [Command(Name = "execute", Description = "Execute a step defined in yaml")]
        public void Execute(
            [Option(LongName = "yaml", ShortName = "y", Description = "The yaml to execute")]
            string? yaml = null,
            [Option(LongName = "path", ShortName = "p", Description = "The path to the yaml to execute")]
            string? path = null) => ExecuteAbstract(yaml, path);

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
        protected override ILogger Logger { get; } =
            new ServiceCollection().AddLogging(cfg => cfg.AddConsole()).BuildServiceProvider().GetService<ILogger<EDRMethods>>();
    }


    internal class Program
    {
        private static async Task Main(string[] args)
        {

            var appRunner = new AppRunner<EDRMethods>()
                .UseDefaultMiddleware();

            await appRunner.RunAsync(args);

            await Task.Delay(1);


        }
    }
}
