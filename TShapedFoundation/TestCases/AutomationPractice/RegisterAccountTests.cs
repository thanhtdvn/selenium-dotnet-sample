using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects.AutomationPractice;
using TShapedFoundation.PageObjects.AutomationPractice.Models;
using TShapedFoundation.Utilities;

namespace TShapedFoundation.TestCases.AutomationPractice
{
    [TestFixture]
    public class RegisterAccountTests : WebDriverManagers
    {
        IWebDriver driver;
        PageManager pageManager;
        FileUtils fileUtils;

        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver(Browser.Chrome);

            var firingDriver = new EventFiringWebDriver(driver);
            firingDriver.ExceptionThrown += FiringDriver_ExceptionThrown;

            pageManager = new PageManager(driver);
            fileUtils = new FileUtils();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("DataObjects\\AutomationPractice\\valid-account-data.json")]
        public void CreateNewAccountSuccessfull(string account_json_path)
        {
            RegisterAccountInfo registerAccountInfo = fileUtils.GetData<RegisterAccountInfo>(account_json_path);
            // make sure account Email is unique
            registerAccountInfo.Email = $"{Guid.NewGuid()}@automail.com";

            AuthenticationPage authenticationPage = pageManager.NavigateToHomePage().ClickToSignInLink();
            MyAccountPage myAccountPage = authenticationPage.RegisterAccount(registerAccountInfo);
            string loggedInUserFullname = myAccountPage.GetUserFullName();
            Console.WriteLine("User's fullname: " + loggedInUserFullname);
            Assert.AreEqual(loggedInUserFullname, registerAccountInfo.FullName, $"Failed! Actual fullname {loggedInUserFullname} is not as expected {registerAccountInfo.FullName}" );
        }

        private static void FiringDriver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            Console.WriteLine(e.ThrownException.StackTrace);
        }
    }
}
