using OpenQA.Selenium;

namespace Pages
{
    public class InventoryItemPage : BasePage
    {
        public InventoryItemPage() : base() { }
        public override string PageUrl => "https://www.saucedemo.com/inventory-item.html";
        public static By AddToCartButton => By.Id("add-to-cart");
        public static By RemoveButton => By.Id("remove");
        public static By ShoppingCartIcon => By.CssSelector("[data-test='shopping-cart-link']");
        public static By ShoppingCartBadge => By.CssSelector("[data-test='shopping-cart-badge']");


        public override bool IsOpened()
        {
            return IsDisplayed(AddToCartButton);
        }

        public void AddInventoryToCart()
        {
            Click(AddToCartButton);
        }

        public int CountItemsInCart()
        {
            int itemsInCart;
            var cartIcon = Find(ShoppingCartIcon);

            if (cartIcon.FindElements(By.XPath(".//*")).Count > 0)
            {
                itemsInCart = int.Parse(GetText(ShoppingCartBadge));
                return itemsInCart;
            }
            else
            {
                Click(AddToCartButton);
                itemsInCart = int.Parse(GetText(ShoppingCartBadge));
                Click(RemoveButton);
                return itemsInCart - 1;          
            }                    
        }
    }
}
