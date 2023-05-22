using Autofac;
using Autofac.Core;
using Trinnk.Caching;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Core.Infrastructure.DependencyManagement;
using Trinnk.Services.Messages;

namespace Trinnk.Utilities.Infrastructure
{
    public class MessagesDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TrinnkConfig config)
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
