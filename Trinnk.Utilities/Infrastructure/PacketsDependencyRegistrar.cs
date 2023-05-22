using Autofac;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Packets;

namespace Trinnk.Utilities.Infrastructure
{
    public class PacketsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {
            builder.RegisterType<PacketService>().As<IPacketService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
