﻿using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Packets;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class PacketsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<PacketService>().As<IPacketService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
