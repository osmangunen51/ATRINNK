using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Payments;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class PaymentsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<BankAccountService>().As<IBankAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<CreditCardService>().As<ICreditCardService>().InstancePerLifetimeScope();
            builder.RegisterType<VirtualPosService>().As<IVirtualPosService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
