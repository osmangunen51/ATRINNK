using Autofac;
using Autofac.Core;
using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Core.Infrastructure.DependencyManagement;
using MakinaTurkiye.Services.Messages;

namespace MakinaTurkiye.Utilities.Infrastructure
{
    public class MessagesDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MakinaTurkiyeConfig config)
        {
            builder.RegisterType<MobileMessageService>().As<IMobileMessageService>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static")).InstancePerLifetimeScope();

            builder.RegisterType<MessagesMTService>().As<IMessagesMTService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoMailRecordService>().As<IAutoMailRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<UserMailTemplateService>().As<IUserMailTemplateService>().InstancePerLifetimeScope();

        }

        public int Order => 2;
    }
}
