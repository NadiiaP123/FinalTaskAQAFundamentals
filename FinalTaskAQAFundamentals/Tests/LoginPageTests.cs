using Core.Config;
using Core.WebDriver;
using Pages;
using Tests.Data;

namespace Tests;

public class LoginPageTests : BaseTest
{
    private LoginPage _page;
    string _username;
    
    
    [SetUp]
    public void PageSetup()
    {
        _page = new LoginPage();
        _username = TestDataLoader.Data.ValidUsernames[0];
    }

    [TestCaseSource(typeof(WebDriverFactory), nameof(WebDriverFactory.GetEnabledBrowsers))]
    [Test]
    public void Login_WithOnlyUsername_ShouldShowPasswordRequiredError(BrowserType browser)
    {
        _page.Open();

        Assert.That(_page.IsOpened(), Is.True, "Login page was not opened.");

        _page.Login(_username, "");

        Assert.That(
            _page.GetErrorMessage(),
            Does.Contain("Password is required"),
            "Unexpected error message is displayed.");
    }
}
