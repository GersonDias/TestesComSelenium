using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Tests.Pages
{
    public class ClientePage
    {
        private readonly IWebDriver driver;

        public ClientePage(IWebDriver browser, string url)
        {
            this.driver = browser;
            browser.Navigate().GoToUrl(url);

            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "FirstName")]
        public IWebElement FirstName { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@for='FirstName']")]
        public IWebElement FirstNameError { get; set; }

        [FindsBy(How = How.Name, Using = "LastName")]
        public IWebElement LastName { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@for='LastName']")]
        public IWebElement LastNameError { get; set; }

        [FindsBy(How = How.Name, Using = "Idade")]
        public IWebElement Idade { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@for='Idade']")]
        public IWebElement IdadeError { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[type=submit]")]
        public IWebElement Criar { get; set; }
        public IEnumerable<string> CriarCliente(string firstName, string lastName, int idade)
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
            Idade.SendKeys(idade.ToString());

            Criar.Click();

            var erros = new List<string>();

            var waiter = new WebDriverWait(this.driver, TimeSpan.FromSeconds(2));
            waiter.IgnoreExceptionTypes(typeof(NoSuchElementException));

            var attr = IdadeError.GetAttribute("FindsBy");

            if (driver.ElementIsPresent(By.XPath("//span[@for='FirstName']")))
                erros.Add(FirstNameError.Text);

            if (driver.ElementIsPresent(By.XPath("//span[@for='LastName']")))
                erros.Add(LastNameError.Text);

            if (driver.ElementIsPresent(By.XPath("//span[@for='Idade']")))
                erros.Add(IdadeError.Text);

            return erros;
        }

        internal bool Exists(By by)
        {
            return driver.ElementIsPresent(by);
        }
    }
}
