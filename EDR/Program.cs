using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using CSharpFunctionalExtensions;
using Reductech.EDR.Connectors.Introspect.Query.processes;
using Reductech.EDR.Connectors.Nuix;
using Reductech.EDR.Connectors.Nuix.processes.meta;
using Reductech.EDR.Processes;
using Reductech.EDR.Processes.Mutable;
using Reductech.Utilities.InstantConsole;

namespace Reductech.EDR
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var (nuixSuccess, _, nuixSettings, nuixError)  = NuixProcessSettings.TryCreate(sn => ConfigurationManager.AppSettings[sn]);
            var (idolSuccess, _, idolSettings, idolError)  = IdolProcessSettings.TryCreate(sn => ConfigurationManager.AppSettings[sn]);

            if (nuixSuccess && idolSuccess)
            {
                var settings = new CombinedSettings(nuixSettings, idolSettings);

                var nuixProcesses = DynamicProcessFinder.GetAllDocumented(settings,
                    new DocumentationCategory("Nuix Processes", typeof(RubyScriptProcess)), typeof(RubyScriptProcess));

                var introspectProcesses = DynamicProcessFinder.GetAllDocumented(settings,
                    new DocumentationCategory("Introspect Processes", typeof(IdolProcess)), typeof(IdolProcess));

                var generalProcesses = DynamicProcessFinder.GetAllDocumented(settings,
                    new DocumentationCategory("General Processes", typeof(Process)), typeof(Process));

                var processes = nuixProcesses.Concat(introspectProcesses).Concat(generalProcesses);

                ConsoleView.Run(args, processes);
            }
            else
            {
                if(!nuixSuccess)
                    foreach (var l in nuixError.Split("\r\n"))
                        Console.WriteLine(l);

                if(!idolSuccess)
                    foreach (var l in idolError.Split("\r\n"))
                        Console.WriteLine(l);
            }
        }

        internal class CombinedSettings : INuixProcessSettings, IIdolProcessSettings
        {
            private readonly INuixProcessSettings _nuixProcessSettingsImplementation;
            private readonly IIdolProcessSettings _idolProcessSettingsImplementation;

            public CombinedSettings(INuixProcessSettings nuixProcessSettingsImplementation, IIdolProcessSettings idolProcessSettingsImplementation)
            {
                _nuixProcessSettingsImplementation = nuixProcessSettingsImplementation;
                _idolProcessSettingsImplementation = idolProcessSettingsImplementation;
            }

            /// <inheritdoc />
            public bool UseDongle => _nuixProcessSettingsImplementation.UseDongle;

            /// <inheritdoc />
            public string NuixExeConsolePath => _nuixProcessSettingsImplementation.NuixExeConsolePath;

            /// <inheritdoc />
            public Version NuixVersion => _nuixProcessSettingsImplementation.NuixVersion;

            /// <inheritdoc />
            public IReadOnlyCollection<NuixFeature> NuixFeatures => _nuixProcessSettingsImplementation.NuixFeatures;

            /// <inheritdoc />
            public string IdolHost => _idolProcessSettingsImplementation.IdolHost;

            /// <inheritdoc />
            public int IdolPort => _idolProcessSettingsImplementation.IdolPort;

            /// <inheritdoc />
            public int IdolIndexPort => _idolProcessSettingsImplementation.IdolIndexPort;

            /// <inheritdoc />
            public bool IdolSSL => _idolProcessSettingsImplementation.IdolSSL;
        }

    }
}
