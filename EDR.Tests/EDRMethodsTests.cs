using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Reductech.EDR;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.ExternalProcesses;
using Reductech.EDR.Core.Internal;
using Xunit;

namespace EDR.Tests
{

public class FakeConnectorManager : IConnectorManager
{
    public Task Add(
        string id,
        string? name = null,
        string? version = null,
        bool prerelease = false,
        bool force = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public Task Update(
        string name,
        string? version = null,
        bool prerelease = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public Task Remove(
        string name,
        bool configurationOnly = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public IEnumerable<(string name, ConnectorData data)> List(string? nameFilter = null) =>
        Array.Empty<(string name, ConnectorData data)>();

    public Task<ICollection<ConnectorMetadata>> Find(
        string? search = null,
        bool prerelease = false,
        CancellationToken ct = new CancellationToken()) => throw new NotImplementedException();

    public Task<bool> Verify(CancellationToken ct = default) => Task.FromResult(true);
}

public class EDRMethodsTests
{
    private const string TheUltimateTestString = "'Hello World'";

    private static IServiceProvider GetDefaultServiceProvider(ILogger<EDRMethods> logger) =>
        GetDefaultServiceProvider(logger, null, null);

    private static IServiceProvider GetDefaultServiceProvider(
        ILogger<EDRMethods> logger,
        IExternalContext? externalContext,
        IFileSystem? fileSystem)
    {
        var edrm = externalContext == null
            ? new EDRMethods(logger, fileSystem, new FakeConnectorManager())
            : new EDRMethods(logger, fileSystem, new FakeConnectorManager(), externalContext);

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
            .RunInMem($"run -s \"{TheUltimateTestString}\"");

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
            .RunInMem("run -b -s \"1 / 0\"");

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
            .RunInMem("run -b -s \"Pront 123\"");

        result.ExitCode.Should().Be(1);
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

        var mockFileSystem = repo.Create<IFileSystem>();

        mockFileSystem.Setup(x => x.File.ReadAllTextAsync(filePath, It.IsAny<CancellationToken>()))
            .ReturnsAsync(TheUltimateTestString);

        IExternalContext externalContext = new ExternalContext(
            repo.Create<IExternalProcessRunner>().Object,
            repo.Create<IConsole>().Object
        );

        var sp = GetDefaultServiceProvider(logger, externalContext, mockFileSystem.Object);

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"run -p {filePath}");

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
            .RunInMem("run -s \"Pront Value: 'Hello World'\"");

        result.ExitCode.Should().Be(1);
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

        var result = Assert.Throws<CommandLineArgumentException>(
            () => new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("run")
        );

        Assert.Equal("Please provide either an SCL string (-s) or path (-p).", result.Message);
    }
}

}
