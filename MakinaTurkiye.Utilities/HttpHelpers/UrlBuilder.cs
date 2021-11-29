using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Utilities.FormatHelpers;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;


namespace MakinaTurkiye.Utilities.HttpHelpers
{
    public static class UrlBuilder
    {
        private static readonly MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
       private static readonly  bool IsRequestLocal = (HttpContext.Current!=null ? HttpContext.Current.Request.IsLocal : false);

        private enum HostNameType
        {
           Default,
           Product,
           Video,
           Store,
           StoreNews
        }

        private static string GetHost(HostNameType hostNameType)
        {
            if (config.ApplicationTestModeEnabled || IsRequestLocal)
            {
                return IsRequestLocal ? string.Empty : "http://www.makinaturkiye.com";
            }

            switch (hostNameType)
            {
                case HostNameType.Product:
                    return "https://www.makinaturkiye.com";
                case HostNameType.Video:
                    return "https://video.makinaturkiye.com";
                case HostNameType.Store:
                    return "https://magaza.makinaturkiye.com";
                case HostNameType.StoreNews:
                    return "https://haber.makinaturkiye.com";
                default:
                    return "https://www.makinaturkiye.com";
            }
        }

        public static string ToUrl(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.ToLower();
                value = value.Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s");
                value = value.Replace("ı", "i").Replace("ö", "o").Replace("ç", "c");
                value = value.Replace(".", "");
                value = value.Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S");
                value = value.Replace("İ", "I").Replace("Ö", "O").Replace("Ç", "C").Replace(" ", "-");
                value = value.Replace("&", "-");
                value = value.Replace("%", "");
                value = value.Replace("+", "");
                value = Regex.Replace(value, "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", "");
                value = value.Replace("–", "").Replace("---", "-");
                value = value.Replace("--", "-");
                value = value.Replace("--", "-");
                value = value.Replace("---", "-");
                value = value.Replace("³", "3");
                value = value.Replace("²", "2");
                value = value.Replace(" ", "");
                value = RemoveSpecialCharacters(value);

                return value;
            }
            return string.Empty;
        }

        public static string GetCategoryUrl(int categoryId, string categoryname,int? brandId,string brandName)
        {
            string url = GetHost(HostNameType.Default)+ "/" + ToUrl(categoryname + "-c-" + categoryId);

            if (brandId != null)
            {
                url = GetHost(HostNameType.Default) + "/" + ToUrl(brandName + "-" + categoryname + "-c-" + categoryId + "-" + brandId);
            }
            return url;
        }

        public static string GetCategoryUrl(int categoryId, string categoryname, int? brandId, string brandName, string searchText)
        {
            searchText = HttpUtility.UrlEncode(searchText);
            string url = GetHost(HostNameType.Default) + "/" + ToUrl(categoryname + "-c-" + categoryId) + "?SearchText=" + searchText;

            if (brandId != null)
                url = GetHost(HostNameType.Default) + "/" + ToUrl(brandName + "-" + categoryname + "-c-" + categoryId + "-" + brandId + "?SearchText=" + searchText );

            return url;
        }

        public static string GetFilterUrl(string url, CategoryFilterHelper filterHelper)
        {
            if(IsRequestLocal)
            {
                var requestUrl = HttpContext.Current.Request.Url;
                url = requestUrl.Scheme+ "://" + requestUrl.Authority + url;
            }
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (filterHelper.CountryId > 0 && filterHelper.CityId > 0 && filterHelper.LocalityId > 0)
            {
                query.Add("ulke", filterHelper.CountryId.ToString());
                query.Add("sehir", filterHelper.CityId.ToString());
                query.Add("ilce", ToUrl(filterHelper.LocalityName) + "-" + filterHelper.LocalityId);
            }
            else if (filterHelper.CountryId > 0 && filterHelper.CityId > 0)
            {
                query.Add("ulke", filterHelper.CountryId.ToString());
                query.Add("sehir", ToUrl(filterHelper.CityName)+ "-" + filterHelper.CityId);
            }
            else if (filterHelper.CountryId > 0)
            {
                query.Add("ulke", ToUrl(filterHelper.CountryName) + "-" + filterHelper.CountryId);
            }

            if (!string.IsNullOrEmpty(filterHelper.SearchText))
            {
                query.Add("SearchText", filterHelper.SearchText);
            }

            if (!string.IsNullOrEmpty(filterHelper.SearchType))
            {
                query.Add("SearchType", filterHelper.SearchType);
            }
            if (!string.IsNullOrEmpty(filterHelper.ViewType))
            {
                query.Add("Gorunum", filterHelper.ViewType);
            }


            uriBuilder.Query = query.ToString();

            var result = uriBuilder.ToString().Replace(":443", "");
            return result;
        }

        public static string GetHelpCategoryUrl(int categoryId,string categoryName)
        {
            string url = GetHost(HostNameType.Default) + "/" + ToUrl(categoryName) + "-y-" + categoryId;
            return url;
        }

        public static string GetStoreProfileProductCategoryUrl(int categoryId, string categoryname,string StoreUrlName)
        {
            return GetHost(HostNameType.Default) + "/" + StoreUrlName + "/" + ToUrl(categoryname) + "-c-" + categoryId;
        }

        public static string GetSerieUrl(int seriId, string seriname, string brandname,string categoryName)
        {
            string url = GetHost(HostNameType.Default) + "/" + ToUrl(brandname + "-" +  seriname + "-" + categoryName + "-s-" + seriId);
            return url;
        }

        public static string GetSerieUrl(int seriId, string seriname, string brandname, string categoryName, string searchText)
        {
            string url = GetHost(HostNameType.Default) + "/" + ToUrl(seriname + "-" + brandname + "-" + categoryName +
                        "-s-" + seriId) + "?SearchText=" + searchText;

            return url;
        }

        public static string GetModelUrl(int modelId,string modelName ,string brandname, string categoryname,int selectedCategoryId)
        {
            string url = GetHost(HostNameType.Default) + "/" + ToUrl(brandname  + "-" + modelName + "-" + categoryname +
                "-m-" + modelId + "-" + selectedCategoryId);
            return url;
        }

        public static string GetModelUrl(int modelId, string modelName, string brandname, string categoryname, int selectedCategoryId, string searchText)
        {
            string url= GetHost(HostNameType.Default) + "/" + ToUrl(brandname + "-" + modelName + "-" + categoryname +
                        "-m-" + modelId + "-" + selectedCategoryId) + "?SearchText=" + searchText;
            return url;
        }

        public static string GetProductUrl(int id, string productName)
        {

            string url = GetHost(HostNameType.Product) + "/" + ToUrl(productName + "-p-" + id);
            return url;
        }

        public static string GetStoreNewUrl(int id, string newName)
        {
            string url = GetHost(HostNameType.StoreNews) + "/" + ToUrl(newName + "-h-" + id);

            return url;
        }

        public static string GetStoreNewsUrl(int storeId, string storeName, string storeUrlName)
        {
            string url = string.Format("{0}/sirket/{1}/{2}/haberler",GetHost(HostNameType.Default), storeId, ToUrl(storeName));
            if (!string.IsNullOrEmpty(storeUrlName))
                url = string.Format("{0}/{1}/haberler", GetHost(HostNameType.Default), storeUrlName);
            return url;
        }


        public static string GetProductContactUrl(int productId,string storeName)
        {
            string url=  GetHost(HostNameType.Default) + "/Product/ProductContact?productId=" + productId + "&storeName=" + ToUrl(storeName);
            return url;
        }

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.-]+", "", RegexOptions.Compiled);
        }

        public static string GetVideoUrl(int videoId, string productName)
        {
            string url = string.Format("{0}/{1}-v-{2}",GetHost(HostNameType.Video), ToUrl(productName), videoId);
            return url;
        }

        public static string GetVideoCategoryUrl(int categoryId, string CategoryName)
        {
            var url = string.Format("{0}/{1}-videolari-vc-{2}", GetHost(HostNameType.Video), ToUrl(CategoryName), categoryId);
            return url;
        }

        public static string GetStoreProfileUrl(int storeId, string storeName, string storeUrlName)
        {
            string url = String.Format("{0}/sirket/{1}/{2}/", GetHost(HostNameType.Default), storeId, ToUrl(storeName));
            if (!string.IsNullOrWhiteSpace(storeUrlName))
            {
                url = string.Format("{0}/{1}",GetHost(HostNameType.Default), storeUrlName);
            }
            return url;
        }


        public static string GetStoreProfileProductUrl(int storeId, string storeName, string storeUrlName)
        {
            string url = string.Format("{0}/sirket/{1}/{2}/urunler", GetHost(HostNameType.Default),storeId, ToUrl(storeName));
            if (!string.IsNullOrWhiteSpace(storeUrlName))
                url = string.Format("{0}/{1}/urunler", GetHost(HostNameType.Default), storeUrlName);
            return url;

        }



        public static string GetStoreVideoUrl(int storeId, string storeName,string storeUrlName)
        {
            string url = string.Format("{0}/sirket/{1}/{2}/videolarimiz", GetHost(HostNameType.Default), storeId, ToUrl(storeName));
            if (!string.IsNullOrEmpty(storeUrlName))
              url = string.Format("{0}/{1}/videolarimiz", GetHost(HostNameType.Default),storeUrlName);
            return url;
        }

        public static string GetStoreConnectUrl(int storeId, string storeName, string storeUrlName)
        {
            string url= string.Format("{0}/sirket/{1}/{2}/iletisim", GetHost(HostNameType.Default), storeId, ToUrl(storeName));
            if (!string.IsNullOrEmpty(storeUrlName))
            {
                url= string.Format("{0}/{1}/iletisim", GetHost(HostNameType.Default), storeUrlName);
            }
            return url;

        }

        public static string GetStoreCategoryUrl(int categoryId, string categoryName, int orderBy=0)
        {
            string url = string.Empty;
            if (orderBy > 0)
                url = string.Format("{3}/{0}-sc-{1}?orderby={2}", ToUrl(categoryName), categoryId, orderBy, GetHost(HostNameType.Store));
            else
                url = string.Format("{2}/{0}-sc-{1}",ToUrl(categoryName), categoryId, GetHost(HostNameType.Store));
            return url;

        }

        public static string GetBrandUrlForStoreProfile(int storeId, string storeName,string storeUrlName)
        {
            string url = string.Format("{2}/sirket/{0}/{1}/markalarimiz", storeId, storeName, GetHost(HostNameType.Default));
            if (!string.IsNullOrEmpty(storeUrlName))
                url= string.Format("{1}/{0}/markalarimiz", storeUrlName, GetHost(HostNameType.Default));

            return url;
        }

        public static string GetProductUrlForStoreProfile(int storeId, string storeName,string storeUrlName, int categoryId = 0)
        {
            string url = string.Empty;
            if (categoryId > 0)
            {
                if (!string.IsNullOrEmpty(storeUrlName))
                    url = string.Format("{2}/{0}/urunler?CategoryId={1}", storeUrlName,categoryId, GetHost(HostNameType.Default));
                else
                    url = string.Format("{3}/sirket/{0}/{1}/urunler?CategoryId={2}", storeId, ToUrl(storeName), categoryId, GetHost(HostNameType.Default));
            }
            else
            {
                if (!string.IsNullOrEmpty(storeUrlName))
                    url = string.Format("{1}/{0}/urunler", storeUrlName, GetHost(HostNameType.Default));
                else
                    url = string.Format("{2}/sirket/{0}/{1}/urunler", storeId, ToUrl(storeName), GetHost(HostNameType.Default));
            }
            return url;
        }


        public static string GetLocalityUrl(int localityId, string localityName, int cityId, int countryId, int categoryId, string categoryName, int brandId=0, string brandName="")
        {
            if (brandId>0)
            {
                return GetHost(HostNameType.Default) + "/" + ToUrl(brandName + "-" + categoryName + "-c-" + categoryId + "-" + brandId) + "?ulke=" + countryId + "&sehir=" +
                    cityId + "&ilce=" + ToUrl(localityName) + "-" + localityId;
            }
            return GetHost(HostNameType.Default) +"/" + ToUrl(categoryName + "-c-" + categoryId) + "?ulke=" + countryId + "&sehir=" + cityId + "&ilce=" +
                ToUrl(localityName) + "-" + localityId;
        }

        public static string GetCityUrl(int cityId, string cityName, int countryId, int categoryId, string categoryName, int brandId=0, string brandName="")
        {
            if (brandId>0)
            {
                return GetHost(HostNameType.Default) + "/" + ToUrl(brandName + "-" + categoryName + "-c-" + categoryId + "-" + brandId) + "?ulke=" + countryId +
                   "&sehir=" + ToUrl(cityName) + "-" + cityId;
            }

            return GetHost(HostNameType.Default) + "/" + ToUrl(categoryName + "-c-" + categoryId) + "?ulke=" + countryId +
                   "&sehir=" + ToUrl(cityName) + "-" + cityId;
        }

        public static string GetCountryUrl(int countryId, string countryName, int categoryId, string categoryName, int brandId=0, string brandName="")
        {
            if (brandId>0)
            {
                return GetHost(HostNameType.Default) + "/" + ToUrl(brandName + "-" + categoryName + "-c-" + categoryId + "-" + brandId) + "?ulke=" + ToUrl(countryName) + "-" + countryId;
            }
            return GetHost(HostNameType.Default) + "/" + ToUrl(categoryName + "-c-" + categoryId) + "?ulke=" + ToUrl(countryName) + "-" + countryId;
        }

    }
}
