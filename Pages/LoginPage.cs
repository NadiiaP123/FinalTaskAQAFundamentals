using Core.Config;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage() : base() { }
        protected override string PageUrl => "https://www.saucedemo.com";

        private static By UsernameLocator => By.Id("user-name");
        private static By PasswordLocator => By.Id("password");
        private static By LoginButtonLocator => By.Id("login-button");
        private static By ErrorContainer => By.CssSelector(".error-message-container.error h3");



        public override bool IsOpened()
        {
            return IsDisplayed(LoginButtonLocator);
        }
        public void EnterUsername(string username)
        {
            Type(UsernameLocator, username);
        }

        public void EnterPassword(string password)
        {
            Type(PasswordLocator, password);
        }

        public void ClickLoginButton()
        {
            Click(LoginButtonLocator);
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public string GetErrorMessage()
        {
            return IsDisplayed(ErrorContainer) ? GetText(ErrorContainer) : string.Empty;
        }

    }
}
