using Autofac;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Authentication;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class AuthenticationDependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
