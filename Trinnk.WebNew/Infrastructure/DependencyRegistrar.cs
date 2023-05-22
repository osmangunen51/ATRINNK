using Autofac;
using Autofac.Core;
using Trinnk.Caching;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using NeoSistem.Trinnk.Web.Controllers;
using NeoSistem.Trinnk.Web.Factories;

namespace Trinnk.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {

            //we cache presentation models between requests
            builder.RegisterType<VideosController>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static"));

            builder.RegisterType<ProductModelFactory>().As<IProductModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SalesDocumentInputFactory>().As<ISalesDocumentInputFactory>().InstancePerLifetimeScope();
            //builder.RegisterType<CatalogController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<CountryController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<CommonController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<NewsController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<PollController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<ProductController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<ShoppingCartController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<TopicController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<WidgetController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")); 
        }

        public int Order
        {
            get { return 1; }
        }
    }
}