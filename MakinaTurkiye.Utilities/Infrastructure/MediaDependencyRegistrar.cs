using Autofac;
using Autofac.Core;
using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Media;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class MediaDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<PictureService>().As<IPictureService>()
               .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
