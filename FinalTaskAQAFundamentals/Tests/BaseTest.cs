using Core.WebDriver;

namespace Tests;

public abstract class BaseTest
{
    [SetUp]
    public void Setup()
    {
        WebDriverFactory.InitDriver();
    }

    [TearDown]
    public void TearDown()
    {
        WebDriverFactory.QuitDriver();
    }
}
