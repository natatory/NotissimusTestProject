using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;


namespace NotissimusTestProject
{
    class XMLObjectModel
    {
        public static XDocument GetXmlData(string ymlURL)
        {
            Console.WriteLine("Loading XML data...");
            Uri baseURI = new Uri(ymlURL);
            try
            {
                var res = XDocument.Load(baseURI.AbsoluteUri);
                Console.WriteLine("Loading XML data successfully");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static IEnumerable<XElement> GetOffers(XDocument xDoc)
        {
            return xDoc.XPathSelectElements("//offers/offer");
        }

        public static IEnumerable<XElement> GetOffersByCategory(XDocument xDoc, string cat)
        {

            return xDoc.XPathSelectElements("//offers/offer")
                .Where(x => (string)x.Attribute("type") == cat);
        }
    }
}
