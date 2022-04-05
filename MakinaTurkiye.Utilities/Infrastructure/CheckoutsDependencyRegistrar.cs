using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Checkouts;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class CheckoutsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderInstallmentService>().As<IOrderInstallmentService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
