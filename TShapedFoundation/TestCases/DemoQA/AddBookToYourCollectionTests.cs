using NUnit.Framework;
using OpenQA.Selenium;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects.DemoQA;

namespace TShapedFoundation.TestCases.DemoQA
{
    [TestFixture]
    class AddBookToYourCollectionTests : WebDriverManagers
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

        [TestCase("Git Pocket Guide")]
        public void AddBookToYourCollectionSuccessfull(string strBookName)
        {
            BookStorePage bookStorePage = pageManager.LetUserLogsIntoApplication();

            // delete existing book in profile page before run test.
            var profilePage = pageManager.NavigateToProfilePage();
            if (profilePage.CheckBookIsShownOnPage(strBookName))
            {
                profilePage.DeleteBookByName(strBookName, out string _);
            }

            // navigate to bookStore page again.
            bookStorePage = pageManager.NavigateToBookStorePage();

            // go to book detail page of this book.
            var bookDetailPage = bookStorePage.GoToBookDetailPage(strBookName);
            bookDetailPage.CloseAdsPopup();
            bookDetailPage.AddBookToCollection();
            bookDetailPage.WaitForAlertIsDisplay();

            // Verify alert “Book added to your collection.” is shown
            var message = bookDetailPage.GetAlertText();
            bookDetailPage.AcceptAlert();
            var expectedMessage = "Book added to your collection.";
            Assert.AreEqual(message, expectedMessage, $"Alert message '{message}' is not as expected '{expectedMessage}'.");

            // verify book is shown in your profile
            profilePage = pageManager.NavigateToProfilePage();
            bool bookIsExistOnProfilePage = profilePage.CheckBookIsShownOnPage(strBookName);
            Assert.IsTrue(bookIsExistOnProfilePage, $"Failed! Book “{strBookName}” didn't show on your profile page.");
        }
    }
}
