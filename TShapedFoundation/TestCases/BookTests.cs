using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TShapedFoundation.Common;
using TShapedFoundation.Models;
using TShapedFoundation.PageObjects;
using TShapedFoundation.Utilities;

namespace TShapedFoundation.TestCases
{
    [TestFixture]
    class BookTests : WebDriverManagers
    {
        IWebDriver driver;
        PageManager pageManager;
        ApiUtils apiUtils;

        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver("chrome");
            pageManager = new PageManager(driver);
            apiUtils = new ApiUtils();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("Git Pocket Guide")]
        public void AddBookToYourCollection(string strBookName)
        {
            BookStorePage bookStorePage = LetUserLogsIntoApplication();

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

        [TestCase("design", "Learning JavaScript Design Patterns", "Designing Evolvable Web APIs with ASP.NET")]
        [TestCase("Design", "Learning JavaScript Design Patterns", "Designing Evolvable Web APIs with ASP.NET")]
        public void SearchBooksWithMultipleResults(string strKeyword, params string[] expectedBookTitles)
        {
            BookStorePage bookStorePage = pageManager.NavigateToBookStorePage();
            bookStorePage.SearchBooks(strKeyword);
            List<Book> books = bookStorePage.GetBooksOnPage();
            Assert.AreEqual(expectedBookTitles.Length, books.Count, "Failed! Number of books are difference.");
            var actualBookTitles = books.Select(x => x.Title).OrderBy(t => t).ToArray();
            expectedBookTitles = expectedBookTitles.OrderBy(x => x).ToArray();
            Assert.IsTrue(actualBookTitles.SequenceEqual(expectedBookTitles), "Actual searching books are not the same with expected result.");
        }

        [TestCase("Speaking JavaScript")]
        public void DeleteBookSuccessfull(string strBookName)
        {
            LetUserLogsIntoApplication();

            BookStorePage bookStorePage = pageManager.NavigateToBookStorePage();
            bookStorePage.CloseAdsPopup();
            BookDetailPage bookDetailPage = bookStorePage.GoToBookDetailPage(strBookName);
            Book bookInfo = bookDetailPage.GetBookInfo();

            apiUtils.AddBookToUserCollection(Constant.USERID, Constant.USERNAME, Constant.PASSWORD, bookInfo.ISBN);

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

        private BookStorePage LetUserLogsIntoApplication()
        {
            var bookStorePage = pageManager.NavigateToBookStorePage();
            bookStorePage.CloseAdsPopup();
            var loginPage = bookStorePage.GoToLoginPage();
            bookStorePage = loginPage.LoginWithValidAccount(Constant.USERNAME, Constant.PASSWORD);
            var userName = bookStorePage.GetUsernameLabelValue();
            Console.WriteLine($"User logged in: {userName}");
            return bookStorePage;
        }
    }
}
