using OpenQA.Selenium;
using System;

namespace TShapedFoundation.PageObjects.DemoQA
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
            SendKeyToElement(searchBoxTextBox, keyword);
        }

        public void DeleteAllBooks()
        {
            WaitForElementClickable(deleteAllBooksButton);
            ScrollToElementAndClick(deleteAllBooksButton);
            WaitForElementVisible(modalDialogOKButton);
            ClickToElement(modalDialogOKButton);
            WaitForAlertIsDisplay();
            DismissAlert();
        }

        public void DeleteBookByName(string strBookName, out string strResultMessage)
        {
            By deleteBookIcon = deleteBookIconByBookName(strBookName);
            WaitForElementClickable(deleteBookIcon);
            ScrollToElementAndClick(deleteBookIcon);
            WaitForElementVisible(modalDialogOKButton);
            ClickToElement(modalDialogOKButton);
            WaitForAlertIsDisplay();
            strResultMessage = GetAlertText();
            AcceptAlert();
        }

        public bool CheckBookIsShownOnPage(string strBookName)
        {
            try
            {
                WaitForElementVisible(bookTitleLinkByBookName(strBookName));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
