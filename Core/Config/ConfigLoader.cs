
using System.Text.Json;

namespace Core.Config
{
    /// <summary>
    /// Loads application settings from "appsettings.json",
    /// validates them, and exposes them through the Settings property.
    /// </summary>
    public static class ConfigLoader
    {
        public static TestSettings Settings { get; } = Load();

        private static TestSettings Load()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"appsettings.json file not found: {path}");
            }
            var json = File.ReadAllText(path);
            var config = JsonSerializer.Deserialize<TestSettings>(json);

            ArgumentNullException.ThrowIfNull(config);

            ValidateTimeouts(config);
            ValidateBrowsers(config);

            config.BrowserTypes = config.Browsers.ToDictionary(
                br => (BrowserType)Enum
                .Parse(typeof(BrowserType), br.Key, true), br => br.Value);

            return config;
        }

        private static void ValidateTimeouts(TestSettings config)
        {
            if (config.Timeouts == null)
            {
                throw new InvalidOperationException("Timeouts section is missing.");
            }

            if (config.Timeouts.ImplicitWait < 0)
            {
                throw new InvalidOperationException("ImplicitWait timeout must be positive or zero.");
            }

            if (config.Timeouts.ExplicitWait <= 0)
            {
                throw new InvalidOperationException("ExplicitWait timeout must be positive.");
            }

            if (config.Timeouts.PageLoad <= 0)
            {
                throw new InvalidOperationException("PageLoad timeout must be positive.");
            }
        }

        private static void ValidateBrowsers(TestSettings config)
        {        
            
            if (config.Browsers == null || config.Browsers.Count == 0)
            {
                throw new InvalidOperationException("Browsers cannot be null or empty.");
            }

            if (!config.Browsers.Values.Any(b => b.Enabled))
            {
                throw new InvalidOperationException("At least one browser must be enabled.");
            }       

            foreach (var key in config.Browsers.Keys)
            {
                if (!Enum.TryParse<BrowserType>(key, true, out _))
                {
                    throw new InvalidOperationException($"Invalid browser: {key}");
                }
            }
        }
    }
}
