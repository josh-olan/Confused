using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confused.Helpers
{
    public class UA
    {
        private IWebDriver driver;

        public UA(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public string SelectByRandom(By locator, bool excludeFirst = false)
        {
            var el = new SelectElement(Get(locator));
            el.SelectByIndex(new Random().Next(excludeFirst ? 1 : 0, el.Options.Count));
            return el.SelectedOption.Text;
        }

        public void Click(By locator)
        {
            Click(Get(locator));
        }

        public void Click(IWebElement el)
        {
            el.Click();
        }

        public IWebElement Get(By locator)
        {
            return driver.FindElement(locator);
        }

        public List<IWebElement> GetAll(By locator)
        {
            return driver.FindElements(locator).ToList();
        }

        public void SendKeys(string text, By locator)
        {
            Get(locator).SendKeys(text);
        }
    }
}
