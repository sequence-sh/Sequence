using System;
using System.Threading.Tasks;
using CommandDotNet;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Core.Internal;
using NLog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Reductech.EDR
{
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
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }
#pragma warning restore CA1031 // Do not catch general exception types

            await Task.Delay(1);
        }
    }
}
