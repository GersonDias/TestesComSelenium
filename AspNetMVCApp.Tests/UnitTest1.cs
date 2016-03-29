using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebApplication2.Tests
{
    [TestClass]
    public class TestesInterface : SeleniumTest
    {
        public TestesInterface() : base("") { }

        [TestInitialize]
        public void GoToUrl()
        {
            ChromeDriver.Navigate().GoToUrl(this.GetAbsoluteUrl("/cliente/create"));
        }

        [TestMethod]
        public void DeveExibirErroSeUsuarioForMenorDeIdade()
        {
            ChromeDriver.FindElement(By.Name("FirstName")).SendKeys("Gerson");
            ChromeDriver.FindElement(By.Name("LastName")).SendKeys("Dias");
            var idade = ChromeDriver.FindElement(By.Name("Idade"));
            idade.SendKeys("11");
            idade.SendKeys(Keys.Tab);
            
            var error = ChromeDriver.FindElement(By.XPath("//span[@for='Idade']"));

            Assert.IsTrue(error.Displayed);
        }

        [TestMethod]
        public void DeveExibirErroSeOFirstNameForVazio()
        {
            ChromeDriver.FindElement(By.Name("LastName")).SendKeys("Dias");

            ChromeDriver.FindElement(By.Name("Idade")).SendKeys("18");

            ChromeDriver.FindElement(By.CssSelector("input[type=submit]")).Click();
            ChromeDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
            var error = ChromeDriver.FindElement(By.XPath("//span[@for='FirstName']"));

            Assert.IsTrue(error.Displayed);
        }
    }
}
