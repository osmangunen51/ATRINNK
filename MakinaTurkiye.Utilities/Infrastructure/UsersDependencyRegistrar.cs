using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Users;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class UsersDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<HelpService>().As<IHelpService>().InstancePerLifetimeScope();
            builder.RegisterType<UserInformationService>().As<IUserInformationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserFileService>().As<IUserFileService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
