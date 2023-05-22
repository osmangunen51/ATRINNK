using Autofac;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Checkouts;

namespace Trinnk.Utilities.Infrastructure
{
    public class CheckoutsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderInstallmentService>().As<IOrderInstallmentService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
