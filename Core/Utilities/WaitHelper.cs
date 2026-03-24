using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using log4net;

namespace Core.Utilities
{
    public static class WaitHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WaitHelper));

        public static IWebElement WaitUntilVisible(IWebDriver driver, By locator)
        {
            int timeout = ConfigLoader.Settings.Timeouts.ExplicitWait;
            
            Log.InfoFormat("Waiting up to {0}s for element: {1}", timeout, locator);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

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
                Log.ErrorFormat("Timeout waiting for element: {0}", locator);
                throw;
            }
        }
    }
}
