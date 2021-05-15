using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using MELT;
using Microsoft.Extensions.Logging;
using Reductech.EDR;
using Xunit;
using static EDR.Tests.Helpers;

namespace EDR.Tests
{

public class ValidateCommandTests
{
    [Fact]
    public void ValidateSCL_WhenSCLIsValid_ReturnsSuccess()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"validate scl \"Log '{TheUltimateTestString}'\"");

        result.ExitCode.Should().Be(0);

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo("Successfully validated SCL");
    }

    [Fact]
    public void ValidateSCL_WhenSCLIsNotValid_ReturnsFailure()
    {
        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"validate scl \"Loog '{TheUltimateTestString}'\"");

        result.ExitCode.Should().Be(1);

        Assert.Contains(
            factory.Sink.LogEntries,
            l => l.LogLevel == LogLevel.Error
              && l.Message!.Contains("The step 'Loog' does not exist")
        );
    }

    [Fact]
    public void ValidateSCL_WhenSCLIsEmpty_Throws()
    {
        var sp = GetDefaultServiceProvider();

        var error = Assert.Throws<CommandLineArgumentException>(
            () => new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("validate scl \"\"")
        );

        Assert.Equal("Please provide a valid SCL string.", error.Message);
    }

    [Fact]
    public void ValidatePath_WhenPathIsEmpty_Throws()
    {
        var sp = GetDefaultServiceProvider();

        var error = Assert.Throws<CommandLineArgumentException>(
            () => new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("validate \"\"")
        );

        Assert.Equal("Please provide a path to a valid SCL file.", error.Message);
    }

    [Fact]
    public void ValidatePath_WhenPathIsValidSCL_ReturnsSuccess()
    {
        const string path = @"c:\temp\file.scl";

        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs      = new MockFileSystem();
        fs.AddFile(path, $"- Log '{TheUltimateTestString}'");

        var sp = GetDefaultServiceProvider(factory, fs, null);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"validate {path}");

        result.ExitCode.Should().Be(0);

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo("Successfully validated SCL");
    }
}

}
