using Autofac;
using Autofac.Core;
using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class CatalogDependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {

            builder.RegisterType<ProductComplainService>().As<IProductComplainService>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<FavoriteProductService>().As<IFavoriteProductService>()
              .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();


            builder.RegisterType<CategoryPropertieService>().As<ICategoryPropertieService>()
               .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<ProductCatologService>().As<IProductCatologService>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<ProductCommentService>().As<IProductCommentService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<ProductHomePageService>().As<IProductHomePageService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<CategoryPlaceChoiceService>().As<ICategoryPlaceChoiceService>()
               .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();


            builder.RegisterType<ProductStatisticService>().As<IProductStatisticService>().InstancePerLifetimeScope();
            builder.RegisterType<SiteMapCategoryService>().As<ISiteMapCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DeletedProductRedirectService>().As<IDeletedProductRedirectService>().InstancePerLifetimeScope();
            builder.RegisterType<CertificateTypeService>().As<ICertificateTypeService>()
        .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

        }

        public int Order => 2;
    }
}
