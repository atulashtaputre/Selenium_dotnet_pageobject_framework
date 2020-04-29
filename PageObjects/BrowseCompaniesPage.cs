using Medicines_Company_Information.BaseFramework;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Linq;

namespace Medicines_Company_Information.PageObjects
{
    public class BrowseCompaniesPage: BasePage
    {
        private readonly ICustomDriver _customDriver;

        public BrowseCompaniesPage(ICustomDriver customDriver) : base(customDriver)
        {
            _customDriver = customDriver;
        }

        public override Uri RelativeUrl => new Uri (ConfigurationManager.AppSettings["BaseUri"]);
        public void SelectFirstCompany()
        {
            FirstCompanyLink.Click();
        }

        public void SelectThirdCompany()
        {
            ThirdCompanyLink.Click();
        }

        public void SelectLastCompany()
        {
            _customDriver.ExecuteAsyncJavascript("window.scrollBy(0,160)");

            //((JavascriptExecutor)_customDriver).executeScript.executeScript("window.scrollBy(0,150)");
            LastCompanyLink.Click();
        }

        private int count => _customDriver.FindElements("div.col-md-6.ingredients.ieleft li > a").Count();
        private IWebElement FirstCompanyLink => _customDriver.FindElement("div.col-md-6.ingredients.ieleft li:nth-child(1) > a");

        private IWebElement ThirdCompanyLink => _customDriver.FindElement("div.col-md-6.ingredients.ieleft li:nth-child(3) > a");
        
        private IWebElement LastCompanyLink => _customDriver.FindElement("div.col-md-6.ingredients.ieleft li:nth-child("+ (count)+") > a");
    }
}
