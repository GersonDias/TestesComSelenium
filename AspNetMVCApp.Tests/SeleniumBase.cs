using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Tests
{
    public abstract class SeleniumTest
    {
        const int iisPort = 2020;
        private string _applicationName;
        private Process _iisProcess;

        public SeleniumTest(string applicationName)
        {
            _applicationName = applicationName;
        }

        public FirefoxDriver FirefoxDriver { get; set; }

        public ChromeDriver ChromeDriver { get; set; }

        public InternetExplorerDriver IEDriver { get; set; }

        [TestInitialize]
        public void Setup()
        {
            StartIIS();
            //this.FirefoxDriver = new FirefoxDriver();
            this.ChromeDriver = new ChromeDriver();
            //this.IEDriver = new InternetExplorerDriver();
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (!_iisProcess.HasExited)
                _iisProcess.Kill();

            //this.FirefoxDriver.Quit();
            this.ChromeDriver.Quit();
            //this.IEDriver.Quit();
        }

        private void StartIIS()
        {
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, iisPort);
            _iisProcess.Start();
        }

        protected virtual string GetApplicationPath(string applicationName)
        {
            var slnFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));

            return Path.Combine(slnFolder, applicationName);
        }

        public string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
                relativeUrl = "/" + relativeUrl;

            return string.Format("http://localhost:{0}{1}", iisPort, relativeUrl);
        }
    }
}

namespace OpenQA.Selenium
{
    
    public static class Config {
        public static readonly TimeSpan ImplicitWait = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan NoWait = TimeSpan.FromSeconds(0);
    }

    public static class WebElementExtensions
    {
        public static bool ElementIsPresent(this IWebDriver driver, By by)
        {
            var present = false;

            driver.Manage().Timeouts().ImplicitlyWait(Config.NoWait);

            try
            {
                present = driver.FindElement(by).Displayed;
            }
            catch(NoSuchElementException)
            {

            }
            driver.Manage().Timeouts().ImplicitlyWait(Config.ImplicitWait); 
            return present;
        }
    }
}
