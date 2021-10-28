using NeoSistem.EnterpriseEntity.Extensions;
using System.Linq;
using System.Xml.Linq;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class ExchangeRatesModel
    {
        private readonly XElement eurCurrency;
        private readonly XElement gbpCurrency;
        private readonly XElement jpyCurrency;
        private readonly XElement usdCurrency;

        public ExchangeRatesModel()
        {
            XDocument doc = null;
            //XDocument.Load(HttpContext.Current.Server.MapPath("/ExChange/exChange.xml"));

            if (doc != null)
            {
                XElement[] elements = doc.Element("Tarih_Date").Elements().ToArray();

                eurCurrency = elements.SingleOrDefault(x => { return x.Attribute("Kod").Value == "EUR"; });
                usdCurrency = elements.SingleOrDefault(x => { return x.Attribute("Kod").Value == "USD"; });
                jpyCurrency = elements.SingleOrDefault(x => { return x.Attribute("Kod").Value == "JPY"; });
                gbpCurrency = elements.SingleOrDefault(x => { return x.Attribute("Kod").Value == "GBP"; });
            }
        }

        public double USD_Buy
        {
            get { return usdCurrency.Element("ForexBuying").Value.Replace(".", ",").ToDouble(); }
        }

        public double USD_Sell
        {
            get { return usdCurrency.Element("ForexSelling").Value.Replace(".", ",").ToDouble(); }
        }

        public double EUR_Buy
        {
            get { return eurCurrency.Element("ForexBuying").Value.Replace(".", ",").ToDouble(); }
        }

        public double EUR_Sell
        {
            get { return eurCurrency.Element("ForexSelling").Value.Replace(".", ",").ToDouble(); }
        }

        public double JPY_Buy
        {
            get { return jpyCurrency.Element("ForexBuying").Value.Replace(".", ",").ToDouble(); }
        }

        public double JPY_Sell
        {
            get { return jpyCurrency.Element("ForexSelling").Value.Replace(".", ",").ToDouble(); }
        }

        public double GBP_Buy
        {
            get { return gbpCurrency.Element("ForexBuying").Value.Replace(".", ",").ToDouble(); }
        }

        public double GBP_Sell
        {
            get { return gbpCurrency.Element("ForexSelling").Value.Replace(".", ",").ToDouble(); }
        }
    }
}