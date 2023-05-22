﻿using System;

namespace Trinnk.Logging.ElasticSearch
{
    public static class ElasticSearchCommonLoggingFactoryExtensions
    {

        public static Microsoft.Extensions.Logging.ILoggerFactory AddElasticSearch(this Microsoft.Extensions.Logging.ILoggerFactory factory, Uri endpoint)
        {
            return AddElasticSearch(factory, new ElasticSearchCommonLoggerOptions()
            {
                ElasticsearchEndpoint = endpoint,
                IndexName = "application.logging"
            });
        }
        public static Microsoft.Extensions.Logging.ILoggerFactory AddElasticSearch(this Microsoft.Extensions.Logging.ILoggerFactory factory, Uri endpoint, string indexPrefix)
        {
            return AddElasticSearch(factory, new ElasticSearchCommonLoggerOptions()
            {
                ElasticsearchEndpoint = endpoint,
                IndexName = indexPrefix
            });
        }

        public static Microsoft.Extensions.Logging.ILoggerFactory AddElasticSearch(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, ElasticSearchCommonLoggerOptions options)
        {
            loggerFactory.AddProvider(new ElasticSearchCommonLoggerProvider(options));
            return loggerFactory;
        }
    }
}
