using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace Medicines_Company_Information.BaseFramework
{
    public class CustomWebElement : IWebElement
    {
        private readonly IWebElement _webElement;

        public CustomWebElement(IWebElement webElement, IWebDriver driver)
        {
            _webElement = webElement;
        }

        public string TagName => _webElement.TagName;

        public string Text => _webElement.Text;

        public bool Enabled => _webElement.Enabled;

        public bool Selected => _webElement.Selected;

        [Obsolete("This will only be implemented when it's required", true)]
        public Point Location { get; }

        [Obsolete("This will only be implemented when it's required", true)]
        public Size Size { get; }

        public bool Displayed => _webElement.Displayed;

        [Obsolete("Please use FindElement(cssSelector) on the ICustomDriver", true)]
        public IWebElement FindElement(By @by)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Please use FindElements(cssSelector) on the ICustomDriver", true)]
        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new NotImplementedException();
        }

        /// <summary>Clear the text content of an element</summary>
        public void Clear()
        {
            _webElement.Clear();
        }

        /// <summary>Simulate typing text into the element</summary>
        /// <param name="text">The text to type</param>
        public void SendKeys(string text)
        {
            _webElement.SendKeys(text);
        }

        public void Submit()
        {
            _webElement.Submit();
        }

        /// <summary>
        /// Use this for clicking on an element - do NOT use to submit a form
        /// To submit a form, please use the <see cref="Submit"/> method
        /// </summary>
        public void Click()
        {
            if (_webElement.TagName == "input")
            {
                Console.WriteLine("Warning: If this is supposed to trigger a form submission, use the Submit() method instead");
            }

            _webElement.Click();
        }

        public string GetAttribute(string attributeName)
        {
            return _webElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return _webElement.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
