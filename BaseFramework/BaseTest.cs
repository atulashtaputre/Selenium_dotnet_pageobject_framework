using Medicines_Company_Information.BaseFramework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Medicines_Company_Information.BaseFramework
{
    public abstract class BaseTest
    {
        private ICustomDriver _Driver;

        protected ICustomDriver Driver => _Driver;
        [OneTimeSetUp]
        public void setup()
        {
            /// create an instance of chrome driver
            _Driver = Create();
            _Driver.MaximizeWindow();
        }

        private static CustomDriver Create()
        { 
            var options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);

            options.AddArgument(@"--incognito");

            var webDriver = new ChromeDriver(options);
            return new CustomDriver(webDriver);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Driver.Dispose();
        }
    }
}
