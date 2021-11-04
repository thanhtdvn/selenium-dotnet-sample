using NUnit.Framework;
using OpenQA.Selenium;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects.DemoQA;
using TShapedFoundation.PageObjects.DemoQA.Models;
using TShapedFoundation.Utilities;

namespace TShapedFoundation.TestCases.DemoQA
{
    [TestFixture]
    class DeleteBookTests : WebDriverManagers
    {
        IWebDriver driver;
        PageManager pageManager;
        ApiUtils apiUtils;

        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver(Browser.Chrome);
            pageManager = new PageManager(driver);
            apiUtils = new ApiUtils();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("Speaking JavaScript")]
        public void DeleteBookSuccessfull(string strBookName)
        {
            BookStorePage bookStorePage = pageManager.LetUserLogsIntoApplication();
            bookStorePage.CloseAdsPopup();
            BookDetailPage bookDetailPage = bookStorePage.GoToBookDetailPage(strBookName);
            Book bookInfo = bookDetailPage.GetBookInfo();

            apiUtils.AddBookToUserCollection(
                pageManager.pageSettings.UserId,
                pageManager.pageSettings.Username,
                pageManager.pageSettings.Password,
                bookInfo.ISBN);

            ProfilePage profilePage = pageManager.NavigateToProfilePage();
            profilePage.SearchBooks(strBookName);
            bool bookIsExist = profilePage.CheckBookIsShownOnPage(strBookName);
            Assert.IsTrue(bookIsExist, "Failed! This book is not exist on profile page");

            profilePage.DeleteBookByName(strBookName, out string message);
            string expectedMessage = "Book deleted.";
            Assert.AreEqual(message, expectedMessage, $"Alert message '{message}' is not as expected '{expectedMessage}'.");

            bookIsExist = profilePage.CheckBookIsShownOnPage(strBookName);
            Assert.IsFalse(bookIsExist, "Failed! This book is still shown on profile page");
        }
    }
}
