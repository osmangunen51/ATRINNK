using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using MakinaTurkiye.Caching;
using MakinaTurkiye.Caching.Redis;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Core.Fakes;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Data;
using MakinaTurkiye.Logging;
using MakinaTurkiye.Services.ProductRequests;
using MakinaTurkiye.Services.Search;
using MakinaTurkiye.Services.SearchEngine;
using System.Linq;
using System.Web;

namespace MakinaTurkiye.Utilities
{
    public class CoreDependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()

                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            builder.Register(x => new EfDataProviderManager()).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();
       

            builder.Register<IDbContext>(c => new MakinaTurkiyeObjectContext(config.EntityFrameworkLazyLoadingEnabled,
                                        config.EntityFrameworkProxyCreationEnabled, config.EntityFrameworkAutoDetectChangesEnabled,
                                        config.EntityFrameworkValidateOnSaveEnabled)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
           

            //cache manager

            if(!config.CachingEnabled)
            {
                builder.RegisterType<MakinaTurkiyeNullCache>().As<ICacheManager>().InstancePerLifetimeScope();
            }
            else
            {
                if (config.RedisCachingEnabled)
                {
                    builder.RegisterType<RedisConnectionWrapper>().As<IRedisConnectionWrapper>()
                        .WithParameter("cachingConnectionString", config.RedisCachingConnectionString).SingleInstance();

                    builder.RegisterType<RedisCacheManager>().As<ICacheManager>()
                        .WithProperty("AllOperationEnabled", config.CachingAllOperationEnabled)
                        .WithProperty("GetOperationEnabled", config.CachingGetOperationEnabled)
                        .WithProperty("SetOperationEnabled", config.CachingSetOperationEnabled)
                        .WithProperty("RemoveOperationEnabled", config.CachingRemoveOperationEnabled)
                        .Named<ICacheManager>("cache_static").InstancePerLifetimeScope();
                }
                else
                {
                    builder.RegisterType<MemoryCacheManager>().As<ICacheManager>()
                        .WithProperty("AllOperationEnabled", config.CachingAllOperationEnabled)
                        .WithProperty("GetOperationEnabled", config.CachingGetOperationEnabled)
                        .WithProperty("SetOperationEnabled", config.CachingSetOperationEnabled)
                        .WithProperty("RemoveOperationEnabled", config.CachingRemoveOperationEnabled)
                        .Named<ICacheManager>("cache_static").SingleInstance();
                }
                builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>()
                        .WithProperty("AllOperationEnabled", config.CachingAllOperationEnabled)
                        .WithProperty("GetOperationEnabled", config.CachingGetOperationEnabled)
                        .WithProperty("SetOperationEnabled", config.CachingSetOperationEnabled)
                        .WithProperty("RemoveOperationEnabled", config.CachingRemoveOperationEnabled)
                        .Named<ICacheManager>("cache_per_request").InstancePerLifetimeScope();
            }

            builder.RegisterType<SearchService>().As<ISearchService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<ProductRequestService>().As<IProductRequestService>().InstancePerLifetimeScope();

            builder.RegisterType<IncrementalProductService>().As<IIncrementalProductService>().InstancePerLifetimeScope();
            builder.RegisterType<SearchEngineService>().As<ISearchEngineService>().InstancePerLifetimeScope();
            builder.RegisterType<SearchScoreService>().As<ISearchScoreService>().InstancePerLifetimeScope();

            builder.RegisterType<SerilogElasticSearchLogger>().As<ILogger>().InstancePerLifetimeScope();

        }
        public int Order
        {
            get { return 1; }
        }
    }
}
