using Autofac;
using Autofac.Core;
using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class MembersDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
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
