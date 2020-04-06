using System;
using System.Configuration;
using System.Xml;

namespace MakinaTurkiye.Core.Configuration
{
    /// <summary>
    /// Represents a NopConfig
    /// </summary>
    public partial class MakinaTurkiyeConfig : IConfigurationSectionHandler
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
            var config = new MakinaTurkiyeConfig();


            var redisCachingNode = section.SelectSingleNode("RedisCaching");
            config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            var cachingNode = section.SelectSingleNode("Caching");
            config.CachingEnabled = GetBool(cachingNode, "Enabled");
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

        public bool CachingAllOperationEnabled { get; set; }

        public bool CachingSetOperationEnabled { get; set; }

        public bool CachingGetOperationEnabled { get; set; }

        public bool CachingRemoveOperationEnabled { get; set; }

        public bool EntityFrameworkLazyLoadingEnabled { get; set; }

        public bool EntityFrameworkProxyCreationEnabled { get; set; }

        public bool EntityFrameworkAutoDetectChangesEnabled { get; set; }

        public bool EntityFrameworkValidateOnSaveEnabled { get; set; }

        public bool CompressEnabled { get; set; }

    }

}
