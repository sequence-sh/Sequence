using System.Linq;
using System.Threading;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using MELT;
using Microsoft.Extensions.Logging;
using Moq;
using Reductech.Sequence;
using Reductech.Sequence.ConnectorManagement.Base;
using Xunit;
using static Sequence.Tests.Helpers;

namespace Sequence.Tests;

public class ConnectorCommandTests
{
    [Fact]
    public void List_WhenValidationFails_Throws()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(m => m.Verify(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var sp = GetDefaultServiceProvider(mock.Object);

        var error = Assert.Throws<ConnectorConfigurationException>(
            () => new AppRunner<ConsoleMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("connector list")
        );

        Assert.Equal("Could not validate installed connectors.", error.Message);
    }

    [Fact]
    public void List_ByDefault_ReturnsSuccess()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(m => m.Verify(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mock.Setup(m => m.List(It.IsAny<string>()))
            .Returns(
                () => new List<(string name, ConnectorData data)>
                {
                    ("Connector", new ConnectorData(new ConnectorSettings(), null))
                }
            );

        var sp = GetDefaultServiceProvider(mock.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector list filter");

        result.ExitCode.Should().Be(0);
    }

    [Fact]
    public void Find_ByDefault_ReturnsSuccess()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(m => m.Find(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new List<ConnectorMetadata> { new("Connector", "1.0.0") });

        var sp = GetDefaultServiceProvider(mock.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector find -f search --prerelease");

        result.ExitCode.Should().Be(0);
    }

    [Fact]
    public void Add_ByDefault_ReturnsSuccess()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(
                m => m.Add(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .Verifiable();

        var sp = GetDefaultServiceProvider(mock.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector add -c config -v 0.9.0 -f --prerelease Sequence.Connector");

        result.ExitCode.Should().Be(0);

        mock.Verify();
    }

    [Fact]
    public void Add_WithPartialNameThatExists_ReturnsSuccess()
    {
        const string id   = "Sequence.Connector";
        const string name = "Connector";

        var connectors = new ConnectorMetadata[] { new(id, "0.9.0") };

        var cm = new Mock<IConnectorManager>();

        cm.Setup(
                m => m.Add(
                    It.Is<string>(s => s.Equals(id)),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .Verifiable();

        cm.Setup(
                m => m.Find(
                    It.Is<string>(s => s.Equals(name)),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(connectors)
            .Verifiable();

        var sp = GetDefaultServiceProvider(cm.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"connector add -c config -v 0.9.0 -f --prerelease {name}");

        result.ExitCode.Should().Be(0);

        cm.Verify();
    }

    [Fact]
    public void Add_WhenMoreThanOneNameMatches_LogsError()
    {
        const string name = "Connector";

        var connectors = new ConnectorMetadata[]
        {
            new("Sequence.Connector", "0.9.0"), new("Sequence.Connector.Another", "0.9.0")
        };

        var cm = new Mock<IConnectorManager>();

        cm.Setup(
                m => m.Find(
                    It.Is<string>(s => s.Equals(name)),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(connectors)
            .Verifiable();

        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory, null, cm.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"connector add -c config -v 0.9.0 -f --prerelease {name}");

        factory.Sink.LogEntries.Where(x => x.LogLevel == LogLevel.Error)
            .Select(x => x.Message)
            .Should()
            .BeEquivalentTo($"Several Connector names match '{name}':");

        cm.Verify();
    }

    [Fact]
    public void Update_ByDefault_ReturnsSuccess()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(m => m.Verify(It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();

        mock.Setup(
                m => m.Update(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .Verifiable();

        var sp = GetDefaultServiceProvider(mock.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector update -v 0.9.0 --prerelease Sequence.Connector");

        result.ExitCode.Should().Be(0);

        mock.Verify();
    }

    [Fact]
    public void Update_WhenValidationFails_Throws()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(m => m.Verify(It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var sp = GetDefaultServiceProvider(mock.Object);

        var error = Assert.Throws<ConnectorConfigurationException>(
            () => new AppRunner<ConsoleMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("connector update -v 0.9.0 --prerelease Sequence.Connector")
        );

        Assert.Equal("Could not validate installed connectors.", error.Message);
    }

    [Fact]
    public void Update_WithPartialNameThatExists_ReturnsSuccess()
    {
        const string id   = "Sequence.Connector";
        const string name = "Connector";

        var connectors = new List<(string name, ConnectorData data)>
        {
            (id, new ConnectorData(new ConnectorSettings(), null))
        };

        var cm = new Mock<IConnectorManager>();

        cm.Setup(m => m.Verify(It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();

        cm.Setup(
                m => m.Update(
                    It.Is<string>(s => s.Equals(id)),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .Verifiable();

        cm.Setup(m => m.List(It.Is<string>(s => s.Equals(name))))
            .Returns(connectors)
            .Verifiable();

        var sp = GetDefaultServiceProvider(cm.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"connector update -v 0.9.0 --prerelease {name}");

        result.ExitCode.Should().Be(0);

        cm.Verify();
    }

    [Fact]
    public void Update_WhenMoreThanOneNameMatches_LogsError()
    {
        const string name = "Connector";

        var connectors = new List<(string name, ConnectorData data)>
        {
            ("Sequence.Connector", new ConnectorData(new ConnectorSettings(),         null)),
            ("Sequence.Connector.Another", new ConnectorData(new ConnectorSettings(), null))
        };

        var cm = new Mock<IConnectorManager>();

        cm.Setup(m => m.Verify(It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();

        cm.Setup(m => m.List(It.Is<string>(s => s.Equals(name))))
            .Returns(connectors)
            .Verifiable();

        var factory = TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));

        var sp = GetDefaultServiceProvider(factory, null, cm.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem($"connector update -v 0.9.0 --prerelease {name}");

        factory.Sink.LogEntries.Where(x => x.LogLevel == LogLevel.Error)
            .Select(x => x.Message)
            .Should()
            .BeEquivalentTo($"More than one configuration matches '{name}'");

        cm.Verify();
    }

    [Fact]
    public void Remove_ByDefault_CallsManagerRemove()
    {
        var mock = new Mock<IConnectorManager>();

        mock.Setup(
                m => m.Remove(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())
            )
            .Verifiable();

        var sp = GetDefaultServiceProvider(mock.Object);

        var result = new AppRunner<ConsoleMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector remove --configurationOnly Sequence.Connector");

        result.ExitCode.Should().Be(0);

        mock.Verify();
    }
}
