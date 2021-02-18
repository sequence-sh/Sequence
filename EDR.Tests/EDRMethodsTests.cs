using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Reductech.EDR;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.ExternalProcesses;
using Thinktecture;
using Thinktecture.IO;
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
        IExternalContext externalContext)
    {
        var settings =
            NuixSettings.CreateSettings(
                "Test Path",
                new Version(0, 0),
                true,
                new List<NuixFeature> { NuixFeature.CASE_CREATION, NuixFeature.METADATA_IMPORT }
            );

        var edrm = externalContext == null
            ? new EDRMethods(logger, settings)
            : new EDRMethods(logger, settings, externalContext);

        var serviceProvider = new ServiceCollection()
            .AddSingleton(edrm)
            .BuildServiceProvider();

        return serviceProvider;
    }

    [Fact]
    public void Execute_WhenSCLFunctionIsSuccess_LogsMessage()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();
        var sp      = GetDefaultServiceProvider(logger);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"-s \"{TheUltimateTestString}\"");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "EDR Sequence Started",
                "Hello World",
                "EDR Sequence Completed"
            );
    }

    [Fact]
    public void Build_WhenSCLFunctionIsValid_LogsMessage()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();
        var sp      = GetDefaultServiceProvider(logger);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"-b -s \"1 / 0\"");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo("Build Successful");
    }

    [Fact]
    public void Build_WhenSCLFunctionIsInvalid_LogsMessage()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();
        var sp      = GetDefaultServiceProvider(logger);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"-b -s \"Pront 123\"");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "The step 'Pront' does not exist - (null) Line: 1, Col: 0, Idx: 0 - Line: 1, Col: 8, Idx: 8 Text: Pront 123"
            );
    }

    [Fact]
    public void Execute_WhenPathFunctionIsSuccess_LogsMessage()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();

        var filePath = @"C:\config.scl";

        var repo = new MockRepository(MockBehavior.Strict);

        var mockFile = repo.Create<IFile>();

        mockFile.Setup(x => x.ReadAllTextAsync(filePath, It.IsAny<CancellationToken>()))
            .ReturnsAsync(TheUltimateTestString);

        IExternalContext externalContext = new ExternalContext(
            new FileSystemAdapter(
                repo.Create<IDirectory>().Object,
                mockFile.Object,
                repo.Create<ICompression>().Object
            ),
            repo.Create<IExternalProcessRunner>().Object,
            repo.Create<IConsole>().Object
        );

        //var fs = new MockFileSystem(
        //    new Dictionary<string, MockFileData>
        //    {
        //        { filePath, new MockFileData(TheUltimateTestString) }
        //    }
        //);

        var sp = GetDefaultServiceProvider(logger, externalContext);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"-p {filePath}");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "EDR Sequence Started",
                "Hello World",
                "EDR Sequence Completed"
            );
    }

    [Fact]
    public void Execute_WhenFunctionIsFailure_LogsErrorMessage()
    {
        var factory = MELT.TestLoggerFactory.Create();
        var logger  = factory.CreateLogger<EDRMethods>();

        var sp = GetDefaultServiceProvider(logger);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("-s \"Pront Value: 'Hello World'\"");

        result.ExitCode.Should().Be(0);
        result.Console.OutText().Should().Be("");

        factory.Sink.LogEntries.Select(x => x.Message)
            .Should()
            .BeEquivalentTo(
                "The step 'Pront' does not exist - (null) Line: 1, Col: 0, Idx: 0 - Line: 1, Col: 25, Idx: 25 Text: Pront Value: 'Hello World'"
            );
    }

    [Fact]
    public void Execute_WhenSCLAndPathAreNull_Throws()
    {
        var factory = MELT.TestLoggerFactory.Create();
        var logger  = factory.CreateLogger<EDRMethods>();

        var sp = GetDefaultServiceProvider(logger);

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
