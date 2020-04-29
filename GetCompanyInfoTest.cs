using Medicines_Company_Information.BaseFramework;
using Medicines_Company_Information.PageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Medicines_Company_Information
{
    public class GetCompanyInfoTest : BaseTest
    {
        [Test]
        public void getCompanyInformationInXmlAndJson()
        {
            try
            {
                var helperFunction = new HelperFunctions();
                var page = new BrowseCompaniesPage(Driver);
                page.NavigateTo();
                page.SelectFirstCompany();
                
                var companyInfo = new CompanyInfoPage(Driver);
                Dictionary<string, string> myDict =new Dictionary<string, string>();

                // Adding key/value pairs in myDict 
                myDict.Add(companyInfo.secondInfoName, companyInfo.secondInfoValue);
                myDict.Add(companyInfo.thirdInfoName, companyInfo.thirdInfoValue);
                myDict.Add(companyInfo.fiveInfoName, companyInfo.fiveInfoValue);
                myDict.Add(companyInfo.sixInfoName, companyInfo.sixInfoValue);
                myDict.Add(companyInfo.sevenInfoName, companyInfo.sevenInfoValue);
                helperFunction.createLogo(companyInfo.imagesrc, companyInfo.CompanyName);
                helperFunction.AddCompanyDataToXmlFile(companyInfo.CompanyName, companyInfo.Address,
                    myDict);

                myDict = new Dictionary<string, string>(); 

                page.NavigateTo();
                page.SelectThirdCompany();
                myDict.Add(companyInfo.secondInfoName, companyInfo.secondInfoValue);
                myDict.Add(companyInfo.thirdInfoName, companyInfo.sthirdInfoValue);
                myDict.Add(companyInfo.forthInfoName, companyInfo.sforthInfoValue);
                myDict.Add(companyInfo.fiveInfoName, companyInfo.sfiveInfoValue);
                myDict.Add(companyInfo.sixInfoName, companyInfo.sixInfoValue);
                myDict.Add(companyInfo.sevenInfoName, companyInfo.ssevenInfoValue);
                myDict.Add(companyInfo.eighthInfoName, companyInfo.eightInfoValue);
                ///createLogo download the logo as an image file in Images folder in solution.
                helperFunction.createLogo(companyInfo.imagesrc, companyInfo.CompanyName);
                ///AddCompanyDataToXmlFile add company information to xml file and save it 
                ///in "Datafiles" folder in soluntion.

                helperFunction.AddCompanyDataToXmlFile(companyInfo.CompanyName, companyInfo.Address,
                    myDict);

                myDict = new Dictionary<string, string>();
                page.NavigateTo();
                page.SelectLastCompany();
                myDict.Add(companyInfo.secondInfoName, companyInfo.secondInfoValue);
                myDict.Add(companyInfo.thirdInfoName, companyInfo.sthirdInfoValue);
                myDict.Add(companyInfo.fiveInfoName, companyInfo.sfiveInfoValue);
                myDict.Add(companyInfo.sixInfoName, companyInfo.sixInfoValue);
                myDict.Add(companyInfo.sevenInfoName, companyInfo.sevenInfoValue);
                helperFunction.createLogo(companyInfo.imagesrc, companyInfo.CompanyName);
                helperFunction.AddCompanyDataToXmlFile(companyInfo.CompanyName, companyInfo.Address,
                    myDict);
                helperFunction.convertXmlToJson();
            }
            catch (Exception e )
            {
                Console.WriteLine("Error message is :" + e.Message + " -- Internal error is : " +e.InnerException );
            }
        }
    }
}
