using System;
using System.Collections.Generic;
using CommandDotNet;
using CommandDotNet.TestTools;
using FluentAssertions;
using Reductech.EDR;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.Steps.Meta;
using Xunit;

namespace EDR.Tests
{
    public class EDRMethodsTests
    {
        [Fact]
        public void TestSuccessfulFunction()
        {
            var logger = new TestLogger();


            EDRMethods.StaticLogger = logger;
            EDRMethods.StaticSettings = new NuixSettings(true, "Test Path", new Version(0,0), new List<NuixFeature>());


            var result = new AppRunner<EDRMethods>()
                .UseDefaultMiddleware()
                .RunInMem("-c \"Print(Value = 'Hello World')\"");

            result.ExitCode.Should().Be(0);
            result.Console.OutText().Should().Be("");

            logger.LoggedValues.Should().BeEquivalentTo(new object[] {"Hello World"});
        }


        [Fact]
        public void TestFailingFunction()
        {
            var logger = new TestLogger();


            EDRMethods.StaticLogger = logger;
            EDRMethods.StaticSettings = new NuixSettings(true, "Test Path", new Version(0, 0), new List<NuixFeature>());


            var result = new AppRunner<EDRMethods>()
                .UseDefaultMiddleware()
                .RunInMem("-c \"Pront(Value = 'Hello World')\"");

            result.ExitCode.Should().Be(0);
            result.Console.OutText().Should().Be("");

            logger.LoggedValues.Should().BeEquivalentTo(new object[] { "The step 'Pront' does not exist", "Line: 1, Col: 1, Idx: 0 - Line: 1, Col: 29, Idx: 28", "{Error} - {Location}" });
        }


    }
}
