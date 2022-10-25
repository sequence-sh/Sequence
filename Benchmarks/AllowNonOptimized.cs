using BenchmarkDotNet.Configs;

namespace Benchmarks;

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

        //this.AddExporter(new HtmlExporter());

        this.ArtifactsPath = "BenchmarkDotNet.Artifacts\\results";
    }
}
