using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Config
{
    public sealed class TestSettings
    {
        public required string BaseUrl { get; set; }
        public required TimeoutSettings Timeouts { get; set; }

        public required RunSettings Run { get; set; }

        public required Dictionary<string, BrowserSettings> Browsers { get; set; }

        public Dictionary<BrowserType, BrowserSettings> BrowserTypes { get; set; } = new();
    }
}
