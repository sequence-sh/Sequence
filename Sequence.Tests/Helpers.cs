using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using MELT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Reductech.Sequence;
using Reductech.Sequence.ConnectorManagement.Base;

namespace Sequence.Tests;

public static class Helpers
{
    internal const string TheUltimateTestString = "Hello World";

    internal static IServiceProvider GetDefaultServiceProvider() =>
        GetDefaultServiceProvider(null, null, null);

    internal static IServiceProvider GetDefaultServiceProvider(ILoggerFactory loggerFactory) =>
        GetDefaultServiceProvider(loggerFactory, null, null);

    internal static IServiceProvider
        GetDefaultServiceProvider(IConnectorManager connectorManager) =>
        GetDefaultServiceProvider(null, null, connectorManager);

    internal static IServiceProvider GetDefaultServiceProvider(
        ILoggerFactory? loggerFactory,
        IFileSystem? fileSystem,
        IConnectorManager? connectorManager)
    {
        var lf = loggerFactory ?? TestLoggerFactory.Create(x => x.SetMinimumLevel(LogLevel.Debug));
        var fs = fileSystem ?? new MockFileSystem();
        var connMan = connectorManager ?? new FakeConnectorManager();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(new ConnectorCommand(connMan, lf.CreateLogger<ConnectorCommand>()))
            .AddSingleton<IAnalyticsWriter>(new NullAnalyticsWriter())
            .AddSingleton(
                new RunCommand(
                    lf.CreateLogger<RunCommand>(),
                    fs,
                    connMan,
                    new NullAnalyticsWriter()
                )
            )
            .AddSingleton(new StepsCommand(connMan))
            .AddSingleton(new ValidateCommand(lf.CreateLogger<ValidateCommand>(), fs, connMan))
            .AddSingleton<ConsoleMethods>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}
