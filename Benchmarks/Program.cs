using System.Diagnostics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Benchmarks
{

public class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Any()) //Just run a particular script - useful for testing
        {
            var scriptName = args[0];
            var sb         = new SequenceBenchmarks();
            await sb.SetupRunnerAsync();

            var iterations = int.Parse(args.ElementAtOrDefault(1) ?? "100");

            for (var i = 0; i < iterations; i++) //Run the script 100 times
            {
                var sw = Stopwatch.StartNew();

                await sb.RunSCLAsync(scriptName);
                Console.WriteLine($@"{i}: {sw.ElapsedMilliseconds}");
            }
        }
        else
        {
            //TODO use optimized versions of packages, especially Core
            IConfig config  = new AllowNonOptimized();
            var     summary = BenchmarkRunner.Run(typeof(Program).Assembly, config);
        }
    }
}

}
