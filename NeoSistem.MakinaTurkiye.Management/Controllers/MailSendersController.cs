namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using NeoSistem.MakinaTurkiye.Cache;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
    using System.Transactions;
    using System.Web.Routing;
    using System.Configuration;
    using DotNetOpenMail;
    using DotNetOpenMail.SmtpAuth;
    using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
    using System.Net.Mail;
    using System.Net;
    using System.Web.UI.WebControls;

    public class MailSendersController : BaseController
    {
        const string STARTCOLUMN = "M.MainPartyId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        const string SessionPage = "member_PAGEDIMENSION";

        static Data.Member dataMember = null;

        static ICollection<MemberModel> collection = null;

        public ActionResult MailAllMemberType()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserAllList()
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

                collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.MainPartyId);
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
        public ActionResult UserAllList(MemberModel model, string OrderName, string Order, int? Page, int PageDimension)
        {
            dataMember = dataMember ?? new Data.Member();

            var whereClause = new StringBuilder("Where");

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

                collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.MainPartyId);
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
                        mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Türkiye"); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(singlemember.MemberEmail); //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = realtitle; //Mail konusu
                        mail.Body = template; //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        SmtpClient sc = new SmtpClient(); //sc adında SmtpClient nesnesi yaratıyoruz.
                        sc.Port = 587; //Gmail için geçerli Portu bildiriyoruz
                        sc.Host = "smtp.gmail.com"; //Gmailin smtp host adresini belirttik
                        sc.EnableSsl = true; //SSL’i etkinleştirdik
                        sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
                        sc.Send(mail); //Mailinizi gönderiyoruz.

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
                        mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Türkiye"); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(singlemember.MemberEmail); //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = subtitle; //Mail konusu
                        mail.Body = template; //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        SmtpClient sc = new SmtpClient(); //sc adında SmtpClient nesnesi yaratıyoruz.
                        sc.Port = 587; //Gmail için geçerli Portu bildiriyoruz
                        sc.Host = "smtp.gmail.com"; //Gmailin smtp host adresini belirttik
                        sc.EnableSsl = true; //SSL’i etkinleştirdik
                        sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
                        sc.Send(mail); //Mailinizi gönderiyoruz.

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
                        SmtpClient sc = new SmtpClient(); //sc adında SmtpClient nesnesi yaratıyoruz.
                        sc.Port = 587; //Gmail için geçerli Portu bildiriyoruz
                        sc.Host = "smtp.gmail.com"; //Gmailin smtp host adresini belirttik
                        sc.EnableSsl = true; //SSL’i etkinleştirdik
                        sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
                        sc.Send(mail); //Mailinizi gönderiyoruz.

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

        [HttpPost]
        public ActionResult ExcelUserAllList(MemberModel model, string OrderName, string Order, int? Page, int PageDimension)
        {
            dataMember = dataMember ?? new Data.Member();

            var whereClause = new StringBuilder("Where");

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

                collection.First(x => x.MainPartyId == memberItem.MainPartyId).PhoneItems = entities.Phones.Where(x => x.MainPartyId == memberItem.MainPartyId);
            }

            var filterItems = new FilterModel<MemberModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection
            };
            var result = filterItems;
            string FileName = "exportList";

            var grid = new GridView();
            grid.DataSource = result;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");

            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Text.Encoding.GetEncoding(1254).GetBytes(sw.ToString());
            Response.Charset = "";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(" <meta http-equiv='Content-Type' content='text/html; charset=windows-1254' />" + sw.ToString());
            Response.End();
            return View();
        }

    }
}
