using Confused.Base;
using Confused.Models;
using Confused.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Confused
{
    [TestClass]
    public class UnitTest1
    {
        HomePage homePage;
        IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            // Create driver object
            driver = new DriverInit().GetChromeDriver();

            // Create instance of homepage
            homePage = new HomePage(driver);

            // Naavigate to the homepage
            homePage.NavigateToHomePage();

            //Maximize the window
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        [Description("Verifies a user can add a new computer and check computer is added to the list.")]
        public void Verify_User_Can_Add_A_Computer()
        {
            ComputerPage addComputerPage = homePage.ClickAddNewComputerBtn();
            var (homePage2, computer) = addComputerPage.CreateNewComputer();
            Assert.IsTrue(homePage2.IsAlertDisplayed(), "Alert message was not displayed after creating a new computer object.");
            Assert.IsTrue(homePage2.AlertContainsText($"{computer.Name} has been created"), "A new computer object was not created successfully.");
            homePage2.EnterComputerName(computer.Name);
            homePage2.ClickFilterByNameButton();
            Assert.IsTrue(homePage2.ComputerIsDisplayedInTable(computer), $"{computer.Name} was not displayed in the search results.");
        }

        [TestMethod]
        [Description("Select a computer and delete it from the list and verify the computer has been deleted from the list.")]
        public void Verify_User_Can_Delete_Computer_From_List()
        {
            var (computer, pageIndex, rowIndex) = homePage.GetRandomComputer();
            ComputerPage computerPage = homePage.ClickComputer(rowIndex);
            HomePage homePage2 = computerPage.ClickDeleteButton();
            Assert.IsTrue(homePage2.AlertContainsText("been deleted"), "Alert message does not contain error message.");
            homePage2.NavigateToPageIndex(pageIndex);
            Assert.IsFalse(homePage2.ComputerIsDisplayedInTable(computer), $"{computer.Name} was displayed in the table after being deleted.");
        }

        [TestMethod]
        [Description("Verifies a user can filter for a computer by name.")]
        public void Verify_A_User_Can_Filter_By_Name()
        {
            var computerName = homePage.GetRandomComputerName();
            homePage.EnterComputerName(computerName);
            homePage.ClickFilterByNameButton();
            homePage.VerifySearchResultsContainName(computerName);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
