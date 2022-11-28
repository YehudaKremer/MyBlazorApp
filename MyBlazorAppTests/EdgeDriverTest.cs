using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MyBlazorAppTests
{
    [TestClass]
    public class EdgeDriverTest
    {
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--ignore-certificate-errors", "--headless");

            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (isWindows)
            {
                using WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    options.AddArgument("--no-sandbox");
                }
            }

            _driver = new ChromeDriver(options);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            _driver.Navigate().GoToUrl("https://www.google.com/");
            Assert.AreEqual("Counter", _driver.Title);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _driver.Quit();
        }
    }
}
