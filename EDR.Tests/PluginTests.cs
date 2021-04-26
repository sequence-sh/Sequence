using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Reductech.EDR;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Abstractions;
using Reductech.EDR.Core.ExternalProcesses;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Serialization;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EDR.Tests
{

public class PluginTests
{
    public ITestOutputHelper TestOutputHelper { get; }

    public PluginTests(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }

    public static readonly string RelativePath = Path.Combine(
        "..",
        "ExampleConnector",
        "bin",
        "Debug",
        "net5.0",
        "ExampleConnector.dll"
    );

    public static readonly string SettingsString = $@"{{
""connectors"": {{
    ""example"":{{
        ""path"": {JsonConvert.SerializeObject(RelativePath)},
           ""ColorSource"": ""Red""
        
    }}
}}
}}";

    [Fact]
    public async Task TestBadlyConfiguredPlugin()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();

        var settings = SCLSettings.CreateFromString(
            SettingsString.Replace("ExampleConnector", "MissingConnector")
        );

        var mockRepository = new MockRepository(MockBehavior.Strict);

        IExternalContext externalContext = new ExternalContext(
            mockRepository.OneOf<IExternalProcessRunner>(),
            mockRepository.OneOf<IConsole>()
        );

        var methods = new EDRMethods(
            logger,
            settings,
            externalContext,
            mockRepository.OneOf<IFileSystem>()
        );

        try
        {
            await methods.Execute(CancellationToken.None, "Log (GetTestString)");
        }
        catch (InvalidOperationException e)
        {
            e.Message.Should().Contain("Dependency resolution failed for component");
            return;
        }

        throw new XunitException("Expected InvalidOperationException");
    }

    [Fact]
    public async Task TestConnectorFromEDRMethods()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();

        var settings = SCLSettings.CreateFromString(SettingsString);

        var mockRepository = new MockRepository(MockBehavior.Strict);

        IExternalContext externalContext = new ExternalContext(
            mockRepository.OneOf<IExternalProcessRunner>(),
            mockRepository.OneOf<IConsole>()
        );

        var methods = new EDRMethods(
            logger,
            settings,
            externalContext,
            mockRepository.OneOf<IFileSystem>()
        );

        var result = await methods.Execute(CancellationToken.None, "Log (GetTestString)");

        result.Should().Be(0, "result should indicate success");

        factory.Sink.LogEntries.Select(x => x.Message).Should().Contain("The Color is Color [Red]");
    }

    [Fact]
    public async Task TestConnectorFromSettings()
    {
        var factory = MELT.TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var logger  = factory.CreateLogger<EDRMethods>();

        var settings = SCLSettings.CreateFromString(SettingsString);

        var stepFactoryStoreResult = StepFactoryStore.TryCreateFromSettings(settings, logger);

        var injectedContextsResult = stepFactoryStoreResult.Value.TryGetInjectedContexts(settings);

        var externalContext = ExternalContext.Default with
        {
            InjectedContexts = injectedContextsResult.Value
        };

        var runner = new SCLRunner(
            SCLSettings.EmptySettings,
            logger,
            stepFactoryStoreResult.Value,
            externalContext
        );

        var r = await
            runner.RunSequenceFromTextAsync(
                "Log (GetTestString)",
                new Dictionary<string, object>(),
                CancellationToken.None
            );

        r.IsSuccess.Should().BeTrue();

        factory.Sink.LogEntries.Select(x => x.Message).Should().Contain("The Color is Color [Red]");
    }
}

}
