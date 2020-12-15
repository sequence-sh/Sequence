using System;
using System.Threading.Tasks;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace Reductech.EDR
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                    config.AddEnvironmentVariables(prefix: "EDR_");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<EDRMethods>();
                    services.Configure<NuixConfig>(context.Configuration.GetSection("connectors:nuix"));
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    var nlogConfig = new NLogLoggingConfiguration(context.Configuration.GetSection("nlog"));
                    LogManager.Configuration = nlogConfig;
                    logging.AddNLog(nlogConfig);
                })
                .Build();

            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                var appRunner = new AppRunner<EDRMethods>()
                    .UseDefaultMiddleware()
                    .UseMicrosoftDependencyInjection(host.Services);

                await appRunner.RunAsync(args);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                logger.Error(e);
            }
#pragma warning restore CA1031 // Do not catch general exception types
            finally
            {
                LogManager.Shutdown();
            }

        }
    }
}
