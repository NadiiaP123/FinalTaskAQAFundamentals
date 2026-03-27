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

        protected IReadOnlyList<IWebElement> FindAll(By locator)
        {
            return WaitHelper.WaitGroupUntilVisible(Driver, locator);
        }

        public void Click(By locator)
        {
            Find(locator).Click();
        }

        protected void Type(By locator, string text)
        {
            var element = Find(locator);
            element.Clear();
            element.SendKeys(text);
        }
        protected string GetText(By locator)
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
