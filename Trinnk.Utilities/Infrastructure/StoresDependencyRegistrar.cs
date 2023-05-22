using Autofac;
using Autofac.Core;
using Trinnk.Caching;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Stores;

namespace Trinnk.Utilities.Infrastructure
{
    public class StoresDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {

            builder.RegisterType<StoreService>().As<IStoreService>()
             .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<StoreActivityTypeService>().As<IStoreActivityTypeService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<StoreInfoNumberShowService>().As<IStoreInfoNumberShowService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<StoreCatologFileService>().As<IStoreCatologFileService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<StoreNewService>().As<IStoreNewService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<DealarBrandService>().As<IDealarBrandService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<StoreDealerService>().As<IStoreDealerService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<StoreBrandService>().As<IStoreBrandService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<FavoriteStoreService>().As<IFavoriteStoreService>()
             .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();


            builder.RegisterType<StoreChangeHistoryService>().As<IStoreChangeHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ActivityTypeService>().As<IActivityTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<WhatsappLogService>().As<IWhatsappLogService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreStatisticService>().As<IStoreStatisticService>().InstancePerLifetimeScope();
            builder.RegisterType<PreRegistirationStoreService>().As<IPreRegistirationStoreService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreActivityCategoryService>().As<IStoreActivityCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreSectorService>().As<IStoreSectorService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreDiscountService>().As<IStoreDiscountService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreSeoNotificationService>().As<IStoreSeoNotificationService>().InstancePerLifetimeScope();
            builder.RegisterType<StorePackagePurchaseRequestService>().As<IStorePackagePurchaseRequestService>().InstancePerLifetimeScope();

        }

        public int Order => 2;
    }
}
