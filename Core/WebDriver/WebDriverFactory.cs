using Core.Config;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.WebDriver
{
    /// <summary>
    /// Static factory for creating and managing thread-local IWebDriver instances.
    /// Configures browser settings (type, headless mode, timeouts) from ConfigLoader.Settings.
    /// Supports Chrome, Firefox, and Edge browsers.
    /// </summary>
    public static class WebDriverFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebDriverFactory));

        private static readonly ThreadLocal<IWebDriver?> _driver = new();

        public static IEnumerable<BrowserType> GetEnabledBrowsers()
        {
            foreach (var browser in ConfigLoader.Settings.BrowserTypes)
            {
                if (browser.Value.Enabled)
                {
                    yield return  browser.Key;
                }
            }
        }

        public static IWebDriver GetDriver()
        {
            if (_driver.Value == null)
            {
                throw new InvalidOperationException("WebDriver is not initialized. Call InitDriver() first.");
            }

            return _driver.Value;
        }

        public static void InitDriver(BrowserType browserType)
        {
            _driver.Value = CreateDriver(browserType);
        }

        private static IWebDriver CreateDriver(BrowserType browserType)
        {
            var driver = browserType switch
            {
                BrowserType.Firefox => CreateFirefoxDriver(),             
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Edge => CreateEdgeDriver(),             
                _ => throw new ArgumentOutOfRangeException(nameof(browserType))
            };

            ApplyTimeouts(driver);

            bool headless = ConfigLoader.Settings.BrowserTypes[browserType].Headless;      
            Log.InfoFormat("New {0}Driver is created.", browserType);
            Log.InfoFormat("HEADLESS MODE = {0}.", headless);

            return driver;
        }

        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            var settings = ConfigLoader.Settings.BrowserTypes[BrowserType.Chrome];

            if (settings.Headless)
            {
                options.AddArgument("--headless=new");
            }

            ApplyChromiumOptions(options);
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            var settings = ConfigLoader.Settings.BrowserTypes[BrowserType.Edge];

            if (settings.Headless)
            {
                options.AddArgument("--headless=new");
            }

            ApplyChromiumOptions(options);
            options.AddArgument("start-maximized");

            return new EdgeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            var settings = ConfigLoader.Settings.BrowserTypes[BrowserType.Firefox];

            if (settings.Headless)
            {
                options.AddArgument("--headless");
            }

            options.SetPreference("signon.rememberSignons", false);
            options.SetPreference("dom.webnotifications.enabled", false);
            options.SetPreference("dom.push.enabled", false);
            options.SetPreference("intl.accept_languages", "en,en-US");

            var driver = new FirefoxDriver(options);
            driver.Manage().Window.Maximize();

            return driver;
        }

        private static void ApplyTimeouts(IWebDriver driver)
        {
            var timeouts = ConfigLoader.Settings.Timeouts;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeouts.ImplicitWait);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeouts.PageLoad);
        }

        public static void QuitDriver()
        {
            if (_driver.Value == null)
            {
                return;
            }
            try
            {
                _driver.Value.Quit();
            }
            finally
            {
                _driver.Value = null;
            }
        }

        private static void ApplyChromiumOptions(ChromiumOptions options)
        {
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArgument("--guest");
            options.AddArgument("--lang=en");
        }
    }
}
