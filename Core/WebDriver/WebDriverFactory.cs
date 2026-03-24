using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.WebDriver
{

    /// <summary>
    /// Creates and manages per-thread IWebDriver instances.
    /// Distributes browsers for parallel execution and applies configuration
    /// from appsettings.json (browser type, headless mode, timeouts).
    /// </summary>
    public static class WebDriverFactory
    {
        private static readonly List<BrowserType> _browserPool = BuildBrowserPool();
        private static int _browserIndex = -1;
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
                throw new InvalidOperationException("WebDriver is not initialized for the current thread.");
            }

            return _driver.Value;
        }

        private static List<BrowserType> BuildBrowserPool()
        {
            var browserList = new List<BrowserType>();
            foreach (var browser in ConfigLoader.Settings.BrowserTypes)
            {
                if (browser.Value.Enabled)
                {
                    browserList.AddRange(Enumerable.Repeat(browser.Key, browser.Value.Instances));
                }
            }

            return browserList;
        }


        private static BrowserType GetNextBrowser()
        {
            if (_browserPool.Count == 0)
            {
                throw new InvalidOperationException("Browser pool is empty.");
            }
            var index = Interlocked.Increment(ref _browserIndex);

            var browser = _browserPool[index % _browserPool.Count];

            return browser;
        }


        public static void InitDriver()
        {
            var browser = GetNextBrowser();

            var driver = CreateDriver(browser);

            _driver.Value = driver;
        }

        private static IWebDriver CreateDriver(BrowserType browserType)
        {
            var driver = browserType switch
            {
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Edge => CreateEdgeDriver(),
                BrowserType.Firefox => CreateFirefoxDriver(),
                _ => throw new ArgumentOutOfRangeException(nameof(browserType))
            };

            ApplyTimeouts(driver);
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

            return new FirefoxDriver(options);
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
    }
}
