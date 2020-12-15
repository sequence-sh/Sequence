using System;

namespace Reductech.EDR
{
    public class NuixConfig
    {
        public bool UseDongle { get; set; }
        public string ExeConsolePath { get; set; }
        public Version Version { get; set; }
        public string[] Features { get; set; }
    }
}
