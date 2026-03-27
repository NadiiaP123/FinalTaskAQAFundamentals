using OpenQA.Selenium;

namespace Pages
{
    public class CartPage : BasePage
    {
        public CartPage() : base() { }
        public override string PageUrl => "https://www.saucedemo.com/cart.html";

        public static By CheckoutButton => By.Id("checkout");

        public override bool IsOpened()
        {
            return IsDisplayed(CheckoutButton);
        }
    }
}
