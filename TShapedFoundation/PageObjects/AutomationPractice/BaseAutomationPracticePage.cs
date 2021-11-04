using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects.AutomationPractice
{
    public abstract class BaseAutomationPracticePage : BasePage
    {
        By byUserFullNameText = By.CssSelector(".account span");
        By byMenuItemLink(string mnuItemText) => By.XPath($"//ul[contains(@class,'sf-menu')]//a[text()='{mnuItemText}']");
        By byMenuSubItemLink(string mnuItemText, string mnuSubItemText)
            => By.XPath($"//ul[contains(@class,'sf-menu')]//a[text()='{mnuItemText}']/..//a[text()='{mnuSubItemText}']");

        //By byWomenMenuItemLink = By.XPath("//ul[contains(@class,'sf-menu')]//a[text()='']");
        //By byWomenTShirtMenuItemLink = By.XPath("//ul[contains(@class,'sf-menu')]//a[text()='Women']/..//a[text()='T-shirts']");
        By bySearchInput = By.Id("search_query_top");
        By bySearchButton = By.CssSelector(".button-search");

        public BaseAutomationPracticePage(IWebDriver driver) : base(driver)
        {
        }

        public SearchPage SearchProducts(string keyword)
        {
            SendKeyToElement(bySearchInput, keyword);
            ClickToElement(bySearchButton);
            return new SearchPage(driver);
        }

        public string GetUserFullName()
        {
            return GetElementText(byUserFullNameText);
        }

        public CategoryPage ClickOnWomenTShirtMenuItem()
        {
            string mnuItemText = "Women";
            string mnuSubItemText = "T-shirts";
            // string mnuSubItemText = "Summer Dresses";

            MoveToElement(byMenuItemLink(mnuItemText));
            By byWomenTShirtMenuItemLink = byMenuSubItemLink(mnuItemText, mnuSubItemText);
            WaitForElementVisible(byWomenTShirtMenuItemLink);
            MoveToElementAndClick(byWomenTShirtMenuItemLink);
            return new CategoryPage(driver);
        }
    }
}
