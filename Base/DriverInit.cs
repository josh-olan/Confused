using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confused.Base
{
    public class DriverInit
    {
        public IWebDriver GetChromeDriver()
        {
            var service = ChromeDriverService.CreateDefaultService();
            var options = new ChromeOptions();
            return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
        }
    }
}
