using Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace Pages
{
    public class InventoryPage : BasePage
    {
        private static readonly Random _random = new Random();
        public InventoryPage() : base() { }
        public override string PageUrl => "https://www.saucedemo.com/inventory.html";
        public static By BurgerMenuButton => By.Id("react-burger-menu-btn");
        public static By SideMenu => By.CssSelector(".bm-menu-wrap");
        public static By SwagLabel => By.CssSelector(".header_label .app_logo");
        public static By ShoppingCartIcon => By.Id("shopping_cart_container");
        public static By SortDropdown => By.CssSelector("[data-test='product-sort-container']");
        public static By InventoryList => By.CssSelector("[data-test='inventory-list']");
        public static By InventoryItems => By.CssSelector("[data-test='inventory-item']");
        public static By InventoryItemNames => By.CssSelector("[data-test='inventory-item-name']");
        public static By CloseSideMenuButton => By.Id("react-burger-cross-btn");

        // Items with Add-to-cart button are considered available:
        public static By AvailableInventoryItems => By.XPath("//div[@data-test='inventory-item'][.//button[contains(@id,'add-to-cart')]]");
        
   

        public override bool IsOpened()
        {
            return IsDisplayed(InventoryList);
        }

        public int CountVisibleInventoryItems()
        {
            return FindAll(InventoryItems).Count;
        }

        public bool WaitUntilSideMenuIsOpened()
        {
            return WaitHelper.WaitUntilAttributeEquals(Driver, SideMenu, "aria-hidden", "false");
        }

        public void SortAtoZ()
        {
            var dropdown = Find(SortDropdown);
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByValue("az");
        }

        public List<string> GetItemNamesList()
        {
            return FindAll(InventoryItemNames)
                .Select(e => e.Text)
                .ToList();
        }

        public void OpenRandomAvailableInventoryItem()
        {
            var items = FindAll(AvailableInventoryItems);
            if (items.Count == 0)
            {
                throw new InvalidOperationException("No Available Inventory Items found.");
            }
            var randomItem = items[_random.Next(items.Count)];
            var itemName = randomItem.FindElement(By.CssSelector("[data-test='inventory-item-name']"));
            itemName.Click();
        }
    }
}
