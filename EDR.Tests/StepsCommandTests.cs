using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using MELT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Reductech.EDR;
using Xunit;

namespace EDR.Tests
{

public class StepsCommandTests
{
    private readonly ServiceProvider _sp;
    private List<StepsCommand.ConnectorRow> _steps;

    public StepsCommandTests()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs      = new MockFileSystem();
        var connMan = new FakeConnectorManager();

        _steps = new List<StepsCommand.ConnectorRow>();

        var stepsMock = new Mock<StepsCommand>(connMan);

        stepsMock.Setup(r => r.WriteTable(It.IsAny<IEnumerable<StepsCommand.ConnectorRow>>()))
            .Callback<IEnumerable<StepsCommand.ConnectorRow>>(steps => _steps = steps.ToList());

        _sp = new ServiceCollection()
            .AddSingleton(new ConnectorCommand(connMan))
            .AddSingleton(
                new RunCommand(
                    factory.CreateLogger<RunCommand>(),
                    fs,
                    connMan,
                    new NullAnalyticsWriter()
                )
            )
            .AddSingleton(stepsMock.Object)
            .AddSingleton(new ValidateCommand(factory.CreateLogger<ValidateCommand>(), fs, connMan))
            .AddSingleton<EDRMethods>()
            .BuildServiceProvider();
    }

    [Fact]
    public void StepsList_ByDefault_ReturnsListOfSteps()
    {
        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(_sp)
            .UseDefaultMiddleware()
            .RunInMem("steps");

        result.ExitCode.Should().Be(0);

        Assert.NotEmpty(_steps);
        Assert.Contains("And", _steps.Select(s => s.Name).ToList());
    }

    [Fact]
    public void StepsList_WhenFilterIsUsed_ReturnsFilteredList()
    {
        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(_sp)
            .UseDefaultMiddleware()
            .RunInMem("steps standard");

        result.ExitCode.Should().Be(0);

        Assert.NotEmpty(_steps);
        var stepNames = _steps.Select(s => s.Name).ToList();
        Assert.Contains("StandardInRead",   stepNames);
        Assert.Contains("StandardOutWrite", stepNames);
        Assert.DoesNotContain("And", stepNames);
    }

    [Fact]
    public void StepsList_WhenNameFilterIsUsed_ReturnsFilteredList()
    {
        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(_sp)
            .UseDefaultMiddleware()
            .RunInMem("steps -n ^modulo$");

        result.ExitCode.Should().Be(0);

        Assert.Single(_steps);
        Assert.Contains("Modulo", _steps.Select(s => s.Name).ToList());
    }

    [Fact]
    public void StepsList_WhenConnectorFilterIsUsed_ReturnsFilteredList()
    {
        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(_sp)
            .UseDefaultMiddleware()
            .RunInMem("steps -c ^Core$");

        result.ExitCode.Should().Be(0);

        Assert.NotEmpty(_steps);
        Assert.Contains("And", _steps.Select(s => s.Name).ToList());
    }
}

}
