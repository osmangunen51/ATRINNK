using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.MailHelpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Messages.Tasks
{
    public class StoreWeeklyStatisticMailSend : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {

            IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();
            IProductStatisticService productStatisticService = EngineContext.Current.Resolve<IProductStatisticService>();
            IMemberStoreService memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            IMessagesMTService messageMtService = EngineContext.Current.Resolve<IMessagesMTService>();
            IMemberService memberService = EngineContext.Current.Resolve<IMemberService>();
            IAutoMailRecordService autoMailRecordService = EngineContext.Current.Resolve<IAutoMailRecordService>();

            short constantMailId = 65;
            DateTime now = DateTime.Now;
            DateTime oneWeekAgo = DateTime.Now.AddDays(-7);

            var stores = storeService.SP_GetStoresForAutoMail(29, constantMailId, 10, 0);
            foreach (var item in stores)
            {
                try
                {
                    var memberStore = memberStoreService.GetMemberStoreByStoreMainPartyId(item.MainPartyId);
                    int memberMainPartyId = memberStore.MemberMainPartyId.Value;
                    var member = memberService.GetMemberByMainPartyId(memberMainPartyId);
                    var productStatistics = productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(memberMainPartyId, oneWeekAgo, now, true).ToList();
                    int totalViewCount = productStatistics.Select(x => (int)x.ViewCount).Sum();
                    int totalSingularCount = productStatistics.Select(x => (int)x.SingularViewCount).Sum();

                    var messageMail = messageMtService.GetMessagesMTByMessageMTName("urunistatistik");

                    string mailContent = messageMail.MessagesMTPropertie.Replace("#enddate#", now.ToString("dd/MM/yyyy")).Replace("#begindate#", oneWeekAgo.ToString("dd/MM/yyyy"))
                        .Replace("#singularviewcount#", totalSingularCount.ToString())
                        .Replace("#viewcount#", totalViewCount.ToString())
                        .Replace("#uyeadisoyadi#", member.MemberEmail);

                    List<string> mails = new List<string>();
                    mails.Add("mustafabas2189@gmail.com");
                    mails.Add("osmanhaciosmanoglu@gmail.com");

                    MailHelper mailHelper = new MailHelper(messageMail.MessagesMTTitle, mailContent,
                        messageMail.Mail, mails, messageMail.MailPassword, messageMail.MailSendFromName, AppSettings.MailHost, AppSettings.MailPort, AppSettings.MailSsl);

                    mailHelper.Send();

                    var autoRecordmail = autoMailRecordService.GetAutoMailRecordServicesByStoreMainPartyId(item.MainPartyId, constantMailId);
                    if (autoRecordmail != null)
                    {
                        autoRecordmail.CreatedDate = DateTime.Now;
                        autoMailRecordService.UpdateAutoMailRecord(autoRecordmail);
                    }
                    else
                    {
                        var autoRecordMail = new AutoMailRecord
                        {
                            MessagesMTId = constantMailId,
                            CreatedDate = DateTime.Now.AddMinutes(-1),
                            StoreMainPartyId = item.MainPartyId
                        };
                        autoMailRecordService.InsertAutoMailRecord(autoRecordMail);
                    }
                }
                catch (Exception ex)
                {
                    //log.Error(ex.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}
