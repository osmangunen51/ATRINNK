using Autofac;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Payments;

namespace Trinnk.Utilities.Infrastructure
{
    public class PaymentsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {
            builder.RegisterType<BankAccountService>().As<IBankAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<CreditCardService>().As<ICreditCardService>().InstancePerLifetimeScope();
            builder.RegisterType<VirtualPosService>().As<IVirtualPosService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
