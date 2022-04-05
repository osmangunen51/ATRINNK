using Autofac;
using Autofac.Core;
using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Seos;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class SeosDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<SeoDefinitionService>().As<ISeoDefinitionService>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
