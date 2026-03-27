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
        _page = new LoginPage();
    }

    [Test]
    public void Login_WithOnlyUsername_ShouldShowPasswordRequiredError()
    {    
        // Act
        _page.Login(_username, "");
        Log.InfoFormat("Login with ({0}, {1}) values.", _username, ' ');

        // Assert
        Assert.That(_page.GetErrorMessage(), Does.Contain("Password is required"), "Unexpected error message is displayed.");
        Log.Info("Assert that an error message \"Password is required\" appears.");
    }
}
