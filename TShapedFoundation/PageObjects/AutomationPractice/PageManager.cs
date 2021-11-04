using OpenQA.Selenium;
using TShapedFoundation.PageObjects.AutomationPractice.Models;
using TShapedFoundation.Utilities;

namespace TShapedFoundation.PageObjects.AutomationPractice
{
    public class PageManager
    {
        private IWebDriver _driver;
        public PageSettings pageSettings;

        public PageManager(IWebDriver driver)
        {
            _driver = driver;
            pageSettings = SettingsUtils.GetApplicationConfiguration<PageSettings>(PageSettings.SectionName);
        }

        public HomePage NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl(pageSettings.HomePageUrl);
            return new HomePage(_driver);
        }
    }
}
