using Autofac;
using Trinnk.Core.Configuration;

namespace Trinnk.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config);

        int Order { get; }
    }
}
