using Autofac;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Bulletins;

namespace Trinnk.Utilities.Infrastructure
{
    public class BulletinsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {
            builder.RegisterType<BulletinService>().As<IBulletinService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
