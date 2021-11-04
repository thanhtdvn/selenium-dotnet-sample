using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects.DemoQA;
using TShapedFoundation.PageObjects.DemoQA.Models;

namespace TShapedFoundation.TestCases.DemoQA
{
    [TestFixture]
    class SearchBookTests : WebDriverManagers
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
    }
}
