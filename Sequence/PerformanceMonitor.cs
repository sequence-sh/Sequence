using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Reductech.Sequence;

/// <summary>
/// Service that monitors performance
/// </summary>
public class PerformanceMonitorService : IDisposable
{
    /// <summary>
    /// Create and start a PerformanceMonitorService
    /// </summary>
    public PerformanceMonitorService(
        ILogger<PerformanceMonitorService> logger,
        IConfiguration configuration)
    {
        Logger    = logger;
        Stopwatch = Stopwatch.StartNew();

        var configSection = configuration.GetSection("PerformanceMonitoring");

        var enabled = configSection.GetValue("Enable", true);

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            enabled = false;

        if (enabled)
        {
            try
            {
                var measureIntervalMs = configSection.GetValue("MeasurementIntervalMs", 100);

                if (measureIntervalMs <= 0)
                    measureIntervalMs = 100;

                var loggingIntervalMs    = configSection.GetValue("LoggingIntervalMs",    10000);
                var measureAllCategories = configSection.GetValue("MeasureAllCategories", false);

                if (measureAllCategories)
                {
                    Monitor = PerformanceMonitor.CreateAll(
                        TimeSpan.FromMilliseconds(measureIntervalMs)
                    );
                }
                else
                {
                    List<(string, string)> categories = new();

                    var section = configSection.GetSection("Categories");

                    foreach (var category in section.GetChildren())
                    {
                        var data = category.Get<List<string>>();

                        foreach (var counter in data)
                        {
                            categories.Add((category.Key, counter));
                        }
                    }

                    Monitor = PerformanceMonitor.Create(
                        TimeSpan.FromMilliseconds(measureIntervalMs),
                        categories
                    );
                }

                Logger.LogDebug("Performance Monitoring Started");

                Monitor.Start();

                if (loggingIntervalMs > 0)
                    Monitor.StartLogging(logger, TimeSpan.FromMilliseconds(loggingIntervalMs));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Access") && e.Message.Contains("denied"))
                {
                    Logger.LogWarning(
                        "Could not start performance monitoring: {error}. Your account is probably not a member of the Performance Monitor Users group.",
                        e.Message
                    );
                }
                else
                {
                    Logger.LogWarning(
                        "Could not start performance monitoring: {error}",
                        e.Message
                    );
                }
            }
        }
    }

    private ILogger<PerformanceMonitorService> Logger { get; }

    private PerformanceMonitor? Monitor { get; }

    private Stopwatch Stopwatch { get; }

    /// <summary>
    /// Report results
    /// </summary>
    public void ReportResults()
    {
        Logger.LogDebug("Elapsed Time: {Elapsed}", Stopwatch.Elapsed);

        if (Monitor is not null)
        {
            foreach (var performanceResult in Monitor.Results.ToArray())
            {
                performanceResult.Log(Logger);
            }
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Monitor?.Dispose();
        Stopwatch.Stop();
    }

    internal class PerformanceMonitor : IDisposable
    {
        private PerformanceMonitor(
            TimeSpan measureInterval,
            IEnumerable<Counter> counters)
        {
            MeasureInterval = measureInterval;
            Counters        = counters.ToList();
        }

        public static PerformanceMonitor CreateAll(TimeSpan interval)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new Exception("Performance monitoring is only supported on windows");
            }

            List<Counter> counters = new();

            var desiredCategories = new HashSet<string> { "Process", "Processor" };

            var thisProcess = SanitizeProcessName(AppDomain.CurrentDomain.FriendlyName);

            foreach (var category in PerformanceCounterCategory.GetCategories())
            {
                if (!desiredCategories.Contains(category.CategoryName))
                    continue;

                PerformanceCounter[] performanceCounters;

                try
                {
                    if (category.InstanceExists(thisProcess))
                    {
                        performanceCounters = category.GetCounters(thisProcess);
                    }
                    else
                    {
                        performanceCounters = category.GetCounters();
                    }
                }
                catch
                {
                    performanceCounters = Array.Empty<PerformanceCounter>();
                }

                counters.AddRange(performanceCounters.Select(counter => new Counter(counter)));
            }

            var pm = new PerformanceMonitor(interval, counters);

            return pm;
        }

        public static PerformanceMonitor Create(
            TimeSpan interval,
            IEnumerable<(string category, string counter)> counterNames)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new Exception("Performance monitoring is only supported on windows");
            }

            var thisProcess = SanitizeProcessName(AppDomain.CurrentDomain.FriendlyName);

            var counters =
                counterNames.Select(x => Counter.Create(x.category, x.counter, thisProcess));

            var pm = new PerformanceMonitor(interval, counters);

            return pm;
        }

        static string SanitizeProcessName(string name)
        {
            return name
                    .Replace('(', '[')
                    .Replace(')', ']')
                    .Replace('#', '_')
                    .Replace('/', '_')
                ;
        }

        /// <summary>
        /// The polling interval
        /// </summary>
        public TimeSpan MeasureInterval { get; }

        /// <summary>
        /// Tracks whether this has been disposed
        /// </summary>
        private readonly Subject<bool> _disposed = new();

        /// <summary>
        /// The counters
        /// </summary>
        private IReadOnlyList<Counter> Counters { get; }

        public IDisposable Start()
        {
            return Observable.Interval(MeasureInterval)
                .TakeUntil(_disposed)
                .Select(
                    _ =>
                    {
                        foreach (var counter in Counters)
                        {
                            counter.Update();
                        }

                        return Unit.Default;
                    }
                )
                .Subscribe();
        }

        public IDisposable StartLogging(ILogger logger, TimeSpan interval)
        {
            return Observable.Interval(interval)
                .TakeUntil(_disposed)
                .Select(
                    _ =>

                    {
                        foreach (var counter in Counters)
                            counter.LogState(logger);

                        return Unit.Default;
                    }
                )
                .Subscribe();
        }

        public void Dispose()
        {
            _disposed.OnNext(true);

            foreach (var counter in Counters)
                counter.PerformanceCounter.Dispose();
        }

        public IEnumerable<PerformanceResult> Results => Counters.Select(x => x.Result);

        private class Counter
        {
            public Counter(PerformanceCounter performanceCounter)
            {
                PerformanceCounter = performanceCounter;
            }

            public static Counter Create(string category, string name, string instanceName)
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                if (PerformanceCounterCategory.InstanceExists(instanceName, category))
                    return new(new PerformanceCounter(category, name, instanceName));

                return new Counter(new PerformanceCounter(category, instanceName));
            }

            public string CategoryName => PerformanceCounter.CategoryName;
            public string CounterName => PerformanceCounter.CounterName;
            #pragma warning restore CA1416 // Validate platform compatibility

            /// <summary>
            /// The Performance Counter
            /// </summary>
            public PerformanceCounter PerformanceCounter { get; }

            public List<float> Data { get; } = new();

            public string FullName => $"{CategoryName} - {CounterName}";

            public void Update()
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                var nv = PerformanceCounter.NextValue();
                #pragma warning restore CA1416 // Validate platform compatibility
                Data.Add(nv);
            }

            /// <inheritdoc />
            public override string ToString() => FullName;

            public PerformanceResult Result => new(
                CategoryName,
                CounterName,
                Data.DefaultIfEmpty(0).Max(),
                Data.DefaultIfEmpty(0).Average(),
                Data.Count
            );

            public void LogState(ILogger logger)
            {
                logger.Log(
                    LogLevel.Trace,
                    "{Category} - {Counter}: {Value}",
                    CategoryName,
                    CounterName,
                    Data.LastOrDefault(0)
                );
            }
        }
    }

    /// <summary>
    /// The result of performance measurement
    /// </summary>
    internal record PerformanceResult(
        string CategoryName,
        string CounterName,
        float Max,
        float Mean,
        int MeasurementsTaken)
    {
        public void Log(ILogger logger)
        {
            logger.Log(
                LogLevel.Debug,
                "{Category} - {Counter}: Max: {Max} Average: {Average}",
                CategoryName,
                CounterName,
                Max,
                Mean
            );
        }
    }
}
