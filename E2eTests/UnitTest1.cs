using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace E2eTests
{
    public class Tests
    {
        private static IWebDriver _driver;

        [SetUp]
        public void Setup()
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

        [Test]
        public void Test1()
        {
            _driver.Navigate().GoToUrl("http://website");
            Assert.That(_driver.Title, Is.EqualTo("Counter"));
        }

        [TearDown]
        public static void ClassCleanup()
        {
            _driver.Quit();
        }
    }
}