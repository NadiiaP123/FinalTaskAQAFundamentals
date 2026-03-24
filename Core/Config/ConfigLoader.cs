using System;
using System.Collections.Generic;
using System.Text;
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

            ValidateUrl(config);
            ValidateTimeouts(config);
            ValidateRunSettings(config);
            ValidateBrowsers(config);

            config.BrowserTypes = config.Browsers.ToDictionary(
                br => (BrowserType)Enum
                .Parse(typeof(BrowserType), br.Key, true), br => br.Value);

            return config;
        }

        private static void ValidateUrl(TestSettings config)
        {
            if (string.IsNullOrWhiteSpace(config.BaseUrl))
            {
                throw new InvalidOperationException("BaseUrl is required.");
            }

            if (!Uri.TryCreate(config.BaseUrl, UriKind.Absolute, out var uri) ||
                 (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new InvalidOperationException("BaseUrl must be a valid HTTP/HTTPS URL.");
            }
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


        private static void ValidateRunSettings(TestSettings config)
        {
            if (config.Run == null)
            {
                throw new InvalidOperationException("Run section is missing.");
            }

            if (config.Run.Threads <= 0)
            {
                throw new InvalidOperationException("Threads must be positive.");
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

            foreach (var browser in config.Browsers)
            {
                if (!Enum.TryParse<BrowserType>(browser.Key, true, out _))
                {
                    throw new InvalidOperationException($"Invalid browser: {browser.Key}");
                }

                if (browser.Value.Enabled && browser.Value.Instances <= 0)
                {
                    throw new InvalidOperationException($"Enabled {browser.Key} Instances must be greater than 0.");
                }
            }

            var totalInstances = config.Browsers
                    .Values
                    .Where(b => b.Enabled)
                    .Sum(b => b.Instances);

            if (totalInstances > config.Run.Threads)
            {
                throw new InvalidOperationException("Total Instances cannot be more than Threads.");
            }
        }
    }
}
