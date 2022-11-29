using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MyBlazorAppTests
{
    [TestClass]
    public class EdgeDriverTest
    {
        static Process process;
        static IWebDriver driver;
        static bool websiteLoaded = false;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            InitiateWebsite();
            InitiateDriver();
        }

        private static void InitiateWebsite()
        {
            var solutionFolderName = "MyBlazorApp";
            var currentDirectory = Directory.GetCurrentDirectory();
            var solutionFolderPath = currentDirectory[..(currentDirectory
                .IndexOf(solutionFolderName) + solutionFolderName.Length)];

            process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "dotnet",
                    WorkingDirectory = solutionFolderPath,
                    Arguments = "run --project MyBlazorApp",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                },
            };
            process.OutputDataReceived += (sender, args) => websiteLoaded = true;
            process.Start();
            process.BeginOutputReadLine();

            while (!websiteLoaded) Task.Delay(10).Wait();
        }

        private static void InitiateDriver()
        {
            ChromeOptions options = new();
            options.AddArguments("--ignore-certificate-errors", "--headless");

            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (isWindows)
            {
                using WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    options.AddArgument("--no-sandbox");
                }
            }

            driver = new ChromeDriver(options);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            driver.Navigate().GoToUrl("http://localhost:5058");// static url!!!
            Assert.AreEqual("Counter", driver.Title);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver?.Quit();
            process?.Dispose();
        }
    }
}
