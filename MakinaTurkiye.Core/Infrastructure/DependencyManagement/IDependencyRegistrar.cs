using Autofac;
using MakinaTurkiye.Core.Configuration;

namespace MakinaTurkiye.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config);

        int Order { get; }
    }
}
