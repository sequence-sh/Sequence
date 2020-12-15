using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CommandDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;

namespace Reductech.EDR
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            NLog.LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

            var provider = new NLogLoggerProvider();
            var logger = provider.CreateLogger("Console Logger");
            EDRMethods.StaticLogger = logger;

            var nuixConfig = config.GetSection(nameof(NuixSettings));
            var featureList = nuixConfig.GetSection("NuixFeatures").Get<string[]>();

            var nuixFeatures = new HashSet<NuixFeature>();
            foreach (var feature in featureList)
                if (Enum.TryParse(typeof(NuixFeature), feature, true, out var nf) && nf is NuixFeature nuixFeature)
                    nuixFeatures.Add(nuixFeature);

            var nuixSettings = new NuixSettings(
                nuixConfig.GetValue<bool>("NuixUseDongle"),
                nuixConfig.GetValue<string>("NuixExeConsolePath"),
                nuixConfig.GetValue<Version>("NuixVersion"),
                nuixFeatures
            );

            EDRMethods.StaticSettings = nuixSettings;

            try
            {
                var appRunner = new AppRunner<EDRMethods>().UseDefaultMiddleware();

                await appRunner.RunAsync(args);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
#pragma warning restore CA1031 // Do not catch general exception types

            await Task.Delay(1);
        }
    }
}
