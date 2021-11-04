using OpenQA.Selenium;

namespace TShapedFoundation.PageObjects.DemoQA
{
    public class LoginPage : BaseDemoQAPage
    {

        private By usernameTextBox = By.Id("userName");
        private By passwordTextBox = By.Id("password");
        private By loginButton = By.Id("login");
        private By invalidLoginMessage = By.Id("name");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public BookStorePage LoginWithValidAccount(string username, string password)
        {
            SendKeyToElement(usernameTextBox, username);
            SendKeyToElement(passwordTextBox, password);
            ClickToElement(loginButton);
            return new BookStorePage(driver);
        }

        public void LoginWithInvalidAccount(string username, string password)
        {
            SendKeyToElement(usernameTextBox, username);
            SendKeyToElement(passwordTextBox, password);
            ClickToElement(loginButton);
        }
        public string GetInvalidLoginMessage()
        {
            WaitForElementVisible(invalidLoginMessage);
            return GetElementText(invalidLoginMessage);
        }
    }
}
