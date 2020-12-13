using System;
using System.Text;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public static class ProductExtensions
    {
        public static string GetFullAddress(this Product product)
        {

            StringBuilder sb = new StringBuilder();
            if (product.Town != null)
            {
                sb.AppendFormat("{0} ", product.Town.TownName);
            }

            if (product.Locality != null)
            {
                sb.AppendFormat(" {0} ", product.Locality.LocalityName);
            }
            if (product.Town != null && product.Town.District != null)
            {
                sb.AppendFormat(" {0} ", product.Town.District.DistrictName);
            }

            if (product.CityId != null)
            {
                sb.AppendFormat(" {0} ", product.City.CityName);
            }

            if (product.Country != null)
            {
                sb.AppendFormat(" {0} ", product.Country.CountryName);
            }
            return sb.ToString();
        }

        public static string GetCurrencyCssName(this Product product)
        {
            byte PriceTypeDiscuss = 241;
            byte PriceTypeAsk = 240;
            if (!product.CurrencyId.HasValue || product.ProductPriceType == PriceTypeDiscuss || product.ProductPriceType == PriceTypeAsk)
                return string.Empty;

            switch (product.CurrencyId.Value)
            {
                case 1: return "fa fa-turkish-lira";
                case 2: return "fa fa-usd";
                case 3: return "fa fa-eur";
                case 4: return "fa fa-jpy";
                default: return "fa fa-turkish-lira";
            }
        }


        public static string GetCurrencyCssName(this string CurrencyName)
        {
            if (CurrencyName==string.Empty)
                return string.Empty;

            switch (CurrencyName)
            {
                case "TL": return "fa fa-turkish-lira";
                case "USD": return "fa fa-usd";
                case "EUR": return "fa fa-eur";
                case "": return "fa fa-jpy";
                default: return "fa fa-turkish-lira";
            }
        }


        public static string GetFormattedPrice(this decimal ProductPrice, byte ProductPriceType)
        {
            byte PriceTypePrice = 238;
            byte PriceTypeRange = 239;
            byte PriceTypeDiscuss = 241;
            //byte PriceTypeAsk = 240;

            if (ProductPriceType == PriceTypePrice || (ProductPriceType == 0 || ProductPriceType == null))
            {
                if (ProductPrice == null || ProductPrice == 0)
                    return string.Empty;
                //return ProductPrice.Value.ToString("0.##").Replace(",", ".");
                string price = ProductPrice.ToString("0.00");
                if (string.Format("{0:#,0.00}", Convert.ToDouble(price)).EndsWith(",00"))
                {
                    price = string.Format("{0:#,0.00}", Convert.ToDouble(price)).Replace(",00", "");
                }
                else
                {

                    price = string.Format("{0:#,0.00}", Convert.ToDouble(price));
                }
                return string.Format("{0}", price);
            }
            else
            {
                if (ProductPriceType == PriceTypeDiscuss)
                    return "Fiyat Görüşülür";
                else
                    return "Fiyat Sorunuz";
            }
        }


        public static string GetFormattedPrice(this Product product)
        {
            byte PriceTypePrice = 238;
            byte PriceTypeRange = 239;
            byte PriceTypeDiscuss = 241;
            //byte PriceTypeAsk = 240;

            if (product.ProductPriceType == PriceTypePrice || (product.ProductPriceType == 0 || product.ProductPriceType == null))
            {
                if (product.ProductPrice==null || product.ProductPrice == 0)
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
                return string.Format("{0}", price);
            }
            else if (product.ProductPriceType == PriceTypeRange)
            {
                if (product.ProductPriceBegin == null)
                    product.ProductPriceBegin = 0;
                if (product.ProductPriceLast == null)
                    product.ProductPriceLast = 0;
                string priceBegin = product.ProductPriceBegin.Value.ToString("0.00");
                string priceLast = product.ProductPriceLast.Value.ToString("0.00");

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
                return string.Format("{0} -{1}", priceBegin, priceLast);
            }
            else
            {
                if (product.ProductPriceType == PriceTypeDiscuss)
                    return "Fiyat Görüşülür";
                else
                    return "Fiyat Sorunuz";
            }
        }
        public static string GetFormattedPriceWithCurrency(this Product product)
        {
             byte PriceTypePrice=238;
             byte PriceTypeRange=239;
             byte PriceTypeDiscuss = 241;
             //byte PriceTypeAsk = 240;
            if(product.ProductPriceType==PriceTypePrice || (product.ProductPriceType==0 || product.ProductPriceType==null))
            {
                if (product.ProductPrice==null || product.ProductPrice == 0)
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
            string currency = GetCurrency(product);
            return string.Format("{0} {1}", price, currency);
            }
            else if(product.ProductPriceType==PriceTypeRange)
            {
                if (product.ProductPriceBegin == null)
                    product.ProductPriceBegin = 0;
                if (product.ProductPriceLast == null)
                    product.ProductPriceLast = 0;
                string priceBegin= product.ProductPriceBegin.Value.ToString("0.00");
                string priceLast=product.ProductPriceLast.Value.ToString("0.00");

                if (string.Format("{0:#,0.00}", Convert.ToDouble(priceBegin)).EndsWith(",00") && string.Format("{0:#,0.00}", Convert.ToDouble(priceLast)).EndsWith(",00") )
                {
                    priceBegin = string.Format("{0:#,0.00}", Convert.ToDouble(priceBegin)).Replace(",00", "");
                    priceLast=string.Format("{0:#,0.00}", Convert.ToDouble(priceLast)).Replace(",00", "");
                }
                else
                {

                    priceBegin = string.Format("{0:#,0.00}", Convert.ToDouble(priceBegin));
                    priceLast = string.Format("{0:#,0.00}", Convert.ToDouble(priceLast));
                }
                string currency = GetCurrency(product);
                return string.Format("{0} - {1} {2}", priceBegin,priceLast, currency);
            }
            else
            {
                if (product.ProductPriceType == PriceTypeDiscuss)
                    return "Fiyat Görüşülür";
                else
                    return "Fiyat Sorunuz";
            }
        }
        public static string GetCurrency(this Product product)
        {
            string currency = string.Empty;
            if (product.CurrencyId == null)
            {
                currency = "TL";
            }
            else
            {
                switch (product.CurrencyId.Value)
                {
                    case 1: currency = "TL"; break;
                    case 2: currency = "USD"; break;
                    case 3: currency = "EUR"; break;
                    case 4: currency = "JPY"; break;
                    default: currency = "TL"; break;
                }
            }
            return currency;
        }

        public static string GetCurrencySymbol(this Product product)
        {
            string currency = string.Empty;
            if (product.CurrencyId == null)
            {
                currency = "₺";
            }
            else
            {
                switch (product.CurrencyId.Value)
                {
                    case 1: currency = "₺"; break;
                    case 2: currency = "$"; break;
                    case 3: currency = "€"; break;
                    case 4: currency = "JPY"; break;
                    default: currency = "₺"; break;
                }
            }
            return currency;
        }

        public static string GetLocation(this Product product)
        {
            if (product.Locality != null && product.City != null)
            {
                return string.Format("{0} / {1} / {2}", product.Town!=null ?  product.Town.TownName : "", product.Locality.LocalityName, product.City.CityName, product.Country.CountryName);
            }
            return string.Empty;
        }

        public static string GetActiveBeginDate(this Product product)
        {
            if (product.ProductLastUpdate.HasValue)
                return product.ProductLastUpdate.Value.ToString("dd.MM.yyyy");
            if (product.ProductAdvertBeginDate.HasValue)
                return product.ProductAdvertBeginDate.Value.ToString("dd.MM.yyyy");
            return string.Empty;

        }

        public static string GetActiveEndDate(this Product product)
        {
            if (product.ProductAdvertEndDate.HasValue)
                return product.ProductAdvertEndDate.Value.ToString("dd.MM.yyyy");
            if (product.ProductLastUpdate.HasValue)
                return product.ProductLastUpdate.Value.ToString("dd.MM.yyyy");
            return string.Empty;
        }

        public static string GetKdvOrFobText(this Product product)
        {
            if (product.Kdv == true)
                return "Kdv Dahil";
            else if (product.Fob == true)
                return "FOB Fiyatı";
            else if (product.Kdv == false)
                return "Kdv Hariç";

            return string.Empty;
        }

        public static bool GetActiveStatus(this Product product)
        {
            if (!product.ProductActive.HasValue)
                return false;
            if (!product.ProductActiveType.HasValue)
                return false;
            if (product.ProductActiveType.Value != 1)
                return false;
            if (!product.ProductActive.Value)
                return false;

            return true;
        }

        public static int GetLastCategoryId(this Product product)
        {
            int lastCategoryId = 0;

            if (product.ModelId.HasValue)
            {
                lastCategoryId = product.ModelId.Value;
            }
            else if (product.SeriesId.HasValue)
            {
                lastCategoryId = product.SeriesId.Value;
            }
            else if (product.BrandId.HasValue)
            {
                lastCategoryId = product.BrandId.Value;
            }
            else if (product.CategoryId.HasValue)
            {
                lastCategoryId = product.CategoryId.Value;
            }
            return lastCategoryId;
        }
    }
}
