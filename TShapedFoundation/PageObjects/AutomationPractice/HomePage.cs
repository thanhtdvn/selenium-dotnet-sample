using OpenQA.Selenium;

namespace TShapedFoundation.PageObjects.AutomationPractice
{
    public class HomePage : BaseAutomationPracticePage
    {
        By bySignInLink = By.CssSelector(".login");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public AuthenticationPage ClickToSignInLink()
        {
            WaitForElementClickable(bySignInLink);
            ClickToElement(bySignInLink);
            return new AuthenticationPage(driver);
        }
    }
}
