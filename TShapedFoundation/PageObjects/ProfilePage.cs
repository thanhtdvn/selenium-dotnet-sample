using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    public class ProfilePage : BaseDemoQAPage
    {
        By searchBoxTextBox = By.Id("searchBox");
        By deleteAllBooksButton = By.XPath("//button[text()='Delete All Books']");
        By modalDialogOKButton = By.Id("closeSmallModal-ok");
        By bookTitleLinkByBookName(string strBookName) => By.Id($"see-book-{strBookName}");
        By deleteBookIconByBookName(string strBookName) => By.XPath($"//span[@id='see-book-{strBookName}']//ancestor::div[@role='row']//span[contains(@id, 'delete-record')]");

        public ProfilePage(IWebDriver driver) : base(driver)
        {
        }

        public void SearchBooks(string keyword)
        {
            this.SendKeyToElement(searchBoxTextBox, keyword);
        }

        public void DeleteAllBooks()
        {
            this.WaitForElementClickable(deleteAllBooksButton);
            this.ScrollToElementAndClick(deleteAllBooksButton);
            this.WaitForElementVisible(modalDialogOKButton);
            this.ClickToElement(modalDialogOKButton);
            this.WaitForAlertIsDisplay();
            this.DismissAlert();
        }

        public void DeleteBookByName(string strBookName, out string strResultMessage)
        {
            By deleteBookIcon = deleteBookIconByBookName(strBookName);
            this.WaitForElementClickable(deleteBookIcon);
            this.ScrollToElementAndClick(deleteBookIcon);
            this.WaitForElementVisible(modalDialogOKButton);
            this.ClickToElement(modalDialogOKButton);
            this.WaitForAlertIsDisplay();
            strResultMessage = this.GetAlertText();
            this.AcceptAlert();
        }

        public bool CheckBookIsShownOnPage(string strBookName)
        {
            try
            {
                this.WaitForElementVisible(bookTitleLinkByBookName(strBookName));
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
