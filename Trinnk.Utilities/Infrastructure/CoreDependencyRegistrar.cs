using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Trinnk.Caching;
using Trinnk.Caching.Redis;
using Trinnk.Core.Configuration;
using Trinnk.Core.Data;
using Trinnk.Core.Fakes;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Data;
using Trinnk.Logging;
using Trinnk.Services.CallCenter;
using Trinnk.Services.ProductRequests;
using Trinnk.Services.Search;
using Trinnk.Services.SearchEngine;
using System.Linq;
using System.Web;

namespace Trinnk.Utilities
{
    public class CoreDependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
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


            builder.Register<IDbContext>(c => new TrinnkObjectContext(config.EntityFrameworkLazyLoadingEnabled,
                                        config.EntityFrameworkProxyCreationEnabled, config.EntityFrameworkAutoDetectChangesEnabled,
                                        config.EntityFrameworkValidateOnSaveEnabled)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();



            //cache manager

            if (!config.CachingEnabled)
            {
                builder.RegisterType<TrinnkNullCache>().As<ICacheManager>().InstancePerLifetimeScope();
            }
            else
            {
                if (config.RedisCachingEnabled)
                {
                    builder.RegisterType<RedisConnectionWrapper>().As<IRedisConnectionWrapper>()
                        .WithParameter("cachingConnectionString", config.RedisCachingConnectionString).SingleInstance();

                    builder.RegisterType<RedisCacheManager>().As<ICacheManager>()
                        .WithProperty("KeyFrefix", config.KeyFrefix)
                        .WithProperty("AllOperationEnabled", config.CachingAllOperationEnabled)
                        .WithProperty("GetOperationEnabled", config.CachingGetOperationEnabled)
                        .WithProperty("SetOperationEnabled", config.CachingSetOperationEnabled)
                        .WithProperty("RemoveOperationEnabled", config.CachingRemoveOperationEnabled)
                        .Named<ICacheManager>("cache_static").InstancePerLifetimeScope();
                }
                else
                {
                    builder.RegisterType<MemoryCacheManager>().As<ICacheManager>()
                         .WithProperty("KeyFrefix", config.KeyFrefix)
                        .WithProperty("AllOperationEnabled", config.CachingAllOperationEnabled)
                        .WithProperty("GetOperationEnabled", config.CachingGetOperationEnabled)
                        .WithProperty("SetOperationEnabled", config.CachingSetOperationEnabled)
                        .WithProperty("RemoveOperationEnabled", config.CachingRemoveOperationEnabled)
                        .Named<ICacheManager>("cache_static").SingleInstance();
                }
                builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>()
                        .WithProperty("KeyFrefix", config.KeyFrefix)
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

            builder.RegisterType<CallCenterService>().As<ICallCenterService>().InstancePerLifetimeScope();

        }
        public int Order
        {
            get { return 1; }
        }
    }
}
