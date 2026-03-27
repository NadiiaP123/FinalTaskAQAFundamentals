using Core.Config;
using Core.WebDriver;
using Pages;
using Tests.Data;


namespace Tests;

[TestFixtureSource(typeof(WebDriverFactory), nameof(WebDriverFactory.GetEnabledBrowsers))]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Category("UC2")]
public class UC2 : BaseTest
{
    private readonly BrowserType _browser;
    private LoginPage _loginPage;
    private InventoryPage _inventoryPage;
    private string _username;
    private string _password;

    public UC2(BrowserType browser)
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
        _loginPage.Login(_username,  _password);
        _inventoryPage = new InventoryPage();
    }

    [Test, Order(1)]
    public void Login_WithValidCredentials_OpensMainPage()
    {
        // Act
        Log.Info("Use InventoryPage.IsOpened() method.");
        bool actual = _inventoryPage.IsOpened();

        // Assert
        Assert.That(actual, Is.True, "InventoryPage is not opened.");
        Log.Info("Assert that after valid login - InventoryPage is opened.");
    }

    [Test]
    public void MainPageContains_BurgerMenuButton()
    {
        // Act
        Log.Info("Use InventoryPage.IsDisplayed(InventoryPage.BurgerMenuButton) method.");
        bool actual = _inventoryPage.IsDisplayed(InventoryPage.BurgerMenuButton);

        // Assert
        Assert.That(actual, Is.True, "BurgerMenuButton is not displayed.");
        Log.Info("Assert that BurgerMenuButton is displayed.");
    }


    [Test]
    public void MainPageContains_SwagLabel()
    {
        // Act
        Log.Info("Use InventoryPage.IsDisplayed(InventoryPage.SwagLabel) method.");
        bool actual = _inventoryPage.IsDisplayed(InventoryPage.SwagLabel);

        // Assert
        Assert.That(actual, Is.True, "SwagLabel is not displayed.");
        Log.Info("Assert that SwagLabel is displayed.");
    }

    [Test]
    public void MainPageContains_ShoppingCartIcon()
    {
        // Act
        Log.InfoFormat("Use InventoryPage.IsDisplayed(InventoryPage.ShoppingCartIcon) method.");
        bool actual = _inventoryPage.IsDisplayed(InventoryPage.ShoppingCartIcon);

        // Assert
        Assert.That(actual, Is.True, "ShoppingCartIcon is not displayed.");
        Log.Info("Assert that ShoppingCartIcon is displayed.");
    }

    [Test]
    public void MainPageContains_SortDropdown()
    {
        // Act
        Log.InfoFormat("Use InventoryPage.IsDisplayed(InventoryPage.SortDropdown) method.");
        bool actual = _inventoryPage.IsDisplayed(InventoryPage.SortDropdown);

        // Assert
        Assert.That(actual, Is.True, "SortDropdown is not displayed.");
        Log.Info("Assert that SortDropdown is displayed.");
    }

    [Test]
    public void MainPageContains_NonEmptyInventoryList()
    {
        // Act
        Log.Info("Use InventoryPage.CountVisibleInventoryItems() method.");
        int visibleItems = _inventoryPage.CountVisibleInventoryItems();
        
        // Assert
        Assert.That(visibleItems > 0, Is.True, "InventoryList is not displayed or empty.");
        Log.Info("Assert that InventoryPage contains list of inventory items.");
    }

    [Test]
    public void ClickingBurgerMenuButton_ShowsSideMenu()
    {
        // Act
        Log.Info("Click on InventoryPage.BurgerMenuButton and use InventoryPage.WaitUntilSideMenuIsOpened() method.");
        _inventoryPage.Click(InventoryPage.BurgerMenuButton);
        bool menuOpened = _inventoryPage.WaitUntilSideMenuIsOpened();     

        // Assert
        Assert.That(menuOpened, Is.True, "SideMenu is still hidden.");
        Log.Info("Assert that SideMenu is open.");
    }


    [Test]
    public void ClickingShoppingCartIcon_OpensCartPage()
    {
        // Act
        Log.Info("Click onInventoryPage.ShoppingCartIcon, check if CartPage.CheckoutButton is displayed and go back to InventoryPage.");
        _inventoryPage.Click(InventoryPage.ShoppingCartIcon);
        bool result = _inventoryPage.IsDisplayed(CartPage.CheckoutButton);
        _inventoryPage.GoBack();
        
        // Assert
        Assert.That(result, Is.True, "Cart Page was not opened.");
        Log.Info("Assert that clicking ShoppingCartIcon opens CartPage.");
    }


    [Test]
    public void SortingAtoZ_SortsInventoryListIn_AZ_Order()
    {
        // Act
        Log.Info("Use InventoryPage.SortAtoZ() method then order (A-Z) ItemNamesList.");
        _inventoryPage.SortAtoZ();
        var actualList = _inventoryPage.GetItemNamesList();
        var expectedList = actualList.OrderBy(n => n).ToList();
        
        // Assert
        Assert.That(actualList, Is.EqualTo(expectedList), "A-Z sorting does not work as expected.");
        Log.Info("Assert that A-Z sorting works as expected.");
    } 
}
