using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using CommandDotNet;
using CommandDotNet.Diagnostics;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core;

namespace Reductech.EDR
{

internal class Program
{
    public static async Task<int> Main(string[] args)
    {
        var host = CreateHostBuilder().Build();

        var logger = LogManager.GetCurrentClassLogger();
        int result;

        Console.WriteLine();

        try
        {
            var appRunner = new AppRunner<EDRMethods>()
                .Configure(a => a.AppSettings.Help.PrintHelpOption = true)
                .UseDefaultMiddleware()
                .UseMicrosoftDependencyInjection(host.Services);

            result = await appRunner.RunAsync(args);
        }
        catch (CommandLineArgumentException ae)
        {
            logger.Info(ae.Message);
            Console.WriteLine();
            ae.GetCommandContext()?.PrintHelp();
            result = 1;
        }
        catch (ConnectorConfigurationException ce)
        {
            logger.Error(ce);
            ce.GetCommandContext()?.PrintHelp();
            result = 1;
        }
        #pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception e)
        {
            logger.Error(e);
            result = 1;
        }
        #pragma warning restore CA1031 // Do not catch general exception types
        finally
        {
            LogManager.Shutdown();
        }

        return result;
    }

    private static IHostBuilder CreateHostBuilder() => new HostBuilder()
        .ConfigureAppConfiguration(
            (_, config) =>
            {
                config.AddJsonFile("appsettings.json", false, false);
                config.AddEnvironmentVariables(prefix: "EDR_");
            }
        )
        .ConfigureServices(
            (context, services) =>
            {
                var fs = new FileSystem();

                services.AddSingleton<IFileSystem>(fs);

                services.AddConnectorManager(context.Configuration);

                services.AddSingleton<ConnectorCommand>();
                services.AddSingleton<RunCommand>();
                services.AddSingleton<StepsCommand>();
                services.AddSingleton<ValidateCommand>();
                services.AddSingleton<EDRMethods>();

                var sclSettings = SCLSettings.CreateFromIConfiguration(context.Configuration);

                services.AddSingleton(sclSettings);
            }
        )
        .ConfigureLogging(
            (context, logging) =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                var nlogConfig =
                    new NLogLoggingConfiguration(context.Configuration.GetSection("nlog"));

                LogManager.Configuration = nlogConfig;
                logging.AddNLog(nlogConfig);
            }
        );
}

}
