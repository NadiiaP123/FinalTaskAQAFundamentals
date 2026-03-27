using Core.Config;
using Core.WebDriver;
using Pages;
using Tests.Data;

namespace Tests;

[TestFixtureSource(typeof(WebDriverFactory), nameof(WebDriverFactory.GetEnabledBrowsers))]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Category("UC1")]
public class UC1 : BaseTest
{
    private readonly BrowserType _browser;
    private LoginPage _page;
    private string _username;
    private string _password;
    public UC1 (BrowserType browser)
    {
        _browser = browser;
    }

    [SetUp]
    public void SetUp()
    {
        LogStart(_browser);
        StartDriver(_browser);
        _username = TestDataLoader.Data.ValidUsernames[0];
        _password = TestDataLoader.Data.ValidPasswords[0];
        _page = new LoginPage();
    }

    [Test]
    public void Login_WithOnlyUsername_ShouldShowPasswordRequiredError()
    {
        // Act
        Log.Info("Try Login with empty password");
        _page.Open();
        _page.EnterUsername(_username);
        _page.ClickLoginButton();
        _page.CloseErrorMessage();
        _page.EnterPassword(_password);
        _page.ClearPassword();
        _page.ClickLoginButton();

        // Assert
        Log.Info("Assert that an error message \"Password is required\" appears.");
        Assert.That(_page.GetErrorMessage(), Does.Contain("Password is required"), "Unexpected error message is displayed.");      
    }
}
