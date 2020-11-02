using System;
using System.Threading.Tasks;
using CommandDotNet;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Reductech.EDR
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var provider = new NLogLoggerProvider();
            var logger = provider.CreateLogger("Console Logger");
            EDRMethods.StaticLogger = logger;

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
