using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Logs;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class LogsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<SearchEngineLogService>().As<ISearchEngineLogService>().InstancePerLifetimeScope();
            builder.RegisterType<LoginLogService>().As<ILoginLogService>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationLogService>().As<IApplicationLogService>().InstancePerLifetimeScope();
            builder.RegisterType<CreditCardLogService>().As<ICreditCardLogService>().InstancePerLifetimeScope();
            builder.RegisterType<UserLogService>().As<IUserLogService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
