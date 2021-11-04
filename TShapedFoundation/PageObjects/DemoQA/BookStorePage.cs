using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using TShapedFoundation.PageObjects.DemoQA.Models;

namespace TShapedFoundation.PageObjects.DemoQA
{
    public class BookStorePage : BaseDemoQAPage
    {
        private By loginButton = By.Id("login");
        private By usernameLabel = By.Id("userName-value");
        private By searchBoxTextBox = By.Id("searchBox");
        private By bookDetailLink = By.XPath("//span[contains(@id, 'see-book-')]/a");
        private By bookTitleLinkByBookName(string bookName) => By.Id($"see-book-{bookName}");

        public BookStorePage(IWebDriver driver) : base(driver)
        {
        }

        public LoginPage GoToLoginPage()
        {
            ClickToElement(loginButton);
            return new LoginPage(driver);
        }

        public void SearchBooks(string keyword)
        {
            SendKeyToElement(searchBoxTextBox, keyword);
        }

        public List<Book> GetBooksOnPage()
        {
            SleepInSeconds(1);
            return FindElements(bookDetailLink).Select(x => new Book
            {
                Title = x.Text
            }).ToList();
        }

        public BookDetailPage GoToBookDetailPage(string bookName)
        {
            var bookLink = bookTitleLinkByBookName(bookName);

            WaitForElementClickable(bookLink);
            ClickToElement(bookLink);
            return new BookDetailPage(driver);
        }

        public string GetUsernameLabelValue()
        {
            WaitForElementVisible(usernameLabel);
            return GetElementText(usernameLabel);
        }
    }
}
