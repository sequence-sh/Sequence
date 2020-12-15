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
using Microsoft.Extensions.Options;
using Reductech.EDR;
using Xunit;

namespace EDR.Tests
{
    public class EDRMethodsTests
    {

        private static IServiceProvider GetDefaultServiceProvider(ILogger<EDRMethods> logger) =>
            GetDefaultServiceProvider(logger, null);
        
        private static IServiceProvider GetDefaultServiceProvider(ILogger<EDRMethods> logger,
            IFileSystem fileSystem)
        {
            var config = Options.Create(new NuixConfig
            {
                UseDongle = true,
                ExeConsolePath = "Test Path",
                Version = new Version(0, 0),
                Features = new[] { "CASE_CREATION", "METADATA_IMPORT" }
            });

            var edrm = fileSystem == null ? new EDRMethods(logger, config) :
                new EDRMethods(logger, config, fileSystem);

            var serviceProvider = new ServiceCollection().AddSingleton(edrm).BuildServiceProvider();

            return serviceProvider;
        }

        [Fact]
        public void Execute_WhenCommandFunctionIsSuccess_LogsMessage()
        {
            var logger = new TestLogger<EDRMethods>();
            var sp = GetDefaultServiceProvider(logger);

            var result = new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("-c \"Print(Value = 'Hello World')\"");

            result.ExitCode.Should().Be(0);
            result.Console.OutText().Should().Be("");

            logger.LoggedValues.Should().BeEquivalentTo(new object[] { "Hello World" });
        }
        
        [Fact]
        public void Execute_WhenPathFunctionIsSuccess_LogsMessage()
        {
            var logger = new TestLogger<EDRMethods>();

            var filePath = @"C:\config.scl";
            
            var fs = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { filePath, new MockFileData("Print(Value = 'Hello World')") }
            });
            
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
            var sp = GetDefaultServiceProvider(logger);

            var result = new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("-c \"Pront(Value = 'Hello World')\"");

            result.ExitCode.Should().Be(0);
            result.Console.OutText().Should().Be("");

            logger.LoggedValues.Should().BeEquivalentTo(new object[] { "The step 'Pront' does not exist", "Line: 1, Col: 1, Idx: 0 - Line: 1, Col: 29, Idx: 28", "{Error} - {Location}" });
        }

        [Fact]
        public void Execute_WhenYamlAndPathAreNull_Throws()
        {
            var logger = new TestLogger<EDRMethods>();
            var sp = GetDefaultServiceProvider(logger);

            var result = Assert.Throws<ArgumentException>(() => new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("-c \"\"")
            );

            Assert.Equal("Please provide either yaml or path", result.Message);
        }

    }
}
