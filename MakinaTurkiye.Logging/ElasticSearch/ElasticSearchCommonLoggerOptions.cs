using System;

namespace MakinaTurkiye.Logging.ElasticSearch
{
    public class ElasticSearchCommonLoggerOptions
    {
        public Uri ElasticsearchEndpoint { get; set; }
        public string IndexName { get; set; } = "application.logging";
    }
}
