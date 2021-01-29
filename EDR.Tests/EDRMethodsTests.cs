using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reductech.EDR;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Xunit;

namespace EDR.Tests
{

public class EDRMethodsTests
{
    private const string TheUltimateTestString = "'Hello World'";

    private static IServiceProvider GetDefaultServiceProvider(ILogger<EDRMethods> logger) =>
        GetDefaultServiceProvider(logger, null);

    private static IServiceProvider GetDefaultServiceProvider(
        ILogger<EDRMethods> logger,
        IFileSystem fileSystem)
    {
        var settings =
            NuixSettings.CreateSettings(
                "Test Path",
                new Version(0, 0),
                true,
                new List<NuixFeature> { NuixFeature.CASE_CREATION, NuixFeature.METADATA_IMPORT }
            );

        var edrm = fileSystem == null
            ? new EDRMethods(logger, settings)
            : new EDRMethods(logger, settings, fileSystem);

        var serviceProvider = new ServiceCollection()
            .AddSingleton(edrm)
            .BuildServiceProvider();

        return serviceProvider;
    }

    [Fact]
    public void Execute_WhenSCLFunctionIsSuccess_LogsMessage()
    {
        var logger = new TestLogger<EDRMethods>();
        var sp     = GetDefaultServiceProvider(logger);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"-s \"{TheUltimateTestString}\"");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        logger.LoggedValues.Should().BeEquivalentTo(new object[] { "Hello World" });
    }

    [Fact]
    public void Execute_WhenPathFunctionIsSuccess_LogsMessage()
    {
        var logger = new TestLogger<EDRMethods>();

        var filePath = @"C:\config.scl";

        var fs = new MockFileSystem(
            new Dictionary<string, MockFileData>
            {
                { filePath, new MockFileData(TheUltimateTestString) }
            }
        );

        var sp = GetDefaultServiceProvider(logger, fs);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"-p {filePath}");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        logger.LoggedValues.Should().BeEquivalentTo(new object[] { "Hello World" });
    }

    [Fact]
    public void Execute_WhenFunctionIsFailure_LogsErrorMessage()
    {
        var logger = new TestLogger<EDRMethods>();
        var sp     = GetDefaultServiceProvider(logger);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("-s \"Pront Value: 'Hello World'\"");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        logger.LoggedValues[0].Should().Be("The step 'Pront' does not exist");
    }

    [Fact]
    public void Execute_WhenSCLAndPathAreNull_Throws()
    {
        var logger = new TestLogger<EDRMethods>();
        var sp     = GetDefaultServiceProvider(logger);

        var result = Assert.Throws<ArgumentException>(
            () => new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("-s \"\"")
        );

        Assert.Equal("Please provide a Sequence string (-s) or path (-p).", result.Message);
    }
}

}
