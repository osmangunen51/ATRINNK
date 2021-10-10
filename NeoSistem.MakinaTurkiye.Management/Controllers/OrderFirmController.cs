using MakinaTurkiye.Services.Checkouts;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.FileHelpers;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using NeoSistem.MakinaTurkiye.Management.Models.Orders;
using NeoSistem.MakinaTurkiye.Management.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;


namespace NeoSistem.MakinaTurkiye.Management.Controllers
{

    public class OrderFirmController : BaseController
    {
        #region Constants

        const string STARTCOLUMN = "OrderId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 50;
        const string SessionPage = "Order_PAGEDIMENSION";

        #endregion

        #region Fields

        private readonly IOrderService _orderService;
        private readonly IStoreService _storeService;
        private readonly IPacketService _packetService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMemberDescriptionService _memberDescriptionService;
        private readonly IAddressService _addressService;
        private readonly IConstantService _constantService;
        private readonly IOrderInstallmentService _orderInstallmentService;
        private readonly IStoreDiscountService _storeDiscountService;

        #endregion

        #region Ctor

        public OrderFirmController(IOrderService orderService, IStoreService storeService,
            IPacketService packetService, IMemberStoreService memberStoreService,
            IAddressService addressService,
            IMemberDescriptionService memberDescriptionService,
            IConstantService constantService,
            IOrderInstallmentService orderInstallmentService,
            IStoreDiscountService storeDiscountService)
        {
            this._orderService = orderService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._memberStoreService = memberStoreService;
            this._addressService = addressService;
            this._memberDescriptionService = memberDescriptionService;
            this._constantService = constantService;
            this._orderInstallmentService = orderInstallmentService;
            this._storeDiscountService = storeDiscountService;
        }

        #endregion

        static Data.Order dataOrder = null;
        static ICollection<OrderModel> collection = null;

        #region Methods

        public ActionResult FaturaKAyit(int id)
        {
            MakinaTurkiyeEntities olsutur = new MakinaTurkiyeEntities();
            var sonuc = (from f in olsutur.Faturachecks
                         where f.MainPartyId == id
                         select f.Onaylı).SingleOrDefault();
            if (sonuc == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                using (var entities = new MakinaTurkiyeEntities())
                {
                    //var faturakayit = new Faturacheck();
                    //faturakayit.Onaylı = 1;
                    //faturakayit.MainPartyId = id;
                    //makina.AddToFaturachecks(faturakayit);
                    //makina.SaveChanges();
                    var fatura = new Faturacheck
                    {
                        Onaylı = 1,
                        MainPartyId = id,
                    };
                    entities.Faturachecks.AddObject(fatura);
                    entities.SaveChanges();
                }
            };
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            PAGEID = PermissionPage.TümSiparisListesi;
            int total = 0;
            dataOrder = new Data.Order();
            var whereClause = new StringBuilder("Where");
            string equalClause = " {0} = {1} ";
            whereClause.AppendFormat(equalClause, "O.IsNew", "1");

            bool op = false;
            if (Request.QueryString["PacketStatu"] != null)
            {
                // Sipariş Listesini Getirirken hataya düşürüldüğü için bu kod kapatıldı.!!
                //if (op)
                //{
                    whereClause.Append("AND");
                //}
                whereClause.AppendFormat(equalClause, "O.PacketStatu", Request.QueryString["PacketStatu"].ToString());
                //op = true;
            }
            if (Session[SessionPage] == null)
            {
                Session[SessionPage] = PAGEDIMENSION;
            }
            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }
            collection = dataOrder.Search(ref total, (int)Session[SessionPage], 1, whereClause.ToString(), STARTCOLUMN, ORDER).AsCollection<OrderModel>();
            var salesUsers = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 16 || g.UserGroupId == 20 || g.UserGroupId == 22 || g.UserGroupId == 18  select u;
            List<SelectListItem> salesUserManagers = new List<SelectListItem>();
            salesUserManagers.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in salesUsers)
            {
                salesUserManagers.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }

            ViewData["SalesUsers"] = salesUserManagers;
            var model = new FilterModel<OrderModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };
            if (Session[SessionPage] != null)
            {
                model.PageDimension = (int)Session[SessionPage];
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(OrderModel model, string OrderName, string OrderCancelled, string Order, string RegisterStartDate, string RegisterEndDate, int? Page, int PageDimension, string LastPayDate, string PaidBill, string SalesUserId, DateTime? OrderEndDate1, string OrderEndDate2)
        {

            var orderNullInvoice = _orderService.GetOrdersWithNullInvoiceNumber();
            if (orderNullInvoice.Count > 0)
            {
                foreach (var item in orderNullInvoice)
                {
                    var orderUpdate = _orderService.GetOrderByOrderId(item.OrderId);
                    orderUpdate.InvoiceNumber = "N" + item.OrderId;
                    _orderService.UpdateOrder(orderUpdate);
                }
            }
            dataOrder = dataOrder ?? new Data.Order();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'{1}%' ";
            string equalClause = " {0} = {1} ";

            whereClause.AppendFormat(equalClause, "O.IsNew", "1");
            bool op = true;


            if (!string.IsNullOrWhiteSpace(model.InvoiceNumber))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "InvoiceNumber", model.InvoiceNumber);

            }
            if (!string.IsNullOrWhiteSpace(model.OrderNo))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "EBillNumber", model.OrderNo);
                op = true;
                whereClause.Append("Or");
                whereClause.AppendFormat(likeClaue, "OrderNo", model.OrderNo);
            }

            if (!string.IsNullOrWhiteSpace(model.StoreName))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.PacketName))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "PacketName", model.PacketName);
                op = true;
            }

            if (model.PacketStatu > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "PacketStatu", model.PacketStatu);
                op = true;
            }


            if (!string.IsNullOrEmpty(RegisterEndDate))
            {
                if (!string.IsNullOrEmpty(RegisterStartDate))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }

                    DateTime startDate = Convert.ToDateTime(RegisterStartDate.Replace(".", "/"), CultureInfo.InvariantCulture);
                    DateTime endDate = Convert.ToDateTime(RegisterEndDate.Replace(".", "/"), CultureInfo.InvariantCulture);
                    string dateEqual = " Cast(O.RecordDate as date) >= Cast('{0}' as date)  and Cast(O.RecordDate as date) <=Cast('{1}' as date) ";
                    whereClause.AppendFormat(dateEqual, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(RegisterStartDate))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    string dateEqual = " Cast(RecordDate as date) = Cast('{0}' as date) ";
                    whereClause.AppendFormat(dateEqual, Convert.ToDateTime(RegisterStartDate).ToString("yyyyMMdd"));
                }
            }

            if (SalesUserId != "0")
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "u.UserId", SalesUserId);
            }


            if (!string.IsNullOrEmpty(LastPayDate))
            {
                if (model.PayDate != null)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }

                    string dateEqual = " Cast(PayDate as date) >= Cast('{0}' as date)  and Cast(PayDate as date) <=Cast('{1}' as date) ";
                    whereClause.AppendFormat(dateEqual, Convert.ToDateTime(model.PayDate).ToString("yyyyMMdd"), Convert.ToDateTime(LastPayDate).ToString("yyyyMMdd"));
                }
            }
            else
            {
                if (model.PayDate != null)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    string dateEqual = " Cast(PayDate as date) = Cast('{0}' as date) ";
                    whereClause.AppendFormat(dateEqual, Convert.ToDateTime(model.PayDate).ToString("yyyyMMdd"));
                }
            }



            if (OrderEndDate1.HasValue)
            {
                if (!string.IsNullOrEmpty(OrderEndDate2))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }


                    string dateEqual = " Cast(S.StorePacketEndDate as date) >= Cast('{0}' as date)  and Cast(S.StorePacketEndDate as date) <=Cast('{1}' as date) ";
                    whereClause.AppendFormat(dateEqual, Convert.ToDateTime(OrderEndDate1).ToString("yyyyMMdd"), DateTime.ParseExact(OrderEndDate2, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                }
            }
            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }
            if (OrderCancelled != "2")
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "OrderCancelled", OrderCancelled);

            }
            if (PaidBill != "0")
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                if (PaidBill == "2")
                {

                    whereClause.AppendFormat(equalClause, "PriceCheck", 1);

                }
                else
                {
                    whereClause.AppendFormat("(PriceCheck=0 or PriceCheck is null)");


                }
            }
            int total = 0;
            Session[SessionPage] = PageDimension;
            collection =
              dataOrder.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<OrderModel>();

            var filterItems = new FilterModel<OrderModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection,
                PageDimension = PageDimension
            };

            return View("OrderList", filterItems);
        }
        [HttpGet]
        public ActionResult ExportExel(OrderModel model, string OrderName, string RegisterStartDate, string RegisterEndDate, string OrderCancelled, string Order, int? Page, string SalesUserId, int PageDimension, string PayDate1, string LastPayDate, string PaidBill)
        {

            dataOrder = dataOrder ?? new Data.Order();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'{1}%' ";
            string equalClause = " {0} = {1} ";

            whereClause.AppendFormat(equalClause, "O.IsNew", "1");
            bool op = true;


            if (!string.IsNullOrWhiteSpace(model.InvoiceNumber))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "InvoiceNumber", model.InvoiceNumber);
            }
            if (!string.IsNullOrWhiteSpace(model.OrderNo))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "OrderNo", model.OrderNo);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.StoreName))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.PacketName))
            {
                if (op == true)
                    whereClause.Append("AND");
                whereClause.AppendFormat(likeClaue, "PacketName", model.PacketName);
                op = true;
            }

            if (model.PacketStatu > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "PacketStatu", model.PacketStatu);
                op = true;
            }

            if (OrderCancelled != "2")
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "OrderCancelled", OrderCancelled);

            }
            if (PaidBill != "0")
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                if (PaidBill == "2")
                {

                    whereClause.AppendFormat(equalClause, "PriceCheck", 1);

                }
                else
                {
                    whereClause.AppendFormat("(PriceCheck=0 or PriceCheck is null)");


                }
            }
            if (RegisterStartDate != "")
            {
                if (!string.IsNullOrEmpty(RegisterEndDate))
                {
                    if (!string.IsNullOrEmpty(RegisterStartDate))
                    {
                        if (op)
                        {
                            whereClause.Append("AND");
                        }

                        string dateEqual = " Cast(RecordDate as date) >= Cast('{0}' as date)  and Cast(RecordDate as date) <=Cast('{1}' as date) ";
                        whereClause.AppendFormat(dateEqual, Convert.ToDateTime(RegisterStartDate).ToString("yyyyMMdd"), Convert.ToDateTime(RegisterEndDate).ToString("yyyyMMdd"));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(RegisterStartDate))
                    {
                        if (op)
                        {
                            whereClause.Append("AND");
                        }
                        string dateEqual = " Cast(RecordDate as date) = Cast('{0}' as date) ";
                        whereClause.AppendFormat(dateEqual, Convert.ToDateTime(RegisterStartDate).ToString("yyyyMMdd"));
                    }
                }

                if (SalesUserId != "0")
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(equalClause, "u.UserId", SalesUserId);
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(LastPayDate))
                {
                    if (!string.IsNullOrEmpty(PayDate1))
                    {
                        if (op)
                        {
                            whereClause.Append("AND");
                        }

                        string dateEqual = " Cast(PayDate as date) >= Cast('{0}' as date)  and Cast(PayDate as date) <=Cast('{1}' as date) ";
                        whereClause.AppendFormat(dateEqual, Convert.ToDateTime(PayDate1).ToString("yyyyMMdd"), Convert.ToDateTime(LastPayDate).ToString("yyyyMMdd"));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(PayDate1))
                    {
                        if (op)
                        {
                            whereClause.Append("AND");
                        }
                        string dateEqual = " Cast(PayDate as date) = Cast('{0}' as date) ";
                        whereClause.AppendFormat(dateEqual, Convert.ToDateTime(PayDate1).ToString("yyyyMMdd"));
                    }
                }
            }
            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            int total = 0;

            var collection = dataOrder.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<OrderModel>();
            FileHelper fileHelper = new FileHelper();
            string filename = DateTime.Now.ToString("dd-MM-yyyy") + "_siparislistesi";
            var exportList = collection.Where(x => x.OrderCancelled == false || x.OrderCancelled == null).ToList();
            List<OrderExcelModel> list = new List<OrderExcelModel>();
            decimal totalPriceRest = 0;
            decimal totalPriceOrder = 0;
            foreach (var item in exportList)
            {
                decimal restAmount = item.OrderPrice;
                var excelModel = new OrderExcelModel
                {
                    FirmaAdi = item.StoreName,
                    TeleSatisSorumlusu = item.SalesUserName
                };
                if (item.PayDate != null)
                    excelModel.OdemeTarih = item.PayDate.Value.ToString("dd-MM-yyyy");
                if (item.RestAmount != null)
                    restAmount = Convert.ToDecimal(item.RestAmount);
                excelModel.KalanFiyat = restAmount.ToString("C2");
                list.Add(excelModel);
                totalPriceRest = totalPriceRest + restAmount;

                totalPriceOrder = totalPriceOrder + item.OrderPrice;
            }
            decimal tookAmount = totalPriceOrder - totalPriceRest;
            list.Add(new OrderExcelModel
            {
                OdemeTarih = "Toplam Miktar:",
                KalanFiyat = totalPriceOrder.ToString("C2")
            });
            list.Add(new OrderExcelModel
            {
                OdemeTarih = "Kalan Miktar:",
                KalanFiyat = totalPriceRest.ToString("C2")
            });
            list.Add(new OrderExcelModel
            {
                OdemeTarih = "Alınan  Miktar:",
                KalanFiyat = tookAmount.ToString("C2")
            });
            fileHelper.ExportExcel<OrderExcelModel>(list, filename);

            return View("Index");


        }
        public ActionResult Confirm(int id)
        {
            var order = _orderService.GetOrderByOrderId(id);
            DateTime orderBeginDate = DateTime.Now;
            if (order.PacketStartDate.HasValue)
                orderBeginDate = order.PacketStartDate.Value;

            //hesap numnarası fiyatı
            var packet = entities.Packets.SingleOrDefault(c => c.PacketId == order.PacketId);
            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == order.MainPartyId);
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
            int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
            var member = entities.Members.FirstOrDefault(x => x.MainPartyId == memberMainPartyId);
            //order.PayDate = DateTime.Now.AddDays(15);
            //var orderDescription = new global::MakinaTurkiye.Entities.Tables.Checkouts.OrderDescription();
            //orderDescription.Description = "Tahsilat vade girişi";
            //orderDescription.OrderId = order.OrderId;
            //orderDescription.PayDate = order.PayDate.Value;
            //orderDescription.RecordDate = DateTime.Now;
            //_orderService.InsertOrderDescription(orderDescription);

            #region emailicin
            var settings = ConfigurationManager.AppSettings;
            MailMessage mail = new MailMessage();
            MessagesMT mailT = entities.MessagesMTs.First(x => x.MessagesMTName == "goldenpro");
            mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
          
            mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
            mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
            string template = mailT.MessagesMTPropertie;
            string dateimiz = orderBeginDate.AddDays(packet.PacketDay).ToString();
            template = template.Replace("#uyeliktipi#", packet.PacketName).Replace("#uyelikbaslangıctarihi#", orderBeginDate.ToString("D")).Replace("#uyelikbitistarihi#", dateimiz).Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#pakettipi#", packet.PacketName);
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
            sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
            sc.Send(mail);
            #endregion

            store.PacketId = packet.PacketId;

            store.StorePacketBeginDate = orderBeginDate;
            var packetfeauture = entities.PacketFeatures.Where(c => c.PacketId == packet.PacketId && c.PacketFeatureTypeId == 3).FirstOrDefault();
            if (packetfeauture.FeatureContent != null)
            {
                store.ProductCount = 9999;
            }
            else if (packetfeauture.FeatureActive != null)
            {
                if (packetfeauture.FeatureActive == true)
                {
                    store.ProductCount = 3;
                }
                else if (packetfeauture.FeatureActive == false)
                {
                    store.ProductCount = 0;
                }
            }
            else
            {
                store.ProductCount = packetfeauture.FeatureProcessCount;
            }
            bool isRenewPacket = false;
            var orderLastList = _orderService.GetOrdersByMainPartyId(store.MainPartyId);

            foreach (var item in orderLastList.ToList())
            {
                item.OrderPacketEndDate = store.StorePacketEndDate;
                _orderService.UpdateOrder(item);
            }
            if (order.PacketDay.HasValue)
                store.StorePacketEndDate = orderBeginDate.AddDays(order.PacketDay.Value);
            else
                store.StorePacketEndDate = orderBeginDate.AddDays(packet.PacketDay);

            if (!order.ProductId.HasValue || (order.ProductId.HasValue && order.ProductId.Value == 0))
            {
                entities.SaveChanges();
            }

            order.PacketStatu = (byte)PacketStatu.Onaylandi;
            if (order.PacketDay.HasValue)
                order.OrderPacketEndDate = orderBeginDate.AddDays(order.PacketDay.Value);
            else
                order.OrderPacketEndDate = orderBeginDate.AddDays(packet.PacketDay);

            if (orderLastList.Count > 1)
            {
                isRenewPacket = true;
            }
            order.IsRenewPacket = isRenewPacket;
            _orderService.UpdateOrder(order);

            #region paymentsadd
            if (order.PriceCheck == true)
            {
                var paymentsNew = _orderService.GetPaymentsByOrderId(order.OrderId);
                var paidPriceSum = paymentsNew.Select(x => x.PaidAmount).Sum();
                var restAmountPaid = order.OrderPrice - paidPriceSum;
                var newPayment1 = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
                newPayment1.OrderId = order.OrderId;
                newPayment1.PaidAmount = restAmountPaid;
                newPayment1.RecordDate = DateTime.Now;
                newPayment1.RestAmount = 0;
                newPayment1.PaymentType = order.OrderType;
                _orderService.InsertPayment(newPayment1);

            }
            #endregion

            return RedirectToAction("Index");
        }

        public ActionResult NotConfirm(int id)
        {
            var order = entities.Orders.SingleOrDefault(c => c.OrderId == id);
            order.PacketStatu = (byte)PacketStatu.Onaylanmadi;
            entities.SaveChanges();
            #region emailicin
            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == order.MainPartyId);
            var packet = entities.Packets.SingleOrDefault(c => c.PacketId == order.PacketId);
            var settings = ConfigurationManager.AppSettings;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye"); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(store.StoreEMail);                                                              //Mailin kime gideceğini belirtiyoruz
            mail.Subject = "Paket onay bilgisi";                                              //Mail konusu
            string template = "<html><body>" +
       "<span style='color:black'>   <p>" + packet.PacketName + " isteğiniz kabul edilmedi..</p>\r\n" +
      "<p>" + store.StoreName + "</p>\r\n\r\n" + "<p>Telefon:0212-255-71-50</p><br/> <img src=\"http://makinaturkiye.com/Content/Images/logo.png \"/ alt=\"Makina Türkiye\"/><br/><a href=\"http://www.makinaturkiye.com\" >www.makinaturkiye.com</a><span></body></html>";
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
            sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
            sc.Send(mail);
            #endregion
            return RedirectToAction("Index");

        }

        public ActionResult DeleteOrder(int id, string type)
        {
            DeleteOrderViewModel deleteOrderView = new DeleteOrderViewModel();
            deleteOrderView.OrderId = id;
            deleteOrderView.Type = type;
            return View(deleteOrderView);
        }

        [HttpPost]
        public ActionResult DeleteOrder(DeleteOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = entities.Users.FirstOrDefault(x => x.UserName == "Yönetici" && x.UserPass == model.AdminPassword);
                if (admin != null)
                {
                    if (model.Type == "delete")
                    {
                        Order order = entities.Orders.First(x => x.OrderId == model.OrderId);
                        var orderDecriptions = _orderService.GetOrderDescriptionsByOrderId(model.OrderId);
                        foreach (var orderDescription in orderDecriptions)
                        {
                            _orderService.DeleteOrderDescription(orderDescription);
                        }
                        var orderInstalmens = _orderInstallmentService.GetOrderInstallmentsByOrderId(model.OrderId);
                        foreach (var item in orderInstalmens)
                        {
                            _orderInstallmentService.DeleteOrderInstallment(item);
                        }
                        entities.Orders.DeleteObject(order);
                        entities.SaveChanges();
                    }
                    else
                    {
                        var order = _orderService.GetOrderByOrderId(model.OrderId);
                        order.OrderCancelled = true;
                        _orderService.UpdateOrder(order);

                    }
                    return RedirectToAction("index", "OrderFirm", new { opr = "success" });
                }
                else
                {
                    ViewData["error"] = "true";
                    return View();
                }
            }

            return View();
        }

        public ActionResult ProductSales()
        {
            PAGEID = PermissionPage.TümSiparisListesi;
            var model = entities.ProductSales.ToList();


            return View(model);
        }

        [HttpPost]
        public JsonResult UpdateStoreName(string id, string name)
        {
            var order = _orderService.GetOrderByOrderId(Convert.ToInt32(id));
            order.StoreNameForInvoice = name;
            _orderService.UpdateOrder(order);
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UpdateOrderEndDate(string id, string date)
        {
            var order = _orderService.GetOrderByOrderId(Convert.ToInt32(id));
            var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
            var dateNew = Convert.ToDateTime(date);
            order.OrderPacketEndDate = dateNew;
            store.StorePacketEndDate = dateNew;
            _orderService.UpdateOrder(order);
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UpdatePaidMount(string id, string paidPrice)
        {
            var newPayment = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
            int ID = Convert.ToInt32(id);
            decimal PaidPrice = Convert.ToDecimal(paidPrice);
            var order = _orderService.GetOrderByOrderId(ID);
            newPayment.OrderId = ID;
            newPayment.PaidAmount = PaidPrice;
            newPayment.RecordDate = DateTime.Now;
            newPayment.RestAmount = order.OrderPrice - PaidPrice;
            _orderService.InsertPayment(newPayment);
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetInvoicePdf(int OrderId)
        {

            var model = new InvoiceModel();
            var order = _orderService.GetOrderByOrderId(OrderId);
            if (order.InvoiceNumber == "" || order.InvoiceNumber == null)
            {
                order.InvoiceNumber = "N" + order.OrderId;
                _orderService.UpdateOrder(order);

            }
            model.InvoiceId = order.OrderId;
            if (order != null)
            {
                var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(order.MainPartyId));
                if (order.StoreNameForInvoice == "" || order.StoreNameForInvoice == null)
                    model.StoreName = store.StoreName;
                else
                    model.StoreName = order.StoreNameForInvoice;

                model.InvoiceNumer = order.InvoiceNumber;
                model.StoreEmail = store.StoreEMail;
                model.PacketBeginDate = order.RecordDate;
                model.PacketEndDate = Convert.ToDateTime(order.OrderPacketEndDate);
                var packet = _packetService.GetPacketByPacketId(order.PacketId);
                model.PacketName = packet.PacketName;

                model.TaxNo = store.TaxNumber;
                model.TaxOffice = store.TaxOffice;
                decimal taxDiscount = (decimal)(order.OrderPrice * 18 / 100);
                model.Price = order.OrderPrice - taxDiscount;
                model.TaxPrice = taxDiscount;
                model.TaxValue = 18;
                model.InvoiceAddress = order.Address;
                model.PriceWord = priceToWord((order.OrderPrice));
            }
            else
            {
                throw new ArgumentNullException("order");
            }

            return View(model);
            //return View();
        }

        public ActionResult GetInvoice(int OrderId)
        {

            var model = new InvoiceModel();
            var order = _orderService.GetOrderByOrderId(OrderId);
            if (order.InvoiceNumber == "" || order.InvoiceNumber == null)
            {
                order.InvoiceNumber = "N" + order.OrderId;
                _orderService.UpdateOrder(order);

            }
            model.InvoiceId = order.OrderId;
            if (order != null)
            {
                var entities1 = new MakinaTurkiyeEntities();
                var store = entities1.Stores.FirstOrDefault(x => x.MainPartyId == order.MainPartyId);
                if (order.StoreNameForInvoice == "" || order.StoreNameForInvoice == null)
                    model.StoreName = store.StoreName;
                else
                    model.StoreName = order.StoreNameForInvoice;

                model.InvoiceNumer = order.InvoiceNumber;
                model.AccountId = order.AccountId;
                model.StoreEmail = store.StoreEMail;
                model.PacketBeginDate = order.RecordDate;
                model.OrderDescription = order.OrderDescription;
                model.OrderNo = order.OrderNo;
                model.PacketEndDate = Convert.ToDateTime(order.OrderPacketEndDate);
                var packet = _packetService.GetPacketByPacketId(order.PacketId);
                model.PacketName = packet.PacketName;
                if (store.TaxNumber == "" || store.TaxOffice == "")
                {
                    model.TaxNo = order.TaxNo;
                    model.TaxOffice = order.TaxOffice;
                }
                else
                {
                    model.TaxNo = store.TaxNumber;
                    model.TaxOffice = store.TaxOffice;
                }

                var storeDiscount = _storeDiscountService.GetStoreDiscountByOrderId(order.OrderId);
                float taxDiscount = 0;
                if (storeDiscount != null)
                {
                    taxDiscount = ((float)packet.PacketPrice - ((float)packet.PacketPrice / (1.18f)));

                    model.NormalPrice = packet.PacketPrice - (decimal)taxDiscount;
                    model.DiscountAmount = storeDiscount.DiscountAmount;
                    model.DiscountPercentage = storeDiscount.DiscountPercentage;
                }

                taxDiscount = ((float)order.OrderPrice - ((float)order.OrderPrice / (1.18f)));
                model.Price = order.OrderPrice - (decimal)taxDiscount;
                model.TaxPrice = (decimal)taxDiscount;
                model.TaxValue = 18;
                model.InvoiceAddress = order.Address;
                model.PriceWord = priceToWord((model.Price));
                model.InvoiceDate = order.InvoiceDate.HasValue ? order.InvoiceDate.Value : DateTime.Now;
            }
            else
            {
                throw new ArgumentNullException("order");
            }

            return View(model);
            //return View();
        }

        [HttpGet]
        public ActionResult PriceCheck(int OrderId, string confirm, string type)
        {
            var order = _orderService.GetOrderByOrderId(OrderId);
            if (confirm == "true" && type == "invoice")
                order.PriceCheck = true;
            else if (confirm == "true" && type == "sendedmail")
                order.SendedMail = true;
            else if (confirm == "false" && type == "invoice")
                order.PriceCheck = false;

            _orderService.UpdateOrder(order);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult UpdateInvoiceStatus(string orderId, string page, string type)
        {
            if (orderId != "")
            {
                var order = _orderService.GetOrderByOrderId(Convert.ToInt32(orderId));
                if (page == "confirm" && type == "invoice")
                    order.InvoiceStatus = true;
                else if (page == "confirm" && type == "sendedmail")
                {
                    order.SendedMail = true;
                }

                _orderService.UpdateOrder(order);
            }

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public JsonResult UpdateStoreAddress(int id, string newAddress)
        {
            var order = _orderService.GetOrderByOrderId(id);
            order.Address = newAddress;
            _orderService.UpdateOrder(order);
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdatePrice(string price, int orderId)
        {
            var order = _orderService.GetOrderByOrderId(orderId);
            try
            {
                order.OrderPrice = Convert.ToDecimal(price);
                _orderService.UpdateOrder(order);
            }
            catch (Exception)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdateRestAmount(int orderId, string newrestAmount, string lastRestAmout)
        {
            decimal newRestAmount = Convert.ToDecimal(newrestAmount);
            decimal lastRestAmountPrice = Convert.ToDecimal(lastRestAmout);
            var payment = _orderService.GetPaymentsByOrderId(orderId).FirstOrDefault(x => x.RestAmount == lastRestAmountPrice);
            if (payment != null)
            {
                payment.RestAmount = newRestAmount;
                _orderService.UpdatePayment(payment);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InvoiceNumberUpdate(int id, string invoiceNumber)
        {
            var order = _orderService.GetOrderByOrderId(id);
            if (order.InvoiceNumber == invoiceNumber)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                order.InvoiceNumber = invoiceNumber;
                _orderService.UpdateOrder(order);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        private string priceToWord(decimal tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
            string lira = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
            string[] onlar = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
            string[] binler = { "katrilyon", "trilyon", "milyar", "milyon", "bin", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
                                //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            lira = lira.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "yüz"; //yüzler                

                if (grupDegeri == "Biryüz") //biryüz düzeltiliyor.
                    grupDegeri = "Yüz";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler                

                if (grupDegeri != "") //binler
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "Birbin") //birbin düzeltiliyor.
                    grupDegeri = "Bin";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " tl ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0") //kuruş onlar
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0") //kuruş birler
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " kr.";
            else
                yazi += "sıfır kr.";

            return yazi;
        }

        public ActionResult OrderCreate(int storeId)
        {

            var store = _storeService.GetStoreByMainPartyId(storeId);

            OrderModel viewModel = new OrderModel();
            viewModel.StoreName = store.StoreName;
            viewModel.TaxNo = store.TaxNumber;
            viewModel.MainPartyId = storeId;
            viewModel.TaxOffice = store.TaxOffice;
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeId);
            int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);



            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();

            var address = entities.Addresses.FirstOrDefault(a => a.MainPartyId == storeId);

            viewModel.Address = EnumModels.AddressEditForOrder(address);
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult OrderCreate(OrderModel order)
        {

            global::MakinaTurkiye.Entities.Tables.Checkouts.Order addOrderModel = new global::MakinaTurkiye.Entities.Tables.Checkouts.Order();
            var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
            addOrderModel.MainPartyId = order.MainPartyId;
            addOrderModel.OrderPrice = order.OrderPrice;
            addOrderModel.Address = order.Address;
            addOrderModel.PacketId = store.PacketId;
            addOrderModel.OrderDescription = order.OrderDescription;
            addOrderModel.TaxNo = order.TaxNo;
            addOrderModel.TaxOffice = order.TaxOffice;
            addOrderModel.RecordDate = DateTime.Now;


            _orderService.InsertOrder(addOrderModel);

            store.TaxNumber = order.TaxNo;
            store.TaxOffice = order.TaxOffice;
            _storeService.UpdateStore(store);
            return RedirectToAction("GetInvoice", "OrderFirm", new { orderId = addOrderModel.OrderId });

        }

        [HttpPost]
        public JsonResult OrderWriteLogAdd(int orderId)
        {
            using (var _entities = new MakinaTurkiyeEntities())
            {
                var order = _entities.Orders.FirstOrDefault(x => x.OrderId == orderId);
                OrderWriteLog orderWriteLog = new OrderWriteLog();
                orderWriteLog.OrderID = orderId;
                orderWriteLog.StoreName = order.Store.StoreName;
                orderWriteLog.Price = order.OrderPrice;
                orderWriteLog.RecordDate = DateTime.Now;
                _entities.OrderWriteLogs.AddObject(orderWriteLog);
                _entities.SaveChanges();
            }
            return Json(true);
        }

        public ActionResult OrderWriteLogs(int? page)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;

            var _entities = new MakinaTurkiyeEntities();
            ViewData["page"] = page == null ? 0 : (int)page;
            ViewData["TotalRecod"] = entities.OrderWriteLogs.ToList().Count;
            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(entities.OrderWriteLogs.ToList().Count / (float)pageSize));
            var orderWriteLogList = entities.OrderWriteLogs.OrderByDescending(x => x.OrderWriteLogID).ThenBy(x => x.RecordDate).Skip(skipRows).Take(pageSize).ToList();

            return View(orderWriteLogList);
        }

        public ActionResult OrderWriteLogDelete(int id, int page1)
        {
            using (var _entitites = new MakinaTurkiyeEntities())
            {
                var orderWriteLog = _entitites.OrderWriteLogs.FirstOrDefault(x => x.OrderWriteLogID == id);
                _entitites.OrderWriteLogs.DeleteObject(orderWriteLog);
                _entitites.SaveChanges();
            }

            return RedirectToAction("OrderWriteLogs", new { page = page1 });

        }

        public ActionResult UpdatePayDate(int orderId)
        {
            var order = _orderService.GetOrderByOrderId(orderId);
            var model = new UpdatePayDateModel();
            model.OrderId = order.OrderId;
            PrepareUpdatePayDateModel(model);
            return View(model);

        }
        [HttpPost]
        public ActionResult UpdatePayDate(UpdatePayDateModel model)
        {
            if (model.WillPayDate == null)
            {
                ModelState.AddModelError("WillPayDate", "Lütfen Boş Geçmeyiniz");
                PrepareUpdatePayDateModel(model);
            }
            else
            {
                var orderDescription = new global::MakinaTurkiye.Entities.Tables.Checkouts.OrderDescription();
                orderDescription.OrderId = model.OrderId;
                orderDescription.PayDate = model.WillPayDate;
                orderDescription.Description = model.Description;
                orderDescription.RecordDate = DateTime.Now;
                _orderService.InsertOrderDescription(orderDescription);



                var order = _orderService.GetOrderByOrderId(model.OrderId);
                order.PayDate = model.WillPayDate;
                _orderService.UpdateOrder(order);

                #region inserttodescriptions
                var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(order.MainPartyId);
                var constant = _constantService.GetConstantByConstantId(444);
                var baseMemberDescription = entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == memberStore.MemberMainPartyId.Value);
                if (baseMemberDescription != null)
                {
                    baseMemberDescription.Description = model.Description;
                    baseMemberDescription.Title = constant.ConstantName;
                    baseMemberDescription.Date = DateTime.Now;
                    baseMemberDescription.UpdateDate = DateTime.Now;
                    entities.SaveChanges();
                }
                var memberDescriptions = _memberDescriptionService.GetMemberDescriptionsByMainPartyId(memberStore.MemberMainPartyId.Value);
                var memberDescForPayment = memberDescriptions.FirstOrDefault(x => x.ConstantId == 444);

                if (memberDescForPayment == null)
                {
                    var memberDesc = new global::MakinaTurkiye.Entities.Tables.Members.MemberDescription();
                    memberDesc.Date = DateTime.Now;
                    memberDesc.MainPartyId = memberStore.MemberMainPartyId;


                    memberDesc.Title = constant.ConstantName;

                    memberDesc.Description = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + model.Description + "-" + "<span style='color:#44000d'>" + CurrentUserModel.CurrentManagement.UserName + "</span>";
                    memberDesc.Status = 0;
                    memberDesc.ConstantId = Convert.ToInt32(constant.ConstantId);
                    memberDesc.FromUserId = CurrentUserModel.CurrentManagement.UserId;
                    memberDesc.UserId = CurrentUserModel.CurrentManagement.UserId;
                    //memberDesc.IsFirst = model.IsFirst;
                    _memberDescriptionService.InsertMemberDescription(memberDesc);
                }
                else
                {
                    string descriptionNew = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + model.Description + "-" + "<span style='color:#44000d'>" + CurrentUserModel.CurrentManagement.UserName + "</span>" + "~" + memberDescForPayment.Description;
                    memberDescForPayment.Description = descriptionNew;
                    memberDescForPayment.Date = DateTime.Now;
                    _memberDescriptionService.UpdateMemberDescription(memberDescForPayment);
                }
                model = new UpdatePayDateModel();
                model.OrderId = order.OrderId;
                PrepareUpdatePayDateModel(model);
                #endregion
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteOrderDesc(int id)
        {

            int tempOrderId = 0;
            var orderDesc = _orderService.GetOrderDescriptionByOrderDescriptionId(id);
            tempOrderId = orderDesc.OrderId;
            _orderService.DeleteOrderDescription(orderDesc);
            var order = _orderService.GetOrderByOrderId(tempOrderId);
            var newDate = _orderService.GetOrderDescriptionsByOrderId(tempOrderId).LastOrDefault();
            if (newDate != null)
                order.PayDate = newDate.PayDate;
            else
                order.PayDate = order.RecordDate;

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void PrepareUpdatePayDateModel(UpdatePayDateModel model)
        {
            var orderdescriptions = _orderService.GetOrderDescriptionsByOrderId(model.OrderId).OrderByDescending(x => x.OrderDescriptionId).ToList();
            foreach (var item in orderdescriptions)
            {
                model.UpdatePayDateModels.Add(new UpdatePayDateModel { Description = item.Description, UpdatePayDateId = item.OrderDescriptionId, WillPayDate = item.PayDate, RecordDate = item.RecordDate });
            }
            var orderInstallments = _orderInstallmentService.GetOrderInstallmentsByOrderId(model.OrderId);
            foreach (var item in orderInstallments)
            {
                model.OrderInstallmentItems.Add(new OrderInstallmentItemModel
                {
                    Amount = item.Amunt,
                    Id = item.OrderInstallmentId,
                    PayDate = item.PayDate,
                    IsPaid = item.IsPaid.HasValue ? item.IsPaid.Value : false
                });
            }
        }
        public ActionResult Payments(int OrderId)
        {
            PaymentModel model = new PaymentModel();
            model.OrderId = OrderId;
            model.PayDate = DateTime.Now.Date.ToString("dd.MM.yyyy");
            PreparePaymentModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Payments(PaymentModel model)
        {

            int ID = Convert.ToInt32(model.OrderId);
            decimal PaidPrice = Convert.ToDecimal(model.PaidAmount);
            var order = _orderService.GetOrderByOrderId(ID);
            decimal paidSumPrice = 0;
            if (_orderService.GetPaymentsByOrderId(order.OrderId).Count > 0)
            {
                paidSumPrice = _orderService.GetPaymentsByOrderId(model.OrderId).Select(x => x.PaidAmount).Sum();
            }

            var newPayment = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
            newPayment.OrderId = ID;
            newPayment.PaidAmount = PaidPrice;
            if (!string.IsNullOrEmpty(model.PayDate))
            {
                newPayment.PaymentDate = DateTime.ParseExact(model.PayDate, "dd.MM.yyyy", null);
            }

            newPayment.Description = model.Description;
            newPayment.RecordDate = DateTime.Now;
            newPayment.PaymentType = (byte)Ordertype.Havale;
            newPayment.RestAmount = order.OrderPrice - (paidSumPrice + PaidPrice);
            newPayment.BankConstantId = Convert.ToInt32(model.BankId);
            newPayment.SenderNameSurname = model.SenderNameSurname;
            _orderService.InsertPayment(newPayment);


            var orderInstallMents = _orderInstallmentService.GetOrderInstallmentsByOrderId(model.OrderId).Where(x => x.IsPaid == false).ToList();
            var firstOrderInstallment = orderInstallMents.FirstOrDefault();
            if (firstOrderInstallment != null)
            {
                var rest = firstOrderInstallment.Amunt - PaidPrice;
                if (rest >= 0)
                {
                    firstOrderInstallment.IsPaid = true;
                    firstOrderInstallment.PaymentId = newPayment.PaymentId;
                    if (orderInstallMents.Count > 1)
                    {
                        var secondInstallment = orderInstallMents[1];
                        secondInstallment.Amunt += rest;
                        order.PayDate = secondInstallment.PayDate;
                        _orderInstallmentService.UpdateOrderInstallment(secondInstallment);
                        _orderService.UpdateOrder(order);
                    }

                }
                else
                {
                    firstOrderInstallment.IsPaid = true;
                    firstOrderInstallment.PaymentId = newPayment.PaymentId;
                    if (orderInstallMents.Count > 1)
                    {
                        for (int i = 1; i < orderInstallMents.Count; i++)
                        {
                            if (rest != 0)
                            {
                                var secondInstallment = orderInstallMents[i];
                                if (Math.Abs(rest) > secondInstallment.Amunt) //--1400 800 
                                {
                                    rest = Math.Abs(rest) - secondInstallment.Amunt;

                                    secondInstallment.IsPaid = true;
                                }
                                else
                                {
                                    if (orderInstallMents[i + 1] != null)
                                    {
                                        var anotherIns = orderInstallMents[i + 1];
                                        anotherIns.Amunt =anotherIns.Amunt+ (secondInstallment.Amunt + rest);
                                        _orderInstallmentService.UpdateOrderInstallment(anotherIns);

                                    }
                                    else
                                    {
                                        secondInstallment.Amunt = secondInstallment.Amunt + rest;
                                    }
                                    secondInstallment.IsPaid = true;
                                    rest = 0;

                                }

                                order.PayDate = secondInstallment.PayDate;
                                _orderInstallmentService.UpdateOrderInstallment(secondInstallment);
                                _orderService.UpdateOrder(order);
                            }
                        }

                    }


                }
                _orderInstallmentService.UpdateOrderInstallment(firstOrderInstallment);

            }
            if (newPayment.RestAmount == 0)
            {
                order.PriceCheck = true;
                _orderService.UpdateOrder(order);
            }

            PreparePaymentModel(model);

            return View(model);

        }

        private void PreparePaymentModel(PaymentModel model)
        {
            var order = _orderService.GetOrderByOrderId(model.OrderId);
            var payments = _orderService.GetPaymentsByOrderId(order.OrderId);

            var memberStore = entities.MemberStores.FirstOrDefault(x => x.StoreMainPartyId == order.MainPartyId);
            LinkHelper lHelper = new LinkHelper();
            var link = lHelper.Encrypt(memberStore.MemberMainPartyId.ToString());
            model.PayUrl = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + link;
            if (order.PriceCheck == true && payments.Count == 0)
            {

                var newPayment = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
                newPayment.OrderId = order.OrderId;
                newPayment.PaidAmount = order.OrderPrice;
                newPayment.RecordDate = DateTime.Now;
                newPayment.RestAmount = 0;
                newPayment.PaymentType = order.OrderType;
                _orderService.InsertPayment(newPayment);
                payments.Add(newPayment);
            }

            var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
            model.TotalPaidAmount = payments.Select(x => x.PaidAmount).Sum();

            foreach (var item in payments)
            {
                string bankName = "";
                if (item.BankConstantId.HasValue)
                {
                    var bank = _constantService.GetConstantByConstantId(Convert.ToInt16(item.BankConstantId.Value));
                    bankName = bank.ConstantName;
                }

                model.PaymentItems.Add(new PaymentItemModel
                {
                    PaymentId = item.PaymentId,
                    PaidAmount = item.PaidAmount,
                    PaymentType = item.PaymentType,
                    RecordDate = item.RecordDate,
                    RestAmount = item.RestAmount,
                    PaymentDate = item.PaymentDate.HasValue ? item.PaymentDate.Value.ToString("dd.MM.yyyy") : (item.RecordDate != null) ? item.RecordDate.ToString("dd.MM.yyyy") : "",
                    Description = item.Description,
                    SenderNameSurname = item.SenderNameSurname,
                    BankName = bankName
                });
            }
            model.StoreName = store.StoreName;
            var returnInvoices = _orderService.GetReturnInvoicesByOrderId(model.OrderId);
            foreach (var item in returnInvoices)
            {
                model.ReturnInvices.Add(new ReturnInvoiceItemModel
                {
                    Amount = item.Amount,
                    Id = item.ReturnInvoiceId,
                    RecordDate = item.RecordDate
                });
            }
            var banks = _constantService.GetConstantByConstantType(ConstantTypeEnum.PaymentBank);
            foreach (var item in banks)
            {
                model.Banks.Add(new SelectListItem
                {
                    Text = item.ConstantName,
                    Value = item.ConstantId.ToString()
                });

            }

        }
        #endregion
        [HttpPost]
        public JsonResult ReturnAmountAdd(string orderId, string amount, string billdate)
        {
            try
            {
                var order = _orderService.GetOrderByOrderId(Convert.ToInt32(orderId));
                _orderService.UpdateOrder(order);
                var returnInvoice = new global::MakinaTurkiye.Entities.Tables.Checkouts.ReturnInvoice();
                returnInvoice.Amount = Convert.ToDecimal(amount);
                returnInvoice.OrderId = Convert.ToInt32(orderId);
                returnInvoice.RecordDate = DateTime.Now;
                if (!string.IsNullOrEmpty(billdate))
                {
                    returnInvoice.InvoiceDate = Convert.ToDateTime(billdate);
                }
                _orderService.InsertReturnInvoice(returnInvoice);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteReturnInvoice(int invoiceId)
        {
            var returnInvoice = _orderService.GetReturnInvoiceByReturnInvoiceId(invoiceId);
            var order = _orderService.GetOrderByOrderId(returnInvoice.OrderId);
            _orderService.UpdateOrder(order);
            _orderService.DelteReturnInvoice(returnInvoice);
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult DeletePayment(int paymentId)
        {
            decimal paidAmount = 0;

            int orderId = 0;
            var payment = _orderService.GetPaymentByPaymentId(paymentId);
            paidAmount = payment.PaidAmount;
            orderId = payment.OrderId;
            _orderService.DeletePayment(payment);
            var paymentLast = _orderService.GetPaymentsByOrderId(orderId).LastOrDefault();

            return RedirectToAction("Payments", new { OrderId = orderId });

        }

        public ActionResult Reports()
        {
            PAGEID = PermissionPage.OrderReport;
            int currentPage = 1;
            int pageDimension = 100;
            var whereClause = new StringBuilder("Where");
            string equalClause = " {0} = {1} ";
            int totalRecord = 0;
            int totalPrice = 0;
            int totalPaid = 0;
            string graderClause = " {0} > {1}";
            //whereClause.AppendFormat(graderClause, "RestAmount", 0);
            OrderReportModel model = new OrderReportModel();
            FilterModel<OrderReportItemModel> filterModel = new FilterModel<OrderReportItemModel>();
            var reports = _orderService.GetOrderReports(pageDimension, currentPage, "", "", "desc", out totalRecord, out totalPrice, out totalPaid);
            var list = PrepareReportList(reports);
            filterModel.Source = list;
            filterModel.TotalRecord = totalRecord;
            filterModel.CurrentPage = currentPage;
            filterModel.PageDimension = pageDimension;
            model.OrderReportItems = filterModel;
            model.TotalPaid = totalPaid;
            model.TotalPrice = totalPrice;
            model.TotalRestPrice = totalPrice - totalPaid;
            return View(model);
        }
        [HttpPost]
        public ActionResult Reports(int page, string recordDate, string storeName, string payDate)
        {
            int pageDimension = 100;
            var whereClause = new StringBuilder("Where");
            int totalRecord = 0;
            int totalPrice = 0;
            int totalPaid = 0;
            bool opt = false;
            //string equalClause = " {0} = {1} ";
            //string graderClause = " {0} > {1}";
            OrderReportModel model = new OrderReportModel();
            FilterModel<OrderReportItemModel> filterModel = new FilterModel<OrderReportItemModel>();
            if (!string.IsNullOrEmpty(recordDate))
            {
                string dateGrader = " Cast(o.RecordDate as date) >= convert(date, '{0}', 103) ";
                whereClause.AppendFormat(dateGrader, recordDate);
                opt = true;
            }
            if (!string.IsNullOrEmpty(payDate))
            {
                if (opt)
                    whereClause.Append(" AND");
                string dateGrader = " Cast(Tarih as date) = convert(date, '{0}', 103) ";
                whereClause.AppendFormat(dateGrader, payDate);
                opt = true;
            }
            if (!string.IsNullOrEmpty(storeName))
            {
                if (opt)
                    whereClause.Append(" AND");
                string likeClaue = " {0} LIKE N'{1}%' ";
                whereClause.AppendFormat(likeClaue, "dbo.Store.StoreName", storeName);

            }

            var reports = _orderService.GetOrderReports(pageDimension, page, whereClause.ToString() == "Where" ? "" : whereClause.ToString(), "", "desc", out totalRecord, out totalPrice, out totalPaid);
            var list = PrepareReportList(reports);
            filterModel.Source = list;
            filterModel.TotalRecord = totalRecord;
            filterModel.CurrentPage = page;
            filterModel.PageDimension = pageDimension;
            model.OrderReportItems = filterModel;
            model.TotalPaid = totalPaid;
            model.TotalPrice = totalPrice;
            model.TotalRestPrice = totalPrice - totalPaid;

            return View("_OrderReportList", model);
        }
        public List<OrderReportItemModel> PrepareReportList(IList<global::MakinaTurkiye.Entities.StoredProcedures.Orders.OrderReportResultModel> reports)
        {
            List<OrderReportItemModel> list = new List<OrderReportItemModel>();
            foreach (var item in reports)
            {
                list.Add(new OrderReportItemModel
                {
                    CallingCount = item.CallingCount,
                    Description = item.Description,
                    InvoiceNumber = item.InvoiceNumber,
                    InvoiveStatus = item.InvoiveStatus.HasValue ? item.InvoiveStatus == true ? "Kesildi" : "Bekliyor" : "",
                    MainPartyId = item.MainPartyId,
                    OrderCancelled = item.OrderCancelled.HasValue ? item.OrderCancelled.Value == true ? "İptal Edildi" : "" : "",
                    OrderId = item.OrderId,
                    orderNo = item.orderNo,
                    OrderPrice = item.OrderPrice.HasValue ? item.OrderPrice : 0,
                    OrderType = item.OrderType.HasValue ? item.OrderType == 1 ? "Havale" : "Kredi Kartı" : "",
                    PacketStatu = item.PacketStatu,
                    PaidAmount = item.PaidAmount.HasValue ? item.PaidAmount : 0,
                    RecordDate = item.RecordDateOrder,
                    RestAmount = item.RestAmount,
                    StoreName = item.StoreName,
                    UserName = item.UserName,
                    PayDate = item.PayDate
                });


            }

            return list;

        }

        public ActionResult BillNumber(int id, string page)
        {
            if (page == "success")
            {
                ViewBag.Success = true;
            }
            var order = _orderService.GetOrderByOrderId(id);
            EbillNumberModel model = new EbillNumberModel();
            model.EbillNumber = order.EBillNumber;
            model.OrderId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult BillNumber(EbillNumberModel model)
        {
            var order = _orderService.GetOrderByOrderId(model.OrderId);
            order.EBillNumber = model.EbillNumber;
            order.InvoiceDate = DateTime.Now;
            _orderService.UpdateOrder(order);

            return RedirectToAction("BillNumber", new { page = "success" });
        }


        public ActionResult OrderCount()
        {
            var dateNow = DateTime.Now;
            OrderCountModel model = new OrderCountModel();
            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            int counter = 1;

            while (dateNow.Date.Month >= counter)
            {
                if (!string.IsNullOrEmpty(monthNames[counter - 1]))
                {
                    if (dateNow.Date.Month >= counter)
                    {
                        model.Months.Add(
                           new SelectListItem
                           {
                               Text = monthNames[counter - 1],
                               Value = counter.ToString()
                           });
                        counter++;
                    }
                    else
                        break;
                }
            }
            model.Months.Reverse();

            var orderCountList = PrepareOrderCoutModel(DateTime.Now);
            model.OrderCountItems = orderCountList;
            return View(model);
        }

        public List<OrderCountItemModel> PrepareOrderCoutModel(DateTime dateNow)
        {

            var orders = _orderService.GetAllOrders().Where(x => x.RecordDate.Date.Month == dateNow.Date.Month && x.RecordDate.Date.Year == dateNow.Date.Year && x.OrderCancelled != true && x.OrderPacketType != 2);
            var s = orders.Where(x => x.AuthorizedId > 0).GroupBy(x => (int)x.AuthorizedId).Select(g => new { id = g.Key, count = g.Count() }).OrderBy(x => x.count);
            List<OrderCountItemModel> list = new List<OrderCountItemModel>();
            foreach (var item in s)
            {
                var user = entities.Users.FirstOrDefault(x => x.UserId == item.id);
                var orderByuser = orders.Where(x => x.AuthorizedId == item.id);
                decimal totalAmount = orderByuser.Select(x => x.OrderPrice).Sum();
                var mainPartyIds = orderByuser.Select(x => x.MainPartyId).ToList();
                var stores = _storeService.GetStoresByMainPartyIds(mainPartyIds);
                var storeNames = string.Join(",", stores.Select(x => x.StoreShortName));
                list.Add(new OrderCountItemModel
                {
                    Count = item.count,
                    Username = user.UserName,
                    TotalAmount = totalAmount.ToString("N2"),
                    StoreNames = storeNames
                });
            }
            return list;
        }

        [HttpGet]
        public PartialViewResult OrderCountItem(string month)
        {

            var dateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(month), DateTime.Now.Date.Day);
            var orderList = PrepareOrderCoutModel(dateTime);
            return PartialView("_OrderCountList", orderList);
        }



        public ActionResult SalesResponsibleUpdate(int orderId)
        {
            SalesResponsibleUpdateModel model = new SalesResponsibleUpdateModel();
            model.OrderId = orderId;
            var order = _orderService.GetOrderByOrderId(orderId);
            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where  g.UserGroupId == 16 || g.UserGroupId == 20 || g.UserGroupId == 22 || g.UserGroupId == 18  select new { u.UserName, u.UserId };
            foreach (var item in users1)
            {
                model.SalesResponsibleUser.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString(), Selected = item.UserId == order.AuthorizedId });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SalesResponsibleUpdate(string OrderId, string SalesUserId)
        {
            var order = _orderService.GetOrderByOrderId(Convert.ToInt32(OrderId));
            order.AuthorizedId = Convert.ToInt32(SalesUserId);
            _orderService.UpdateOrder(order);
            TempData["Message"] = "Başarıyla Kayıt Edilmiştir";
            return RedirectToAction("SalesResponsibleUpdate", new { orderId = OrderId });
        }

    }


}