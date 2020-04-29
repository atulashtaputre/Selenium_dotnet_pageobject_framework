using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Medicines_Company_Information
{
    public class HelperFunctions
    {
        public void convertXmlToJson()
        {
            string xml = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\DataFiles\CompanyInformation.xml");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string json = JsonConvert.SerializeXmlNode(doc.FirstChild.NextSibling);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\DataFiles\CompanyInformation.xml".Replace(".xml", ".json"), json);
        }

        public void AddCompanyDataToXmlFile(string companyName, string address, Dictionary<string, string> myDict)
        {
            var fileLocation = AppDomain.CurrentDomain.BaseDirectory + @"..\..\DataFiles\CompanyInformation.xml";
            if (!File.Exists(fileLocation))
            {
                XmlTextWriter writer = new XmlTextWriter(fileLocation, null);
                writer.WriteStartElement("Companies");
                writer.WriteEndElement();
                writer.Close();
            }

            XElement xml = XElement.Load(fileLocation);
            var node = new XElement("Company");
            xml.Add(node);
            node.Add(new XElement("Name", companyName), new XElement("Address", address));

            foreach (var pair in myDict)
            {
                if (pair.Key.ToString().Contains(" "))
                    node.Add(new XElement(pair.Key.ToString().Replace(" ", "_"), pair.Value));
                else
                {
                    node.Add(new XElement(pair.Key.ToString(), pair.Value));
                }
            }
            xml.Save(fileLocation);
        }

        public void createLogo(string src, string name)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(src), AppDomain.CurrentDomain.BaseDirectory + @"..\..\Images\" + name + ".jpg");
            }
        }
    }
}
