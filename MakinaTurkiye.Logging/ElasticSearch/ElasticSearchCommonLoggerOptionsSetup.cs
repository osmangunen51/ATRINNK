using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace MakinaTurkiye.Logging.ElasticSearch
{
    internal class ElasticSearchCommonLoggerOptionsSetup : ConfigureFromConfigurationOptions<ElasticSearchCommonLoggerOptions>
    {
        public ElasticSearchCommonLoggerOptionsSetup(ILoggerProviderConfiguration<ElasticSearchCommonLoggerProvider> providerConfiguration)
          : base(providerConfiguration.Configuration)
        {

        }
    }
}
