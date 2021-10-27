using OpenQA.Selenium;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    public class PageManager
    {
        private IWebDriver _driver;

        public PageManager(IWebDriver driver)
        {
            _driver = driver;
        }

        public BookStorePage NavigateToBookStorePage()
        {
            _driver.Navigate().GoToUrl(Constant.BOOK_STORE_PAGE_URL);
            return new BookStorePage(_driver);
        }

        public ProfilePage NavigateToProfilePage()
        {
            _driver.Navigate().GoToUrl(Constant.PROFILE_PAGE_URL);
            return new ProfilePage(_driver);
        }
    }
}
