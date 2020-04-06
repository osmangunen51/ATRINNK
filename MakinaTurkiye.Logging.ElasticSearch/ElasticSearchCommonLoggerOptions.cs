using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Logging.ElasticSearch
{
    public class ElasticSearchCommonLoggerOptions
    {
        public Uri ElasticsearchEndpoint { get; set; }
        public string IndexName { get; set; } = "application.logging";
    }
}
