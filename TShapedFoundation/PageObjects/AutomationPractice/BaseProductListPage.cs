using OpenQA.Selenium;
using System.Linq;
using TShapedFoundation.PageObjects.AutomationPractice.Models;

namespace TShapedFoundation.PageObjects.AutomationPractice
{
    public class BaseProductListPage : BaseAutomationPracticePage
    {
        By byProductContainers = By.CssSelector(".product-container");
        By byProductName = By.CssSelector(".product-name");
        By byProductDesc = By.CssSelector(".product-desc");
        By byProductPrice = By.CssSelector(".product-price");
        By byColorPickLinks = By.CssSelector(".color_pick");
        By byAvailabilityBox = By.CssSelector(".availability");
        

        public BaseProductListPage(IWebDriver driver) : base(driver)
        {
        }

        public ProductInfo GetFirstProductOnPage()
        {
            this.WaitForElementVisible(byProductContainers);
            var firstProductContainer = driver.FindElement(byProductContainers);
            ProductInfo productInfo = ParseProductInfo(firstProductContainer);

            return productInfo;
        }

        public ProductInfo[] GetAllProductsOnPage()
        {
            this.WaitForElementVisible(byProductContainers);
            var productContainers = driver.FindElements(byProductContainers);
            return productContainers.Select(x => ParseProductInfo(x)).ToArray();
        }

        private ProductInfo ParseProductInfo(IWebElement productContainer)
        {
            return new ProductInfo()
            {
                Name = productContainer.FindElement(byProductName).Text,
                Desc = productContainer.FindElement(byProductDesc).Text,
                Price = productContainer.FindElement(byProductPrice).Text,
                Availability = productContainer.FindElement(byAvailabilityBox).Text,
                Colors = productContainer.FindElements(byColorPickLinks)
                                .Select(x => x.GetAttribute("href")).ToArray()
            };
        }
    }
}
