using OpenQA.Selenium;
using TShapedFoundation.Models;

namespace TShapedFoundation.PageObjects
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
            this.WaitForElementClickable(addToYourCollectionButton);
            this.ScrollToElementAndClick(addToYourCollectionButton);
        }

        public Book GetBookInfo()
        {
            this.WaitForElementVisible(bookInfoISBNLabel);
            Book book = new Book();
            book.Title = this.FindElement(bookInfoTitleLabel).Text;
            book.ISBN = this.FindElement(bookInfoISBNLabel).Text;

            return book;
        }
    }
}
