using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Logging.ElasticSearch
{
    internal class ElasticSearchCommonLoggerOptionsSetup : ConfigureFromConfigurationOptions<ElasticSearchCommonLoggerOptions>
    {
        public ElasticSearchCommonLoggerOptionsSetup(ILoggerProviderConfiguration<ElasticSearchCommonLoggerProvider> providerConfiguration)
          : base(providerConfiguration.Configuration)
        {

        }
    }
}
