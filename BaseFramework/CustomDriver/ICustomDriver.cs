using System;
using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Medicines_Company_Information.BaseFramework
{
    public interface ICustomDriver : IWebDriver, ITakesScreenshot
    {
        Uri BaseUri { get; }

        /// <summary>Gets the name that is assigned to the instance of the implementation</summary>
        string Name { get; }

        Size WindowSize { get; }

        void Navigate(Uri relativeUri);

        void SwitchToOpenedTabWindow(int tabNumber);

        void MaximizeWindow();

        IWebElement FindElement(string cssSelector);

        IEnumerable<IWebElement> FindElements(string cssSelector);

        SelectElement SelectFromDropwDown(string cssSelector);

        void WaitForElementIsVisible(string cssSelector);

        void WaitForElementTextChange(string cssSelector, string textValue);

        void WaitForElementNotToBeVisible(string cssSelector);

        void WaitForElementExists(string cssSelector);

        /// <summary>This will wait for the overlay (ie. spinner/loading screen) to not be displayed before resuming test execution</summary>
        void WaitForOverlay();

        /// <summary>This will wait for any outstanding ajax calls to complete before resuming test execution</summary>
        void WaitForAjax();

        void ClearCookies();

        /// <summary>This will allow execution of any arbitrary javascript in the context of the current browser</summary>
        /// <param name="script">The javascript to execute</param>
        /// <param name="args">The arguments to pass to the script when executing</param>
        void ExecuteAsyncJavascript(string script, params string[] args);

        /// <summary>
        /// Allows for finding an <see cref="IWebElement"/> using javascript
        /// As long as the script returns a DOM element or a jQuery object, it will be converted to a <see cref="IWebElement"/>
        /// Should only be used when selecting via css would require looping through many elements or is actually not possible
        /// </summary>
        /// <param name="script">The script to execute</param>
        /// <returns>The <see cref="IWebElement"/> representing the DOM element</returns>
        /// <exception cref="NoSuchElementException">Will be thrown if no element found</exception>
        IWebElement FindElementByJavascript(string script);

        /// <summary>
        /// This will allow pausing test execution until a specific message is output to the javascript console
        /// This is used to ensure the javascript rendering has finished before we try and validate the page that we're on
        /// </summary>
        /// <param name="message">The message to wait for</param>
        void WaitForReady(string message);

        /// <summary>
        /// This will pause test execution until the specified url has been navigated to
        /// Use this if page redirection is occurring via javascipt
        ///
        /// This will check the AbsolutePath of the url Selenium sees against the the <param name="relativeUrl"></param>
        /// This needs to match preceeding and trailing slashes.
        /// </summary>
        void WaitForNavigation(Uri relativeUrl);

        bool IsAuthenticated(string cookieName);

        void SnapshotElement(string cssSelector, string folder, string filename);
    }
}