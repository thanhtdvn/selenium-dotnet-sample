using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using WebDriverManager.DriverConfigs.Impl;

namespace TShapedFoundation.Common
{
    public class WebDriverManagers
    {
        private static IWebDriver _driver;

        public static IWebDriver CreateBrowserDriver(Browser browser)
        {
            switch (browser)
            {
                case Browser.Firefox:
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    _driver = new FirefoxDriver();
                    break;
                case Browser.Chrome:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new ChromeDriver();
                    break;
                case Browser.Edge:
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    _driver = new EdgeDriver();
                    break;
                default:
                    throw new Exception($"Browser {browser.ToString()} doesn't support yet!");
                    break;
            }
            _driver.Manage().Window.Maximize();
            return _driver;
        }
    }
}
