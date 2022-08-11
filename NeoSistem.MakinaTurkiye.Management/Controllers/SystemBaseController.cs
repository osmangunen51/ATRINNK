﻿using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Users;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class SystemBaseController : Controller
    {
        public void SendMessageAsync(string message, int UserID)
        {
            //var HubSystem = EngineContext.Current.Resolve<NeoSistem.MakinaTurkiye.Management.Hubs.System>();
            //HubSystem.SendMessage(message, UserID);
        }

        public bool SendMail(MailMessage mailMessage, NetworkCredential NetworkCredential = null)
        {
            bool Status = false;
            try
            {
                if (NetworkCredential == null)
                {
                    NetworkCredential = new NetworkCredential(AppSettings.MailUserName, AppSettings.MailPassword);
                }
                using (SmtpClient sc = new SmtpClient())
                {
                    sc.Port = AppSettings.MailPort;
                    sc.Host = AppSettings.MailHost;
                    sc.EnableSsl = AppSettings.MailSsl;
                    sc.Credentials = NetworkCredential;
                    sc.Send(mailMessage);
                    Status = true;
                }
            }
            catch (Exception Hata)
            {
                Status = false;
                TempData["StoreEmailError"] = Hata.Message;
            }
            return Status;
        }

        public ActionResult MemberDescriptionOrganizer()
        {
            IMemberDescriptionService memberDescriptionService = EngineContext.Current.Resolve<IMemberDescriptionService>();
            IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();
            IMemberStoreService memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            IUserService userService = EngineContext.Current.Resolve<IUserService>();
            IPreRegistirationStoreService preRegistirationStoreService = EngineContext.Current.Resolve<IPreRegistirationStoreService>();

            List<MemberDescription> processmemberDescriptionListesi = new List<MemberDescription>();
            var users = userService.GetAll().ToList();
            var stores = storeService.GetAllStores();
            var preRegistrationStores = preRegistirationStoreService.GetPreRegistrationStores();
            var memberstores = memberStoreService.GetMemberStores();
            var memberDescriptionListesi = memberDescriptionService.GetMemberDescriptionsByDate(DateTime.Now);
            var storememberDescriptionListesi = memberDescriptionListesi.Where(x => x.PreRegistrationStoreId == null);
            var preRegistrationStorememberDescriptionListesi = memberDescriptionListesi.Where(x => x.MainPartyId == null);

            Parallel.ForEach(storememberDescriptionListesi.Select(x => x.MainPartyId).Distinct(), item =>
            {
                var currentmemberstore = memberstores.Where(x => x.MemberMainPartyId == item).FirstOrDefault();
                if (currentmemberstore != null)
                {
                    var currentstore = stores.Where(x => x.MainPartyId == currentmemberstore.StoreMainPartyId);
                    if (currentstore != null)
                    {
                        var currentstorestorememberDescriptionListesi = storememberDescriptionListesi.Where(x => x.MainPartyId == item);
                        var currentappropriatestorestorememberDescription = currentstorestorememberDescriptionListesi.Where(x => x.Title == "SATIŞ YAPILDI" || x.Title == "Satişa Uygun Değil" || x.Title == "Churn").ToList();
                        if (currentappropriatestorestorememberDescription.Count() == 0)
                        {
                            var lastmemberDescription = currentstorestorememberDescriptionListesi.Where(x=>x.Status==0).OrderByDescending(x => x.Date)?.FirstOrDefault();
                            if (lastmemberDescription != null)
                            {
                                var tmp = users.Where(x => x.UserId == lastmemberDescription.UserId);
                                if (tmp != null)
                                {
                                    if (tmp.Count() > 0)
                                    {
                                        var currentUser = tmp.FirstOrDefault();
                                        if (currentUser != null)
                                        {
                                            if (currentUser.UserName.Contains("PY-") || currentUser.UserName.Contains("TS-"))
                                            {
                                                var currentuserFirstmemberDescription = currentstorestorememberDescriptionListesi.Where(x => x.UserId == lastmemberDescription.UserId).OrderBy(x => x.Date).FirstOrDefault();
                                                if (currentuserFirstmemberDescription != null)
                                                {
                                                    int Days = (int)(DateTime.Now - currentuserFirstmemberDescription.Date).TotalDays;
                                                    if (Days > 59)
                                                    {
                                                        lock (this)
                                                        {
                                                            if (!processmemberDescriptionListesi.Contains(lastmemberDescription))
                                                            {
                                                                processmemberDescriptionListesi.Add(lastmemberDescription);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            Parallel.ForEach(preRegistrationStorememberDescriptionListesi.Select(x => x.PreRegistrationStoreId).Distinct(), item =>
            {
                var currentpreRegistrationStore = preRegistrationStores.Where(x => x.PreRegistrationStoreId == item);
                if (currentpreRegistrationStore != null)
                {
                    var currentstorestorememberDescriptionListesi = preRegistrationStorememberDescriptionListesi.Where(x => x.PreRegistrationStoreId == item);
                    var currentappropriatestorestorememberDescription = currentstorestorememberDescriptionListesi.Where(x => x.Title == "SATIŞ YAPILDI" || x.Title == "Satişa Uygun Değil" || x.Title == "Churn").ToList();
                    if (currentappropriatestorestorememberDescription.Count() == 0)
                    {
                        var lastmemberDescription = currentstorestorememberDescriptionListesi.OrderByDescending(x => x.Date)?.FirstOrDefault();
                        if (lastmemberDescription != null)
                        {
                            var tmp = users.Where(x => x.UserId == lastmemberDescription.UserId);
                            if (tmp != null)
                            {
                                if (tmp.Count() > 0)
                                {
                                    var currentUser = tmp.FirstOrDefault();
                                    if (currentUser != null)
                                    {
                                        if (currentUser.UserName.Contains("PY-") || currentUser.UserName.Contains("TS-"))
                                        {
                                            var currentuserFirstmemberDescription = currentstorestorememberDescriptionListesi.Where(x => x.UserId == lastmemberDescription.UserId).OrderBy(x => x.Date).FirstOrDefault();
                                            if (currentuserFirstmemberDescription != null)
                                            {
                                                int Days = (int)(DateTime.Now - currentuserFirstmemberDescription.Date).TotalDays;
                                                if (Days > 59)
                                                {
                                                    lock (this)
                                                    {
                                                        if (!processmemberDescriptionListesi.Contains(lastmemberDescription))
                                                        {
                                                            processmemberDescriptionListesi.Add(lastmemberDescription);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            });
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var HavuzUser = users.FirstOrDefault(x => x.UserName == "**DATA HAVUZU");
                        Parallel.ForEach(processmemberDescriptionListesi, memberDescription =>
                        {
                            if (memberDescription.UserId!=HavuzUser.UserId)
                            {
                                memberDescription.Status = 1;
                                memberDescriptionService.UpdateMemberDescription(memberDescription);

                                string transactionType = "I";
                                MemberDescription AddMemberDescription = new MemberDescription()
                                {
                                    BaseID = memberDescription.BaseID,
                                    ConstantId = 429,
                                    Title = "Satıcı Değiştirme",
                                    Date = DateTime.Now,
                                    FromUserId = memberDescription.UserId,
                                    Description = memberDescription.Description,
                                    DescriptionDegree = (memberDescription.DescriptionDegree == null ? 0 : memberDescription.DescriptionDegree),
                                    IsFirst = memberDescription.IsFirst,
                                    IsImmediate = memberDescription.IsImmediate,
                                    MainPartyId = memberDescription.MainPartyId,
                                    PreRegistrationStoreId = memberDescription.PreRegistrationStoreId,
                                    Status = memberDescription.Status,
                                    UpdateDate = memberDescription.UpdateDate,
                                    UserId = HavuzUser.UserId,
                                };
                                memberDescriptionService.InsertMemberDescription(AddMemberDescription);
                                var memberDescriptionLog = new MemberDescriptionLog();
                                memberDescriptionLog.BaseID = memberDescription.BaseID;
                                memberDescriptionLog.ConstantId = memberDescription.ConstantId;
                                memberDescriptionLog.Date = memberDescription.Date;
                                memberDescriptionLog.descId = memberDescription.descId;
                                memberDescriptionLog.DescriptionDegree = memberDescription.DescriptionDegree;
                                memberDescriptionLog.FromUserId = memberDescription.FromUserId;
                                memberDescriptionLog.FromUserIdName = users.FirstOrDefault(x => x.UserId == memberDescription.FromUserId)?.UserName;
                                memberDescriptionLog.UpdateDate = memberDescription.UpdateDate;
                                memberDescriptionLog.UserId = memberDescription.UserId;
                                memberDescriptionLog.UserIdName = users.FirstOrDefault(x => x.UserId == memberDescription.UserId)?.UserName;
                                memberDescriptionLog.Status = memberDescription.Status;
                                memberDescriptionLog.Title = memberDescription.Title;
                                memberDescriptionLog.RecordDate = DateTime.Now;
                                memberDescriptionLog.MainPartyId = memberDescription.MainPartyId;
                                memberDescriptionLog.PreRegistrationStoreId = memberDescription.PreRegistrationStoreId;
                                if (memberDescription.PreRegistrationStoreId > 0)
                                    transactionType = transactionType + " O";
                                memberDescriptionLog.TransactionType = transactionType;
                                memberDescriptionService.InsertMemberDescriptionLog(memberDescriptionLog);
                            }                        
                        }
                    );
                    scope.Complete();
                }
                catch (Exception Hata)
                {

                }
            }
            TempData["Message"] = "Data Havuzu Organize Edilme İşlemi Tamamlandı.";
            return View();
        }
    }
}