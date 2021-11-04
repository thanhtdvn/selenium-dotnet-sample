using NUnit.Framework;
using OpenQA.Selenium;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects.DemoQA;

namespace TShapedFoundation.TestCases.DemoQA
{
    [TestFixture]
    class LoginTests : WebDriverManagers
    {
        IWebDriver driver;
        PageManager pageManager;
        BookStorePage bookStorePage;
        LoginPage loginPage;


        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver(Browser.Chrome);
            pageManager = new PageManager(driver);
            pageManager.NavigateToBookStorePage();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void LoginWithValidUser()
        {
            string validUsername = pageManager.pageSettings.Username;
            string validPassword = pageManager.pageSettings.Password;

            bookStorePage = new BookStorePage(driver);
            bookStorePage.CloseAdsPopup();
            loginPage = bookStorePage.GoToLoginPage();
            bookStorePage = loginPage.LoginWithValidAccount(validUsername, validPassword);

            string headerText = bookStorePage.GetUsernameLabelValue();
            Assert.AreEqual(headerText, validUsername, "Username is not displayed as expected.");
        }

        [Test]
        public void LoginWithInvalidUser()
        {
            string invalidUsername = "abc";
            string invalidPassword = "123456";

            bookStorePage = new BookStorePage(driver);
            bookStorePage.CloseAdsPopup();
            loginPage = bookStorePage.GoToLoginPage();
            loginPage.LoginWithValidAccount(invalidUsername, invalidPassword);

            string invalidLoginMessage = loginPage.GetInvalidLoginMessage();
            Assert.AreEqual(invalidLoginMessage, "Invalid username or password!", "Warning message is not displayed as expected.");
        }


    }
}
