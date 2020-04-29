using Medicines_Company_Information.BaseFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines_Company_Information.PageObjects
{
    public class CompanyInfoPage: BasePage
    {
        private readonly ICustomDriver _customDriver;

        public CompanyInfoPage(ICustomDriver customDriver) : base(customDriver)
        {
            _customDriver = customDriver;
        }

        public override Uri RelativeUrl => new Uri(ConfigurationManager.AppSettings["BaseUri"]);

        public string CompanyName => _customDriver.FindElement("h1").Text;

        public string imagesrc => _customDriver.FindElement(".companyLogoWrapper img").GetAttribute("src");
        public string Address => _customDriver.FindElement("div.gfdCompanyDetailsCol div p").Text;

        public string secondInfoName => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(3)").Text;
        public string secondInfoValue => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(3) + div p").Text;

        public string thirdInfoName => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(5)").Text;
        public string thirdInfoValue => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(5) + div p a").Text;

        public string sthirdInfoValue => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(5) + div p").Text;
        public string forthInfoName => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(7)").Text;
        public string forthInfoValue => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(7) + div p a").Text;

        public string sforthInfoValue => _customDriver.FindElement("div.gfdCompanyDetailsTitle:nth-child(7) + div p").Text;

        public string fiveInfoName => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(1)").Text;

        public string fiveInfoValue => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(1) + div p").Text;

        public string sfiveInfoValue => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(1) + div p a").Text;
        public string sixInfoName => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(3)").Text;

        public string sixInfoValue => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(3) + div p").Text;

        public string sevenInfoName => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(5)").Text;

        public string sevenInfoValue => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(5) + div p").Text;

        public string ssevenInfoValue => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(5) + div p a").Text;
        public string eighthInfoName => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(7)").Text;

        public string eightInfoValue => _customDriver.FindElement("div.col-md-4 div.gfdCompanyDetailsTitle:nth-child(7) + div p").Text;

    }
}
