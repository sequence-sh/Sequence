using System.Collections.Generic;
using System.Threading;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using CommandDotNet.TestTools;
using FluentAssertions;
using Moq;
using Reductech.EDR;
using Reductech.EDR.ConnectorManagement.Base;
using Xunit;
using static EDR.Tests.Helpers;

namespace EDR.Tests
{

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
            () => new AppRunner<EDRMethods>()
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

        var result = new AppRunner<EDRMethods>()
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

        var result = new AppRunner<EDRMethods>()
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

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector add -c config -v 0.9.0 -f --prerelease EDR.Connector");

        result.ExitCode.Should().Be(0);

        mock.Verify();
    }

    [Fact]
    public void Update_ByDefault_ReturnsSuccess()
    {
        var mock = new Mock<IConnectorManager>();

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

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector update -v 0.9.0 --prerelease EDR.Connector");

        result.ExitCode.Should().Be(0);

        mock.Verify();
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

        var result = new AppRunner<EDRMethods>()
            .UseMicrosoftDependencyInjection(sp)
            .UseDefaultMiddleware()
            .RunInMem("connector remove --configurationOnly EDR.Connector");

        result.ExitCode.Should().Be(0);

        mock.Verify();
    }
}

}
