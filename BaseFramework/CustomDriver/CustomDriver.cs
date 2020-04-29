using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Medicines_Company_Information.BaseFramework
{
    public class CustomDriver : CustomDriverBase, ICustomDriver
    {
        public CustomDriver(IWebDriver webDriver) : base(webDriver)
        {
        }

        public bool IsAuthenticated(string cookieName) => WebDriver.Manage().Cookies.GetCookieNamed(cookieName) != null;

        public void Navigate(Uri relativeUri)
        {
            Uri uri = new Uri(relativeUri, "");
            WebDriver.Navigate().GoToUrl(uri);
        }

        public IWebElement FindElement(string cssSelector)
        {
            By by = By.CssSelector(cssSelector);
            IWebElement element = WebDriver.FindElement(by);
            return new CustomWebElement(element, WebDriver);
        }

        public IEnumerable<IWebElement> FindElements(string cssSelector)
        {
            By by = By.CssSelector(cssSelector);
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> elements = WebDriver.FindElements(by);
            return elements.Select(x => new CustomWebElement(x, WebDriver));
        }

        public void ClearCookies()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }

        public void WaitForElementIsVisible(string cssSelector)
        {
            By selector = By.CssSelector(cssSelector);
            TimeSpan fromSeconds = TimeSpan.FromSeconds(20);
            new WebDriverWait(WebDriver, fromSeconds).Until(ExpectedConditions.ElementIsVisible(selector));
        }

        public void WaitForElementTextChange(string cssSelector, string elementText)
        {
            By selector = By.CssSelector(cssSelector);
            TimeSpan fromSeconds = TimeSpan.FromSeconds(20);
            new WebDriverWait(WebDriver, fromSeconds).Until(ExpectedConditions.InvisibilityOfElementWithText(selector, elementText));
        }

        public void WaitForElementNotToBeVisible(string cssSelector)
        {
            By selector = By.CssSelector(cssSelector);
            TimeSpan fromSeconds = TimeSpan.FromSeconds(20);
            new WebDriverWait(WebDriver, fromSeconds).Until(ExpectedConditions.InvisibilityOfElementLocated(selector));
        }

        public void WaitForOverlay()
        {
            By selector = By.CssSelector(".sheath");
            TimeSpan fromSeconds = TimeSpan.FromSeconds(60);
            new WebDriverWait(WebDriver, fromSeconds).Until(ExpectedConditions.InvisibilityOfElementLocated(selector));
        }

        public void WaitForElementExists(string cssSelector)
        {
            By selector = By.CssSelector(cssSelector);
            TimeSpan fromSeconds = TimeSpan.FromSeconds(20);
            new WebDriverWait(WebDriver, fromSeconds).Until(ExpectedConditions.ElementExists(selector));
        }

        public void WaitForAjax()
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(driver => (bool)(WebDriver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        public void ExecuteAsyncJavascript(string script, params string[] args)
        {
            object result = ((IJavaScriptExecutor)WebDriver).ExecuteScript(script, args);
            if (result == null)
            {
                return;
            }

            List<string> resultStrings = ((IList<object>)result).Select(x => x.ToString()).ToList();
            if (resultStrings.First().ToLower() == "error")
            {
                throw new Exception("Javascript execution failure: " + resultStrings[1]);
            }
        }

        public IWebElement FindElementByJavascript(string script)
        {
            string wrapper = script;
            if (script.StartsWith("return") == false)
            {
                Console.WriteLine("Provided script missing return - prepending 'return '");
                wrapper = $"return {script}";
            }

            object elementCollection = (WebDriver as IJavaScriptExecutor).ExecuteScript(wrapper);

            ICollection<IWebElement> elements = elementCollection as ICollection<IWebElement>;
            if (elements == null)
            {
                throw new NoSuchElementException($"Unable to find element using '{script}'");
            }

            IWebElement element = elements.First();
            return new CustomWebElement(element, WebDriver);
        }

        public void WaitForReady(string message)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3));
            wait.Until(driver =>
            {
                System.Collections.ObjectModel.ReadOnlyCollection<LogEntry> logs = driver.Manage().Logs.GetLog(LogType.Browser);
                return logs.Any(log => log.Message.Contains(message));
            });
        }

        public void WaitForNavigation(Uri relativeUrl)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            string targetUrl = relativeUrl.ToString().ToLower().TrimEnd(new[] { '/' });
            wait.Until(driver =>
            {
                Uri currentUri = new Uri(driver.Url);
                return currentUri.AbsolutePath.ToLower().TrimEnd(new[] { '/' }) == targetUrl;
            });
        }

        public void SnapshotElement(string cssSelector, string folder, string filename)
        {
            string randomFilename = Path.Combine(folder, $"{Guid.NewGuid()}.png");
            CaptureFullPageImage(randomFilename);
            IWebElement element = WebDriver.FindElement(By.CssSelector(cssSelector));
            int width = element.Size.Width;
            int height = element.Size.Height;
            int x = element.Location.X;
            int y = element.Location.Y;
            Rectangle rect = new Rectangle(x, y, width, height);
            Bitmap bitmap = new Bitmap(randomFilename);

            // we *might* end up with elements that are wider/taller than the captured image.
            // this will ensure we don't try and crop an image outside the bounds of the full screen image
            if (x + width > bitmap.Width)
            {
                rect.Width = bitmap.Width - x;
            }

            if (y + height > bitmap.Height)
            {
                rect.Height = bitmap.Height - y;
            }

            Bitmap cloneFile = bitmap.Clone(rect, bitmap.PixelFormat);
            cloneFile.Save(filename);
            bitmap.Dispose();
            File.Delete(randomFilename);
        }

        private void CaptureFullPageImage(string filename)
        {
            IJavaScriptExecutor jsDriver = (IJavaScriptExecutor)WebDriver;
            string html2Canvas = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "html2canvas.min.js"));
            jsDriver.ExecuteScript(html2Canvas);

            string generateScreenshotJS = @"function genScreenshot () {
	                                        var canvasImgContentDecoded;
	                                        html2canvas(document.body).then(function(canvas) {                               
		                                        window.canvasImgContentDecoded = canvas.toDataURL(""image/png"");
                                            });
                                        }
                                        genScreenshot();";
            jsDriver.ExecuteScript(generateScreenshotJS);

            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            wait.Until(
                wd =>
                {
                    object response = jsDriver.ExecuteScript("return (typeof canvasImgContentDecoded === 'undefined' || canvasImgContentDecoded === null)");
                    if (string.IsNullOrEmpty(response.ToString()))
                    {
                        return false;
                    }

                    return !bool.Parse(response.ToString());
                });

            wait.Until(wd =>
            {
                object result = jsDriver.ExecuteScript("return canvasImgContentDecoded;");
                return !string.IsNullOrEmpty(result.ToString());
            });

            string pngContent = (string)jsDriver.ExecuteScript("return canvasImgContentDecoded;");
            pngContent = pngContent.Replace("data:image/png;base64,", string.Empty);
            byte[] data = Convert.FromBase64String(pngContent);
            Image image;
            using (MemoryStream ms = new MemoryStream(data))
            {
                image = Image.FromStream(ms);
            }

            image.Save(filename, ImageFormat.Png);
        }
    }
}