using Autofac;
using Autofac.Core;
using Trinnk.Caching;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Common;

namespace Trinnk.Utilities.Infrastructure
{
    public class CommonDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {

            builder.RegisterType<AddressService>().As<IAddressService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<BannerService>().As<IBannerService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<PhoneService>().As<IPhoneService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<ConstantService>().As<IConstantService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<PhoneChangeHistoryService>().As<IPhoneChangeHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<AddressChangeHistoryService>().As<IAddressChangeHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            builder.RegisterType<UrlRedirectService>().As<IUrlRedirectService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
