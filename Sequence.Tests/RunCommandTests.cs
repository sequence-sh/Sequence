using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using MELT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Reductech.Sequence;
using Reductech.Sequence.ConnectorManagement.Base;
using Xunit;
using static Sequence.Tests.Helpers;

namespace Sequence.Tests;

public class RunCommandTests
{
    [Fact]
    public void RunSCL_WhenSCLFunctionIsSuccess_ReturnsSuccess()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"run scl \"Print '{TheUltimateTestString}'\"");

        result.ExitCode.Should().Be(0);

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "Sequence Started",
                "Sequence Completed"
            );
    }

    [Fact]
    public void RunSCL_WhenSCLFunctionIsFailure_ReturnsFailure()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"run scl \"Loog '{TheUltimateTestString}'\"");

        result.ExitCode.Should().Be(1);

        Assert.Contains(
            factory.Sink.LogEntries,
            l => l.LogLevel == LogLevel.Error
              && l.Message!.Contains("The step 'Loog' does not exist")
        );
    }

    [Fact]
    public void RunSCL_WhenSCLIsEmpty_Throws()
    {
        var sp = GetDefaultServiceProvider();

        var error = Assert.Throws<CommandLineArgumentException>(
            () => new AppRunner<ConsoleMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("run scl \"\"")
        );

        Assert.Equal("Please provide a valid SCL string.", error.Message);
    }

    [Fact]
    public void RunSCL_WhenRunnerIsFailure_LogsErrorAndReturnsFailure()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs      = new MockFileSystem();

        var mockRepository       = new MockRepository(MockBehavior.Strict);
        var connectorManagerMock = mockRepository.Create<IConnectorManager>();

        connectorManagerMock
            .Setup(x => x.Verify(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var run = new Mock<RunCommand>(
            factory.CreateLogger<RunCommand>(),
            fs,
            connectorManagerMock.Object,
            new NullAnalyticsWriter()
        );

        var sp = new ServiceCollection()
            .AddSingleton(
                new ConnectorCommand(
                    connectorManagerMock.Object,
                    new NullLogger<ConnectorCommand>()
                )
            )
            .AddSingleton(run.Object)
            .AddSingleton(new StepsCommand(connectorManagerMock.Object))
            .AddSingleton(
                new ValidateCommand(
                    factory.CreateLogger<ValidateCommand>(),
                    fs,
                    connectorManagerMock.Object
                )
            )
            .AddSingleton<ConsoleMethods>()
            .BuildServiceProvider();

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"run scl \"Log '{TheUltimateTestString}'\"");

        result.ExitCode.Should().Be(1);

        Assert.Contains(
            factory.Sink.LogEntries,
            l => l.LogLevel == LogLevel.Error
              && l.Message!.Contains("Could not validate installed connectors")
        );
    }

    [Fact]
    public void RunPath_WhenPathIsEmpty_Throws()
    {
        var sp = GetDefaultServiceProvider();

        var error = Assert.Throws<CommandLineArgumentException>(
            () => new AppRunner<ConsoleMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("run \"\"")
        );

        Assert.Equal("Please provide a path to a valid SCL file.", error.Message);
    }

    [Fact]
    public void RunPath_WhenSCLFunctionIsSuccess_ReturnsSuccess()
    {
        const string path = @"c:\temp\file.scl";

        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs      = new MockFileSystem();
        fs.AddFile(path, $"- Log '{TheUltimateTestString}'");

        var sp = GetDefaultServiceProvider(factory, fs, null);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"run {path}");

        result.Console.ErrorText().Should().BeNullOrWhiteSpace();
        result.ExitCode.Should().Be(0);

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "Sequence Started",
                TheUltimateTestString,
                "Sequence Completed"
            );
    }

    ///// <summary>
    ///// Encodes an argument for passing into a program
    ///// </summary>
    ///// <param name="original">The value that should be received by the program</param>
    ///// <returns>The value which needs to be passed to the program for the original value 
    ///// to come through</returns>
    //private static string EncodeParameterArgument(string original)
    //{
    //    if (string.IsNullOrEmpty(original))
    //        return original;

    //    string value = Regex.Replace(original, @"(\\*)" + "\"", @"$1\$0");
    //    value = Regex.Replace(value, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"");
    //    return value;
    //}

    [Fact]
    public void RunPath_WithInjectedVariables_WhenSCLFunctionIsSuccess_ReturnsSuccess()
    {
        const string path = @"c:\temp\file.scl";

        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs      = new MockFileSystem();
        fs.AddFile(path, $"- Log <myVar>");

        var sp = GetDefaultServiceProvider(factory, fs, null);

        var argsString = $"run path {path} --variable \"<myVar> = Hello World\"";

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem(argsString);

        result.Console.ErrorText().Should().BeNullOrWhiteSpace();

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "Sequence Started",
                "Hello World",
                "Sequence Completed"
            );

        result.ExitCode.Should().Be(0);
    }

    [Fact]
    public void RunSCL_WithInjectedVariables_WhenSCLFunctionIsSuccess_ReturnsSuccess()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory, new MockFileSystem(), null);

        var argsString = $"run scl \"Log <a> + <b>\" -v \"<a> = 3\" -v \"<b> = 4\"";

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem(argsString);

        result.Console.ErrorText().Should().BeNullOrWhiteSpace();

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "Sequence Started",
                "7",
                "Sequence Completed"
            );

        result.ExitCode.Should().Be(0);
    }

    [Fact]
    public void Run_WithInjectedVariables_WhenSCLFunctionIsSuccess_ReturnsSuccess()
    {
        const string path = @"c:\temp\file.scl";

        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs      = new MockFileSystem();
        fs.AddFile(path, $"- Log <myVar>");

        var sp = GetDefaultServiceProvider(factory, fs, null);

        var argsString = $"run {path} -v \"<myVar> = Hello World\"";

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem(argsString);

        result.Console.ErrorText().Should().BeNullOrWhiteSpace();

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "Sequence Started",
                "Hello World",
                "Sequence Completed"
            );

        result.ExitCode.Should().Be(0);
    }
}
