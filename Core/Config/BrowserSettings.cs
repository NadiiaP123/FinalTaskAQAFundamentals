using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Config
{
    public sealed class BrowserSettings
    {
        public required bool Enabled { get; set; }
        public required int Instances { get; set; }
        public required bool Headless { get; set; }
    }
}
