using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    public abstract class BaseDemoQAPage : BasePage
    {
        private By closeAdsArrowButton = By.Id("close-fixedban");

        public BaseDemoQAPage(IWebDriver driver) : base(driver)
        {
        }

        public void CloseAdsPopup()
        {
            try
            {
                ClickToElement(closeAdsArrowButton);
            }catch(Exception ex)
            {
                Console.WriteLine("CloseAdsPopup Error!", ex);
            }
        }
    }
}
