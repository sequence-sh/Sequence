using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Reductech.EDR.ConnectorManagement;
using Xunit;

namespace EDR.Tests
{

public class StartupTests
{
    [Fact]
    public void SetConnectorsConfigPath_WhenConfigPathIsSet_DoesNothing()
    {
        const string configPath = @"path\conf.json";

        var confValues = new Dictionary<string, string>
        {
            {
                $"{ConnectorManagerSettings.Key}:{nameof(ConnectorManagerSettings.ConfigurationPath)}",
                configPath
            }
        };

        ConfigurationBuilder builder = new();
        builder.AddInMemoryCollection(confValues);
        var config = builder.Build();

        var fs = new MockFileSystem();

        var envMock = new Mock<IHostEnvironment>();
        envMock.Setup(e => e.EnvironmentName).Returns(Environments.Development);

        Reductech.EDR.Program.SetConnectorsConfigPath(config, envMock.Object, fs);

        Assert.Equal(
            configPath,
            config.GetSection(ConnectorManagerSettings.Key)[
                nameof(ConnectorManagerSettings.ConfigurationPath)]
        );
    }

    [Fact]
    public void SetConnectorsConfigPath_WhenConfigPathIsNotSetAndFileDoesNotExist_DoesNothing()
    {
        var confValues = new Dictionary<string, string>
        {
            {
                $"{ConnectorManagerSettings.Key}:{nameof(ConnectorManagerSettings.ConnectorPath)}",
                "connectors"
            }
        };

        ConfigurationBuilder builder = new();
        builder.AddInMemoryCollection(confValues);
        var config = builder.Build();

        var fs = new MockFileSystem();

        var envMock = new Mock<IHostEnvironment>();
        envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production);

        Reductech.EDR.Program.SetConnectorsConfigPath(config, envMock.Object, fs);

        Assert.Null(
            config.GetSection(ConnectorManagerSettings.Key)[
                nameof(ConnectorManagerSettings.ConfigurationPath)]
        );

        Assert.Equal(
            "connectors",
            config.GetSection(ConnectorManagerSettings.Key)[
                nameof(ConnectorManagerSettings.ConnectorPath)]
        );
    }

    [Fact]
    public void SetConnectorsConfigPath_WhenConfigPathIsNotSetAndFileExists_SetsConfigPath()
    {
        var env  = Environments.Development;
        var path = Path.Combine(AppContext.BaseDirectory, $"connectors.{env}.json");

        ConfigurationBuilder builder = new();
        builder.AddInMemoryCollection();
        var config = builder.Build();

        var fs = new MockFileSystem();

        fs.AddFile(path, new MockFileData(string.Empty));

        var envMock = new Mock<IHostEnvironment>();
        envMock.Setup(e => e.EnvironmentName).Returns(env);

        Reductech.EDR.Program.SetConnectorsConfigPath(config, envMock.Object, fs);

        Assert.Equal(
            path,
            config.GetSection(ConnectorManagerSettings.Key)[
                nameof(ConnectorManagerSettings.ConfigurationPath)]
        );
    }
}

}
