using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using MELT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sequence;
using Xunit;

namespace Sequence.Tests;

public class PerformanceMonitoringTests
{
    [Fact]
    [Category("Integration")] //Must be integration test as it only works on windows
    public void MustLogMessagesForChosenCategories()
    {
        var configText = @"{
  ""PerformanceMonitoring"": {
    ""Enable"": true,
    ""MeasurementIntervalMs"": 100,
    ""LoggingIntervalMs"": -1,
    ""MeasureAllCategories"": false,

    ""Categories"": {
      ""Process"": [
        ""% Processor Time"",
        ""Working Set""
      ]
    } 
  }
}
";

        var configStream = new MemoryStream(Encoding.UTF8.GetBytes(configText));

        var loggerFactory = TestLoggerFactory.Create();
        var logger        = loggerFactory.CreateLogger<PerformanceMonitorService>();

        IConfiguration configuration =
            new ConfigurationManager().AddJsonStream(configStream).Build();

        var performanceMonitorService = new PerformanceMonitorService(logger, configuration);

        performanceMonitorService.ReportResults();

        performanceMonitorService.Dispose();

        loggerFactory.Sink.LogEntries.Should()
            .SatisfyRespectively(
                x => x.Message.Should().Be("Performance Monitoring Started"),
                x => x.Message.Should().Contain("Elapsed Time"),
                x => x.Message.Should().Contain("Process - % Processor Time"),
                x => x.Message.Should().Contain("Process - Working Set")
            );
    }

    [Fact]
    [Category("Integration")]
    public void MustLogMessagesAllCategoriesIfChosen()
    {
        var configText = @"{
  ""PerformanceMonitoring"": {
    ""Enable"": true,
    ""MeasurementIntervalMs"": 100,
    ""LoggingIntervalMs"": -1,
    ""MeasureAllCategories"": true
  }
}
";

        var configStream = new MemoryStream(Encoding.UTF8.GetBytes(configText));

        var loggerFactory = TestLoggerFactory.Create();
        var logger        = loggerFactory.CreateLogger<PerformanceMonitorService>();

        IConfiguration configuration =
            new ConfigurationManager().AddJsonStream(configStream).Build();

        var performanceMonitorService = new PerformanceMonitorService(logger, configuration);

        performanceMonitorService.ReportResults();

        performanceMonitorService.Dispose();

        loggerFactory.Sink.LogEntries.Take(2)
            .Should()
            .SatisfyRespectively(
                x => x.Message.Should().Be("Performance Monitoring Started"),
                x => x.Message.Should().Contain("Elapsed Time")
            );

        foreach (var logEntry in loggerFactory.Sink.LogEntries.Skip(2))
        {
            logEntry.Message.Should().Contain("Max");
        }
    }
}
