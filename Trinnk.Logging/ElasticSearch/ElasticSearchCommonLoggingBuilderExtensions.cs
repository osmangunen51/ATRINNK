using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Trinnk.Logging.ElasticSearch
{
    public static class ElasticSearchCommonLoggingBuilderExtensions
    {
        private static ILoggingBuilder AddElasticSearch(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<Microsoft.Extensions.Logging.ILoggerProvider, ElasticSearchCommonLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ElasticSearchCommonLoggerOptions>, ElasticSearchCommonLoggerOptionsSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<ElasticSearchCommonLoggerOptions>, LoggerProviderOptionsChangeTokenSource<ElasticSearchCommonLoggerOptions, ElasticSearchCommonLoggerProvider>>());
            return builder;
        }

        public static ILoggingBuilder AddElasticSearch(this ILoggingBuilder builder, Action<ElasticSearchCommonLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddElasticSearch();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
