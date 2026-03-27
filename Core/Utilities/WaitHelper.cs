using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using log4net;

namespace Core.Utilities
{
    /// <summary>
    /// Helper class for explicit waits on WebDriver elements.
    /// Provides methods to wait for element visibility and attribute changes.
    /// Uses timeout from appsettings.json configuration.
    /// </summary>
    public static class WaitHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WaitHelper));
        private static int _timeout = ConfigLoader.Settings.Timeouts.ExplicitWait;

        private static WebDriverWait CreateWait(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(_timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            return wait;
        }

        public static IWebElement WaitOneUntilVisible(IWebDriver driver, By locator)
        {
            var wait = CreateWait(driver);

            try
            {
                var element = wait.Until(d =>
                {
                    var el = d.FindElement(locator);
                    return el.Displayed ? el : null;
                });

                Log.InfoFormat("Element found: {0}", locator);
                return element;
            }
            catch (WebDriverTimeoutException)
            {
                Log.ErrorFormat("Element not found after {0} seconds of waiting: {1}", _timeout, locator);
                throw;
            }
        }


        public static IReadOnlyList<IWebElement> WaitGroupUntilVisible(IWebDriver driver, By locator)
        {
            var wait = CreateWait(driver);

            try
            {
                var elements = wait.Until(d =>
                {
                    var els = d.FindElements(locator);

                    var visible = els.Where(e => e.Displayed).ToList();

                    return visible.Count > 0 ? visible : null;
                });

                Log.InfoFormat("Elements found: {0} (count = {1})", locator, elements.Count);
                return elements;
            }
            catch (WebDriverTimeoutException)
            {
                Log.ErrorFormat("Elements not found after {0} seconds of waiting: {1}", _timeout, locator);
                throw;
            }
        }

        public static bool WaitUntilAttributeEquals(IWebDriver driver, By locator, string attributeName, string expectedValue)
        {
            var wait = CreateWait(driver);

            try
            {
                bool attrEquals = wait.Until(d =>
                {
                    var element = d.FindElement(locator);
                    var actualValue = element.GetAttribute(attributeName);
                    return actualValue == expectedValue;
                });
                Log.InfoFormat("Attribute {0} is equal {1}", attributeName, expectedValue);
                return attrEquals;
            }
            catch(WebDriverTimeoutException)
            {
                Log.ErrorFormat("Attribute '{0}' did not equal '{1}' after {2} seconds of waiting: {3}", attributeName, expectedValue, _timeout, locator);
                throw;
            }
        }
    }
}
