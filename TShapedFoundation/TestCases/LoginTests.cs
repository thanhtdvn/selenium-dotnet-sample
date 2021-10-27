using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects;

namespace TShapedFoundation.TestCases
{
    [TestFixture]
    class LoginTests : WebDriverManagers
    {
        IWebDriver driver;
        BookStorePage bookStorePage;
        LoginPage loginPage;
        

        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver("chrome");
            driver.Navigate().GoToUrl(Common.Constant.BOOK_STORE_PAGE_URL);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void LoginWithValidUser()
        {
            String validUsername = Common.Constant.USERNAME;
            String validPassword = Common.Constant.PASSWORD;

            bookStorePage = new BookStorePage(driver);
            bookStorePage.CloseAdsPopup();
            loginPage = bookStorePage.GoToLoginPage();
            bookStorePage = loginPage.LoginWithValidAccount(validUsername, validPassword);

            String headerText = bookStorePage.GetUsernameLabelValue();
            Assert.AreEqual(headerText, validUsername, "Username is not displayed as expected.");
        }

        [Test]
        public void LoginWithInvalidUser()
        {
            String invalidUsername = "abc";
            String invalidPassword = "123456";

            bookStorePage = new BookStorePage(driver);
            bookStorePage.CloseAdsPopup();
            loginPage = bookStorePage.GoToLoginPage();
            loginPage.LoginWithValidAccount(invalidUsername, invalidPassword);

            String invalidLoginMessage = loginPage.GetInvalidLoginMessage(); 
            Assert.AreEqual(invalidLoginMessage, "Invalid username or password!", "Warning message is not displayed as expected.");
        }

        
    }
}
