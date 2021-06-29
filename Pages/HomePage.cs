using Confused.Helpers;
using Confused.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confused.Pages
{
    public class HomePage : BasePage
    {
        private static readonly By addNewComputerBtn = By.Id("add");
        private static readonly By alertMessage = By.ClassName("alert-message");
        private static readonly By searchBox = By.Id("searchbox");
        private static readonly By filterByNameButton = By.Id("searchsubmit");
        private static readonly By tableHeaders = By.CssSelector(".computers thead tr th");
        private static readonly By rows = By.CssSelector(".computers tbody tr");
        private static readonly By computersLinks = By.CssSelector(".computers tbody tr td a");
        private static readonly By cell = By.TagName("td");
        private static readonly By nextButton = By.PartialLinkText("Next");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void NavigateToHomePage()
        {
            Driver.Navigate().GoToUrl("https://computer-database.herokuapp.com/");
        }

        public ComputerPage ClickAddNewComputerBtn()
        {
            UA.Click(addNewComputerBtn);
            return new ComputerPage(Driver);
        }

        public bool IsAlertDisplayed()
        {
            return UA.Get(alertMessage).Displayed;
        }

        public bool AlertContainsText(string text)
        {
            return UA.Get(alertMessage).Text.Contains(text);
        }

        public bool ComputerIsDisplayedInTable(Computer computer)
        {
            var hds = GetTableHeaders();
            var compRows = UA.GetAll(rows);
            
            foreach (var row in compRows)
            {
                var rowCells = row.FindElements(cell);

                try
                {
                    // Check each row details match the computer details

                    if (rowCells[hds.IndexOf("Computer name")].Text.Contains(computer.Name)
                    && DateTime.Parse(rowCells[hds.IndexOf("Introduced")].Text) == computer.IntroducedDate
                    && DateTime.Parse(rowCells[hds.IndexOf("Discontinued")].Text) == computer.DiscontinuedDate
                    && rowCells[hds.IndexOf("Company")].Text.Contains(computer.Company))
                    {
                        return true;
                    }
                }
                catch (FormatException) { }
            }

            return false;
        }

        public void NavigateToPageIndex(int pageIndex)
        {
            for (int i = 0; i < pageIndex; i++)
            {
                UA.Click(nextButton);
            }
        }

        public void VerifySearchResultsContainName(string computerName)
        {
            var hds = GetTableHeaders();

            UA.GetAll(rows).Select(row =>
            {
                // Extract only the names
                return row.FindElements(cell)[hds.IndexOf("Computer name")].Text;

            }).ToList().ForEach(name =>
            {
                Assert.IsTrue(name.Contains(computerName), $"Name => '{name}' found in filter results that does not contain '{computerName}'.");
            });
        }

        public string GetRandomComputerName()
        {
            var hds = GetTableHeaders();
            var compRows = UA.GetAll(rows);

            // Random computer row
            var row = compRows[new Random().Next(0, compRows.Count)];

            return row.FindElements(cell)[hds.IndexOf("Computer name")].Text;
        }

        public (Computer, int pageIndex, int rowIndex) GetRandomComputer()
        {
            var hds = GetTableHeaders();
            var compRows = UA.GetAll(rows);
            var count = compRows.Count;
            var pageIndex = 0;
            var rowIndex = 0;
            var computer = new Computer();

            // Get random computer from list
            for (int i = 0; i < count; i++)
            {
                // Use a random number instead
                var index = new Random().Next(0, count);
                rowIndex = index;
                var rowCells = compRows[index].FindElements(cell);

                // Use a computer with an introduced, discontinued date and a company
                var introduced = rowCells[hds.IndexOf("Introduced")].Text;
                var discontinued = rowCells[hds.IndexOf("Discontinued")].Text;
                var company = rowCells[hds.IndexOf("Company")].Text;

                if (introduced != "-" && discontinued != "-" && company != "-")
                {
                    computer.Name = rowCells[hds.IndexOf("Computer name")].Text;
                    computer.IntroducedDate = DateTime.Parse(introduced);
                    computer.DiscontinuedDate = DateTime.Parse(discontinued);
                    computer.Company = company;

                    break;
                }

                // Go to the next page if no computer was found with valid details
                if (i == count - 1 && pageIndex < 10)
                {
                    UA.Click(nextButton);

                    // Update the rows
                    compRows = UA.GetAll(rows);

                    //Update the count
                    count = compRows.Count;

                    i = 0;

                    pageIndex++;
                }

                // If on the 10th page, fail test
                if (pageIndex == 10)
                {
                    Assert.Fail($"Random computer with valid introduced date, discontinued " +
                        $"date and company not found after checking {pageIndex} pages.");
                }
            }

            return (computer, pageIndex, rowIndex);
        }

        public ComputerPage ClickComputer(int index)
        {
            UA.Click(UA.GetAll(computersLinks)[index]);
            return new ComputerPage(Driver);
        }

        public void EnterComputerName(string computerName)
        {
            UA.SendKeys(computerName, searchBox);
        }

        public void ClickFilterByNameButton()
        {
            UA.Click(filterByNameButton);
        }

        private List<string> GetTableHeaders()
        {
            return UA.GetAll(tableHeaders).Select(x => x.Text).ToList();
        }
    }
}
