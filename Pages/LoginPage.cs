using OpenQA.Selenium;

namespace Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage() : base() { }
        public override string PageUrl => "https://www.saucedemo.com";
        public static By UsernameLocator => By.Id("user-name");
        public static By PasswordLocator => By.Id("password");
        public static By LoginButtonLocator => By.Id("login-button");
        public static By ErrorContainer => By.CssSelector(".error-message-container.error h3");

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
            Open();
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
