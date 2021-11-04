using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects.AutomationPractice;
using TShapedFoundation.PageObjects.AutomationPractice.Models;

namespace TShapedFoundation.TestCases.AutomationPractice
{
    [TestFixture]
    public class SearchProductTests : WebDriverManagers
    {
        IWebDriver driver;
        PageManager pageManager;

        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver(Browser.Chrome);
            pageManager = new PageManager(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase]
        public void SearchProductsSuccessfull()
        {
            HomePage homePage = pageManager.NavigateToHomePage();
            CategoryPage categoryPage = homePage.ClickOnWomenTShirtMenuItem();
            ProductInfo firstProductOnTShirtPage = categoryPage.GetFirstProductOnPage();
            SearchPage searchPage = categoryPage.SearchProducts(firstProductOnTShirtPage.Name);

            ProductInfo[] allProductsOnSearchPage = searchPage.GetAllProductsOnPage();
            Console.WriteLine($"Total products found on page: {allProductsOnSearchPage.Count()}");
            Assert.IsFalse(allProductsOnSearchPage.Length == 0, "Failed! Can not found any products!");

            var productsMatchSearchedProductName = allProductsOnSearchPage.Where(x => x.Name.Equals(firstProductOnTShirtPage.Name)).ToArray();
            Console.WriteLine($"Total products match searched product name: {productsMatchSearchedProductName.Count()}");
            Assert.IsFalse(productsMatchSearchedProductName.Length == 0, $"Failed! Have no products match exactly searched product name {firstProductOnTShirtPage.Name} ");
            
            var productsMatchSearchedProductInfo = allProductsOnSearchPage.Where(x => x.Equals(firstProductOnTShirtPage)).ToArray();
            Console.WriteLine($"Total products match searched product details: {productsMatchSearchedProductInfo.Count()}");
            Assert.IsTrue(productsMatchSearchedProductInfo.Any(), "Failed! Can not found any products with same details of searched product");
        }
    }
}
