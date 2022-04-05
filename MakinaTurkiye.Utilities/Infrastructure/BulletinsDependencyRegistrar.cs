using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Bulletins;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class BulletinsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<BulletinService>().As<IBulletinService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
