using Autofac;
using Autofac.Core;
using Trinnk.Caching;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Members;

namespace Trinnk.Utilities.Infrastructure
{
    public class MembersDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
        {
            builder.RegisterType<MemberService>().As<IMemberService>()
               .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<MemberStoreService>().As<IMemberStoreService>()
              .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();


            builder.RegisterType<MemberDescriptionService>().As<IMemberDescriptionService>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyDemandMembershipService>().As<ICompanyDemandMembershipService>().InstancePerLifetimeScope();

        }

        public int Order => 2;
    }
}
