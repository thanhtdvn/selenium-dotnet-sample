using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TShapedFoundation.Common
{
    public abstract class BasePage
    {
        public IWebDriver driver;
        private IWebElement element;
        IList<IWebElement> elements;
        private WebDriverWait explicitWait;
        private readonly long longtimeout = 20;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenUrl(String url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement FindElement(By byLocator)
        {
            return driver.FindElement(byLocator);
        }

        public IList<IWebElement> FindElements(By byLocator)
        {
            return driver.FindElements(byLocator);
        }

        public void ClickToElement(By byLocator)
        {
            this.FindElement(byLocator).Click();
        }

        public void MoveToElementAndClick(By byLocator)
        {
            var el = this.FindElement(byLocator);
            Actions action = new Actions(driver);
            action.MoveToElement(el).Click();
            action.Perform();
        }

        public void ScrollToElementAndClick(By byLocator)
        {
            var el = this.FindElement(byLocator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView()", el);
            el.Click();
        }

        public void SendKeyToElement(By byLocator, string value)
        {
            element = this.FindElement(byLocator);
            element.Clear();
            element.SendKeys(value);
        }

        public String GetElementText(By byLocator)
        {
            return this.FindElement(byLocator).Text;
        }

        public String GetElementText(IWebElement element)
        {
            return element.Text;
        }

        public void SleepInSeconds(int time)
        {
            try
            {
                Thread.Sleep(time);
            }
            catch (ThreadInterruptedException e)
            {
                e.StackTrace.ToString();

            }
        }


        public void WaitForElementVisible(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byLocator));

        }

        public void WaitForElementExists(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(byLocator));

        }

        public void WaitForElementInvisible(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(byLocator));
        }
        public void WaitForElementClickable(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(byLocator));

        }

        public void WaitForAlertIsDisplay()
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public void DismissAlert()
        {
            driver.SwitchTo().Alert().Dismiss();
        }

        public void AcceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }

        public string GetAlertText()
        {
            return driver.SwitchTo().Alert().Text;
        }
    }
        
}
