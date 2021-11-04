using OpenQA.Selenium;
using System;
using TShapedFoundation.PageObjects.DemoQA.Models;
using TShapedFoundation.Utilities;

namespace TShapedFoundation.PageObjects.DemoQA
{
    public class PageManager
    {
        private IWebDriver _driver;
        public PageSettings pageSettings;

        public PageManager(IWebDriver driver)
        {
            _driver = driver;
            pageSettings = SettingsUtils.GetApplicationConfiguration<PageSettings>(PageSettings.SectionName);
        }

        public BookStorePage NavigateToBookStorePage()
        {
            _driver.Navigate().GoToUrl(pageSettings.BookStorePageUrl);
            return new BookStorePage(_driver);
        }

        public ProfilePage NavigateToProfilePage()
        {
            _driver.Navigate().GoToUrl(pageSettings.ProfilePageUrl);
            return new ProfilePage(_driver);
        }

        public BookStorePage LetUserLogsIntoApplication()
        {
            var bookStorePage = NavigateToBookStorePage();
            bookStorePage.CloseAdsPopup();
            var loginPage = bookStorePage.GoToLoginPage();
            bookStorePage = loginPage.LoginWithValidAccount(pageSettings.Username, pageSettings.Password);
            var userName = bookStorePage.GetUsernameLabelValue();
            Console.WriteLine($"User logged in: {userName}");
            return bookStorePage;
        }
    }
}
