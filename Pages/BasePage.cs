using Core.Utilities;
using Core.WebDriver;
using OpenQA.Selenium;

namespace Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }
        public abstract string PageUrl { get; }

        protected BasePage()
        {
            Driver = WebDriverFactory.GetDriver();
        }

        public void Open()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }

        public abstract bool IsOpened();

        public IWebElement Find(By locator)
        {
            return WaitHelper.WaitOneUntilVisible(Driver, locator);
        }

        public IReadOnlyList<IWebElement> FindAll(By locator)
        {
            return WaitHelper.WaitGroupUntilVisible(Driver, locator);
        }

        public void Click(By locator)
        {
            Find(locator).Click();
        }

        public void ClearInput(By locator)
        {
            var element = Driver.FindElement(locator);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Backspace);
            element.SendKeys(Keys.Tab);
        }

        public void Type(By locator, string text)
        {
            var element = Find(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public string GetText(By locator)
        {
            return Find(locator).Text;
        }

        public bool IsDisplayed(By locator)
        {
            try
            {
                return Find(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void GoBack()
        {
            Driver.Navigate().Back();
        }

    }
}
