using System;
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

        private IServiceProvider getDefaultServiceProvider(ILogger<EDRMethods> logger)
        {
            var config = Options.Create(new NuixConfig
            {
                UseDongle = true,
                ExeConsolePath = "Test Path",
                Version = new Version(0, 0),
                Features = Array.Empty<string>()
            });

            var edrm = new EDRMethods(logger, config);

            var serviceProvider = new ServiceCollection().AddSingleton(edrm).BuildServiceProvider();

            return serviceProvider;
        }

        [Fact]
        public void TestSuccessfulFunction()
        {
            var logger = new TestLogger<EDRMethods>();
            var sp = getDefaultServiceProvider(logger);

            var result = new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("-c \"Print(Value = 'Hello World')\"");

            result.ExitCode.Should().Be(0);
            result.Console.OutText().Should().Be("");

            logger.LoggedValues.Should().BeEquivalentTo(new object[] { "Hello World" });
        }


        [Fact]
        public void TestFailingFunction()
        {
            var logger = new TestLogger<EDRMethods>();
            var sp = getDefaultServiceProvider(logger);

            var result = new AppRunner<EDRMethods>()
                .UseMicrosoftDependencyInjection(sp)
                .UseDefaultMiddleware()
                .RunInMem("-c \"Pront(Value = 'Hello World')\"");

            result.ExitCode.Should().Be(0);
            result.Console.OutText().Should().Be("");

            logger.LoggedValues.Should().BeEquivalentTo(new object[] { "The step 'Pront' does not exist", "Line: 1, Col: 1, Idx: 0 - Line: 1, Col: 29, Idx: 28", "{Error} - {Location}" });
        }
        
    }
}
