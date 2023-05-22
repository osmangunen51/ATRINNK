﻿using System;
using System.Configuration;
using System.Xml;

namespace Trinnk.Core.Configuration
{
    /// <summary>
    /// Represents a NopConfig
    /// </summary>
    public partial class TrinnkConfig : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new TrinnkConfig();


            var redisCachingNode = section.SelectSingleNode("RedisCaching");
            config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            var cachingNode = section.SelectSingleNode("Caching");
            config.CachingEnabled = GetBool(cachingNode, "Enabled");
            config.KeyFrefix = GetString(cachingNode, "KeyFrefix");
            config.CachingAllOperationEnabled = GetBool(cachingNode, "AllOperationEnabled");
            config.CachingGetOperationEnabled = GetBool(cachingNode, "GetOperationEnabled");
            config.CachingSetOperationEnabled = GetBool(cachingNode, "SetOperationEnabled");
            config.CachingRemoveOperationEnabled = GetBool(cachingNode, "RemoveOperationEnabled");


            var applicationNode = section.SelectSingleNode("Application");
            config.ApplicationTestModeEnabled = GetBool(applicationNode, "TestModeEnabled");
            config.ApplicationStartingTasksEnabled = GetBool(applicationNode, "StartingTasksEnabled");
            config.ApplicationLogEnabled = GetBool(applicationNode, "LogEnabled");

            var entityFrameworkNode = section.SelectSingleNode("EntityFramework");
            config.EntityFrameworkLazyLoadingEnabled = GetBool(entityFrameworkNode, "LazyLoadingEnabled");
            config.EntityFrameworkProxyCreationEnabled = GetBool(entityFrameworkNode, "ProxyCreationEnabled");
            config.EntityFrameworkAutoDetectChangesEnabled = GetBool(entityFrameworkNode, "AutoDetectChangesEnabled");
            config.EntityFrameworkValidateOnSaveEnabled = GetBool(entityFrameworkNode, "ValidateOnSaveEnabled");

            var compressNode = section.SelectSingleNode("Compress");
            config.CompressEnabled = GetBool(compressNode, "Enabled");


            var productNode = section.SelectSingleNode("Product");
            config.ProductDescriptionHtmlClear = GetBool(productNode, "DescriptionHtmlClear");


            var PinterestNode = section.SelectSingleNode("Pinterest");
            config.PinDurum = GetBool(PinterestNode, "Durum");
            config.PinBaseUrl = GetString(PinterestNode, "BaseUrl");
            config.PinBasePano = GetString(PinterestNode, "BasePano");
            config.PinUsername = GetString(PinterestNode, "Username");
            config.PinPassword = GetString(PinterestNode, "Password");
            config.PinZaman = GetString(PinterestNode, "Zaman");
            config.PinZamanlamaPinAdet = GetString(PinterestNode, "ZamanlamaPinAdet");
            config.ProxyServerListesi = GetString(PinterestNode, "ProxyServerListesi");

            var ElasticSearchNode = section.SelectSingleNode("ElasticSearch");
            config.ElasticSearchUrl = GetString(ElasticSearchNode, "BaseUrl");
            config.ElasticSearchEnabled = GetBool(ElasticSearchNode, "Durum");

            return config;
        }

        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }


        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node == null || node.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        /// <summary>
        /// Indicates whether we should use Redis server for caching (instead of default in-memory caching)
        /// </summary>
        public bool RedisCachingEnabled { get; private set; }

        /// <summary>
        /// Redis connection string. Used when Redis caching is enabled
        /// </summary>
        public string RedisCachingConnectionString { get; private set; }

        public bool ApplicationTestModeEnabled { get; set; }

        public bool ApplicationStartingTasksEnabled { get; set; }

        public bool ApplicationLogEnabled { get; set; }

        public string LoggerProviderName { get; set; }


        public bool CachingEnabled { get; set; }
        public string KeyFrefix { get; set; }
        public bool CachingAllOperationEnabled { get; set; }

        public bool CachingSetOperationEnabled { get; set; }

        public bool CachingGetOperationEnabled { get; set; }

        public bool CachingRemoveOperationEnabled { get; set; }

        public bool EntityFrameworkLazyLoadingEnabled { get; set; }

        public bool EntityFrameworkProxyCreationEnabled { get; set; }

        public bool EntityFrameworkAutoDetectChangesEnabled { get; set; }

        public bool EntityFrameworkValidateOnSaveEnabled { get; set; }

        public bool CompressEnabled { get; set; }
        public bool ProductDescriptionHtmlClear { get; set; }

        public bool PinDurum { get; set; } = true;
        public string PinBaseUrl { get; set; } = "https://pinterest.com";
        public string PinBasePano { get; set; } = "";
        public string PinUsername { get; set; } = "";
        public string PinPassword { get; set; } = "";
        public string PinZamanlamaPinAdet { get; set; } = "5";
        public string PinZaman { get; set; } = "10";
        public string ProxyServerListesi { get; set; } = "";

        public bool ElasticSearchEnabled { get; set; }
        public string ElasticSearchUrl { get; set; } = "http://localhost:9200";

    }

}
