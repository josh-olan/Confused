using Confused.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confused.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver { get; private set; }

        protected UA UA { get; private set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            UA = new UA(Driver);
        }
    }
}
