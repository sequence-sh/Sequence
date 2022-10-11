using System.IO.Abstractions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging.Abstractions;
using Reductech.Sequence.ConnectorManagement;
using Reductech.Sequence.Core;
using Reductech.Sequence.Core.Abstractions;
using Reductech.Sequence.Core.Connectors;
using Reductech.Sequence.Core.ExternalProcesses;
using Reductech.Sequence.Core.Internal.Serialization;
using NullLogger = Microsoft.Extensions.Logging.Abstractions.NullLogger;

namespace Benchmarks
{

public class Program
{
    public static async Task Main(string[] args)
    {
        //TODO use optimized versions of packages, especially Core
        IConfig config  = new AllowNonOptimized();
        var     summary = BenchmarkRunner.Run(typeof(Program).Assembly, config);
    }
}

[KeepBenchmarkFiles]
public class SequenceBenchmarks
{
    public SCLRunner SCLRunner { get; private set; }

    //[GlobalSetup]
    public async Task SetupRunnerAsync()
    {
        var fileSystem = new FileSystem();

        var externalContext = new ExternalContext(
            ExternalProcessRunner.Instance,
            DefaultRestClientFactory.Instance,
            ConsoleAdapter.Instance,
            ("FileSystem", fileSystem)
        );

        var settings = ConnectorManagerSettings.Default;

        settings = settings with
        {
            ConnectorPath = Path.Combine(
                AppContext.BaseDirectory,
                $"connectors.json"
            )
        };

        var connectorRegistry = new ConnectorRegistry(
            NullLogger<ConnectorRegistry>.Instance,
            settings
        );

        var connectorManager = await ConnectorManager.CreateAndPopulate(
            NullLogger<ConnectorManager>.Instance,
            settings,
            connectorRegistry,
            fileSystem,
            null
        );

        var stepFactoryStoreResult =
            await connectorManager.GetStepFactoryStoreAsync(
                externalContext,
                CancellationToken.None
            );

        var stepFactoryStore = stepFactoryStoreResult.Value;

        var runner = new SCLRunner(NullLogger.Instance, stepFactoryStore, externalContext);

        await runner.RunSequenceFromTextAsync(
            "log 123",
            new Dictionary<string, object>(),
            CancellationToken.None
        );

        SCLRunner = runner;
    }

    [Benchmark]
    public async Task RunBasicSCL() => RunSCLAsync("Log 123");

    //public void RunScl(string scl)
    //{
    //    RunSCLAsync(scl).RunSynchronously();
    //}

    public async Task RunSCLAsync(string scl)
    {
        var result = await SCLRunner.RunSequenceFromTextAsync(
            scl,
            new Dictionary<string, object>(),
            CancellationToken.None
        );

        if (!result.IsSuccess)
        {
            throw new Exception(result.Error.AsStringWithLocation);
        }
    }
}

public class AllowNonOptimized : ManualConfig
{
    public AllowNonOptimized()
    {
        this.WithOption(ConfigOptions.DisableOptimizationsValidator, true);
        //AddValidator( (JitOptimizationsValidator.DontFailOnError); // ALLOW NON-OPTIMIZED DLLS

        AddLogger(
            DefaultConfig.Instance.GetLoggers().ToArray()
        ); // manual config has no loggers by default

        AddExporter(
            DefaultConfig.Instance.GetExporters().ToArray()
        ); // manual config has no exporters by default

        AddColumnProvider(
            DefaultConfig.Instance.GetColumnProviders().ToArray()
        ); // manual config has no columns by default

        this.ArtifactsPath = "..\\..\\.\\..\\BenchmarkDotNet.Artifacts\\results";
    }
}

}
