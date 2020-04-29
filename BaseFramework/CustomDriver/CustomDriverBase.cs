using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net.Sockets;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Medicines_Company_Information.BaseFramework
{
    public abstract class CustomDriverBase : IWebDriver, ITakesScreenshot
    {
        protected CustomDriverBase(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public Uri BaseUri { get; }

        public string Name { get; }

        // This is deliberately marked as protected to force Test calls to go via the CustomDriver class
        // this allows us to have control over how the driver is used and to ensure things like logging
        public string Url
        {
            get { return WebDriver.Url; }

            set { throw new NotImplementedException("This isn't allowed"); }
        }

        public string Title => WebDriver.Title;

        [Obsolete("This will only be implemented when it's required", true)]
        public string PageSource { get; }

        [Obsolete("This will only be implemented when it's required", true)]
        public string CurrentWindowHandle { get; }

        [Obsolete("This will only be implemented when it's required", true)]
        public ReadOnlyCollection<string> WindowHandles { get; }

        public Size WindowSize
        {
            get
            {
                return WebDriver.Manage().Window.Size;
            }
        }

        protected IWebDriver WebDriver { get; }

        [Obsolete("Please use FindElement(cssSelector)", true)]
        public IWebElement FindElement(By @by)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Please use FindElements(cssSelector)", true)]
        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing of driver '{0}'", Name);
            try
            {
                WebDriver.Dispose();
            }
            catch (SocketException)
            {
                Console.WriteLine("Sockets exception handled during Dispose. KillChrome method should handle any instances left running");
            }
        }

        [Obsolete("If we really need the ability to close the browser, we will implement this. Otherwise, use Dispose", true)]
        public void Close()
        {
            WebDriver.Close();
        }

        [Obsolete("Please use Dispose instead", true)]
        public void Quit()
        {
            throw new NotImplementedException();
        }

        [Obsolete("This shouldn't be called directly - use the CustomDriverSettings class", true)]
        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        [Obsolete("Please use the Navigate method on ICustomDriver", true)]
        public INavigation Navigate()
        {
            throw new NotImplementedException();
        }

        [Obsolete("We will only implement this if it's required", true)]
        public ITargetLocator SwitchTo()
        {
            throw new NotFoundException();
        }

        public void SwitchToOpenedTabWindow(int tabNumber)
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[tabNumber]);
        }

        public SelectElement SelectFromDropwDown(string cssSelector)
        {
            SelectElement select = new SelectElement(WebDriver.FindElement(By.CssSelector(cssSelector)));

            return select;
        }

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)WebDriver).GetScreenshot();
        }

        public void MaximizeWindow()
        {
            WebDriver.Manage().Window.Maximize();
        }
    }
}