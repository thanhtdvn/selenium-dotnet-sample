using OpenQA.Selenium;
using TShapedFoundation.PageObjects.DemoQA.Models;

namespace TShapedFoundation.PageObjects.DemoQA
{
    public class BookDetailPage : BaseDemoQAPage
    {
        By addToYourCollectionButton = By.XPath("//button[@id='addNewRecordButton'][text()='Add To Your Collection']");
        By bookInfoISBNLabel = By.XPath("//label[@id='ISBN-label']/..//following-sibling::div/label");
        By bookInfoTitleLabel = By.XPath("//label[@id='title-label']/..//following-sibling::div/label");

        public BookDetailPage(IWebDriver driver) : base(driver)
        {
        }

        public void AddBookToCollection()
        {
            WaitForElementClickable(addToYourCollectionButton);
            ScrollToElementAndClick(addToYourCollectionButton);
        }

        public Book GetBookInfo()
        {
            WaitForElementVisible(bookInfoISBNLabel);
            Book book = new Book();
            book.Title = FindElement(bookInfoTitleLabel).Text;
            book.ISBN = FindElement(bookInfoISBNLabel).Text;

            return book;
        }
    }
}
