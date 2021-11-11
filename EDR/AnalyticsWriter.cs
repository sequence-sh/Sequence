using ConsoleTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reductech.EDR.Core.Internal.Analytics;

namespace Reductech.EDR
{

/// <summary>
/// Logs analytics
/// </summary>
public interface IAnalyticsWriter
{
    /// <summary>
    /// Log analytics for a sequence
    /// </summary>
    void LogAnalytics(SequenceAnalytics sequenceAnalytics);
}

public class NullAnalyticsWriter : IAnalyticsWriter
{
    /// <inheritdoc />
    public void LogAnalytics(SequenceAnalytics sequenceAnalytics)
    {
        //Do nothing
    }
}

/// <summary>
/// Logs analytics
/// </summary>
public class AnalyticsWriter : IAnalyticsWriter
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AnalyticsWriter> _logger;

    /// <summary>
    /// Create a new analytics writer
    /// </summary>
    public AnalyticsWriter(IConfiguration configuration, ILogger<AnalyticsWriter> logger)
    {
        _configuration = configuration;
        _logger        = logger;
    }

    /// <inheritdoc />
    public void LogAnalytics(SequenceAnalytics sequenceAnalytics)
    {
        if (!_logger.IsEnabled(LogLevel.Information))
            return;

        var logAnalyticsString = _configuration["LogAnalytics"];

        if (!string.IsNullOrWhiteSpace(logAnalyticsString)
         && bool.TryParse(logAnalyticsString, out var logAnalytics) && !logAnalytics)
        {
            return; //If log analytics is false do nothing
        }

        var detailedAnalysis = sequenceAnalytics.Synthesize();

        var table = new ConsoleTable(
            "Text",
            nameof(DetailedStepAnalysis.TextLocation),
            nameof(DetailedStepAnalysis.TimesRun),
            nameof(DetailedStepAnalysis.TotalTime)
        );

        foreach (var step in detailedAnalysis.Steps)
        {
            table.AddRow(
                step.TextLocation.Text.Replace("\r", "\\r").Replace("\n", "\\n"),
                $"{step.TextLocation.Start} - {step.TextLocation.Stop}",
                step.TimesRun,
                step.TotalTime
            );
        }

        var ts = "\r\n" + table.ToString();

        _logger.LogInformation(ts);
    }
}

}
