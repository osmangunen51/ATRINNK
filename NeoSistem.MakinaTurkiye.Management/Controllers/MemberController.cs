using global::MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Members;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Cache;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Routing;
namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using global::MakinaTurkiye.Core;
    using global::MakinaTurkiye.Services.Bulletins;
    using global::MakinaTurkiye.Services.Catalog;
    using global::MakinaTurkiye.Services.Checkouts;
    using global::MakinaTurkiye.Services.Common;
    using global::MakinaTurkiye.Services.Messages;
    using global::MakinaTurkiye.Utilities.FormatHelpers;
    using global::MakinaTurkiye.Utilities.HttpHelpers;
    using global::MakinaTurkiye.Utilities.MailHelpers;
    using Models;
    using NeoSistem.EnterpriseEntity.Business;
    using NeoSistem.MakinaTurkiye.Management.Models.MemberModels;
    using System.IO;
    using System.Web;

    public class MemberController : BaseController
    {
        #region Constants

        const string STARTCOLUMN = "M.MainPartyId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;
        const string SessionPage = "member_PAGEDIMENSION";

        #endregion

        #region Fields

        private IMemberDescriptionService _memberDescService;
        private IStoreService _storeService;
        private IMemberStoreService _memberstoreService;
        private IConstantService _constantService;
        private IMemberService _memberService;
        private IUserMailTemplateService _usermailTemplateService;
        private IBulletinService _bulletinService;
        private ICategoryService _categoryService;
        private IPreRegistirationStoreService _preRegistrationStoreService;
        private IMessageService _messageService;
        private IWhatsappLogService _whatsappLogService;
        private IOrderService _orderService;
        #endregion

        #region Ctor


        public MemberController(IMemberDescriptionService memberDescService, IStoreService storeService, IMemberStoreService memberstoreService, IConstantService constantService, IMemberService memberService, IUserMailTemplateService usermailTemplateService, IBulletinService bulletinService, ICategoryService categoryService, IPreRegistirationStoreService preRegistrationStoreService, IMessageService messageService, IWhatsappLogService whatsappLogService, IOrderService orderService)
        {
            _memberDescService = memberDescService;
            _storeService = storeService;
            _memberstoreService = memberstoreService;
            _constantService = constantService;
            _memberService = memberService;
            _usermailTemplateService = usermailTemplateService;
            _bulletinService = bulletinService;
            _categoryService = categoryService;
            _preRegistrationStoreService = preRegistrationStoreService;
            _messageService = messageService;
            _whatsappLogService = whatsappLogService;
            _orderService = orderService;
        }

        #endregion

        static Data.Member dataMember = null;

        static ICollection<MemberModel> collection = null;


        #region Methods

        public ActionResult CategoryBottom(int CategoryId)
        {
            Data.Category dataCategory = null;
            ICollection<CategoryModel> LeftSide = null;
            LeftSide = dataCategory.CategoryMainPartyLeft(CategoryId).AsCollection<CategoryModel>();
            return View(LeftSide);
        }

        public ActionResult MailAllMemberType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MailAllMemberType(string StoreId, string RelatedCategory, string SectoreId, string chose, string TaslakId)
        {
            int logmailid = 0;
            string logmailname = "";
            int logcount = 0;
            int logfailcount = 0;
            DateTime logstart = new DateTime();
            DateTime logfinish = new DateTime();
            string aciklamalog = "";
            var memberforsendmail = new List<Member>();
            var storeforsendmail = new List<Store>();
            ICollection<MemberModel> memberforsendmaillayer = null;
            int TypeofMember = RelatedCategory.ToInt32();
            if (TypeofMember == 1)
            {
                //tüm üyeler
                memberforsendmail = entities.Members.ToList();
                logmailid = (int)MailLogId.TumUyeler;
                logmailname = "Tüm Üyeler";
                aciklamalog = "tüm üyeler";

            }
            else if (TypeofMember == 2)
            {
                //hızlı üyelik
                memberforsendmail = entities.Members.Where(c => c.MemberType == (int)MemberType.FastIndividual).ToList();
                //var test = entities.Members.Where(c => c.MainPartyId == 49434).SingleOrDefault();
                //var test1 = entities.Members.Where(c => c.MainPartyId == 51659).SingleOrDefault();
                //  var test2 = entities.Members.Where(c => c.MainPartyId == 51659).SingleOrDefault();
                //  memberforsendmail.Add(test);
                //  memberforsendmail.Add(test1);
                //  memberforsendmail.Add(test2);
                logmailid = (int)MailLogId.HızlıUyeler;
                logmailname = "hızlı üyelik";
                aciklamalog = "tüm hızlı üyeler";


            }
            else if (TypeofMember == 3)
            {
                //bireysel üyelik

                //categori id var ise onlara gönder
                //categori id var ise onlara yoksa paket tipi var sa onlara gönder.
                if (SectoreId != null)
                {
                    //
                    int SectorCategory = SectoreId.ToInt32();
                    memberforsendmaillayer = dataMember.MemberActivationCategory(SectorCategory).AsCollection<MemberModel>();
                    var sector = entities.Categories.Where(c => c.CategoryId == SectorCategory).SingleOrDefault();
                    logmailid = (int)MailLogId.BireyselUyeler;
                    logmailname = "bireysel üyelik";
                    aciklamalog = "Bireysel Üyeler " + sector.CategoryName + " kategorisi";
                }
                else
                {
                    memberforsendmail = entities.Members.Where(c => c.MemberType == (int)MemberType.Individual).ToList();
                    logmailid = (int)MailLogId.BireyselUyeler;
                    logmailname = "bireysel üyelik";
                    aciklamalog = "Tüm Bireysel Üyeler ";

                }



            }

            else if (TypeofMember == 4)
            {
                //firma üyelik
                //categori id var ise onlara yoksa paket tipi var sa onlara gönder.
                if (chose != null)
                {
                    byte storechosen = chose.ToByte();

                    if (storechosen == 1)
                    {
                        //kategoriye göre
                        if (SectoreId != null)
                        {
                            //
                            int SectorCategory = SectoreId.ToInt32();
                            if (SectorCategory == 1)
                            {
                                memberforsendmaillayer = dataMember.MemberActivationCategory(1).AsCollection<MemberModel>();
                                var sector = entities.Categories.Where(c => c.CategoryId == SectorCategory).SingleOrDefault();
                                logmailid = (int)MailLogId.FirmaUyeleri;
                                logmailname = "Firma üyelik";
                                aciklamalog = "Firma Üyeliği " + sector.CategoryName + " kategorisi";
                            }
                            else
                            {
                                memberforsendmaillayer = dataMember.MemberActivationCategory(SectorCategory).AsCollection<MemberModel>();
                                var sector = entities.Categories.Where(c => c.CategoryId == SectorCategory).SingleOrDefault();
                                logmailid = (int)MailLogId.FirmaUyeleri;
                                logmailname = "Firma üyelik";
                                aciklamalog = "Firma Üyeliği " + sector.CategoryName + " kategorisi";
                            }
                        }


                    }
                    else
                    {
                        //paket tipine göre
                        int Storepacketid = StoreId.ToInt32();
                        if (Storepacketid == 0)
                        {
                            storeforsendmail = entities.Stores.ToList();

                            logmailid = (int)MailLogId.FirmaUyeleri;
                            logmailname = "Firma üyelik";
                            aciklamalog = "Firma Üyeliği Tüm üyeler";
                        }
                        else
                        {
                            storeforsendmail = entities.Stores.Where(c => c.PacketId == Storepacketid).ToList();
                            var packet = entities.Packets.Where(c => c.PacketId == Storepacketid).SingleOrDefault();
                            logmailid = (int)MailLogId.FirmaUyeleri;
                            logmailname = "Firma üyelik";
                            aciklamalog = "Firma Üyeliği " + packet.PacketName + " üyeleri";
                        }


                    }




                }

                logmailid = (int)MailLogId.FirmaUyeleri;
                logmailname = "Firma üyelik";
                aciklamalog = "Tüm Firma Üyelik tipideki üyeler";

            }
            //mail gönderilecek
            int taslak = TaslakId.ToInt32();
            var constatn = entities.Constants.Where(c => c.ConstantId == taslak).SingleOrDefault();
            string aciklama = constatn.ContstantPropertie + "</br>";
            string subtitle = constatn.ConstantTitle;

            logstart = DateTime.Now;
            int dividemail = 0;
            if (memberforsendmaillayer != null && memberforsendmaillayer.Count > 0)
            {
                logcount = memberforsendmaillayer.Count;
                foreach (var singlemember in memberforsendmaillayer)
                {
                    dividemail += 1;
                    if (dividemail % 500 == 0)
                    {
                        System.Threading.Thread.Sleep(20000);//20 saniye
                    }
                    try
                    {
                        string intemplate = aciklama;
                        string realtitle = subtitle;
                        MailMessage mail = new MailMessage();

                        intemplate = Resources.Email.masterpage.Replace("#aciklama#", intemplate);
                        string template = Resources.Email.firmaonaylanmadi;
                        //string template = entities.MessagesMTs.SingleOrDefault(c => c.MessagesMTId == 15).MessagesMTPropertie;
                        template = template.Replace("#firmabilgileri#", intemplate).Replace("#uyeadisoyadi#", singlemember.MemberName + " " + singlemember.MemberSurname).Replace("#kullaniciadi#", singlemember.MemberEmail).Replace("#sifre#", singlemember.MemberPassword);
                        realtitle = realtitle.Replace("#firmabilgileri#", intemplate).Replace("#uyeadisoyadi#", singlemember.MemberName + " " + singlemember.MemberSurname).Replace("#kullaniciadi#", singlemember.MemberEmail).Replace("#sifre#", singlemember.MemberPassword);
                        mail.From = new MailAddress(AppSettings.MailUserName, "Makina Türkiye"); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(singlemember.MemberEmail); //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = realtitle; //Mail konusu
                        mail.Body = template; //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;



                        this.SendMail(mail);
                    }
                    catch
                    {
                        logfailcount += 1;
                    }
                }
            }
            else if (memberforsendmail.Count > 0 && memberforsendmail != null)
            {
                logcount = memberforsendmail.Count;
                foreach (var singlemember in memberforsendmail)
                {
                    dividemail += 1;
                    if (dividemail % 500 == 0)
                    {
                        System.Threading.Thread.Sleep(20000);//20 saniye
                    }
                    try
                    {
                        string intemplate = aciklama;
                        MailMessage mail = new MailMessage();

                        intemplate = Resources.Email.masterpage.Replace("#aciklama#", intemplate);
                        string template = Resources.Email.firmaonaylanmadi;
                        template = template.Replace("#firmabilgileri#", intemplate).Replace("#uyeadisoyadi#", singlemember.MemberName + " " + singlemember.MemberSurname).Replace("#kullaniciadi#", singlemember.MemberEmail).Replace("#sifre#", singlemember.MemberPassword);
                        subtitle = subtitle.Replace("#firmabilgileri#", intemplate).Replace("#uyeadisoyadi#", singlemember.MemberName + " " + singlemember.MemberSurname).Replace("#kullaniciadi#", singlemember.MemberEmail).Replace("#sifre#", singlemember.MemberPassword);
                        mail.From = new MailAddress(AppSettings.MailUserName, "Makina Türkiye"); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(singlemember.MemberEmail); //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = subtitle; //Mail konusu
                        mail.Body = template; //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;

                        this.SendMail(mail);

                    }
                    catch
                    {
                        logfailcount += 1;

                    }
                }
            }
            else if (storeforsendmail.Count > 0 && storeforsendmail != null)
            {
                logcount = storeforsendmail.Count;
                foreach (var singlemember in storeforsendmail)
                {
                    dividemail += 1;
                    if (dividemail % 500 == 0)
                    {
                        System.Threading.Thread.Sleep(20000);//20 saniye
                    }
                    try
                    {
                        string intemplate = aciklama;
                        MailMessage mail = new MailMessage();
                        intemplate = Resources.Email.masterpage.Replace("#aciklama#", intemplate);

                        string template = Resources.Email.firmaonaylanmadi;
                        template = template.Replace("#firmabilgileri#", intemplate).Replace("#uyeadisoyadi#", singlemember.StoreName).Replace("#kullaniciadi#", singlemember.StoreEMail);
                        subtitle = subtitle.Replace("#firmabilgileri#", intemplate).Replace("#uyeadisoyadi#", singlemember.StoreName).Replace("#kullaniciadi#", singlemember.StoreEMail);
                        mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Türkiye"); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(singlemember.StoreEMail); //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = subtitle; //Mail konusu
                        mail.Body = template; //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        this.SendMail(mail);

                    }
                    catch
                    {
                        logfailcount += 1;

                    }
                }
            }
            logfinish = DateTime.Now;

            var Maillog = new MailLog
            {
                MaillogTypeId = logmailid,
                MaillogTypeName = logmailname,
                Explanesth = aciklamalog,
                MaillogCount = logcount,
                MaillogFailCount = logfailcount,
                MaillogStart = logstart,
                MaillogFinish = logfinish
            };
            entities.MailLogs.AddObject(Maillog);
            entities.SaveChanges();



            return RedirectToAction("MailAllMemberSender");
        }

        public ActionResult MailAllMemberSender()
        {

            return View();

        }

        public ActionResult memberactivation()
        {
            var dataConstant = new Data.Constant();
            var model = dataConstant.ConstantGetByConstantType(15).AsCollection<ConstantModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult memberactivation(int id, string[] RelatedCategory)
        {
            int sayi = 0;
            string aciklama = "";
            string subtitle = "";
            var constatn = new Constant();
            if (RelatedCategory != null)
            {
                for (int i = 0; i < RelatedCategory.Length; i++)
                {
                    if (RelatedCategory.GetValue(i).ToString() != "false")
                    {
                        sayi = RelatedCategory.GetValue(i).ToInt32();
                        constatn = entities.Constants.Where(c => c.ConstantId == sayi).SingleOrDefault();
                        aciklama = aciklama + constatn.ContstantPropertie + "</br>";
                        subtitle = constatn.ConstantTitle;
                    }
                }
            }

            var mailadress = entities.Members.Where(c => c.MainPartyId == id).SingleOrDefault();
            string adress = mailadress.MemberEmail.ToString();
            string packettype = "Bireysel Üyesiniz.";
            string membertype = "";
            if (mailadress.MemberType == 5)
            {
                membertype = "Hızlı Üyelik";
            }
            else if (mailadress.MemberType == 10)
            {
                membertype = "Bireysel Üyelik";
            }
            else
            {
                membertype = "Firma Üyeliği";
                //packettype = entities.Packets.Where(a => a.PacketId == entities.Stores.Where(b => b.MainPartyId == entities.MemberStores.Where(c => c.MemberMainPartyId == mailadress.MainPartyId).SingleOrDefault().MemberStoreId).SingleOrDefault().PacketId).SingleOrDefault().PacketName;

            }
            if (mailadress.Active == false)
            {
                //activation ekle//#OnayKodu##OnayLink#
                string actlink = "http://www.makinaturkiye.com/" + "Uyelik/Aktivasyon/" + mailadress.ActivationCode;


                aciklama = aciklama.Replace("#activationcode#", mailadress.ActivationCode).Replace("#OnayLink#", actlink).Replace("#OnayKodu#", mailadress.ActivationCode).Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#uyeliktipi#", membertype).Replace("#pakettipi#", packettype).Replace("#sifre#", mailadress.MemberPassword);
                subtitle = subtitle.Replace("#activationcode#", mailadress.ActivationCode).Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#uyeliktipi#", membertype).Replace("#pakettipi#", packettype).Replace("#sifre#", mailadress.MemberPassword);

                #region kullanici
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(adress); //Mailin kime gideceğini belirtiyoruz
                mail.Subject = subtitle; //Mail konusu
                string template = aciklama;
                mail.Body = template; //Mailin içeriği
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
                BaseMemberDescription baseMember = new BaseMemberDescription();
                baseMember.Date = DateTime.Now;
                baseMember.MainPartyId = Convert.ToInt32(id);
                baseMember.Title = "Hesap Onaylanmamış";
                baseMember.Description = "Aktivasyon Maili Gönderildi";
                baseMember.UpdateDate = null;
                entities.BaseMemberDescriptions.AddObject(baseMember);
                entities.SaveChanges();
                MemberDescription memberDesc = new MemberDescription();
                memberDesc.MainPartyId = Convert.ToInt32(id);
                memberDesc.Description = "Aktivasyon Maili Gönderildi";
                memberDesc.Title = "Hesap Onaylanmamış";
                memberDesc.UpdateDate = null;
                memberDesc.Date = DateTime.Now;
                entities.MemberDescriptions.AddObject(memberDesc);
                entities.SaveChanges();


                #endregion
            }
            else
            {
                #region kullanici
                aciklama = Resources.Email.masterpage.Replace("#aciklama#", aciklama);
                MailMessage mail = new MailMessage();

                string template = Resources.Email.firmaonaylanmadi;
                template = template.Replace("#firmabilgileri#", aciklama).Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#sifre#", mailadress.MemberPassword).Replace("#uyeliktipi#", membertype);
                subtitle = subtitle.Replace("#firmabilgileri#", aciklama).Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#sifre#", mailadress.MemberPassword).Replace("#uyeliktipi#", membertype);
                mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Türkiye"); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(adress); //Mailin kime gideceğini belirtiyoruz
                mail.Subject = subtitle; //Mail konusu
                mail.Body = template; //Mailin içeriği
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
                BaseMemberDescription baseMember = new BaseMemberDescription();
                baseMember.Date = DateTime.Now;
                baseMember.MainPartyId = Convert.ToInt32(id);
                baseMember.Title = "K.B.M";
                baseMember.Description = "Kullanıcı bilgleri maili gönderildi.";
                baseMember.UpdateDate = null;
                entities.BaseMemberDescriptions.AddObject(baseMember);
                entities.SaveChanges();
                MemberDescription memberDesc = new MemberDescription();
                memberDesc.MainPartyId = Convert.ToInt32(id);
                memberDesc.Description = "Kullanıcı bilgleri maili gönderildi.";
                memberDesc.Title = "K.B.M";
                memberDesc.UpdateDate = null;
                memberDesc.Date = DateTime.Now;
                entities.MemberDescriptions.AddObject(memberDesc);
                entities.SaveChanges();
                #endregion
            }
            return RedirectToAction("Succees");
        }

        public ActionResult storemail(int id)
        {
            var dataConstant = new Data.Constant();
            var model = dataConstant.ConstantGetByConstantType(14).AsCollection<ConstantModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult storemail(int id, string[] RelatedCategory)
        {
            try
            {
                int sayi = 0;
                string aciklama = "";
                string subtitle = "";
                string aciklamabaslik = "";
                var constatn = new Constant();

                if (RelatedCategory != null)
                {
                    for (int i = 0; i < RelatedCategory.Length; i++)
                    {
                        if (RelatedCategory.GetValue(i).ToString() != "false")
                        {
                            sayi = RelatedCategory.GetValue(i).ToInt32();
                            constatn = entities.Constants.Where(c => c.ConstantId == sayi).SingleOrDefault();
                            aciklama = aciklama + constatn.ContstantPropertie + "</br>";
                            subtitle = constatn.ConstantTitle;
                            aciklamabaslik = constatn.ConstantName;


                        }
                    }
                }


                string allusersubtitle = subtitle;
                var mailadress = entities.Members.Where(c => c.MainPartyId == id).SingleOrDefault();
                var memberStore = entities.MemberStores.Where(c => c.MemberMainPartyId == id).SingleOrDefault();
                var storeid = memberStore.StoreMainPartyId;
                LinkHelper linkHelper = new LinkHelper();
                var crtypoUyeId = linkHelper.Encrypt(memberStore.MemberMainPartyId.ToString());
                var loginLink = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + crtypoUyeId;

                var firma = entities.Stores.Where(c => c.MainPartyId == storeid).SingleOrDefault();
                string adress = mailadress.MemberEmail.ToString();
                #region firmaolayi
                var urunler = entities.Products.Where(c => c.MainPartyId == id).ToList();
                int tekil = 0;
                int cogul = 0;
                string firmaurunlinki = UrlBuilder.GetStoreProfileProductUrl(firma.MainPartyId, firma.StoreName, firma.StoreUrlName);



                string istatistikfix = loginLink + "&returnUrl=/Account/Statistic/Index?pagetype=1";

                string istatistikilanfix = loginLink + "&returnUrl=/Account/Statistic/Index?pagetype=3";
                string packetupgrade = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&pagetype=5";
                if (urunler.Count != 0)
                {
                    foreach (var product in urunler)
                    {
                        tekil = tekil + product.SingularViewCount.ToInt32();
                        cogul = cogul + product.ViewCount.ToInt32();
                    }
                    float oran = tekil / urunler.Count;
                    aciklama = aciklama.Replace("#tekililantiklama#", tekil.ToString()).Replace("#cogulilantiklama#", cogul.ToString()).Replace("#ilantiklamaorani#", oran.ToString());
                }

                string linkuyeliktipi = "https://www.makinaturkiye.com/magaza-paket-fiyatlari-y-143135";
                //singular view count ve view count değişecek.

                aciklama = aciklama.Replace("#firmatekiltiklama#", firma.SingularViewCount.ToString()).Replace("#firmacogultiklama#", firma.ViewCount.ToString()).Replace("#ilansayisi#", urunler.Count.ToString()).Replace("#firmaurunlerkopru#", Resources.Email.firmalink.Replace("#firmalink#", firmaurunlinki)).Replace("#firmaistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikfix)).Replace("#ilanistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikilanfix)).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#sifre#", mailadress.MemberPassword).Replace("#uyelikpaket#", Resources.Email.firmalink.Replace("#firmalink#", linkuyeliktipi)).Replace("#firmauyelikyukseltme#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", packetupgrade));
                subtitle = subtitle.Replace("#firmatekiltiklama#", firma.SingularViewCount.ToString()).Replace("#firmacogultiklama#", firma.ViewCount.ToString()).Replace("#ilansayisi#", urunler.Count.ToString()).Replace("#firmaurunlerkopru#", Resources.Email.firmalink.Replace("#firmalink#", firmaurunlinki)).Replace("#firmaistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikfix)).Replace("#ilanistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikilanfix)).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#sifre#", mailadress.MemberPassword).Replace("#uyelikpaket#", Resources.Email.firmalink.Replace("#firmalink#", linkuyeliktipi));
                #endregion
                #region kullanici
                string template = "";
                var settings = ConfigurationManager.AppSettings;
                MailMessage mail = new MailMessage();
                string storefix = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&pagetype=2";

                string firmalinki = UrlBuilder.GetStoreProfileUrl(firma.MainPartyId, firma.StoreName, firma.StoreUrlName);
                aciklama = aciklama.Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#firmaduzenlemekopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", storefix)).Replace("#firmakopru#", Resources.Email.firmalink.Replace("#firmalink#", firmalinki)).Replace("#uyeliktarihi#", firma.StoreRecordDate.ToDateTime().ToString("dd/MM/yyyy"));
                subtitle = subtitle.Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#firmaduzenlemekopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", storefix)).Replace("#firmakopru#", Resources.Email.firmalink.Replace("#firmalink#", firmalinki));
                if (sayi != 247 && sayi != 246 && sayi != 248)
                {
                    aciklama = Resources.Email.masterpage.Replace("#aciklama#", aciklama);
                }
                else
                {
                    aciklama = aciklama.Replace("#loginlink#", loginLink);
                }

                template = aciklama.Replace("#firmaadi#", firma.StoreName);

                if (sayi == 144 || sayi == 145)
                {
                    if (sayi == 144)
                    {
                        sayi = 22;
                    }
                    else
                        sayi = 23;
                    string iskontoluyelik = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&sifre=" + mailadress.MemberPassword + "&Packetid=" + sayi;
                    template = template.Replace("#baslangiczamani#", DateTime.Now.ToShortDateString()).Replace("#bitiszamani#", DateTime.Now.AddDays(8).ToShortDateString()).Replace("#iskontolulink#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", iskontoluyelik));
                    var storeindirim = new Storeindirim()
                    {
                        MainPartyId = mailadress.MainPartyId,
                        BeginDate = DateTime.Now,
                        Enddate = DateTime.Now.AddDays(8)
                    };
                    entities.Storeindirims.AddObject(storeindirim);
                    entities.SaveChanges();
                }
                //date time karşılaştırmak için compare
                //DateTime.Compare(t1, t2);

                mail.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(adress); //Mailin kime gideceğini belirtiyoruz
                mail.Subject = subtitle; //Mail konusu
                mail.Body = template; //Mailin içeriği
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                if (constatn.ConstantId == 248)
                {
                    mail.Attachments.Add(new Attachment(Server.MapPath("~/Content/teklif.pdf")));
                }

                //mail.HtmlPart = new HtmlAttachment(template);

                //mail.From = new EmailAddress(settings["MailAddress"], settings["MailName"]);

                //mail.AddToAddress(new EmailAddress(adress));

                //SmtpServer smtpServer = new SmtpServer(settings["SmtpServer"], int.Parse(settings["SmtpPort"]));

                //smtpServer.SmtpAuthToken = new LoginAuthToken(settings["SmtpUserName"], settings["SmtpUserPassword"]);

                //mail.HeaderCharSet = Encoding.GetEncoding("UTF-8");

                //mail.HtmlPart.CharSet = Encoding.GetEncoding("UTF-8");

                this.SendMail(mail);

                #endregion
                #region digerkullanicilar
                var alluserfriend = entities.MainPartyIdEpostas.Where(c => c.MainPartyId == storeid).SingleOrDefault();
                if (alluserfriend != null)
                {
                    if (alluserfriend.Eposta1check == true)
                    {

                        if (alluserfriend.Eposta1 != null)
                        {
                            MailMessage maila = new MailMessage();
                            mail.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName);
                            maila.To.Add(alluserfriend.Eposta1); //Mailin kime gideceğini belirtiyoruz
                            allusersubtitle = allusersubtitle.Replace("#uyeadisoyadi#", alluserfriend.Ad1 + " " + alluserfriend.SoyAd2);
                            maila.Subject = allusersubtitle; //Mail konusu
                            maila.Body = template; //Mailin içeriği
                            maila.IsBodyHtml = true;
                            maila.Priority = MailPriority.Normal;
                            this.SendMail(maila);
                        }
                        else
                        {
                            alluserfriend.Eposta1check = false;
                        }
                    }
                    if (alluserfriend.Eposta2check == true)
                    {
                        if (alluserfriend.EPosta2 != null)
                        {
                            MailMessage mailb = new MailMessage();
                            mailb.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName); //Mailin kimden gittiğini belirtiyoruz
                            mailb.To.Add(alluserfriend.EPosta2); //Mailin kime gideceğini belirtiyoruz
                            allusersubtitle = allusersubtitle.Replace("#uyeadisoyadi#", alluserfriend.Ad2 + " " + alluserfriend.SoyAd2);
                            mailb.Subject = allusersubtitle; //Mail konusu
                            mailb.Body = template; //Mailin içeriği
                            mailb.IsBodyHtml = true;
                            mailb.Priority = MailPriority.High;
                            this.SendMail(mailb);
                        }
                    }
                }

                BaseMemberDescription baseMember = new BaseMemberDescription();
                baseMember.Date = DateTime.Now;
                baseMember.MainPartyId = Convert.ToInt32(id);
                baseMember.Title = aciklamabaslik;

                if (sayi == 128)
                {
                    template = "Ürün Çoğul Tıklanma Sayısı:" + cogul + "<br>Ürün Tekil Tıklanma Sayısı:" + tekil + "<br>Firma Tekil Tıklanma Sayısı:" + firma.SingularViewCount.ToString() + "<br>Firma Çoğul Tıklanma Sayısı:" + firma.ViewCount.ToString();

                    var whatsappLog = _whatsappLogService.GetWhatsappLogsByMainPartyId(memberStore.StoreMainPartyId.Value);
                    int whatsappCount = 0;
                    if (whatsappLog.Count > 0)
                    {
                        whatsappCount = whatsappLog.Select(x => x.ClickCount).Sum();
                    }
                    template += "<br>Whatsapp Tıklanma:" + whatsappCount;

                }
                baseMember.Description = template;
                baseMember.UpdateDate = null;
                entities.BaseMemberDescriptions.AddObject(baseMember);
                entities.SaveChanges();

                MemberDescription memd = new MemberDescription();
                memd.MainPartyId = mailadress.MainPartyId;
                memd.Date = DateTime.Now;
                memd.Description = template;
                memd.Title = aciklamabaslik;
                memd.UpdateDate = null;
                entities.MemberDescriptions.AddObject(memd);
                entities.SaveChanges();
                #endregion
                return RedirectToAction("storemail");
            }
            catch (Exception)
            {
                TempData["StoreEmailError"] = "Mail Gönderilirken Bir Hata Oluştu. Lütfen yöneticinize bildiriniz.";
                return RedirectToAction("storemail");
            }

        }



        [HttpGet]
        public ActionResult stororderremembermail(int orderId)
        {
            try
            {
                int id = 0;
                var order = _orderService.GetOrderByOrderId(orderId);
                var payment = _orderService.GetPaymentsByOrderId(orderId).FirstOrDefault();
                var ReturnInvoices = _orderService.GetReturnInvoicesByOrderId(orderId);
                if (order != null)
                {
                    id = order.MainPartyId;
                    DateTime SonOdemetarihi = default;
                    decimal KalanTutar = 0;
                    decimal PaketTutar = 0;
                    decimal OdeneneTutar = 0;
                    int sayi = 0;
                    string aciklama = "";
                    string subtitle = "";
                    string aciklamabaslik = "";
                    var constatn = new Constant();
                    if (payment != null)
                    {
                        PaketTutar = payment.RestAmount;
                    }
                    else
                    {
                        PaketTutar = order.OrderPrice;
                    }

                    if (ReturnInvoices!=null)
                    {
                        OdeneneTutar = ReturnInvoices.Sum(x => x.Amount);
                    }
                    KalanTutar = PaketTutar - OdeneneTutar;
                    SonOdemetarihi = order.PayDate.Value;

                    constatn = entities.Constants.Where(c => c.ConstantName == "Paket Ödeme Hatırlatma").SingleOrDefault();
                    sayi = constatn.ConstantId;
                    aciklama = aciklama + constatn.ContstantPropertie + "</br>";
                    subtitle = constatn.ConstantTitle;
                    aciklamabaslik = constatn.ConstantName;

                    if (string.IsNullOrEmpty(subtitle))
                    {
                        subtitle = "";
                    }
                    var firma = _storeService.GetStoreByMainPartyId(id);
                    var memberStore = entities.MemberStores.Where(c => c.StoreMainPartyId == firma.MainPartyId).FirstOrDefault();
                    string allusersubtitle = subtitle;
                    var mailadress = entities.Members.Where(c => c.MainPartyId == memberStore.MemberMainPartyId).SingleOrDefault();
                    var storeid = memberStore.StoreMainPartyId;
                    LinkHelper linkHelper = new LinkHelper();
                    var crtypoUyeId = linkHelper.Encrypt(memberStore.MemberMainPartyId.ToString());
                    var loginLink = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + crtypoUyeId;

                    string adress = mailadress.MemberEmail.ToString();
                    #region firmaolayi
                    var urunler = entities.Products.Where(c => c.MainPartyId == id).ToList();
                    int tekil = 0;
                    int cogul = 0;
                    string firmaurunlinki = UrlBuilder.GetStoreProfileProductUrl(firma.MainPartyId, firma.StoreName, firma.StoreUrlName);


                    string istatistikfix = loginLink + "&returnUrl=/Account/Statistic/Index?pagetype=1";

                    string istatistikilanfix = loginLink + "&returnUrl=/Account/Statistic/Index?pagetype=3";
                    string packetupgrade = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&pagetype=5";
                    if (urunler.Count != 0)
                    {
                        foreach (var product in urunler)
                        {
                            tekil = tekil + product.SingularViewCount.ToInt32();
                            cogul = cogul + product.ViewCount.ToInt32();
                        }
                        float oran = tekil / urunler.Count;
                        aciklama = aciklama.Replace("#tekililantiklama#", tekil.ToString()).Replace("#cogulilantiklama#", cogul.ToString()).Replace("#ilantiklamaorani#", oran.ToString());
                    }

                    string linkuyeliktipi = "https://www.makinaturkiye.com/magaza-paket-fiyatlari-y-143135";
                    //singular view count ve view count değişecek.

                    aciklama = aciklama.Replace("#firmatekiltiklama#", firma.SingularViewCount.ToString()).Replace("#firmacogultiklama#", firma.ViewCount.ToString()).Replace("#ilansayisi#", urunler.Count.ToString()).Replace("#firmaurunlerkopru#", Resources.Email.firmalink.Replace("#firmalink#", firmaurunlinki)).Replace("#firmaistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikfix)).Replace("#ilanistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikilanfix)).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#sifre#", mailadress.MemberPassword).Replace("#uyelikpaket#", Resources.Email.firmalink.Replace("#firmalink#", linkuyeliktipi)).Replace("#firmauyelikyukseltme#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", packetupgrade));
                   
                    subtitle = subtitle.Replace("#firmatekiltiklama#", firma.SingularViewCount.ToString()).Replace("#firmacogultiklama#", firma.ViewCount.ToString()).Replace("#ilansayisi#", urunler.Count.ToString()).Replace("#firmaurunlerkopru#", Resources.Email.firmalink.Replace("#firmalink#", firmaurunlinki)).Replace("#firmaistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikfix)).Replace("#ilanistatistikkopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", istatistikilanfix)).Replace("#kullaniciadi#", mailadress.MemberEmail).Replace("#sifre#", mailadress.MemberPassword).Replace("#uyelikpaket#", Resources.Email.firmalink.Replace("#firmalink#", linkuyeliktipi));

                    #endregion
                    #region kullanici
                    string template = "";
                    var settings = ConfigurationManager.AppSettings;
                    MailMessage mail = new MailMessage();
                    string storefix = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&pagetype=2";

                    string firmalinki = UrlBuilder.GetStoreProfileUrl(firma.MainPartyId, firma.StoreName, firma.StoreUrlName);
                    aciklama = aciklama.Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#firmaduzenlemekopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", storefix)).Replace("#firmakopru#", Resources.Email.firmalink.Replace("#firmalink#", firmalinki)).Replace("#uyeliktarihi#", firma.StoreRecordDate.ToDateTime().ToString("dd/MM/yyyy"));
                    subtitle = subtitle.Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#firmaduzenlemekopru#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", storefix)).Replace("#firmakopru#", Resources.Email.firmalink.Replace("#firmalink#", firmalinki));
                    if (sayi != 247 && sayi != 246 && sayi != 248)
                    {
                        aciklama = Resources.Email.masterpage.Replace("#aciklama#", aciklama);
                    }
                    else
                    {
                        aciklama = aciklama.Replace("#loginlink#", loginLink);
                    }

                    template = aciklama.Replace("#firmaadi#", firma.StoreName);


                    #region  Ödeme Bilgileri Ayarlanıyor...
                    template = template.Replace("#sonodemetarih#", SonOdemetarihi.ToString("D"));
                    template = template.Replace("#pakettutar#", PaketTutar.ToString("N2"));
                    template = template.Replace("#kalantutar#", KalanTutar.ToString("N2"));
                    #endregion

                    if (sayi == 144 || sayi == 145)
                    {
                        if (sayi == 144)
                        {
                            sayi = 22;
                        }
                        else
                            sayi = 23;
                        string iskontoluyelik = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&sifre=" + mailadress.MemberPassword + "&Packetid=" + sayi;
                        template = template.Replace("#baslangiczamani#", DateTime.Now.ToShortDateString()).Replace("#bitiszamani#", DateTime.Now.AddDays(8).ToShortDateString()).Replace("#iskontolulink#", Resources.Email.firmaduzenle.Replace("#firmaduzenle#", iskontoluyelik));
                        var storeindirim = new Storeindirim()
                        {
                            MainPartyId = mailadress.MainPartyId,
                            BeginDate = DateTime.Now,
                            Enddate = DateTime.Now.AddDays(8)
                        };
                        entities.Storeindirims.AddObject(storeindirim);
                        entities.SaveChanges();
                    }

                    //date time karşılaştırmak için compare
                    //DateTime.Compare(t1, t2);

                    mail.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName); //Mailin kimden gittiğini belirtiyoruz
                    mail.To.Add(adress); //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = subtitle; //Mail konusu
                    mail.Body = template; //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    if (constatn.ConstantId == 248)
                    {
                        mail.Attachments.Add(new Attachment(Server.MapPath("~/Content/teklif.pdf")));
                    }

                    //mail.HtmlPart = new HtmlAttachment(template);

                    //mail.From = new EmailAddress(settings["MailAddress"], settings["MailName"]);

                    //mail.AddToAddress(new EmailAddress(adress));

                    //SmtpServer smtpServer = new SmtpServer(settings["SmtpServer"], int.Parse(settings["SmtpPort"]));

                    //smtpServer.SmtpAuthToken = new LoginAuthToken(settings["SmtpUserName"], settings["SmtpUserPassword"]);

                    //mail.HeaderCharSet = Encoding.GetEncoding("UTF-8");

                    //mail.HtmlPart.CharSet = Encoding.GetEncoding("UTF-8");

                    this.SendMail(mail);

                    #endregion
                    #region digerkullanicilar
                    var alluserfriend = entities.MainPartyIdEpostas.Where(c => c.MainPartyId == storeid).SingleOrDefault();
                    if (alluserfriend != null)
                    {
                        if (alluserfriend.Eposta1check == true)
                        {

                            if (alluserfriend.Eposta1 != null)
                            {
                                MailMessage maila = new MailMessage();
                                mail.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName);
                                maila.To.Add(alluserfriend.Eposta1); //Mailin kime gideceğini belirtiyoruz
                                allusersubtitle = allusersubtitle.Replace("#uyeadisoyadi#", alluserfriend.Ad1 + " " + alluserfriend.SoyAd2);
                                maila.Subject = allusersubtitle; //Mail konusu
                                maila.Body = template; //Mailin içeriği
                                maila.IsBodyHtml = true;
                                maila.Priority = MailPriority.Normal;
                                this.SendMail(maila);
                            }
                            else
                            {
                                alluserfriend.Eposta1check = false;
                            }
                        }
                        if (alluserfriend.Eposta2check == true)
                        {
                            if (alluserfriend.EPosta2 != null)
                            {
                                MailMessage mailb = new MailMessage();
                                mailb.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName); //Mailin kimden gittiğini belirtiyoruz
                                mailb.To.Add(alluserfriend.EPosta2); //Mailin kime gideceğini belirtiyoruz
                                allusersubtitle = allusersubtitle.Replace("#uyeadisoyadi#", alluserfriend.Ad2 + " " + alluserfriend.SoyAd2);
                                mailb.Subject = allusersubtitle; //Mail konusu
                                mailb.Body = template; //Mailin içeriği
                                mailb.IsBodyHtml = true;
                                mailb.Priority = MailPriority.High;
                                this.SendMail(mailb);
                            }
                        }
                    }

                    BaseMemberDescription baseMember = new BaseMemberDescription();
                    baseMember.Date = DateTime.Now;
                    baseMember.MainPartyId = Convert.ToInt32(id);
                    baseMember.Title = aciklamabaslik;

                    if (sayi == 128)
                    {
                        template = "Ürün Çoğul Tıklanma Sayısı:" + cogul + "<br>Ürün Tekil Tıklanma Sayısı:" + tekil + "<br>Firma Tekil Tıklanma Sayısı:" + firma.SingularViewCount.ToString() + "<br>Firma Çoğul Tıklanma Sayısı:" + firma.ViewCount.ToString();

                        var whatsappLog = _whatsappLogService.GetWhatsappLogsByMainPartyId(memberStore.StoreMainPartyId.Value);
                        int whatsappCount = 0;
                        if (whatsappLog.Count > 0)
                        {
                            whatsappCount = whatsappLog.Select(x => x.ClickCount).Sum();
                        }
                        template += "<br>Whatsapp Tıklanma:" + whatsappCount;

                    }
                    baseMember.Description = template;
                    baseMember.UpdateDate = null;
                    entities.BaseMemberDescriptions.AddObject(baseMember);
                    entities.SaveChanges();

                    MemberDescription memd = new MemberDescription();
                    memd.MainPartyId = mailadress.MainPartyId;
                    memd.Date = DateTime.Now;
                    memd.Description = template;
                    memd.Title = aciklamabaslik;
                    memd.UpdateDate = null;
                    entities.MemberDescriptions.AddObject(memd);
                    entities.SaveChanges();
                    #endregion

                }
                return RedirectToAction("", "orderfirm");
            }
            catch (Exception)
            {
                TempData["StoreEmailError"] = "Mail Gönderilirken Bir Hata Oluştu. Lütfen yöneticinize bildiriniz.";
                return RedirectToAction("", "orderfirm");
            }
        }

        public ActionResult SendSpecialEmailToStore(int id, string success)
        {


            var memberId = _memberstoreService.GetMemberStoreByStoreMainPartyId(id).MemberMainPartyId;
            var member = _memberService.GetMemberByMainPartyId(Convert.ToInt32(memberId));
            var dataConstant = new Data.Constant();
            var files = dataConstant.ConstantGetByConstantType((byte)ConstantType.StoreSpecialMailFile).AsCollection<ConstantModel>();
            var constant = dataConstant.ConstantGetByConstantType(14).AsCollection<ConstantModel>().FirstOrDefault(x => x.ConstantId == 404);

            StoreSpecialMailModel model = new StoreSpecialMailModel();
            model.StoreMail = member.MemberEmail;
            model.MemberID = member.MainPartyId;
            model.Subject = constant.ConstantTitle;
            model.Content = constant.ConstantMailContent;
            foreach (var item in files)
            {
                var name = item.ConstantTitle;
                model.Files.Add(new SelectListItem { Text = name, Value = item.ConstantId.ToString() });
            }

            if (!string.IsNullOrEmpty(success))
            {
                model.Message = "Mail Başarıyla Gönderildi.";
            }

            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SendSpecialEmailToStore(StoreSpecialMailModel model, HttpPostedFileBase[] files, string[] defaultFile, string FileSave)
        {

            string sablon = NeoSistem.MakinaTurkiye.Management.Properties.Resources.EmailSablon.ToString();
            var icerik = sablon.Replace("#icerik", model.Content);
            var member = _memberService.GetMemberByMainPartyId(model.MemberID);
            icerik = icerik.Replace("#uyeadisoyadi#", member.MemberName + " " + member.MemberSurname);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("makina@makinaturkiye.com"); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(model.StoreMail); //Mailin kime gideceğini belirtiyoruz
            mail.Subject = model.Subject; //Mail konusu
            mail.Body = icerik; //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            foreach (HttpPostedFileBase file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {

                    var InputFileName = Path.GetFileName(file.FileName);
                    if (!string.IsNullOrEmpty(FileSave))
                    {
                        var ServerSavePath = Path.Combine(Server.MapPath("~/FilesMail/") + InputFileName);
                        file.SaveAs(ServerSavePath);
                        mail.Attachments.Add(new Attachment(Server.MapPath("~/FilesMail/") + InputFileName));
                        var constant = new global::MakinaTurkiye.Entities.Tables.Common.Constant();
                        constant.ConstantType = (byte)ConstantType.StoreSpecialMailFile;
                        constant.ConstantTitle = "~/FilesMail/" + InputFileName;


                        _constantService.InsertConstant(constant);

                    }
                    else
                    {
                        var inputType = InputFileName.Split('.');

                        string newFileName = inputType[0].Replace(" ", "_") + "_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace("/", "_").Replace(":", "_") + "." + inputType[1];

                        var ServerSavePath = Path.Combine(Server.MapPath("~/FilesMail/") + newFileName);
                        file.SaveAs(ServerSavePath);
                        mail.Attachments.Add(new Attachment(Server.MapPath("~/FilesMail/") + newFileName));
                    }


                }
            }


            if (defaultFile != null)
            {
                foreach (var item in defaultFile)
                {
                    short consId = Convert.ToInt16(item);
                    var constant = _constantService.GetConstantByConstantId(consId);

                    mail.Attachments.Add(new Attachment(constant.ConstantTitle));
                }
            }

            this.SendMail(mail);

            return RedirectToAction("SendSpecialEmailToStore", new { success = "true" });

        }
        public ActionResult advertmail(int id)
        {
            var dataConstant = new Data.Constant();
            var model = dataConstant.ConstantGetByConstantType(13).AsCollection<ConstantModel>();
            return View(model);
        }
        [HttpPost]
        public JsonResult SpecialMailSend(int constandId, int storeid)
        {

            var sablon = entities.MessagesMTs.FirstOrDefault(x => x.MessagesMTId == 54);
            int userId = CurrentUserModel.CurrentManagement.UserId;
            var user = entities.Users.FirstOrDefault(x => x.UserId == userId);
            var mailTempate = _usermailTemplateService.GetUserMailTemplatesByUserId(userId).FirstOrDefault(x => x.SpecialMailId == constandId);
            if (mailTempate != null)
            {
                try
                {
                    var memberId = _memberstoreService.GetMemberStoreByStoreMainPartyId(storeid).MemberMainPartyId;
                    var member = _memberService.GetMemberByMainPartyId(Convert.ToInt32(memberId));
                    var store = _storeService.GetStoreByMainPartyId(storeid);
                    var constant = _constantService.GetConstantByConstantId((short)constandId);
                    var icerik = sablon.MessagesMTPropertie.Replace("#icerik#", mailTempate.MailContent);
                    icerik = icerik.Replace("#adsoyad#", member.MemberName + " " + member.MemberSurname).Replace("#firmaadi#", store.StoreName);
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(user.UserMail); //Mailin kimden gittiğini belirtiyoruz
                    var storeeposta = entities.MainPartyIdEpostas.Where(c => c.MainPartyId == storeid).SingleOrDefault();
                    if (storeeposta != null)
                    {
                        if (!string.IsNullOrEmpty(storeeposta.Eposta1) && storeeposta.Eposta1check == true)
                            mail.To.Add(storeeposta.Eposta1);
                        if (!string.IsNullOrEmpty(storeeposta.EPosta2) && storeeposta.Eposta2check == true)
                            mail.To.Add(storeeposta.EPosta2);

                    }
                    if (!string.IsNullOrEmpty(member.MemberEmail))
                        mail.To.Add(member.MemberEmail);
                    mail.To.Add(store.StoreEMail); //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = mailTempate.Subject; //Mail konusu
                    mail.Body = icerik; //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    if (constant.ConstantName.ToLower().Contains("teklif"))
                    {
                        mail.Attachments.Add(new Attachment(Server.MapPath("~/FilesMail/Teklif.pdf")));
                        mail.Attachments.Add(new Attachment(Server.MapPath("~/FilesMail/Tanıtım.pdf")));
                    }
                    this.SendMail(mail);
                    var anyMember = entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == member.MainPartyId);

                    var basemember = new BaseMemberDescription();
                    if (anyMember != null)
                        basemember = anyMember;
                    basemember.ConstantId = constandId;
                    basemember.Date = DateTime.Now;

                    basemember.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                    basemember.Title = constant.ConstantName;
                    basemember.MainPartyId = member.MainPartyId;
                    if (anyMember == null)
                        entities.BaseMemberDescriptions.AddObject(basemember);

                    entities.SaveChanges();
                    var memberDescription = new MemberDescription();
                    memberDescription.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                    memberDescription.BaseID = basemember.ID;
                    memberDescription.ConstantId = constandId;
                    memberDescription.Date = DateTime.Now;

                    memberDescription.MainPartyId = member.MainPartyId;
                    memberDescription.Status = 1;
                    memberDescription.Title = constant.ConstantName;
                    entities.MemberDescriptions.AddObject(memberDescription);
                    entities.SaveChanges();

                    return Json(new { Message = "Mail Başarıyla Gönderildi" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { Message = ex.InnerException.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Message = "Kullanıcınıza bu mail tanımlı değildir." }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult advertmail(int id, string[] RelatedCategory)
        {
            int Productid = Request.QueryString["productid"].ToInt32();

            int sayi = 0;
            string aciklama = "";
            string subtitle = "";
            var constatn = new Constant();
            if (RelatedCategory != null)
            {
                for (int i = 0; i < RelatedCategory.Length; i++)
                {
                    if (RelatedCategory.GetValue(i).ToString() != "false")
                    {
                        sayi = RelatedCategory.GetValue(i).ToInt32();
                        constatn = entities.Constants.Where(c => c.ConstantId == sayi).SingleOrDefault();
                        aciklama = aciklama + constatn.ContstantPropertie + "</br>";
                        subtitle = constatn.ConstantTitle;
                    }
                }
            }
            var product = entities.Products.Where(c => c.ProductId == Productid).SingleOrDefault();
            var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
            var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
            string urunlink = "http://www.makinaturkiye.com/" + Helpers.ToUrl(groupname) + "/" + product.ProductId + "/" + Helpers.ToUrl(categoryname) + "/" + Helpers.ToUrl(product.ProductName);
            var mailadress = entities.Members.Where(c => c.MainPartyId == id).SingleOrDefault();
            string adress = mailadress.MemberEmail.ToString();
            string advertfix = "http://www.makinaturkiye.com/uyelikgiris?email=" + adress + "&pagetype=" + product.ProductId;
            //buraya mail gödnersin sorna başarılı falan diye sayfa oluştur o çıksın.
            #region kullanici
            //var settings = ConfigurationManager.AppSettings;
            //EmailMessage mail = new EmailMessage();
            //mail.HeaderCharSet = System.Text.Encoding.UTF8;
            string template = Resources.Email.ilanonaylanmadi;
            if (mailadress.MemberType == 10)
            {
                aciklama = aciklama.Replace("#bireyseldenfirmauyeligikopru#", Resources.Email.bireyseluyefirmaolmalı.Replace("#email", adress));
            }
            string linkuyeliktipi = "http://www.makinaturkiye.com/Home/Prices";
            aciklama = aciklama.Replace("#ilanno#", product.ProductNo).Replace("#ilanadi#", Resources.Email.linkurunadi.Replace("#urunlink#", urunlink).Replace("#urunadi#", product.ProductName)).Replace("#ilanduzenlemekopru#", Resources.Email.ilanduzeltme.Replace("#ilangiris#", advertfix)).Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#ilanadikopru#", Resources.Email.linkkopru.Replace("#urunlink#", urunlink));
            subtitle = subtitle.Replace("#ilanno#", product.ProductNo).Replace("#ilanadi#", Resources.Email.linkurunadi.Replace("#urunlink#", urunlink).Replace("#urunadi#", product.ProductName)).Replace("#ilanduzenlemekopru#", Resources.Email.ilanduzeltme.Replace("#ilangiris#", advertfix)).Replace("#uyeadisoyadi#", mailadress.MemberName + " " + mailadress.MemberSurname).Replace("#ilanadikopru#", Resources.Email.linkkopru.Replace("#urunlink#", urunlink)).Replace("#uyelikpaket#", Resources.Email.firmalink.Replace("#firmalink#", linkuyeliktipi));
            //buraya firmanın sistemde kayıtlı mesajkutusunada mail gitmesi için gereklikısım yazılacak.
            var forproductmessagesave = new Message
            {
                Active = true,
                MessageSubject = subtitle,
                MessageContent = aciklama,
                MessageDate = DateTime.Now,
                ProductId = product.ProductId
            };
            entities.Messages.AddObject(forproductmessagesave);
            entities.SaveChanges();
            var usermessage = new MessageMainParty
            {
                MessageId = forproductmessagesave.MessageId,
                MainPartyId = mailadress.MainPartyId,
                InOutMainPartyId = 52410,
                MessageType = 0
            };
            entities.MessageMainParties.AddObject(usermessage);
            entities.SaveChanges();
            var usermessageforus = new MessageMainParty
            {
                MessageId = forproductmessagesave.MessageId,
                MainPartyId = 52410,
                InOutMainPartyId = mailadress.MainPartyId,
                MessageType = 1
            };
            entities.MessageMainParties.AddObject(usermessageforus);
            entities.SaveChanges();

            aciklama = Resources.Email.masterpage.Replace("#aciklama#", aciklama);
            template = template.Replace("#ilanbilgileri#", aciklama);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(AppSettings.MailUserName, AppSettings.MailDisplayName); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(adress); //Mailin kime gideceğini belirtiyoruz
            mail.Subject = subtitle; //Mail konusu
            mail.Body = template; //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            this.SendMail(mail);
            #endregion
            return RedirectToAction("Succees");
        }

        public ActionResult Succees()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.UyeDuzenle;

            var model = new MemberModel();
            var curMember = new Classes.Member();
            curMember.LoadEntity(id);

            int mainPartyId = id;
            if (curMember.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = entities.MemberStores.FirstOrDefault(c => c.MemberMainPartyId == mainPartyId).StoreMainPartyId.Value;
            }

            var address = entities.Addresses.FirstOrDefault(c => c.MainPartyId == mainPartyId);
            var phone = entities.Phones.Where(c => c.MainPartyId == mainPartyId).ToList();

            model.PhoneItems = phone;
            model.Address = address;

            List<Locality> localityItems = new List<Locality>() { new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
            List<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };

            if (address != null)
            {
                model.Street = address.Street;
                model.Avenue = address.Avenue;
                model.DoorNo = address.DoorNo;
                model.ApartmentNo = address.ApartmentNo;

                if (address.CityId.HasValue)
                {
                    model.CityId = address.CityId.Value;
                    localityItems = entities.Localities.Where(c => c.CityId == model.CityId).OrderBy(c => c.LocalityName).ToList();
                    localityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
                }

                if (address.LocalityId.HasValue)
                {
                    model.LocalityId = address.LocalityId.Value;
                    townItems = entities.Towns.Where(c => c.LocalityId == address.LocalityId.Value).ToList();
                    townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });
                }

                if (address.TownId.HasValue)
                {
                    model.TownId = address.TownId.Value;
                }

                if (address.AddressTypeId.HasValue)
                {
                    model.AddressTypeId = address.AddressTypeId.Value;
                }

                if (address.CountryId.HasValue)
                {
                    model.CountryId = address.CountryId.Value;

                }
            }
            else
                model.CountryId = AppSettings.Turkiye;

            var countryItems = entities.Countries.ToList();
            countryItems.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });

            var cityItems = entities.Cities.Where(c => c.CountryId == model.CountryId).ToList();
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            model.CountryItems = new SelectList(countryItems, "CountryId", "CountryName", 0);
            model.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
            model.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
            model.TownItems = new SelectList(townItems, "TownId", "TownName");


            Session["MainPartyId"] = id;

            var dataAddress = new Data.Address();
            var dataPhone = new Data.Phone();


            var store = entities.MemberStores.FirstOrDefault(c => c.MemberMainPartyId == id);
            if (store != null)
            {
                model.hasStore = true;
                model.StoreMainPartyId = store.StoreMainPartyId.Value;
            }
            else
                model.hasStore = false;

            model.MemberMainPartyId = id;

            var memberlistAddress = dataAddress.GetAddressByMainPartyId(id).AsCollection<AddressModel>().ToList<AddressModel>();

            MemberModel memberModel = model;
            UpdateClass(curMember, memberModel);

            var dataCategory = new Data.Category();
            memberModel.SectorItems = entities.Categories.Where(c => c.CategoryParentId == null);
            var dataRelMainPartyCategory = new Data.RelMainPartyCategory();

            model.MainPartyRelatedSectorItems = dataRelMainPartyCategory.GetMainPartyRelatedCategoryItemsByMainPartyId(id).AsCollection<RelMainPartyCategoryModel>();

            memberModel.CategoryItems = CacheUtilities.GetCategories().AsCollection<CategoryModel>();

            return View(memberModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, MemberModel model, FormCollection coll)
        {

            var member = entities.Members.FirstOrDefault(c => c.MainPartyId == id);

            int mainPartyId = id;
            if (member.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = entities.MemberStores.FirstOrDefault(c => c.MemberMainPartyId == mainPartyId).StoreMainPartyId.Value;
            }
            if (member.MemberType > (byte)MemberType.FastIndividual)
            {
                bool hasAddress = false;
                var address = entities.Addresses.FirstOrDefault(c => c.MainPartyId == mainPartyId);
                if (address != null)
                {
                    hasAddress = true;
                }
                else
                {
                    address = new Address();
                }

                if (model.CityId > 0)
                    address.CityId = model.CityId;
                else
                    address.CityId = null;

                if (model.CountryId > 0)
                    address.CountryId = model.CountryId;
                else
                    address.CountryId = null;

                if (model.TownId > 0)
                    address.TownId = model.TownId;
                else
                    address.TownId = null;


                if (model.LocalityId > 0)
                    address.LocalityId = model.LocalityId;
                else
                    address.LocalityId = null;

                address.MainPartyId = mainPartyId;

                if (model.AddressTypeId > 0)
                    address.AddressTypeId = model.AddressTypeId;
                else
                    address.AddressTypeId = null;

                address.Street = model.Street;
                address.DoorNo = model.DoorNo;
                address.Avenue = model.Avenue;
                address.ApartmentNo = model.ApartmentNo;

                if (hasAddress)
                {
                    entities.SaveChanges();
                }
                else
                {
                    entities.Addresses.AddObject(address);
                    entities.SaveChanges();
                }
            }


            var phoneItems = entities.Phones.Where(c => c.MainPartyId == mainPartyId);
            if (phoneItems != null)
            {
                using (var trans = new TransactionScope())
                {
                    foreach (var item in phoneItems)
                    {
                        var phone = entities.Phones.SingleOrDefault(c => c.PhoneId == item.PhoneId);

                        entities.Phones.DeleteObject(phone);
                    }

                    entities.SaveChanges();
                    trans.Complete();
                }

            }

            if (model.InstitutionalPhoneNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber))
            {
                var phone1 = new Phone
                {
                    MainPartyId = mainPartyId,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode,
                    PhoneCulture = model.InstitutionalPhoneCulture,
                    PhoneNumber = model.InstitutionalPhoneNumber,
                    PhoneType = (byte)PhoneType.Phone,
                    GsmType = null
                };
                entities.Phones.AddObject(phone1);
                entities.SaveChanges();
            }

            if (model.InstitutionalPhoneNumber2 != null && !string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber2))
            {
                var phone2 = new Phone
                {
                    MainPartyId = mainPartyId,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode2,
                    PhoneCulture = model.InstitutionalPhoneCulture2,
                    PhoneNumber = model.InstitutionalPhoneNumber2,
                    PhoneType = (byte)PhoneType.Phone,
                    GsmType = null
                };
                entities.Phones.AddObject(phone2);
                entities.SaveChanges();
            }

            if (model.InstitutionalGSMNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber))
            {
                var phoneGsm = new Phone
                {
                    MainPartyId = mainPartyId,
                    PhoneAreaCode = model.InstitutionalGSMAreaCode,
                    PhoneCulture = model.InstitutionalGSMCulture,
                    PhoneNumber = model.InstitutionalGSMNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                    GsmType = model.GsmType
                };
                entities.Phones.AddObject(phoneGsm);
                entities.SaveChanges();
            }

            if (model.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalFaxNumber))
            {
                var phoneFax = new Phone
                {
                    MainPartyId = mainPartyId,
                    PhoneAreaCode = model.InstitutionalFaxAreaCode,
                    PhoneCulture = model.InstitutionalFaxCulture,
                    PhoneNumber = model.InstitutionalFaxNumber,
                    PhoneType = (byte)PhoneType.Fax,
                    GsmType = null
                };
                entities.Phones.AddObject(phoneFax);
                entities.SaveChanges();
            }

            member.MemberEmail = model.MemberEmail;
            member.MemberName = model.MemberName;
            member.MemberSurname = model.MemberSurname;
            member.Active = model.Active;
            member.MemberPassword = model.MemberPassword;
            member.ReceiveEmail = model.ReceiveEmail;
            member.Gender = model.Gender;

            if (model.BirthDate.ToString() != "01.01.0001 00:00:00")
            {
                member.BirthDate = model.BirthDate;
            }

            entities.SaveChanges();

            var curMainParty = new Classes.MainParty();
            curMainParty.LoadEntity(id);
            curMainParty.MainPartyFullName = model.MemberName + " " + model.MemberSurname;
            curMainParty.Save();

            if (coll["MemberRelatedSectorIdItems"] != null)
            {
                string[] memberSector = coll["MemberRelatedSectorIdItems"].Split(',');
                var dataRelMainPartyCategory = new Data.RelMainPartyCategory();
                dataRelMainPartyCategory.MainPartyRelatedCategoryDeleteByMainPartyId(id);
                if (memberSector != null)
                {
                    for (int i = 0; i < memberSector.Length; i++)
                    {
                        if (memberSector.GetValue(i).ToString() != "false")
                        {
                            var curRelMainPartyCategory = new Classes.RelMainPartyCategory
                            {
                                MainPartyId = id,
                                CategoryId = memberSector.GetValue(i).ToInt32()
                            };
                            curRelMainPartyCategory.Save();
                        }
                    }
                }
            }
            return base.RedirectToAction("Edit", new { @id = member.MainPartyId });
        }

        public ActionResult Detail(int id)
        {

            var curMember = new Classes.Member();
            curMember.LoadEntity(id);

            var dataAddress = new Data.Address();
            var dataPhone = new Data.Phone();

            var memberlistPhone = dataPhone.GetPhoneItemsByMainPartyId(id).AsCollection<PhoneModel>().ToList<PhoneModel>();

            MemberModel model = new MemberModel();
            UpdateClass(curMember, model);

            var dataCategory = new Data.Category();
            model.SectorItems = entities.Categories.Where(c => c.CategoryParentId == null);

            var dataRelMainPartyCategory = new Data.RelMainPartyCategory();
            model.MainPartyRelatedSectorItems = dataRelMainPartyCategory.GetMainPartyRelatedCategoryItemsByMainPartyId(id).AsCollection<RelMainPartyCategoryModel>();

            model.CategoryItems = CacheUtilities.GetCategories().AsCollection<CategoryModel>();

            return View(model);
        }

        public ActionResult Index()
        {
            PAGEID = PermissionPage.Uyeler;
            int total = 0;
            dataMember = new Data.Member();

            if (Session[SessionPage] == null)
            {
                Session[SessionPage] = PAGEDIMENSION;
            }

            collection = dataMember.Search(ref total, (int)Session[SessionPage], 1, string.Empty, STARTCOLUMN, ORDER).AsCollection<MemberModel>();

            foreach (var memberItem in collection)
            {
                if (memberItem.MemberType != (byte)MemberType.Enterprise)
                {
                    collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.MainPartyId);

                }
                else
                {
                    collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.StoreMainPartyId);

                }
            }

            var model = new FilterModel<MemberModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MemberModel model, string OrderName, string Order, int? Page, int PageDimension, string Gsm)
        {
            dataMember = dataMember ?? new Data.Member();

            var whereClause = new StringBuilder("Where");
            //var whereClause = new StringBuilder();
            string likeClaue = " {0} LIKE N'{1}%' ";
            string equalClause = " {0} = {1} ";
            bool op = false;

            if (!string.IsNullOrWhiteSpace(model.MainPartyFullName))
            {
                whereClause.AppendFormat(likeClaue, "MainPartyFullName", model.MainPartyFullName);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.MemberNo) && model.MemberNo.Length == 9)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "MemberNo", model.MemberNo);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.MemberEmail))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "MemberEmail", model.MemberEmail);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(Gsm) && Gsm.Length > 3)
            {
                Gsm = Gsm.Trim();
                Gsm = Gsm.Substring(3, Gsm.Length - 3);
                string phoneAreaCode = Gsm.Substring(0, 3);
                string phoneNumber = Gsm.Substring(3, Gsm.Length - 3);

                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "PhoneAreaCode", phoneAreaCode);
                whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "PhoneNumber", phoneNumber);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.StoreName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                op = true;
            }

            if (model.MemberType > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "MemberType", model.MemberType);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.Active_Text))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "M.Active", model.Active_Text == "true" ? 1 : 0);
                op = true;
            }

            if (model.MainPartyRecordDate != new DateTime())
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = " Cast(MainPartyRecordDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.MainPartyRecordDate.ToString("yyyyMMdd"));
            }


            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            int total = 0;
            Session[SessionPage] = PageDimension;
            collection =
              dataMember.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<MemberModel>();
            foreach (var memberItem in collection)
            {
                if (memberItem.MemberType != (byte)MemberType.Enterprise)
                {
                    collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.MainPartyId);

                }
                else
                {
                    collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.StoreMainPartyId);

                }
            }

            var filterItems = new FilterModel<MemberModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection
            };

            return View("MemberList", filterItems);
        }

        [HttpPost]
        public JsonResult PhoneMailSend(int id)
        {
            var member = entities.Members.FirstOrDefault(x => x.MainPartyId == id);
            var settings = ConfigurationManager.AppSettings;
            //var settings = entities.MessagesMTs.SingleOrDefault(c => c.MessagesMTId == 1);
            MailMessage mail = new MailMessage();
            MessagesMT mailMessage = entities.MessagesMTs.Where(x => x.MessagesMTName == "sifreolusturma").First();

            mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName);
            mail.To.Add(member.MemberEmail);
            mail.Subject = mailMessage.MessagesMTTitle;
            string actLink = "http://makinaturkiye.com/uyelik/sifreolustur/" + member.ActivationCode;
            string template = mailMessage.MessagesMTPropertie;
            var phone = entities.Phones.First(x => x.MainPartyId == id && x.PhoneType == (byte)PhoneType.Gsm);
            var errorMessage = entities.SendMessageErrors.First(x => x.SenderID == id);
            var product = entities.Products.First(x => x.ProductId == errorMessage.ProductID);
            template = template.Replace("#OnayLink#", actLink).Replace("#telefonno#", phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber).Replace("#ilanno#", product.ProductNo).Replace("#ilanadı#", product.ProductName);
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            this.SendMail(mail);
            BaseMemberDescription baseMember = new BaseMemberDescription();
            baseMember.Date = DateTime.Now;
            baseMember.MainPartyId = Convert.ToInt32(id);
            baseMember.Title = "Aktivasyon";
            baseMember.Description = "ŞO.M";
            baseMember.UpdateDate = null;
            entities.BaseMemberDescriptions.AddObject(baseMember);
            entities.SaveChanges();
            MemberDescription memberDesc = new MemberDescription();
            memberDesc.MainPartyId = Convert.ToInt32(id);
            memberDesc.Description = "ŞO.M";
            memberDesc.Title = "Aktivasyon";
            memberDesc.UpdateDate = null;
            memberDesc.Date = DateTime.Now;
            entities.MemberDescriptions.AddObject(memberDesc);
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);



        }

        public JsonResult Cities(int id)
        {
            var curCity = new Classes.City();
            var model = curCity.GetDataTable().AsCollection<CityModel>().Where(c => c.CountryId == id).ToList();
            model.Insert(0, new CityModel { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(model, "CityId", "CityName"));
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var mainParty = new Classes.MainParty();
            var member = entities.Members.Where(c => c.MainPartyId == id).SingleOrDefault();
            entities.DeleteObject(member);
            entities.SaveChanges();
            return Json(new { m = mainParty.Delete(id) });
        }

        [HttpPost]

        public JsonResult Activation(int id)
        {

            var ourmember = entities.Members.Where(c => c.MainPartyId == id).FirstOrDefault();
            if (ourmember.ActivationCode != null)
            {
                ActivationCodeSend(ourmember.MemberEmail, ourmember.ActivationCode, ourmember.MemberName + " " + ourmember.MemberSurname);
                SendMessageError(id);
                BaseMemberDescription baseMember = new BaseMemberDescription();
                baseMember.Date = DateTime.Now;
                baseMember.MainPartyId = Convert.ToInt32(id);
                baseMember.Title = "Aktivasyon";
                baseMember.Description = "AM.G";
                baseMember.UpdateDate = null;
                entities.BaseMemberDescriptions.AddObject(baseMember);
                entities.SaveChanges();

                MemberDescription memberDesc = new MemberDescription();
                memberDesc.MainPartyId = Convert.ToInt32(id);
                memberDesc.Description = "AM.G";
                memberDesc.Title = "Aktivasyon";
                memberDesc.UpdateDate = null;
                memberDesc.Date = DateTime.Now;
                entities.MemberDescriptions.AddObject(memberDesc);
                entities.SaveChanges();
            }
            return Json(true);
        }
        public void SendMessageError(int id)
        {
            var messageErrors = _messageService.GetSendMessageErrorsBySenderId(id);

            if (messageErrors.ToList().Count > 0)//gönderilmeyen tüm mailleri gönder
            {
                foreach (var messageItem in messageErrors)
                {
                    using (TransactionUI tran = new TransactionUI())
                    {

                        var message = new global::MakinaTurkiye.Entities.Tables.Messages.Message
                        {
                            Active = true,
                            MessageContent = messageItem.MessageContent,
                            MessageSubject = messageItem.MessageSubject,
                            MessageDate = DateTime.Now,
                            MessageRead = false,
                            ProductId = messageItem.ProductID,
                            MessageSeenAdmin = false
                        };
                        _messageService.InsertMessage(message);
                        int messageId = message.MessageId;
                        int mainPartyId = Convert.ToInt32(messageItem.ReceiverID);

                        var messageMainParty = new global::MakinaTurkiye.Entities.Tables.Messages.MessageMainParty
                        {
                            MainPartyId = (int)messageItem.SenderID,
                            MessageId = messageId,
                            InOutMainPartyId = mainPartyId,
                            MessageType = (byte)MessageType.Outbox
                        };
                        _messageService.InsertMessageMainParty(messageMainParty);

                        var curMessageMainParty = new global::MakinaTurkiye.Entities.Tables.Messages.MessageMainParty
                        {
                            InOutMainPartyId = (int)messageItem.SenderID,
                            MessageId = messageId,
                            MainPartyId = mainPartyId,
                            MessageType = (byte)MessageType.Inbox,
                        };
                        _messageService.InsertMessageMainParty(curMessageMainParty);
                        var receiverUser = _memberService.GetMemberByMainPartyId(mainPartyId);
                        if (messageItem.ProductID != 0)
                        {
                            #region messageissendbilgilendirme
                            var product = entities.Products.Where(c => c.ProductId == messageItem.ProductID).SingleOrDefault();
                            var kullaniciemail = entities.Members.Where(c => c.MainPartyId == messageItem.ReceiverID).SingleOrDefault();
                            string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                            string productName = product.ProductName.ToString();
                            var productno = entities.Products.Where(c => c.ProductId == messageItem.ProductID).SingleOrDefault().ProductNo;
                            var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
                            var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
                            var categorymodelname = "";

                            if (entities.Categories.FirstOrDefault(x => x.CategoryId == product.ModelId) != null)
                            {
                                categorymodelname = entities.Categories.Where(c => c.CategoryId == product.ModelId).SingleOrDefault().CategoryName;

                            }
                            string categorybrandname = "";
                            var brand = entities.Categories.SingleOrDefault(c => c.CategoryId == product.BrandId);
                            if (brand != null)
                            {
                                categorybrandname = brand.CategoryName;
                            }
                            DecyptHelper decyptHelper = new DecyptHelper();
                            var enciprtText = decyptHelper.Encrypt(mainPartyId.ToString());
                            var returnurl = "/Account/Message/Detail/" + messageId + "?RedirectMessageType=0";
                            string loginAutoLink = string.Format("/membership/LogonAuto?validateId={0}=&returnUrl={1}", enciprtText, returnurl);
                            string productnosub = productName + " " + categorybrandname + " " + categorymodelname + " İlan no:" + productno;
                            string productUrl = "http://www.makinaturkiye.com" + NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.ProductUrl(product.ProductId, productName);
                            MessagesMT mailTemplate = entities.MessagesMTs.First(x => x.MessagesMTName == "mesajınızvar");
                            //Mail konusu
                            string templatet = mailTemplate.MessagesMTPropertie;
                            templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", productno).Replace("#messagecontent#", messageItem.MessageContent).Replace("#loginautolink#", loginAutoLink);
                            MailHelper mailHelper = new MailHelper(productnosub, templatet, mailTemplate.Mail, mailadresifirma, mailTemplate.MailPassword, mailTemplate.MailSendFromName, AppSettings.MailHost, AppSettings.MailPort, AppSettings.MailSsl);
                            var memberStore = _memberstoreService.GetMemberStoreByMemberMainPartyId(messageItem.ReceiverID);
                            if (memberStore != null)
                            {
                                var memberMainPartyIds = _memberstoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Where(x => x.MemberMainPartyId != id).Select(x => x.MemberMainPartyId).ToList();
                                var members = _memberService.GetMembersByMainPartyIds(memberMainPartyIds).Select(x => x.MemberEmail).ToList();
                                members.ForEach(x => mailHelper.ToMails.Add(x));
                            }
                            mailHelper.Send();

                            #endregion
                            _messageService.DeleteSendMessageError(messageItem);

                        }
                    }
                }
            }
        }
        public void ActivationCodeSend(string Email, string activationCode, string memberNameSurname)
        {
            var settings = ConfigurationManager.AppSettings;
            //var settings = entities.MessagesMTs.SingleOrDefault(c => c.MessagesMTId == 1);
            MailMessage mail = new MailMessage();
            MessagesMT mailMessage = entities.MessagesMTs.Where(x => x.MessagesMTName == "Aktivasyon").First();

            mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName);
            mail.To.Add(Email);
            mail.Subject = mailMessage.MessagesMTTitle;
            string actLink = "http://makinaturkiye.com/Uyelik/Aktivasyon/" + activationCode;
            string template = mailMessage.MessagesMTPropertie;
            template = template.Replace("#OnayKodu#", activationCode).Replace("#OnayLink#", actLink).Replace("#uyeadisoyadi#", memberNameSurname);
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            this.SendMail(mail);
        }

        [HttpPost]
        public JsonResult Localities(int id)
        {
            var dataAddress = new Data.Address();
            var items = dataAddress.LocalityGetItemByCityId(id).AsCollection<LocalityModel>().OrderBy(c => c.LocalityName).ToList();
            items.Insert(0, new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(items, "LocalityId", "LocalityName"));
        }

        [HttpPost]
        public JsonResult Towns(int id)
        {
            var townItems = entities.Towns.Where(c => c.LocalityId.Value == id).OrderBy(c => c.TownName).ToList();
            townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(townItems, "TownId", "TownName"));
        }

        public ActionResult ZipCode(int DistrictId)
        {
            var district = entities.Districts.SingleOrDefault(c => c.DistrictId == DistrictId);
            if (district != null)
            {
                return Json(district.ZipCode);
            }
            return Json("Bulunamadı");
        }

        public ActionResult AreaCode(string CityId)
        {
            int id = CityId.ToInt32();
            var city = entities.Cities.SingleOrDefault(c => c.CityId == id);
            if (city != null)
            {
                return Content(city.AreaCode);
            }
            else
                return Content("");
        }

        public ActionResult CultureCode(string CountryId)
        {
            int id = CountryId.ToInt32();
            var country = entities.Countries.SingleOrDefault(c => c.CountryId == id);
            if (country != null)
            {
                return Content(country.CultureCode);
            }
            else
                return Content("");
        }

        public ActionResult BrowseDesc(int id)
        {

            var memstore = from c in entities.MemberStores
                           select c;
            IList<MemberDescription> md = (from c in entities.MemberDescriptions
                                           where c.MainPartyId == id
                                           orderby c.Date descending
                                           select c).ToList();
            TempData["id"] = id;
            var member = (from c in entities.Members
                          where c.MainPartyId == id
                          select c).SingleOrDefault();
            ViewData["mem"] = member.MemberName.ToString() + " " + member.MemberSurname;
            ViewData["mi"] = member.MainPartyId;
            return View(md);
        }
        public ActionResult BrowSeDesc1(int id)
        {
            var MemberDescription = _memberDescService.GetByMainPartyIDOrderByColumn("Date", id);
            var model = new BrowseDescModel();
            List<BaseMemberDescriptionModelItem> memberDescList = new List<BaseMemberDescriptionModelItem>();
            var Member = entities.Members.FirstOrDefault(x => x.MainPartyId == id);
            string memberName = "";
            string memberSurname = "";
            string storeName = "";
            if (Member == null)
            {
                var preRegistration = _preRegistrationStoreService.GetPreRegistirationStoreByPreRegistrationStoreId(id);
                memberName = preRegistration.MemberName;
                memberSurname = preRegistration.MemberSurname;
                storeName = preRegistration.StoreName;
                model.PreRegistrationStoreId = preRegistration.PreRegistrationStoreId;
                model.RegistrationType = (byte)RegistrationType.Pre;
            }
            else
            {
                memberName = Member.MemberName;
                memberSurname = Member.MemberSurname;
                model.MemberMainPartyId = Member.MainPartyId;
                model.RegistrationType = (byte)RegistrationType.Full;

            }
            var storeM = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == id);
            if (storeM != null)
            {
                var store = entities.Stores.First(x => x.MainPartyId == storeM.StoreMainPartyId);

                model.IsProductAdded = store.IsProductAdded;

                storeName = store.StoreName;
                model.StoreMainPartyId = store.MainPartyId;
                var curAuth = entities.Users.FirstOrDefault(x => x.UserId == store.AuthorizedId);
                if (curAuth != null)
                {
                    model.AuthName = curAuth.UserName;
                }
                else
                {
                    var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.GroupName == "Portföy Yöneticisi" select new { u.UserName, u.UserId };
                    //var users = entities.Users.OrderBy(x => x.UserName).ToList();
                    var users = users1.ToList();

                    model.Users.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
                    foreach (var item in users)
                    {

                        var selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };

                        model.Users.Add(selectListItem);
                    }
                }

            }

            model.MemberNameSurname = memberName + " " + memberSurname;
            model.StoreName = storeName;
            foreach (var item in MemberDescription)
            {
                BaseMemberDescriptionModelItem memberDesc = new BaseMemberDescriptionModelItem
                {
                    ID = Convert.ToInt32(item.descId),
                    InputDate = item.Date,
                    Title = item.Title,
                    DescriptionDegree = item.DescriptionDegree

                };

                if (item.Description != null)
                {
                    memberDesc.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                }


                if (item.UpdateDate != null)
                {
                    memberDesc.LastDate = Convert.ToDateTime(item.UpdateDate);
                }
                if (model.RegistrationType == (byte)RegistrationType.Full)
                {
                    memberDesc.MainPartyId = item.MainPartyId.HasValue ? item.MainPartyId.Value : 0;
                    var anyStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == item.MainPartyId);
                    if (anyStore != null)
                        memberDesc.StoreID = anyStore.StoreMainPartyId;
                }

                var userManager = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                if (userManager != null)
                    memberDesc.UserName = userManager.UserName;
                var userTo = entities.Users.FirstOrDefault(x => x.UserId == item.UserId);
                if (userTo != null)
                    memberDesc.ToUserName = userTo.UserName;
                //,RestAmount=(select top 1 RestAmount from Payment as P where P.OrderId=O.OrderId order by RecordDate desc) - ISNULL((select sum(Amount) from ReturnInvoice where OrderId=O.OrderId), 0)
                decimal RestAmount = 0;
                int totalRecord = 0;
                int totalPrice = 0;
                int totalPaid = 0;
                memberDesc.OrderReport= _orderService.GetOrderReports(25, 1, "", "", "desc", out totalRecord, out totalPrice, out totalPaid).OrderByDescending(x=>x.RecordDateOrder).FirstOrDefault();
                memberDescList.Add(memberDesc);
            }

            model.BaseMemberDescriptionModelItems = memberDescList;
            return View(model);
        }

        [HttpPost]
        public ActionResult BrowseDesc1(int AuthorizedId, int StoreMainPartyId, int MemberMainPartyId)
        {
            var store = _storeService.GetStoreByMainPartyId(StoreMainPartyId);
            store.AuthorizedId = AuthorizedId;
            _storeService.UpdateStore(store);
            return RedirectToAction("BrowseDesc1", new { id = MemberMainPartyId });

        }
        [HttpPost]
        public JsonResult DeleteDescription(int id)
        {
            var isAllowDelete = IsAccess(PermissionPage.DeleteMemberDescription);
            ResponseModel<string> res = new ResponseModel<string>();
            //if (CurrentUserModel.CurrentManagement.Permissions != null)
            //{
            //    isAllowDelete=  CurrentUserModel.CurrentManagement.Permissions.Any(c => c.PermissionId == PermissionPage.DeleteMemberDescription);
            //}
            if (isAllowDelete)
            {
                var memberDescDelete = entities.MemberDescriptions.First(x => x.descId == id);
                int MainPartyID = Convert.ToInt32(memberDescDelete.MainPartyId);
                var BaseMemberDesc = entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == memberDescDelete.MainPartyId && x.Title == memberDescDelete.Title);
                if (BaseMemberDesc != null)
                {
                    entities.MemberDescriptions.DeleteObject(memberDescDelete);
                    entities.SaveChanges();
                    var lastMemberDesc = entities.MemberDescriptions.Where(x => x.MainPartyId == MainPartyID).OrderByDescending(x => x.Date).FirstOrDefault();
                    if (lastMemberDesc != null)
                    {
                        BaseMemberDesc.Description = lastMemberDesc.Description;
                        BaseMemberDesc.Date = lastMemberDesc.Date;
                        BaseMemberDesc.DescriptionDegree = lastMemberDesc.DescriptionDegree;
                        BaseMemberDesc.Title = lastMemberDesc.Title;
                        BaseMemberDesc.UpdateDate = lastMemberDesc.UpdateDate;
                        entities.SaveChanges();
                    }
                    else
                    {
                        entities.BaseMemberDescriptions.DeleteObject(BaseMemberDesc);
                        entities.SaveChanges();
                    }
                }
                else
                {
                    var memberDescriptionsLog = entities.MemberDescription_log.Where(x => x.descId == memberDescDelete.descId);
                    foreach (var item in memberDescriptionsLog)
                    {
                        entities.MemberDescription_log.DeleteObject(item);

                    }
                    entities.SaveChanges();
                    entities.MemberDescriptions.DeleteObject(memberDescDelete);
                    entities.SaveChanges();
                }

                res.IsSuccess = true;
                res.Message = "Üye Açıklaması Başarıyla Silinmiştir.";
            }
            else
            {
                res.IsSuccess = false;
                res.Message = "Bu işlemi yapmaya izniniz bulunmamaktadır";


            }

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreateDesc()
        {
            TempData["id2"] = TempData["id"];
            ViewData["mid"] = TempData["id"];
            return View();
        }

        [HttpPost]
        public ActionResult CreateDesc(MemberDescription me)
        {
            int? id;
            MemberDescription memd = new MemberDescription();
            if (TempData["id2"] == null)
                return RedirectToAction("Index");
            else
            {
                id = (int)TempData["id2"];
            }
            memd.MainPartyId = (int)TempData["id2"];
            memd.Date = DateTime.Now;
            memd.Description = me.Description;
            memd.Title = me.Title;
            memd.UpdateDate = null;
            memd.UserId = CurrentUserModel.CurrentManagement.UserId;

            entities.MemberDescriptions.AddObject(memd);
            entities.SaveChanges();
            return RedirectToAction("BrowseDesc", new RouteValueDictionary(
      new { controller = "Member", action = "BrowseDesc", id = id }));


        }

        public ActionResult CreateDesc1(int id, int? descId, byte? regType)
        {
            if (descId != null)
            {
                ViewData["descId"] = descId;

            }
            ViewData["mainPartyId"] = id;
            BaseMemberDescriptionModel model = new BaseMemberDescriptionModel();


            if (regType != null)
            {
                model.RegistrationType = Convert.ToByte(regType);
                model.RegistrationStoreId = id;
            }
            else
            {
                model.RegistrationType = (byte)RegistrationType.Full;
            }

            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();
            foreach (var item in constants)
            {
                var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };

                model.ConstantModel.Add(selectListItem);
            }
            var users = entities.Users.Where(u => u.ActiveForDesc == true).OrderBy(x => x.UserName).ToList();
            model.ConstantModel.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
            foreach (var item in users)
            {
                SelectListItem selectList = new SelectListItem();

                if (Convert.ToInt32(CurrentUserModel.CurrentManagement.UserId) == item.UserId)
                    selectList.Selected = true;
                selectList.Value = item.UserId.ToString();
                selectList.Text = item.UserName;
                model.Users.Add(selectList);
            }
            model.LastDate = null;
            model.IsFirst = false;
            model.IsImmediate = false;
            return View(model);

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateDesc1(BaseMemberDescriptionModel model, string userId, string mainPartyId, string time, string LastDate, string descId, string constantId)
        {
            if (!constantId.Equals("0"))
            {
                int constantID = Convert.ToInt32(constantId);

                if (constantID == 412)
                {
                    var memberStore = _memberstoreService.GetMemberStoreByMemberMainPartyId(model.MainPartyId);
                    if (memberStore != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                        if (store != null)
                        {
                            store.AuthorizedId = CurrentUserModel.CurrentManagement.UserId;
                            _storeService.UpdateStore(store);
                        }
                    }
                }

                var constant = entities.Constants.FirstOrDefault(x => x.ConstantId == constantID);
                if (!string.IsNullOrEmpty(model.Description))
                {

                    int id = 0;
                    if (int.TryParse(mainPartyId, out id))
                    {
                        if (string.IsNullOrEmpty(LastDate))
                        {
                            LastDate = DateTime.Now.Date.ToString("dd.MM.yyyy");
                        }

                        if (!string.IsNullOrEmpty(LastDate))
                        {

                            int year = 0, day = 0, month = 0;
                            int hour = 0, minute = 0;
                            DateTime lastDate = new DateTime();
                            string[] time1 = LastDate.ToString().Split('.');
                            year = Convert.ToInt32(time1[2]);
                            month = Convert.ToInt32(time1[1]);
                            day = Convert.ToInt32(time1[0]);


                            if (!string.IsNullOrEmpty(time))
                            {
                                hour = Convert.ToInt32(time.Split(':')[0]);
                                minute = Convert.ToInt32(time.Split(':')[1]);
                            }
                            else
                            {
                                hour = DateTime.Now.Hour;
                                minute = DateTime.Now.Minute + 1;

                                if (day != DateTime.Now.Day)
                                {
                                    hour = 8;
                                    minute = 30;
                                }
                            }

                            lastDate = new DateTime(year, month, day, hour, minute, 0);
                            if (lastDate > DateTime.Now)
                            {
                                #region basemember

                                var AnyDesc = entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == id);
                                if (AnyDesc != null)
                                {
                                    AnyDesc.Date = DateTime.Now;
                                    AnyDesc.Title = constant.ConstantName;
                                    AnyDesc.Description = model.Description;
                                    AnyDesc.ConstantId = constantID;
                                    AnyDesc.UpdateDate = lastDate;
                                    entities.SaveChanges();
                                }
                                else
                                {
                                    if (model.RegistrationType == (byte)RegistrationType.Full)
                                    {

                                        var baseMemberDesc = new BaseMemberDescription();

                                        baseMemberDesc.MainPartyId = id;
                                        baseMemberDesc.Title = constant.ConstantName;
                                        baseMemberDesc.Description = model.Description;
                                        baseMemberDesc.Date = DateTime.Now;
                                        baseMemberDesc.UserId = CurrentUserModel.CurrentManagement.UserId;
                                        baseMemberDesc.UpdateDate = lastDate;
                                        baseMemberDesc.ConstantId = constantID;

                                        entities.BaseMemberDescriptions.AddObject(baseMemberDesc);
                                        entities.SaveChanges();
                                    }

                                }
                                #endregion

                                MemberDescription memberDesc = new MemberDescription();
                                memberDesc.Date = DateTime.Now;
                                if (model.RegistrationType == (byte)RegistrationType.Full)
                                    memberDesc.MainPartyId = id;
                                else
                                    memberDesc.PreRegistrationStoreId = model.RegistrationStoreId;

                                memberDesc.UpdateDate = lastDate;
                                memberDesc.Title = constant.ConstantName;
                                memberDesc.Description = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + model.Description + "-" + "<span style='color:#44000d'>" + CurrentUserModel.CurrentManagement.UserName + "</span>";
                                memberDesc.Status = 0;
                                memberDesc.ConstantId = Convert.ToInt32(constantId);
                                memberDesc.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                                memberDesc.UserId = Convert.ToInt32(userId);
                                memberDesc.IsFirst = model.IsFirst;
                                memberDesc.IsImmediate = model.IsImmediate;
                                entities.MemberDescriptions.AddObject(memberDesc);
                                entities.SaveChanges();
                                AddMemberDescriptionLog(memberDesc, "I");
                                return RedirectToAction("BrowseDesc1", new RouteValueDictionary(
                                  new { controller = "Member", action = "BrowseDesc1", id = id }));
                            }
                            else
                            {
                                TempData["idCreate"] = id;
                                ModelState.AddModelError("LastDate", "Girilen Tarih Ve Saat Şu an ki Tarihten Küçük Olamaz");
                                PrepareBaseMemberDescriptionModel(model);
                                return View(model);
                            }
                        }
                        else
                        {

                            PrepareBaseMemberDescriptionModel(model);
                            ModelState.AddModelError("LastDate", "Lütfen Tarih Giriniz");

                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Description", "Lütfen Açıklama Giriniz");
                        var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();
                        foreach (var item in constants)
                        {
                            var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };

                            model.ConstantModel.Add(selectListItem);
                        }
                        var users = entities.Users.Where(x => x.ActiveForDesc == true).OrderBy(x => x.UserName).ToList();
                        model.ConstantModel.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
                        foreach (var item in users)
                        {
                            SelectListItem selectList = new SelectListItem();

                            if (Convert.ToInt32(CurrentUserModel.CurrentManagement.UserId) == item.UserId)
                                selectList.Selected = true;
                            selectList.Value = item.UserId.ToString();
                            selectList.Text = item.UserName;
                            model.Users.Add(selectList);
                        }
                        return View(model);

                    }
                }
                else
                {
                    PrepareBaseMemberDescriptionModel(model);
                    ModelState.AddModelError("Description", "Lütfen Açıklama Giriniz");
                    return View(model);

                }
            }
            else
            {
                PrepareBaseMemberDescriptionModel(model);
                ViewData["Error"] = "Lütfen Başlık Seçiniz";
                return View(model);
            }


        }
        public void PrepareBaseMemberDescriptionModel(BaseMemberDescriptionModel model)
        {
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).ToList();
            foreach (var item in constants)
            {
                var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };

                model.ConstantModel.Add(selectListItem);
            }
            var users = entities.Users.OrderBy(x => x.UserName).ToList();
            model.ConstantModel.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
            foreach (var item in users)
            {
                SelectListItem selectList = new SelectListItem();

                if (Convert.ToInt32(CurrentUserModel.CurrentManagement.UserId) == item.UserId)
                    selectList.Selected = true;
                selectList.Value = item.UserId.ToString();
                selectList.Text = item.UserName;
                model.Users.Add(selectList);
            }
        }

        public ActionResult DeleteDesc(int? id)
        {
            MemberDescription md = (from c in entities.MemberDescriptions
                                    where c.descId == id
                                    select c).SingleOrDefault();
            TempData["id3"] = TempData["id"];
            ViewData["mpid"] = TempData["id"];
            return View(md);
        }

        [HttpPost]
        public ActionResult DeleteDesc(int? id, FormCollection collection)
        {
            try
            {
                int ids = (int)TempData["id3"];

                MemberDescription md = (from c in entities.MemberDescriptions
                                        where c.descId == id
                                        select c).SingleOrDefault();
                entities.MemberDescriptions.DeleteObject(md);
                entities.SaveChanges();
                return RedirectToAction("BrowseDesc", new RouteValueDictionary(
        new { controller = "Member", action = "BrowseDesc", id = ids }));


            }
            catch
            {
                MemberDescription md = (from c in entities.MemberDescriptions
                                        where c.descId == id
                                        select c).SingleOrDefault();
                return View(md);
            }
        }

        public ActionResult eEditDesc(int id)
        {
            var md = (from c in entities.MemberDescriptions
                      where c.descId == id
                      select c).SingleOrDefault();
            TempData["memberDescId"] = TempData["id"];
            ViewData["id4"] = TempData["id"];
            List<SelectListItem> usersList = new List<SelectListItem>();

            return View(md);
        }
        [HttpPost]
        public ActionResult EditDesc(int id, FormCollection collection)
        {
            MemberDescription md = (from c in entities.MemberDescriptions
                                    where c.descId == id
                                    select c).SingleOrDefault();

            if (ModelState.IsValid)
            {
                md.UpdateDate = DateTime.Now;
                UpdateModel(md);
                entities.SaveChanges();
            }
            if (TempData["id4"] == null)
            {
                return RedirectToAction("Index");
            }
            int ids = (int)TempData["id4"];
            return RedirectToAction("BrowseDesc", new RouteValueDictionary(
      new { controller = "Member", action = "BrowseDesc", id = ids }));


        }
        public ActionResult EditDesc1(int id)
        {
            var MemberDesc = entities.MemberDescriptions.First(x => x.descId == id);

            BaseMemberDescriptionModel bModelDesc = new BaseMemberDescriptionModel();
            if (!MemberDesc.MainPartyId.HasValue)
                if (MemberDesc.PreRegistrationStoreId.HasValue)
                    bModelDesc.RegistrationStoreId = MemberDesc.PreRegistrationStoreId.Value;

            bModelDesc.Title = MemberDesc.Title;
            bModelDesc.Description = MemberDesc.Description.Replace("~", "");

            if (MemberDesc.UpdateDate != null)
            {
                bModelDesc.LastDate = Convert.ToDateTime(MemberDesc.UpdateDate);
                ViewData["time"] = bModelDesc.LastDate.ToString().Split(' ')[1];
            }
            bModelDesc.MainPartyId = Convert.ToInt32(MemberDesc.MainPartyId);
            bModelDesc.InputDate = Convert.ToDateTime(MemberDesc.Date);
            bModelDesc.ID = MemberDesc.descId;
            if (MemberDesc.ConstantId != null)
                bModelDesc.ConstantId = MemberDesc.ConstantId.ToString();
            bModelDesc.StoreName = "";
            bModelDesc.IsFirst = false;
            bModelDesc.IsImmediate = false;

            string memberName = "", memberSurname = "", storeName = "";
            if (MemberDesc.MainPartyId.HasValue)
            {
                var anyStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == MemberDesc.MainPartyId);
                if (anyStore != null)
                    bModelDesc.StoreID = anyStore.StoreMainPartyId;
                var anyStore1 = entities.Stores.FirstOrDefault(x => x.MainPartyId == anyStore.StoreMainPartyId);
                if (anyStore1 != null)
                {
                    storeName = anyStore1.StoreName;
                    var storeUserManager = entities.Users.FirstOrDefault(x => x.UserId == anyStore1.AuthorizedId);
                    var storePorfoy = entities.Users.FirstOrDefault(x => x.UserId == anyStore1.PortfoyUserId);
                    if (storeUserManager != null)
                    {
                        bModelDesc.AuthorizeName = storeUserManager.UserName;

                    }
                    if (storePorfoy != null)
                        bModelDesc.PortfoyName = storePorfoy.UserName;
                }
            }


            var member = entities.Members.FirstOrDefault(x => x.MainPartyId == MemberDesc.MainPartyId);
            if (member != null)
            {
                memberName = member.MemberName;
                memberSurname = member.MemberSurname;
            }
            else
            {
                if (MemberDesc.PreRegistrationStoreId.HasValue)
                {
                    var preRegistration = _preRegistrationStoreService.GetPreRegistirationStoreByPreRegistrationStoreId(MemberDesc.PreRegistrationStoreId.Value);
                    memberName = preRegistration.MemberName;
                    memberSurname = preRegistration.MemberSurname;
                    storeName = preRegistration.StoreName;
                }

            }
            bModelDesc.StoreName = storeName;
            ViewData["MemberName"] = memberName + " " + memberSurname;
            if (MemberDesc.Status == null) ViewData["status"] = "0";
            else ViewData["status"] = MemberDesc.Status.ToString();
            List<BaseMemberDescriptionModel> memberDescriptionsOther = new List<BaseMemberDescriptionModel>();
            var otherMemberDescs = entities.MemberDescriptions.Where(x => x.MainPartyId == bModelDesc.MainPartyId).OrderByDescending(x => x.descId).ToList();
            if (otherMemberDescs.Count() == 0)
                otherMemberDescs = entities.MemberDescriptions.Where(x => x.PreRegistrationStoreId == bModelDesc.RegistrationStoreId).OrderByDescending(x => x.descId).ToList();
            foreach (var item in otherMemberDescs)
            {
                BaseMemberDescriptionModel otherItem = new BaseMemberDescriptionModel();
                otherItem.ID = item.descId;
                otherItem.InputDate = item.Date.ToDateTime();
                if (item.UpdateDate != null) otherItem.LastDate = Convert.ToDateTime(item.UpdateDate);
                otherItem.MainPartyId = item.MainPartyId.ToInt32();
                var memberSub = entities.Members.FirstOrDefault(m => m.MainPartyId == item.MainPartyId);

                if (memberSub != null && memberSub.MainPartyId != 0)
                {
                    var memberStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == item.MainPartyId);
                    otherItem.Title = item.Title;
                    if (item.Description != null)
                    {

                        otherItem.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                        otherItem.StoreID = memberStore.StoreMainPartyId;
                    }
                }
                else
                {
                    var preRegistration = _preRegistrationStoreService.GetPreRegistirationStoreByPreRegistrationStoreId(MemberDesc.PreRegistrationStoreId.Value);
                    otherItem.StoreName = preRegistration.StoreName;
                    otherItem.Member.MemberName = preRegistration.MemberName;
                    otherItem.Member.MemberSurname = preRegistration.MemberSurname;
                }

                var user = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                if (user != null)
                    otherItem.UserName = user.UserName;

                var userTo = entities.Users.FirstOrDefault(x => x.UserId == item.UserId);
                if (userTo != null)
                {
                    otherItem.ToUserName = userTo.UserName;
                }
                memberDescriptionsOther.Add(otherItem);
            }
            bModelDesc.BaseMemberDescriptionByUser = memberDescriptionsOther;
            var users = entities.Users.Where(x => x.ActiveForDesc == true).OrderBy(x => x.UserName).ToList();
            foreach (var item in users)
            {
                SelectListItem selectList = new SelectListItem();
                if (MemberDesc.UserId != null)
                {
                    if (CurrentUserModel.CurrentManagement.UserId == item.UserId)
                        selectList.Selected = true;

                }
                selectList.Value = item.UserId.ToString();
                selectList.Text = item.UserName;
                bModelDesc.Users.Add(selectList);
            }
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();
            //bModelDesc.ConstantModel.Add(new SelectListItem { Text="Seçiniz",Value="0",Selected=true});
            foreach (var item in constants)
            {

                if (item.ConstantName != "Ödeme")
                {
                    var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };

                    if (item.ConstantId == MemberDesc.ConstantId)
                    {
                        selectListItem.Selected = true;
                    }
                    bModelDesc.ConstantModel.Add(selectListItem);
                }

            }
            string constantId = MemberDesc.ConstantId.ToString();
            var subConstants = entities.Constants.Where(x => x.ConstantTitle == constantId);
            foreach (var item in subConstants)
            {
                bModelDesc.SubConstants.Add(item.ContstantPropertie);
            }

            return View(bModelDesc);
        }





        [HttpPost, ValidateInput(false)]
        public ActionResult EditDesc1(BaseMemberDescriptionModel model, string time, string constantId, string DescriptionNew, string LastDate, string userId, string LastDateNew, string timeNew)
        {


            if (!string.IsNullOrEmpty(constantId) && constantId != "0" && !string.IsNullOrEmpty(DescriptionNew))
            {
                int constandID = Convert.ToInt32(constantId);

                if (constandID == 412)
                {
                    var memberStore = _memberstoreService.GetMemberStoreByMemberMainPartyId(model.MainPartyId);
                    if (memberStore != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                        if (store != null)
                        {
                            store.AuthorizedId = CurrentUserModel.CurrentManagement.UserId;
                            _storeService.UpdateStore(store);
                        }
                    }
                }
                var constant = entities.Constants.FirstOrDefault(x => x.ConstantId == constandID);
                model.Title = constant.ConstantName;
                var MemberDescUp = entities.MemberDescriptions.First(x => x.descId == model.ID);
                int tempConstantId = 0;
                if (MemberDescUp.ConstantId != null)
                    tempConstantId = MemberDescUp.ConstantId.Value;
                DateTime lastDate = new DateTime();
                if (string.IsNullOrEmpty(LastDateNew))
                {
                    LastDateNew = DateTime.Now.Date.ToString("dd.MM.yyyy");
                }
                if (!string.IsNullOrEmpty(LastDateNew))
                {
                    int year = 0, day = 0, month = 0;
                    string[] time1 = LastDateNew.ToString().Split('.');

                    year = Convert.ToInt32(time1[2]);
                    month = Convert.ToInt32(time1[1]);
                    day = Convert.ToInt32(time1[0]);

                    int hour = 0, minute = 0;
                    if (!string.IsNullOrEmpty(timeNew))
                    {
                        hour = Convert.ToInt32(timeNew.Split(':')[0]);
                        minute = Convert.ToInt32(timeNew.Split(':')[1]);

                    }
                    else
                    {
                        hour = DateTime.Now.Hour;
                        minute = DateTime.Now.Minute + 1;

                        if (day != DateTime.Now.Day)
                        {
                            hour = 8;
                            minute = 30;
                        }

                    }



                    lastDate = new DateTime(year, month, day, hour, minute, 0);
                    if (lastDate > DateTime.Now)
                    {
                        string userIdMember = "0";

                        if (MemberDescUp.UserId != null)
                        {
                            userIdMember = Convert.ToString(MemberDescUp.UserId);
                        }
                        var AnyDesc = entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == MemberDescUp.MainPartyId);
                        if (AnyDesc != null)
                        {
                            AnyDesc.Date = DateTime.Now;
                            AnyDesc.Title = constant.ConstantName;
                            if (!string.IsNullOrEmpty(DescriptionNew))
                                AnyDesc.Description = DescriptionNew;
                            else
                                AnyDesc.Description = model.Description;
                            AnyDesc.UpdateDate = lastDate;
                            AnyDesc.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                            AnyDesc.ConstantId = constandID;

                        }
                        if (tempConstantId != constandID)
                        {
                            MemberDescUp.Status = 1;
                            MemberDescUp.UpdateDate = null;
                        }
                        else
                        {
                            if (AnyDesc != null)
                                MemberDescUp.BaseID = AnyDesc.ID;

                            MemberDescUp.Status = 0;
                            DescriptionNew = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + DescriptionNew + "-" + "<span style='color:#44000d; font-weight:600;'>" + CurrentUserModel.CurrentManagement.UserName + "</span>" + "~" + MemberDescUp.Description;
                            MemberDescUp.Description = DescriptionNew;
                            MemberDescUp.UpdateDate = lastDate;
                            MemberDescUp.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                            MemberDescUp.UserId = Convert.ToInt32(userId);
                            MemberDescUp.Date = DateTime.Now;
                            MemberDescUp.IsFirst = model.IsFirst;
                            MemberDescUp.IsImmediate = model.IsImmediate;
                            AddMemberDescriptionLog(MemberDescUp, "U");
                        }
                        entities.SaveChanges();
                        int redirectId = model.MainPartyId;
                        if (redirectId == 0) redirectId = MemberDescUp.PreRegistrationStoreId.Value;
                        if (tempConstantId != constandID)
                        {
                            var memberDescNew = new MemberDescription();
                            if (AnyDesc != null)
                                memberDescNew.BaseID = AnyDesc.ID;
                            memberDescNew.ConstantId = Convert.ToInt32(constantId);
                            memberDescNew.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                            memberDescNew.UserId = Convert.ToInt32(userId);
                            memberDescNew.Status = 0;
                            memberDescNew.Title = constant.ConstantName;
                            memberDescNew.Date = DateTime.Now;
                            memberDescNew.UpdateDate = lastDate;
                            if (model.MainPartyId != 0)
                                memberDescNew.MainPartyId = model.MainPartyId;
                            if (model.RegistrationStoreId != 0)
                                memberDescNew.PreRegistrationStoreId = model.RegistrationStoreId;
                            memberDescNew.IsFirst = model.IsFirst;
                            memberDescNew.IsImmediate = model.IsImmediate;
                            if (!string.IsNullOrEmpty(DescriptionNew))
                                memberDescNew.Description = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + DescriptionNew + "<span style='color:#44000d'>" + CurrentUserModel.CurrentManagement.UserName + "</span>";
                            else
                                memberDescNew.Description = model.Description;
                            entities.MemberDescriptions.AddObject(memberDescNew);
                            entities.SaveChanges();

                            AddMemberDescriptionLog(memberDescNew, "U");
                        }


                        return RedirectToAction("BrowseDesc1", new RouteValueDictionary(
new { controller = "Member", action = "BrowseDesc1", id = redirectId }));

                    }
                    else
                    {
                        ModelState.AddModelError("date", "Hatırlatma Tarihi şu anki tarihten küçük olamaz");
                    }
                }

            }
            else
            {
                if (string.IsNullOrEmpty(DescriptionNew))
                {
                    ModelState.AddModelError("descriptiona", "Yeni Açıklama Girmelisiniz");

                }
                if (constantId == "0")
                    ModelState.AddModelError("subject", "Konu Seçiniz");
            }
            if (string.IsNullOrEmpty(LastDateNew))
                ModelState.AddModelError("date", "Hatırlatma Tarihi Boş Geçilemez");
            var MemberDesc = entities.MemberDescriptions.First(x => x.descId == model.ID);
            BaseMemberDescriptionModel bModelDesc = new BaseMemberDescriptionModel();
            bModelDesc.Title = MemberDesc.Title;
            bModelDesc.Description = model.Description;
            bModelDesc.DescriptionNew = DescriptionNew;

            if (MemberDesc.UpdateDate != null)
            {
                bModelDesc.LastDate = Convert.ToDateTime(MemberDesc.UpdateDate);
                ViewData["time"] = bModelDesc.LastDate.ToString().Split(' ')[1];
            }
            bModelDesc.MainPartyId = Convert.ToInt32(MemberDesc.MainPartyId);
            bModelDesc.InputDate = Convert.ToDateTime(MemberDesc.Date);
            bModelDesc.ID = MemberDesc.descId;
            bModelDesc.StoreName = "";
            var anyStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == MemberDesc.MainPartyId);
            if (anyStore != null)
                bModelDesc.StoreID = anyStore.StoreMainPartyId;
            var anyStore1 = entities.Stores.FirstOrDefault(x => x.MainPartyId == anyStore.StoreMainPartyId);
            if (anyStore1 != null)
            {
                bModelDesc.StoreName = anyStore1.StoreName;
                var userManager = entities.Users.FirstOrDefault(x => x.UserId == anyStore1.AuthorizedId);
                if (userManager != null)
                    bModelDesc.AuthorizeName = userManager.UserName;
            }

            var member = entities.Members.First(x => x.MainPartyId == MemberDesc.MainPartyId);
            ViewData["MemberName"] = member.MemberName + " " + member.MemberSurname;
            if (MemberDesc.Status == null) ViewData["status"] = "0";
            else ViewData["status"] = MemberDesc.Status.ToString();
            List<BaseMemberDescriptionModel> memberDescriptionsOther = new List<BaseMemberDescriptionModel>();
            var otherMemberDescs = entities.MemberDescriptions.Where(x => x.MainPartyId == bModelDesc.MainPartyId).OrderByDescending(x => x.descId).ToList();
            foreach (var item in otherMemberDescs)
            {
                BaseMemberDescriptionModel otherItem = new BaseMemberDescriptionModel();
                otherItem.ID = item.descId;
                otherItem.InputDate = item.Date.ToDateTime();
                if (item.UpdateDate != null) otherItem.LastDate = Convert.ToDateTime(item.UpdateDate);
                otherItem.MainPartyId = item.MainPartyId.ToInt32();
                otherItem.Member = entities.Members.First(m => m.MainPartyId == item.MainPartyId);
                var memberStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == item.MainPartyId);
                if (memberStore != null)
                {
                    otherItem.StoreID = memberStore.StoreMainPartyId;
                    otherItem.StoreName = entities.Stores.First(x => x.MainPartyId == memberStore.StoreMainPartyId).StoreName;
                }
                otherItem.Title = item.Title;
                if (item.Description != null)
                {
                    if (item.Description.Contains("<div"))
                    {
                        otherItem.Description = item.Title;
                    }
                    else
                    {
                        otherItem.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                    }

                }
                var fromUser = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                if (fromUser != null)
                    otherItem.UserName = fromUser.UserName;
                var userTo = entities.Users.FirstOrDefault(x => x.UserId == item.UserId);
                if (userTo != null)
                {
                    otherItem.ToUserName = userTo.UserName;
                }
                memberDescriptionsOther.Add(otherItem);
            }
            bModelDesc.BaseMemberDescriptionByUser = memberDescriptionsOther;
            var users = entities.Users.Where(x => x.ActiveForDesc == true).OrderBy(x => x.UserName).ToList();
            foreach (var item in users)
            {
                SelectListItem selectList = new SelectListItem();
                if (MemberDesc.UserId != null)
                {
                    if (CurrentUserModel.CurrentManagement.UserId == item.UserId)
                        selectList.Selected = true;

                }
                selectList.Value = item.UserId.ToString();
                selectList.Text = item.UserName;
                bModelDesc.Users.Add(selectList);
            }
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();
            if (constantId == "0")
                bModelDesc.ConstantModel.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
            foreach (var item in constants)
            {
                var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };
                if (item.ConstantId.ToString() == constantId)
                {
                    selectListItem.Selected = true;
                }
                bModelDesc.ConstantModel.Add(selectListItem);
            }


            return View(bModelDesc);
        }

        public void AddMemberDescriptionLog(MemberDescription memberDescription, string transactionType)
        {
            var memberDescriptionLog = new global::MakinaTurkiye.Entities.Tables.Members.MemberDescriptionLog();
            memberDescriptionLog.BaseID = memberDescription.BaseID;
            memberDescriptionLog.ConstantId = memberDescription.ConstantId;
            memberDescriptionLog.Date = memberDescription.Date;
            memberDescriptionLog.descId = memberDescription.descId;
            memberDescriptionLog.DescriptionDegree = memberDescription.DescriptionDegree;
            memberDescriptionLog.FromUserId = memberDescription.FromUserId;
            memberDescriptionLog.FromUserIdName = entities.Users.FirstOrDefault(x => x.UserId == memberDescription.FromUserId).UserName;
            memberDescriptionLog.UpdateDate = memberDescription.UpdateDate;
            memberDescriptionLog.UserId = memberDescription.UserId;
            memberDescriptionLog.UserIdName = entities.Users.FirstOrDefault(x => x.UserId == memberDescription.UserId).UserName;
            memberDescriptionLog.Status = memberDescription.Status;
            memberDescriptionLog.Title = memberDescription.Title;
            memberDescriptionLog.RecordDate = DateTime.Now;
            memberDescriptionLog.MainPartyId = memberDescription.MainPartyId;
            memberDescriptionLog.PreRegistrationStoreId = memberDescription.PreRegistrationStoreId;
            if (memberDescription.PreRegistrationStoreId > 0)
                transactionType = transactionType + " O";

            memberDescriptionLog.TransactionType = transactionType;
            _memberDescService.InsertMemberDescriptionLog(memberDescriptionLog);

        }
        public ActionResult BulletinMember()
        {
            var members = _bulletinService.GetBulletinMembers();
            int totalrecord = members.Count;
            int pageSize = 50;
            members = members.OrderByDescending(x => x.BulletinMemberId).Skip(0).Take(pageSize).ToList();

            var sectoreCategories = _categoryService.GetMainCategories();
            List<SelectListItem> sectorecategoriesModel = new List<SelectListItem>();
            sectorecategoriesModel.Add(new SelectListItem { Text = "Tümü", Value = "1", Selected = true });
            foreach (var item in sectoreCategories)
            {
                sectorecategoriesModel.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryId.ToString() });
            }
            ViewBag.SectorCategories = sectorecategoriesModel;
            List<BulletinMemberModel> source = new List<BulletinMemberModel>();
            foreach (var item in members)
            {
                var modelItem = new BulletinMemberModel { BulletinMemberId = item.BulletinMemberId, RecordDate = item.RecordDate, Email = item.MemberEmail, MemberName = item.MemberName, MemberSurname = item.MemberSurname };
                foreach (var itemCat in item.BulletinMemberCategories.ToList())
                {
                    var categoryModel = new CategoryModel();
                    if (itemCat.CategoryId != 0)
                    {
                        var category = _categoryService.GetCategoryByCategoryId(itemCat.CategoryId);
                        if (category != null)
                        {
                            categoryModel.CategoryName = category.CategoryName;
                            modelItem.Categories.Add(categoryModel);
                        }
                    }
                }

                source.Add(modelItem);
            }
            FilterModel<BulletinMemberModel> model = new FilterModel<BulletinMemberModel> { CurrentPage = 1, PageDimension = pageSize, Source = source, Order = "desc", TotalRecord = totalrecord };

            return View(model);
        }
        [HttpPost]
        public PartialViewResult BulletinMember(string categoryId, int CurrentPage, string Order)
        {
            var members = new List<global::MakinaTurkiye.Entities.Tables.Bullettins.BulletinMember>();
            int totalRecord = 0;
            int pageSize = 50;

            if (!string.IsNullOrEmpty(categoryId) && categoryId != "1")
            {
                members = _bulletinService.GetBulletinMembersByCategoryId(Convert.ToInt32(categoryId));
                totalRecord = members.Count;
                if (Order == "desc")
                    members = members.OrderByDescending(x => x.BulletinMemberId).Skip((CurrentPage * pageSize) - pageSize).Take(pageSize).ToList();
                else
                    members = members.OrderBy(x => x.BulletinMemberId).Skip((CurrentPage * pageSize) - pageSize).Take(pageSize).ToList();
            }
            else
            {
                members = _bulletinService.GetBulletinMembers();
                totalRecord = members.Count;
                if (Order == "desc")
                    members = members.OrderByDescending(x => x.BulletinMemberId).Skip((CurrentPage * pageSize) - pageSize).Take(pageSize).ToList();
                else
                    members = members.OrderBy(x => x.BulletinMemberId).Skip((CurrentPage * pageSize) - pageSize).Take(pageSize).ToList();
            }
            List<BulletinMemberModel> source = new List<BulletinMemberModel>();
            foreach (var item in members)
            {
                var modelItem = new BulletinMemberModel { BulletinMemberId = item.BulletinMemberId, RecordDate = item.RecordDate, Email = item.MemberEmail, MemberName = item.MemberName, MemberSurname = item.MemberSurname };
                foreach (var itemCat in item.BulletinMemberCategories.ToList())
                {
                    var categoryModel = new CategoryModel();
                    if (itemCat.CategoryId != 0)
                    {
                        var category = _categoryService.GetCategoryByCategoryId(itemCat.CategoryId);
                        if (category != null)
                        {
                            categoryModel.CategoryName = category.CategoryName;
                            modelItem.Categories.Add(categoryModel);
                        }
                    }
                }

                source.Add(modelItem);
            }
            FilterModel<BulletinMemberModel> model = new FilterModel<BulletinMemberModel> { CurrentPage = CurrentPage, PageDimension = pageSize, Source = source, Order = Order, TotalRecord = totalRecord };

            return PartialView("_BulletinRegisterList", model);
        }
        [HttpPost]
        public JsonResult BulletinDelete(int id)
        {
            var bulletin = _bulletinService.GetBulletinMemberByBulletinMemberId(id);
            _bulletinService.DeleteBulletinMember(bulletin);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchPhone()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchPhone(string phoneText)
        {
            var records = _memberService.SPGetInfoByPhone(phoneText);
            List<SearchPhoneModel> model = new List<SearchPhoneModel>();
            foreach (var item in records)
            {
                SearchPhoneModel searchPhone = new SearchPhoneModel();
                if (item.MemberType == 2)
                {
                    var preRegistrationStore = _preRegistrationStoreService.GetPreRegistirationStoreByPreRegistrationStoreId(item.PreRegistrationStoreId);
                    searchPhone.NameSurname = preRegistrationStore.MemberName + " -" + preRegistrationStore.StoreName;
                    searchPhone.MemberTypeText = "Ön Kayıt";
                }
                else
                {
                    var member = _memberService.GetMemberByMainPartyId(item.MainPartyId);
                    if (member != null)
                    {
                        searchPhone.NameSurname = member.MemberName + " " + member.MemberSurname;
                        searchPhone.MemberTypeText = "Normal Üye";
                    }
                    else
                    {
                        var store = _storeService.GetStoreByMainPartyId(item.MainPartyId);
                        if (store != null)
                        {
                            searchPhone.MemberTypeText = "Firma";
                            searchPhone.NameSurname = store.StoreName;
                            searchPhone.Url = "/Store/EditStore/" + item.MainPartyId;
                        }
                    }
                }
                searchPhone.MemberType = item.MemberType;
                searchPhone.PhoneNumber = item.PhoneNumber;
                if (!string.IsNullOrEmpty(searchPhone.MemberTypeText))
                {
                    model.Add(searchPhone);
                }
            }
            return PartialView("_SearchPhoneList", model);
        }
        #endregion
    }
}
