namespace Core.Config
{
    public sealed class TestSettings
    {
        public required TimeoutSettings Timeouts { get; set; }
        public required Dictionary<string, BrowserSettings> Browsers { get; set; }
        public Dictionary<BrowserType, BrowserSettings> BrowserTypes { get; set; } = new();
    }
}
