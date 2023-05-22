using Trinnk.Core;
using NeoSistem.Trinnk.Management.Models.Entities;
using Quartz;
using System;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NeoSistem.Trinnk.Management.Jobs
{
    public class PacketReminderMailSendJob : IJob
    {
        //private readonly IStoreService _storeService;

        //public PacketReminderMailSendJob(IStoreService storeService)
        //{
        //    this._storeService = storeService;
        //}

        public Task Execute(IJobExecutionContext context)
        {
            var entities = new TrinnkEntities();

            var storeList = (from s in entities.Stores where EntityFunctions.TruncateTime(s.StorePacketEndDate) >= EntityFunctions.AddMonths(DateTime.Now, -1) && EntityFunctions.TruncateTime(s.StorePacketEndDate) <= EntityFunctions.AddMonths(DateTime.Now, 1) select s).ToList();

            MessagesMT mailTemplate = new MessagesMT();
            foreach (var item in storeList)
            {
                if (item.Packet.SendReminderMail)
                {
                    DateTime storePacketEndDate = (DateTime)item.StorePacketEndDate.Value.Date;
                    DateTime pre15Days = (DateTime)DateTime.Now.Date.AddDays(+15);
                    DateTime pre7Days = (DateTime)DateTime.Now.Date.AddDays(+7);
                    DateTime pre3Days = (DateTime)DateTime.Now.Date.AddDays(+3);
                    DateTime pre1Days = (DateTime)DateTime.Now.Date.AddDays(+1);
                    DateTime onDays = (DateTime)DateTime.Now.Date;
                    DateTime next1Week = DateTime.Now.Date.AddDays(-7);
                    DateTime next2Week = DateTime.Now.Date.AddDays(-14);
                    DateTime next3Week = DateTime.Now.Date.AddDays(-21);
                    var preMonth = DateTime.Now.AddMonths(-1).Date;


                    if (DateTime.Compare(storePacketEndDate, preMonth) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme-30gün");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme -30 gün";
                    }
                    else if (DateTime.Compare(storePacketEndDate, pre15Days) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme-15gün");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme -15 gün";

                    }
                    else if (DateTime.Compare(storePacketEndDate, pre7Days) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme-7gün");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme -7 gün";

                    }
                    else if (DateTime.Compare(storePacketEndDate, pre3Days) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme-3gün");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme -3 gün";
                    }
                    else if (DateTime.Compare(storePacketEndDate, pre1Days) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme-1gün");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme -1 gün";
                    }
                    else if (DateTime.Compare(storePacketEndDate, onDays) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme(Bitiş)";
                    }
                    else if (DateTime.Compare(storePacketEndDate, next1Week) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme+1hafta");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme +1 hafta";
                    }
                    else if (DateTime.Compare(storePacketEndDate, next2Week) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme+2hafta");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme +2 hafta";
                    }
                    else if (DateTime.Compare(storePacketEndDate, next3Week) == 0)
                    {
                        mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "firmapaketyenileme+3hafta");
                        mailTemplate.MessagesMTTitle = "Firma Paket Yenileme +3 hafta";

                    }

                    if (mailTemplate.MessagesMTId > 0)
                    {
                        #region emailicin
                        var memberStore = entities.MemberStores.First(x => x.StoreMainPartyId == item.MainPartyId);
                        var member = entities.Members.First(x => x.MainPartyId == memberStore.MemberMainPartyId);
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(item.StoreEMail);                                                              //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = mailTemplate.MessagesMTTitle;                                              //Mail konusu
                        string template = mailTemplate.MessagesMTPropertie;
                        template = template.Replace("#uyeadi#", member.MemberName + " " + member.MemberSurname);
                        if (template.IndexOf("#bitistarih#") > 0)
                            template = template.Replace("#bitistarih#", item.StorePacketEndDate.ToString());
                        var packet = entities.Packets.First(x => x.PacketId == item.PacketId);
                        template = template.Replace("#paketadi#", packet.PacketName);
                        mail.Body = template;                                                            //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                        sc.Port = AppSettings.MailPort;                                                                   //Gmail için geçerli Portu bildiriyoruz
                        sc.Host = AppSettings.MailHost;                                                      //Gmailin smtp host adresini belirttik
                        sc.EnableSsl = AppSettings.MailSsl;                                                             //SSL’i etkinleştirdik
                        sc.Credentials = new NetworkCredential(AppSettings.MailUserName, AppSettings.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                        sc.Send(mail);
                        #endregion

                        using (var entities1 = new TrinnkEntities())
                        {
                            int memberMainPartyId = Convert.ToInt32(entities1.MemberStores.FirstOrDefault(x => x.StoreMainPartyId == item.MainPartyId).MemberMainPartyId);
                            BaseMemberDescription baseMember = new BaseMemberDescription();
                            baseMember.Date = DateTime.Now;
                            baseMember.Description = template;
                            baseMember.Title = mailTemplate.MessagesMTTitle;
                            baseMember.MainPartyId = memberMainPartyId;
                            entities1.BaseMemberDescriptions.AddObject(baseMember);
                            entities1.SaveChanges();

                            MemberDescription memberDescription = new MemberDescription();
                            memberDescription.Date = DateTime.Now;
                            memberDescription.Description = template;
                            memberDescription.Title = mailTemplate.MessagesMTTitle;
                            baseMember.MainPartyId = memberMainPartyId;
                            entities1.MemberDescriptions.AddObject(memberDescription);
                            entities1.SaveChanges();
                        }
                    }


                }
            }

            return Task.CompletedTask;

        }
    }
}