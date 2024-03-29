﻿using System.IO.Abstractions;
using CommandDotNet;
using CommandDotNet.Diagnostics;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Sequence.ConnectorManagement;

namespace Sequence;

internal class Program
{
    public static async Task<int> Main(string[] args)
    {
        var logger = LogManager.GetCurrentClassLogger();

        var host = CreateHostBuilder()
            .ConfigureServices(x => x.AddSingleton<PerformanceMonitorService>())
            .Build();

        var appRunner = new AppRunner<ConsoleMethods>()
            .Configure(a => a.AppSettings.Help.PrintHelpOption = true)
            .UseDefaultMiddleware()
            .UseMicrosoftDependencyInjection(host.Services);

        var pms = host.Services.GetRequiredService<PerformanceMonitorService>();

        var result = await Run(appRunner, pms, logger, args);

        return result;
    }

    internal static async Task<int> Run(
        AppRunner appRunner,
        PerformanceMonitorService? performanceMonitorService,
        NLog.ILogger logger,
        string[] args)
    {
        int result;

        Console.WriteLine();

        try
        {
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
        catch (Exception e)
        {
            logger.Error(e);
            result = 1;
        }
        finally
        {
            performanceMonitorService?.ReportResults();
            performanceMonitorService?.Dispose();
            LogManager.Shutdown();
        }

        return result;
    }

    internal static void SetConnectorsConfigPath(
        IConfiguration config,
        IHostEnvironment env,
        IFileSystem fs)
    {
        var managerSettings = config.GetSection(ConnectorManagerSettings.Key);

        var configPath = managerSettings[nameof(ConnectorManagerSettings.ConfigurationPath)];

        if (!string.IsNullOrEmpty(configPath))
            return;

        var envPath = fs.Path.Combine(
            AppContext.BaseDirectory,
            $"connectors.{env.EnvironmentName}.json"
        );

        if (fs.File.Exists(envPath))
            managerSettings[nameof(ConnectorManagerSettings.ConfigurationPath)] = envPath;
    }

    internal static IHostBuilder CreateHostBuilder() => new HostBuilder()
        .ConfigureAppConfiguration(
            (context, config) =>
            {
                var env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", false, false)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, false);

                config.AddEnvironmentVariables(prefix: "SEQUENCE_");
            }
        )
        .ConfigureServices(
            (context, services) =>
            {
                var fs = new FileSystem();

                services.AddSingleton<IFileSystem>(fs);

                SetConnectorsConfigPath(context.Configuration, context.HostingEnvironment, fs);

                services.AddConnectorManager(context.Configuration);

                services.AddSingleton<IAnalyticsWriter, AnalyticsWriter>();

                services.AddSingleton<ConnectorCommand>();
                services.AddSingleton<RunCommand>();
                services.AddSingleton<StepsCommand>();
                services.AddSingleton<ValidateCommand>();
                services.AddSingleton<ConsoleMethods>();
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
