namespace NeoSistem.Trinnk.Cache
{
    using NeoSistem.EnterpriseEntity.DataCache;
    using NeoSistem.Trinnk.BaseModule.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Xml.Linq;

    public class CacheUtilities
    {
        public static TrinnkEntities entities = new TrinnkEntities();
        private struct CacheNameCollection
        {
            public const string Categories = "MainCategories";
            public const string CategoryFullRows = "CategoryFullRows";
            public const string Currency = "Currency";

            public const string SectorItems = "SectorItems";
            public const string ConstantItems = "ConstantItems";
            public const string ProductGroupItems = "ProductGroupItems";
            public const string CityItems = "CityItems";
            public const string CountryItems = "CountryItems";
            public const string ExchangeRatesItems = "ExchangeRatesItems";
            public const string ProductGroupParentItems = "ProductGroupParentItems";
            public const string SectorTop10Items = "SectorTop10Items";
            public const string ProductGroupNames = "ProductGroupNames";
            public const string SeoItems = "SeoItems";
        }

        //select distinct CategoryName from Category where CategoryType = 6
        public static void ClearAllCache()
        {
            DataCaching.ClearCache();
        }

        public static XDocument ExchangeRatesItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.ExchangeRatesItems))
            {
                var excRateItems = XDocument.Load("http://www.tcmb.gov.tr/kurlar/today.xml");
                DataCaching.AddCache(CacheNameCollection.ExchangeRatesItems, excRateItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<XDocument>(CacheNameCollection.ExchangeRatesItems);
        }

        public static IEnumerable<string> ProductGroupNames()
        {
            if (!DataCaching.IsCache(CacheNameCollection.ProductGroupNames))
            {
                var names = (from c in entities.Categories where c.CategoryType == (byte)CategoryType.ProductGroup && c.CategoryParentId != null select c.CategoryName).Distinct();
                DataCaching.AddCache(CacheNameCollection.ProductGroupNames, names, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<string>>(CacheNameCollection.ProductGroupNames);
        }

        public static IEnumerable<Constant> ConstantItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.ConstantItems))
            {
                var constantItems = entities.Constants.AsEnumerable().ToList();
                DataCaching.AddCache(CacheNameCollection.ConstantItems, constantItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Constant>>(CacheNameCollection.ConstantItems);
        }

        public static IEnumerable<City> GetCityItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.CityItems))
            {
                var cityItems = entities.Cities.AsEnumerable().ToList();
                cityItems.Insert(0, new City { CityId = 0, CityName = "< Şehir Seçiniz >" });
                DataCaching.AddCache(CacheNameCollection.CityItems, cityItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<City>>(CacheNameCollection.CityItems);
        }

        public static IEnumerable<Country> GetCountryItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.CountryItems))
            {
                var countryItems = entities.Countries.AsEnumerable();
                DataCaching.AddCache(CacheNameCollection.CountryItems, countryItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Country>>(CacheNameCollection.CountryItems);
        }

        public static IEnumerable<Currency> CurrencyItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.Currency))
            {
                var currencyItems = entities.Currencies.AsEnumerable();
                DataCaching.AddCache(CacheNameCollection.Currency, currencyItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Currency>>(CacheNameCollection.Currency);
        }

        public static IEnumerable<Category> SectorItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.SectorItems))
            {
                var sectors = entities.Categories.Where(c => c.CategoryParentId == null).OrderBy(c => c.CategoryOrder).OrderBy(c => c.CategoryName);
                DataCaching.AddCache(CacheNameCollection.SectorItems, sectors, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Category>>(CacheNameCollection.SectorItems);
        }

        public static IEnumerable<Category> ProductGroupItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.ProductGroupItems))
            {
                var productGroupItems = entities.Categories.Where(c => c.CategoryType == (byte)CategoryType.ProductGroup).AsEnumerable();
                DataCaching.AddCache(CacheNameCollection.ProductGroupItems, productGroupItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Category>>(CacheNameCollection.ProductGroupItems);
        }

        public static IEnumerable<Category> ProductGroupParentItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.ProductGroupParentItems))
            {
                var ids = (from c in entities.Categories where c.CategoryType == (byte)CategoryType.ProductGroup select c.CategoryId);
                var parentItems = from c in entities.Categories where ids.Contains(c.CategoryParentId.Value) select c;
                DataCaching.AddCache(CacheNameCollection.ProductGroupParentItems, parentItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Category>>(CacheNameCollection.ProductGroupParentItems);
        }

        public static IEnumerable<Category> SectorTop10Items()
        {
            if (!DataCaching.IsCache(CacheNameCollection.SectorTop10Items))
            {
                var rnd = new Random();
                int isf = rnd.Next(1, 9);
                var parentItems = entities.Categories.Where(c => c.CategoryParentId == null).Take(10).OrderBy(c => isf).AsEnumerable();
                DataCaching.AddCache(CacheNameCollection.SectorTop10Items, parentItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Category>>(CacheNameCollection.SectorTop10Items);
        }

        public static IEnumerable<Seo> SeoItems()
        {
            if (!DataCaching.IsCache(CacheNameCollection.SeoItems))
            {
                var seoItems = entities.Seos.AsEnumerable();
                DataCaching.AddCache(CacheNameCollection.SeoItems, seoItems, TimeOfLayer.Day, 1);
            }
            return DataCaching.GetCache<IEnumerable<Seo>>(CacheNameCollection.SeoItems);
        }

        public static DataTable GetCategories()
        {
            var dataCategory = new Data.Category();
            var categoryItems = dataCategory.GetCategorySectorAndProductGroup();
            return categoryItems;
        }

        public static DataTable CategoryAllRows()
        {
            if (DataCaching.IsCache(CacheNameCollection.CategoryFullRows))
            {
                return DataCaching.GetCache<DataTable>(CacheNameCollection.CategoryFullRows);
            }
            else
            {
                var curCategory = new Classes.Category();
                var dtCategoryItems = curCategory.GetDataTable();
                DataCaching.AddCache(CacheNameCollection.CategoryFullRows, dtCategoryItems, TimeOfLayer.Day, 1);
                return dtCategoryItems;
            }
        }

        private enum CategoryType : byte
        {
            Brand = 3,
            Series = 4,
            Model = 5,
            ProductGroup = 6
        }

    }
}