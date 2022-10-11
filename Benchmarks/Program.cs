using System.IO.Abstractions;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Reductech.Sequence.Core;
using Reductech.Sequence.Core.Abstractions;
using Reductech.Sequence.Core.Connectors;
using Reductech.Sequence.Core.ExternalProcesses;
using Reductech.Sequence.Core.Internal;
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

    public Dictionary<string, string> SequenceDictionary { get; private set; }

    [GlobalSetup]
    public async Task SetupRunnerAsync()
    {
        var fileSystem = new FileSystem();

        var externalContext = new ExternalContext(
            ExternalProcessRunner.Instance,
            DefaultRestClientFactory.Instance,
            ConsoleAdapter.Instance,
            (Reductech.Sequence.Connectors.FileSystem.ConnectorInjection.FileSystemKey, fileSystem)
        );

        var stepFactoryStoreResult = StepFactoryStore.TryCreateFromAssemblies(
            externalContext,
            Assembly.GetAssembly(typeof(Reductech.Sequence.Connectors.FileSystem.Steps.FileRead)),
            Assembly.GetAssembly(typeof(Reductech.Sequence.Connectors.StructuredData.FromCSV))
        );

        var stepFactoryStore = stepFactoryStoreResult.Value;

        var runner = new SCLRunner(NullLogger.Instance, stepFactoryStore, externalContext);

        await runner.RunSequenceFromTextAsync(
            "log 123",
            new Dictionary<string, object>(),
            CancellationToken.None
        );

        SCLRunner = runner;

        CreateSequenceDictionary();
    }

    private void CreateSequenceDictionary()
    {
        if (SequenceDictionary is not null)
            return;

        var dict  = new Dictionary<string, string>();
        var files = Directory.EnumerateFiles("SCL", "*.scl");

        foreach (var file in files)
        {
            var text = File.ReadAllText(file);

            if (!string.IsNullOrWhiteSpace(text))
            {
                var name = Path.GetFileNameWithoutExtension(file);
                dict[name] = text;
            }
        }

        SequenceDictionary = dict;
    }

    public IEnumerable<string> GetKeys
    {
        get
        {
            CreateSequenceDictionary();
            return SequenceDictionary.Keys;
        }
    }

    [Benchmark]
    //[Arguments("log 123")]
    [ArgumentsSource(nameof(GetKeys))]
    public async Task RunSCLAsync(string key)
    {
        var scl = SequenceDictionary[key];

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
