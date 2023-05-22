using Autofac;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Logs;

namespace Trinnk.Utilities.Infrastructure
{
    public class LogsDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
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
