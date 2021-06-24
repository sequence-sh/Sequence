using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Reductech.EDR;
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

        Program.SetConnectorsConfigPath(config, envMock.Object, fs);

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

        Program.SetConnectorsConfigPath(config, envMock.Object, fs);

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

        Program.SetConnectorsConfigPath(config, envMock.Object, fs);

        Assert.Equal(
            path,
            config.GetSection(ConnectorManagerSettings.Key)[
                nameof(ConnectorManagerSettings.ConfigurationPath)]
        );
    }

    [Fact]
    public void CreateHostBuilder_CreatesConfigurationAndServices()
    {
        var host = Program.CreateHostBuilder().Build();

        Assert.Null(host.Services.GetService(typeof(StartupTests)));
        Assert.NotNull(host.Services.GetService(typeof(IFileSystem)));
        Assert.NotNull(host.Services.GetService(typeof(IConnectorManager)));
        Assert.NotNull(host.Services.GetService(typeof(ConnectorCommand)));
        Assert.NotNull(host.Services.GetService(typeof(RunCommand)));
        Assert.NotNull(host.Services.GetService(typeof(StepsCommand)));
        Assert.NotNull(host.Services.GetService(typeof(ValidateCommand)));
        Assert.NotNull(host.Services.GetService(typeof(EDRMethods)));
        Assert.NotNull(host.Services.GetService(typeof(ILogger<ValidateCommand>)));

        var config = host.Services.GetService(typeof(IConfiguration)) as IConfigurationRoot;

        Assert.NotNull(config);
        Assert.Contains(config!.Providers, p => p is JsonConfigurationProvider);
    }
}

}
