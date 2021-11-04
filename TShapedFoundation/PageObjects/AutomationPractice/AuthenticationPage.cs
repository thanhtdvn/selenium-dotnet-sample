using OpenQA.Selenium;
using TShapedFoundation.PageObjects.AutomationPractice.Models;

namespace TShapedFoundation.PageObjects.AutomationPractice
{
    public class AuthenticationPage : BaseAutomationPracticePage
    {
        By byEmailAddressInput = By.Id("email_create");
        By byCreateAnAccountButton = By.Id("SubmitCreate");
        By byGenderOption = By.CssSelector("input[name='id_gender']");
        By byFirstNameInput = By.Id("customer_firstname");
        By byLastNameInput = By.Id("customer_lastname");
        By byEmailInput = By.Id("email");
        By byPasswordInput = By.Id("passwd");
        By byDaysSelect = By.Id("days");
        By byMonthsSelect = By.Id("months");
        By byYearsSelect = By.Id("years");
        By byAddressFirstNameInput = By.Id("firstname");
        By byAddressLastNameInput = By.Id("lastname");
        By byAddressInput = By.Id("address1");
        By byCityInput = By.Id("city");
        By byStateSelect = By.Id("id_state");
        By byPostCodeInput = By.Id("postcode");
        By byCountrySelect = By.Id("id_country");
        By byMobileNumberInput = By.Id("phone_mobile");
        By byAddressAliasInput = By.Id("alias");
        By byRegisterButton = By.Id("submitAccount");

        public AuthenticationPage(IWebDriver driver) : base(driver)
        {
        }

        public MyAccountPage RegisterAccount(RegisterAccountInfo accountInfo)
        {
            WaitForElementVisible(byEmailAddressInput);
            SendKeyToElement(byEmailAddressInput, accountInfo.Email);
            ClickToElement(byCreateAnAccountButton);

            WaitForElementVisible(byGenderOption);
            ClickToElement(byGenderOption);
            SendKeyToElement(byFirstNameInput, accountInfo.FirstName);
            SendKeyToElement(byLastNameInput, accountInfo.LastName);
            SendKeyToElement(byEmailInput, accountInfo.Email);
            SendKeyToElement(byPasswordInput, accountInfo.Password);
            SelectByValue(byDaysSelect, accountInfo.BirthdayDay);
            SelectByValue(byMonthsSelect, accountInfo.BirthdayMonth);
            SelectByValue(byYearsSelect, accountInfo.BirthdayYear);
            SendKeyToElement(byAddressFirstNameInput, accountInfo.FirstName);
            SendKeyToElement(byAddressLastNameInput, accountInfo.LastName);
            SendKeyToElement(byAddressInput, accountInfo.Address);
            SendKeyToElement(byCityInput, accountInfo.City);
            SelectByText(byStateSelect, accountInfo.State);
            SendKeyToElement(byPostCodeInput, accountInfo.PostCode);
            SelectByText(byCountrySelect, accountInfo.Country);
            SendKeyToElement(byMobileNumberInput, accountInfo.Mobile);
            SendKeyToElement(byAddressAliasInput, accountInfo.AddressAlias);
            ClickToElement(byRegisterButton);

            return new MyAccountPage(driver);
        }
    }
}
