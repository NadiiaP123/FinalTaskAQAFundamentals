using Core.Config;
using Core.WebDriver;
using Pages;
using Tests.Data;

namespace Tests;

[TestFixtureSource(typeof(WebDriverFactory), nameof(WebDriverFactory.GetEnabledBrowsers))]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Category("UC3")]
public class UC3 : BaseTest
{
    private readonly BrowserType _browser;
    private LoginPage _loginPage;
    private InventoryPage _inventoryPage;
    private InventoryItemPage _inventoryItemPage;
    private string _username;
    private string _password;

    public UC3 (BrowserType browser)
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
        _loginPage = new LoginPage();
        _loginPage.Login(_username, _password);
        _inventoryPage = new InventoryPage();
        _inventoryItemPage = new InventoryItemPage();
    }

    [Test, Order(1)]
    public void ClickingRandomInventoryItem_OpensInventoryDetails()
    {
        // Act
        Log.Info("Open random inventory item details.");
        _inventoryPage.OpenRandomAvailableInventoryItem();
        bool actual = _inventoryItemPage.IsOpened();

        // Assert
        Assert.That(actual, Is.True, "InventoryItemPage is not opened.");
        Log.Info("Assert that clicking any inventory opens a page with details (InventoryItemPage)");
    }

    [Test]
    public void InventoryItemPageContains_AddToCartButton()
    {
        // Act
        Log.Info("Open randon inventory and use InventoryItemPage.IsDisplayed(InventoryItemPage.AddToCartButton) method.");
        _inventoryPage.OpenRandomAvailableInventoryItem();
        bool actual = _inventoryItemPage.IsDisplayed(InventoryItemPage.AddToCartButton);

        // Assert
        Assert.That(actual, Is.True, "AddToCartButton is not displayed.");
        Log.Info("Assert that AddToCartButton is displayed.");
    }

    [Test]
    public void AddingInventoryToCart_UpdatesShoppingCartIcon()
    {
        // Act
        Log.Info("Open random inventory item details. Count current number of items in the Cart. Add a new item. Count again.");
        _inventoryPage.OpenRandomAvailableInventoryItem();
        int currentNumber = _inventoryItemPage.CountItemsInCart();
        _inventoryItemPage.AddInventoryToCart();
        int newNumber = _inventoryItemPage.CountItemsInCart();

        // Assert
        Assert.That(newNumber == (currentNumber +1), Is.True, $"Current number {currentNumber} and new number {newNumber} are not equal.");
        Log.Info("Assert that adding item to the Cart increases the number by one.");
    }
}
