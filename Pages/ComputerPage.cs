using Confused.Helpers;
using Confused.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confused.Pages
{
    public class ComputerPage : BasePage
    {
        public ComputerPage(IWebDriver driver) : base(driver) { }

        private static readonly By computerNameField = By.Id("name");
        private static readonly By introducedDateField = By.Id("introduced");
        private static readonly By discontinuedDateField = By.Id("discontinued");
        private static readonly By companyDropdown = By.Id("company");
        private static readonly By createComputerButton = By.CssSelector("[type='submit']");
        private static readonly By deleteThisComputerButton = By.XPath("//*[contains(@value, 'Delete')]");

        public (HomePage, Computer) CreateNewComputer()
        {
            // Introduced date will be current date
            var currentDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            // Create new Computer object
            var computer = new Computer
            {
                Name = DataGenerator.GenerateRandomString(7),
                IntroducedDate = currentDate,
                DiscontinuedDate = currentDate.AddDays(double.Parse(new Random().Next(1, 30).ToString())),
                Company = UA.SelectByRandom(companyDropdown, true)
            };

            // Input the data in the fields
            UA.SendKeys(computer.Name, computerNameField);
            UA.SendKeys(computer.IntroducedDate.ToString("yyyy-MM-dd"), introducedDateField);
            UA.SendKeys(computer.DiscontinuedDate.ToString("yyyy-MM-dd"), discontinuedDateField);

            // Click create new computer button
            UA.Click(createComputerButton);

            return (new HomePage(Driver), computer);
        }

        public HomePage ClickDeleteButton()
        {
            UA.Click(deleteThisComputerButton);
            return new HomePage(Driver);
        }
    }
}
