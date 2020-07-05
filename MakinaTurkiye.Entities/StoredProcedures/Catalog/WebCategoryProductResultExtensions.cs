using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public static class WebCategoryProductResultExtensions
    {
        public static string GetCurrencyCssName(this WebCategoryProductResult product)
        {
            byte PriceTypeDiscuss = 241;
            byte PriceTypeAsk = 240;
            if (string.IsNullOrEmpty(product.CurrencyCodeName) || product.ProductPriceType == PriceTypeDiscuss || product.ProductPriceType == PriceTypeAsk)
                return string.Empty;
            switch (product.CurrencyCodeName)
            {
                case "tr-TR": return "fa fa-turkish-lira";
                case "en-US": return "fa fa-usd";
                case "de-DE": return "fa fa-eur";
                case "ja-JP": return "fa fa-jpy";
                default: return "fa fa-turkish-lira";
            }
        }
        public static string GetCurrencySymbol(this WebCategoryProductResult product)
        {
            string currency = string.Empty;
            if (product.CurrencyCodeName == null)
            {
                currency = "₺";
            }
            else
            {
                switch (product.CurrencyCodeName)
                {
                    case "tr-TR": currency = "₺"; break;
                    case "en-US": currency = "$"; break;
                    case "de-DE": currency = "€"; break;
                    case "ja-JP": currency = "JPY"; break;
                    default: currency = "₺"; break;
                }
            }
            return currency;
        }

        public static string GetKdvOrFobText(this WebCategoryProductResult product)
        {
            if (product.Kdv == true)
                return "Kdv Dahil";
            else if (product.Fob == true)
                return "FOB Fiyatı";
            else if (product.Kdv == false)
                return "Kdv Hariç";
    
            return string.Empty;
        }
        public static string GetFormattedPrice(this WebCategoryProductResult product)
        {

                 byte PriceTypePrice = 238;
            byte PriceTypeRange = 239;
            byte PriceTypeDiscuss = 241;
            byte PriceTypeAsk = 240;

            if (product.ProductPriceType == PriceTypePrice || (product.ProductPriceType == 0 || product.ProductPriceType == null))
            {
                if (!product.ProductPrice.HasValue)
                    return string.Empty;

                if (product.ProductPrice.HasValue && product.ProductPrice.Value == 0)
                    return string.Empty;

                //return product.ProductPrice.Value.ToString("0.##").Replace(",", ".");
                string price = product.ProductPrice.Value.ToString("0.00");
                if (string.Format("{0:#,0.00}", Convert.ToDouble(price)).EndsWith(",00"))
                {
                    price = string.Format("{0:#,0.00}", Convert.ToDouble(price)).Replace(",00", "");
                }
                else
                {

                    price = string.Format("{0:#,0.00}", Convert.ToDouble(price));
                }
                return price;
            }
            else if (product.ProductPriceType == PriceTypeRange)
            {
                string priceBegin = "0";
                string priceLast = "0";

                if(product.ProductPriceBegin!=null)
                {
                    priceBegin = product.ProductPriceBegin.Value.ToString("0.00");
                }
                if(product.ProductPriceLast!=null)
                {
                    priceLast = product.ProductPriceLast.Value.ToString("0.00");
                }

                if (string.Format("{0:#,0.00}", Convert.ToDouble(priceBegin)).EndsWith(",00") && string.Format("{0:#,0.00}", Convert.ToDouble(priceLast)).EndsWith(",00"))
                {
                    priceBegin = string.Format("{0:#,0.00}", Convert.ToDouble(priceBegin)).Replace(",00", "");
                    priceLast = string.Format("{0:#,0.00}", Convert.ToDouble(priceLast)).Replace(",00", "");
                }
                else
                {
                    priceBegin = string.Format("{0:#,0.00}", Convert.ToDouble(priceBegin));
                    priceLast = string.Format("{0:#,0.00}", Convert.ToDouble(priceLast));
                }
                return string.Format("{0} - {1}", priceBegin, priceLast);
            }
            else
            {
                if (product.ProductPriceType == PriceTypeDiscuss)
                    return "Fiyat Görüşülür";
                else 
                    return "Fiyat Sorunuz";
            }
        }
 
    }
}
