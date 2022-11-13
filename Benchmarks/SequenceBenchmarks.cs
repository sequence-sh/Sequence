using System.IO.Abstractions;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;
using Sequence.Core;
using Sequence.Core.Abstractions;
using Sequence.Core.ExternalProcesses;
using Sequence.Core.Internal;
using Sequence.Core.Internal.Serialization;

namespace Benchmarks;

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
            (Sequence.Connectors.FileSystem.ConnectorInjection.FileSystemKey, fileSystem)
        );

        var stepFactoryStoreResult = StepFactoryStore.TryCreateFromAssemblies(
            externalContext,
            Assembly.GetAssembly(typeof(Sequence.Connectors.FileSystem.Steps.FileRead)),
            Assembly.GetAssembly(typeof(Sequence.Connectors.StructuredData.FromCSV))
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
