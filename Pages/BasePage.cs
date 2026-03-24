using Core.Utilities;
using Core.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }
        protected abstract string PageUrl { get; }

        protected BasePage()
        {
            Driver = WebDriverFactory.GetDriver();
        }

        public void Open()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }

        public abstract bool IsOpened();

        protected IWebElement Find(By locator)
        {
            return WaitHelper.WaitUntilVisible(Driver, locator);
        }

        protected void Click(By locator)
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

        protected bool IsDisplayed(By locator)
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

    }
}
