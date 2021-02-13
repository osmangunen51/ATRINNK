using global::MakinaTurkiye.Services.Catalog;
using global::MakinaTurkiye.Services.Checkouts;
using global::MakinaTurkiye.Services.Common;
using global::MakinaTurkiye.Services.Members;
using global::MakinaTurkiye.Services.Packets;
using global::MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.MailHelpers;
using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using NeoSistem.MakinaTurkiye.Management.Models.Orders;
using NeoSistem.MakinaTurkiye.Management.Models.Stores;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using AdressExtenstionNew = global::MakinaTurkiye.Entities.Tables.Common.AddressExtensions;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class StoreController : BaseController
    {
        #region Constants

        const string STARTCOLUMN = "S.MainPartyId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;
        const string SessionPage = "store_PAGEDIMENSION";

        string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };
        #endregion

        #region Fields

        private readonly ILoginLogService _loginLogService;
        private readonly IWhatsappLogService _whatsapplogService;
        private readonly IStoreService _storeService;
        private readonly IPacketService _packetService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMemberService _memberService;
        private readonly IAddressService _adressService;
        private readonly IPhoneService _phoneService;
        private readonly IProductService _productService;
        private readonly IStoreNewService _storeNewService;
        private readonly IOrderService _orderService;
        private readonly IOrderInstallmentService _orderInstallmentService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreSectorService _storeSectorService;
        private readonly IStoreDiscountService _storeDiscountService;
        private readonly IStoreSeoNotificationService _storeSeoNotificationService;


        #endregion

        #region Ctor

        public StoreController(ILoginLogService loginLogService, IWhatsappLogService whatsapplogService, IStoreService storeService, IPacketService packetService, IMemberStoreService memberStoreService, IMemberService memberService,
            IPhoneService phoneService, IAddressService addressService, IProductService productService,
            IStoreNewService storeNewService, IOrderService orderService
            , IOrderInstallmentService orderInstallmentService,
             IStoreSectorService storeSectorService,
             ICategoryService categoryService,
              IStoreDiscountService storeDiscountService,
              IStoreSeoNotificationService storeSeoNotificationService)
        {
            this._loginLogService = loginLogService;
            this._whatsapplogService = whatsapplogService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._memberStoreService = memberStoreService;
            this._memberService = memberService;
            this._phoneService = phoneService;
            this._adressService = addressService;
            this._productService = productService;
            this._storeNewService = storeNewService;
            this._orderService = orderService;
            this._orderInstallmentService = orderInstallmentService;
            this._storeSectorService = storeSectorService;
            this._categoryService = categoryService;
            this._storeDiscountService = storeDiscountService;
            this._storeSeoNotificationService = storeSeoNotificationService;

            _phoneService.CachingGetOrSetOperationEnabled = false;


        }

        #endregion

        static Data.Store dataStore = null;
        static ICollection<StoreModel> collection = null;

        #region Methods

        public ActionResult PacketStatuSearch(int id)
        {
            return RedirectToAction("Index", new { PacketStatu = id });
        }

        public string Statistic(int id)
        {
            var product = entities.Products.Where(c => c.MainPartyId == id).ToList();

            int singleproduct = 0;
            int pluralproduct = 0;
            int activepro = 0;
            int pasifpro = 0;
            if (product != null)
            {
                foreach (var spro in product)
                {
                    singleproduct = singleproduct + spro.SingularViewCount.ToInt32();
                    pluralproduct = pluralproduct + spro.ViewCount.ToInt32();
                    if (spro.ProductActive == false)
                    {
                        pasifpro = pasifpro + 1;
                    }
                    else activepro = activepro + 1;
                }
            }

            StringBuilder s = new StringBuilder();
            s.Append("<span style='color:#bababa;'>Toplam Ürün: </span>");
            s.Append(product.Count);
            s.Append("<br />");
            s.Append("<span style='color:#bababa;'>Aktif: </span>");
            s.Append(activepro);
            s.Append("<br />");
            s.Append("<span style='color:#bababa;'>Pasif: </span>");
            s.Append(pasifpro);
            s.Append("<br />");
            s.Append("<span style='color:#bababa;'>Çoğul: </span>");
            s.Append(pluralproduct);
            s.Append("<br />");
            s.Append("<span style='color:#bababa;'>Tekil: </span>");
            s.Append(singleproduct);
            return s.ToString();
        }

        public ActionResult Index()
        {
            PAGEID = PermissionPage.Firmalar;

            int total = 0;
            dataStore = new Data.Store();

            string whereClause = string.Empty;
            if (Request.QueryString["PacketStatu"] != null)
            {
                whereClause = "Where S.StoreActiveType = " + Request.QueryString["PacketStatu"].ToInt32();
            }

            if (Session[SessionPage] == null)
            {
                Session[SessionPage] = PAGEDIMENSION;
            }
            int page = 1;
            if (Request.QueryString["page"] != null) page = Convert.ToInt32(Request.QueryString["page"]);
            collection = dataStore.Search(ref total, (int)Session[SessionPage], page, whereClause, STARTCOLUMN, ORDER).AsCollection<StoreModel>();
            LinkHelper lHelper = new LinkHelper();
            foreach (var item in collection)
            {

                var link = lHelper.Encrypt(item.MemberMainPartyId.ToString());
                item.LogingLink = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + link + "&returnUrl=/account/advert/advert";

            }
            List<SelectListItem> portfoyUsersModel = new List<SelectListItem>();
            List<SelectListItem> salesUsersModel = new List<SelectListItem>();

            var salesUsers = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 16 || g.UserGroupId == 20 select new { u.UserName, u.UserId };
            salesUsersModel.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in salesUsers)
            {
                salesUsersModel.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }

            var portfoyUsers = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 18 select new { u.UserName, u.UserId };
            portfoyUsersModel.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in portfoyUsers)
            {
                portfoyUsersModel.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }
            ViewData["PortfoyUsers"] = portfoyUsersModel;
            ViewData["SalesUsers"] = salesUsersModel;
            var model = new FilterModel<StoreModel>
            {
                CurrentPage = page,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };
            foreach (var item in model.Source)
            {
                foreach (var item2 in entities.MemberStores)
                {
                    if (item.MainPartyId == item2.StoreMainPartyId)
                    {
                        item.member = (from c in entities.Members
                                       where c.MainPartyId == item2.MemberMainPartyId
                                       select c).SingleOrDefault();
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(StoreModel model, string OrderName, string Order, int? Page, int PageDimension, byte? PacketStatus)
        {

            dataStore = dataStore ?? new Data.Store();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'%{1}%' ";
            string equalClause = " {0} = {1} ";
            bool op = false;

            if (!string.IsNullOrWhiteSpace(model.StoreNo))
            {
                whereClause.AppendFormat(likeClaue, "StoreNo", model.StoreNo);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.StoreName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                whereClause.Append("Or");
                whereClause.AppendFormat(likeClaue, "StoreShortName", model.StoreName);
                op = true;
            }
            if (PacketStatus > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "S.StoreActiveType", PacketStatus);
                op = true;
            }
            if (!string.IsNullOrWhiteSpace(model.MainPartyFullName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "MainPartyFullName", model.MainPartyFullName);
                op = true;
            }

            if (model.PacketId > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "S.PacketId", model.PacketId);
                op = true;
            }

            if (model.StoreRecordDate != new DateTime())
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = " Cast(StoreRecordDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.StoreRecordDate.ToString("yyyyMMdd"));
            }

            if (model.StorePacketEndDate.HasValue)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = " Cast(StorePacketEndDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.StorePacketEndDate.Value.ToString("yyyyMMdd"));
            }

            if (model.StoreActiveType.HasValue)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "StoreActiveType", model.StoreActiveType);
                op = true;
            }
            if (model.AuthorizedId != 0 && model.AuthorizedId != null)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "AuthorizedId", model.AuthorizedId);
                op = true;
            }
            if (model.PortfoyUserId != 0 && model.PortfoyUserId != null)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "PortfoyUserId", model.PortfoyUserId);
                op = true;
            }
            if (!string.IsNullOrWhiteSpace(model.StoreWeb))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "StoreWeb", model.StoreWeb);
                op = true;
            }


            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            int total = 0;

            Session[SessionPage] = PageDimension;
            collection =
              dataStore.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<StoreModel>();
            LinkHelper lHelper = new LinkHelper();
            foreach (var item in collection)
            {
                var link = lHelper.Encrypt(item.MemberMainPartyId.ToString());
                item.LogingLink = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + link + "&returnUrl=/account/advert/advert";
            }
            var filterItems = new FilterModel<StoreModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection
            };

            return View("StoreList", filterItems);
        }


        [HttpGet]
        public ActionResult ExportStores(StoreModel model, string OrderName, string Order, int? Page, int PageDimension, byte? PacketStatus)
        {
            dataStore = dataStore ?? new Data.Store();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'%{1}%' ";
            string equalClause = " {0} = {1} ";
            bool op = false;

            if (!string.IsNullOrWhiteSpace(model.StoreNo))
            {
                whereClause.AppendFormat(likeClaue, "StoreNo", model.StoreNo);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.StoreName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                whereClause.Append("Or");
                whereClause.AppendFormat(likeClaue, "StoreShortName", model.StoreName);
                op = true;
            }
            if (PacketStatus > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "S.StoreActiveType", PacketStatus);
                op = true;
            }
            if (!string.IsNullOrWhiteSpace(model.MainPartyFullName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "MainPartyFullName", model.MainPartyFullName);
                op = true;
            }

            if (model.PacketId > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "S.PacketId", model.PacketId);
                op = true;
            }

            if (model.StoreRecordDate != new DateTime())
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = " Cast(StoreRecordDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.StoreRecordDate.ToString("yyyyMMdd"));
            }

            if (model.StorePacketEndDate.HasValue)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = " Cast(StorePacketEndDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.StorePacketEndDate.Value.ToString("yyyyMMdd"));
            }

            if (model.StoreActiveType.HasValue)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "StoreActiveType", model.StoreActiveType);
                op = true;
            }
            if (model.AuthorizedId != 0 && model.AuthorizedId != null)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "AuthorizedId", model.AuthorizedId);
                op = true;
            }
            if (model.PortfoyUserId != 0 && model.PortfoyUserId != null)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "PortfoyUserId", model.PortfoyUserId);
                op = true;
            }
            if (!string.IsNullOrWhiteSpace(model.StoreWeb))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "StoreWeb", model.StoreWeb);
                op = true;
            }


            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            int total = 0;

            Session[SessionPage] = PageDimension;
            collection =
              dataStore.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<StoreModel>();
            LinkHelper lHelper = new LinkHelper();
            var list = new List<MTStoreExcelItem>();
            foreach (var item in collection)
            {
                var link = lHelper.Encrypt(item.MemberMainPartyId.ToString());
                string logingLink = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + link + "&returnUrl=/account/Store/UpdateStore";
                var phones = _phoneService.GetPhonesByMainPartyId(item.MainPartyId);
                var gsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Gsm);
                var localPhones = phones.Where(x => x.PhoneType == (byte)PhoneTypeEnum.Phone);
                var whatsapp = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Whatsapp);
                string gsmPhone = "", phone1 = "", phone2 = "", whatsappPhone = "";
                gsmPhone = PhoneNumberToString(gsm);
                whatsappPhone = PhoneNumberToString(whatsapp);
                int i = 1;
                foreach (var ph in localPhones)
                {
                    if (i == 1)
                        phone1 = PhoneNumberToString(ph);
                    else
                        phone2 = PhoneNumberToString(ph);
                    i++;
                }
                list.Add(new MTStoreExcelItem
                {
                    StoreMainPartyId = item.MainPartyId,
                    StoreName = GetText(item.StoreName),
                    StoreEstablishDate = item.StoreEstablishmentDate.HasValue ? item.StoreEstablishmentDate.Value : 0,
                    StoreProfileHomeDescription = GetText(item.StoreProfileHomeDescription),
                    StoreAbout = GetText(item.StoreAbout),
                    StoreShortName = GetText(item.StoreShortName),
                    StoreUrlName = GetText(item.StoreUrlName),
                    StoreWeb = GetText(item.StoreWeb),
                    StoreCapital = item.StoreCapital,
                    StoreEmail = GetText(item.StoreEMail),
                    StoreEmployeesCount = item.StoreEmployeesCount,
                    StoreEndorsement = item.StoreEndorsement,
                    TaxNumber = GetText(item.TaxNumber),
                    TaxOffice = GetText(item.TaxOffice),
                    TradeRegistrNo = GetText(item.TradeRegistrNo),
                    MersisNo = GetText(item.MersisNo),
                    PurchasingDepartmentEmail = GetText(item.PurchasingDepartmentEmail),
                    PurchasingDepartmentName = GetText(item.PurchasingDepartmentName),
                    SeoDescription = GetText(item.SeoDescription),
                    SeoKeyword = GetText(item.SeoKeyword),
                    SeoTitle = GetText(item.SeoTitle),
                    Gsm = gsmPhone,
                    Phone1 = phone1,
                    Phone2 = phone2,
                    Whatsapp = whatsappPhone,
                    Status = item.Active.ToString(),
                    AccountLink = logingLink
                });
            }
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("sheet1");

            var properties = new[] { "StoreMainPartyId", "StoreName", "StoreEstablishDate", "StoreProfileHomeDescription", "StoreAbout", "StoreShortName", "StoreUrlName",
                "StoreWeb","StoreCapital","StoreEmail","StoreEmployeesCount","StoreEndorsement","TaxNumber","TaxOffice","TradeRegistrNo","MersisNo","PurchasingDepartmentEmail","PurchasingDepartmentName",
            "SeoDescription", "SeoKeyword","SeoTitle", "Gsm","Phone1","Phone2","Whatsapp","Status", "AccountLink"};

            var headers = new[] { "StoreMainPartyId", "StoreName", "StoreEstablishDate", "StoreProfileHomeDescription", "StoreAbout", "StoreShortName", "StoreUrlName",
                "StoreWeb","StoreCapital","StoreEmail","StoreEmployeesCount","StoreEndorsement","TaxNumber","TaxOffice","TradeRegistrNo","MersisNo","PurchasingDepartmentEmail","PurchasingDepartmentName",
            "SeoDescription", "SeoKeyword","SeoTitle", "Gsm","Phone1","Phone2","Whatsapp","Status", "AccountLink"};

            var headerRow = sheet.CreateRow(0);

            // create header
            for (int i = 0; i < properties.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
            }

            // fill content 
            for (int i = 0; i < list.Count; i++)
            {
                var rowIndex = i + 1;
                var row = sheet.CreateRow(rowIndex);

                for (int j = 0; j < properties.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var o = list[i];

                    cell.SetCellValue(o.GetType().GetProperty(properties[j]).GetValue(o, null).ToString());
                }
            }

            using (var stream = new MemoryStream())
            {
                workbook.Write(stream);
                stream.Close();
                return File(stream.ToArray(), "application/vnd.ms-excel", "firma_listesi.xls");
            }


        }
        private string PhoneNumberToString(global::MakinaTurkiye.Entities.Tables.Common.Phone phone)
        {
            if (phone != null)
            {
                string phSplitter = "_";
                return phone.PhoneCulture.Replace("+", "") + phSplitter + phone.PhoneAreaCode + phSplitter + phone.PhoneNumber;
            }
            return "";
        }
        public ActionResult UpdateStoreByExel()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UpdateStoreByExel(HttpPostedFileBase file)
        {
            bool updated = false;
            if (file != null && file.ContentLength > 0)
            {
                HttpPostedFileBase files = Request.Files[0]; //Read the Posted Excel File  
                ISheet sheet; //Create the ISheet object to read the sheet cell values  
                string filename = Path.GetFileName(Server.MapPath(files.FileName)); //get the uploaded file name  
                var fileExt = Path.GetExtension(filename); //get the extension of uploaded excel file  
                string[] arr = new string[] { ".xls", ".xlsx" };
                if (arr.Contains(fileExt))
                {

                    HSSFWorkbook hssfwb = new HSSFWorkbook(files.InputStream); //HSSWorkBook object will read the Excel 97-2000 formats  
                    sheet = hssfwb.GetSheetAt(0); //get first Excel sheet from workbook  


                    for (int row = 1; row <= sheet.LastRowNum; row++) //Loop the records upto filled row  
                    {
                        if (sheet.GetRow(row) != null) //null is when the row only contains empty cells   
                        {
                            int mainPartyId = GetNumericalValueFromExcel(sheet.GetRow(row).GetCell(0));
                            string storeName = sheet.GetRow(row).GetCell(1).StringCellValue;
                            int establishYear = GetNumericalValueFromExcel(sheet.GetRow(row).GetCell(2));
                            string storeProfileHomeDescription = GetStringValueFromExcel(sheet.GetRow(row).GetCell(3));
                            string storeAbout = GetStringValueFromExcel(sheet.GetRow(row).GetCell(4));
                            string storeShortName = GetStringValueFromExcel(sheet.GetRow(row).GetCell(5));
                            string storeUrlName = GetStringValueFromExcel(sheet.GetRow(row).GetCell(6));
                            string storeWeb = GetStringValueFromExcel(sheet.GetRow(row).GetCell(7));
                            int storeCapital = GetNumericalValueFromExcel(sheet.GetRow(row).GetCell(8));
                            string storeEmail = GetStringValueFromExcel(sheet.GetRow(row).GetCell(9));
                            int storeEmployeeCount = GetNumericalValueFromExcel(sheet.GetRow(row).GetCell(10));
                            int storeEndorsement = GetNumericalValueFromExcel(sheet.GetRow(row).GetCell(11));
                            string taxNumber = GetStringValueFromExcel(sheet.GetRow(row).GetCell(12));
                            string taxOffice = GetStringValueFromExcel(sheet.GetRow(row).GetCell(13));
                            string tradeRegistrNo = GetStringValueFromExcel(sheet.GetRow(row).GetCell(14));
                            string mersisNo = GetStringValueFromExcel(sheet.GetRow(row).GetCell(15));
                            string pDeparmentEmail = GetStringValueFromExcel(sheet.GetRow(row).GetCell(16));
                            string pDeparmentName = GetStringValueFromExcel(sheet.GetRow(row).GetCell(17));
                            string seoDescription = GetStringValueFromExcel(sheet.GetRow(row).GetCell(18));
                            string seoKeywords = GetStringValueFromExcel(sheet.GetRow(row).GetCell(19));
                            string seoTitle = GetStringValueFromExcel(sheet.GetRow(row).GetCell(20));
                            string gsm = GetStringValueFromExcel(sheet.GetRow(row).GetCell(21));
                            string phone1 = GetStringValueFromExcel(sheet.GetRow(row).GetCell(22));
                            string phone2 = GetStringValueFromExcel(sheet.GetRow(row).GetCell(23));
                            string whatsappGsm = GetStringValueFromExcel(sheet.GetRow(row).GetCell(24));

                            #region updatestoreinfo
                            var store = _storeService.GetStoreByMainPartyId(mainPartyId);
                            store.StoreName = storeName;
                            store.StoreEstablishmentDate = establishYear != 0 ? establishYear : store.StoreEstablishmentDate;
                            store.StoreProfileHomeDescription = storeProfileHomeDescription;
                            store.StoreAbout = storeAbout;
                            store.StoreShortName = storeShortName;
                            store.StoreUrlName = storeUrlName;
                            store.StoreWeb = storeWeb;
                            store.StoreCapital = storeCapital != 0 ? (byte)storeCapital : store.StoreCapital;
                            store.StoreEMail = storeEmail;
                            store.StoreEmployeesCount = storeEmployeeCount != 0 ? (byte)storeEmployeeCount : store.StoreEmployeesCount;
                            store.StoreEndorsement = storeEndorsement != 0 ? (byte)storeEndorsement : store.StoreEndorsement;
                            store.TaxNumber = taxNumber;
                            store.TaxOffice = taxOffice;
                            store.TradeRegistrNo = tradeRegistrNo;
                            store.MersisNo = mersisNo;
                            store.PurchasingDepartmentName = pDeparmentName;
                            store.PurchasingDepartmentEmail = pDeparmentEmail;
                            store.SeoDescription = seoDescription;
                            store.SeoKeyword = seoKeywords;
                            store.SeoTitle = seoTitle;
                            _storeService.UpdateStore(store);

                            try
                            {
                                char phSplitter = '_';
                                if (!string.IsNullOrEmpty(gsm) && gsm.IndexOf(phSplitter) > 0)
                                {

                                    var p = entities.Phones.FirstOrDefault(x => x.MainPartyId == store.MainPartyId && x.PhoneType == (byte)PhoneType.Gsm);
                                    if (p != null)
                                    {
                                        entities.Phones.DeleteObject(p);
                                        entities.SaveChanges();
                                    }


                                    var gsmPhone = gsm.Split(phSplitter);
                                    var gsmNewPhone = new global::MakinaTurkiye.Entities.Tables.Common.Phone
                                    {
                                        active = 1,
                                        MainPartyId = store.MainPartyId,
                                        PhoneAreaCode = gsmPhone[1],
                                        PhoneCulture = gsmPhone[0],
                                        PhoneType = (byte)PhoneType.Gsm,
                                        PhoneNumber = gsmPhone[2]
                                    };
                                    _phoneService.InsertPhone(gsmNewPhone);
                                }

                                if (!string.IsNullOrEmpty(phone1) && phone1.IndexOf(phSplitter) > 0)
                                {
                                    var phones = entities.Phones.Where(x => x.MainPartyId == store.MainPartyId && x.PhoneType == (byte)PhoneType.Phone).ToList();
                                    foreach (var item in phones)
                                    {
                                        entities.Phones.DeleteObject(item);
                                        entities.SaveChanges();
                                    }
                                    var phoneOne = phone1.Split(phSplitter);
                                    var phoneNew1 = new global::MakinaTurkiye.Entities.Tables.Common.Phone
                                    {
                                        active = 1,
                                        MainPartyId = store.MainPartyId,
                                        PhoneAreaCode = phoneOne[1],
                                        PhoneCulture = "+" + phoneOne[0],
                                        PhoneType = (byte)PhoneType.Phone,
                                        PhoneNumber = phoneOne[2]
                                    };
                                    _phoneService.InsertPhone(phoneNew1);
                                }
                                if (!string.IsNullOrEmpty(phone2) && phone2.IndexOf(phSplitter) > 0)
                                {

                                    var phoneTwo = phone2.Split(phSplitter);
                                    var phoneNew2 = new global::MakinaTurkiye.Entities.Tables.Common.Phone
                                    {
                                        active = 1,
                                        MainPartyId = store.MainPartyId,
                                        PhoneAreaCode = phoneTwo[1],
                                        PhoneCulture = "+" + phoneTwo[0],
                                        PhoneType = (byte)PhoneType.Phone,
                                        PhoneNumber = phoneTwo[2]
                                    };
                                    _phoneService.InsertPhone(phoneNew2);

                                }
                                if (!string.IsNullOrEmpty(whatsappGsm) && whatsappGsm.IndexOf(phSplitter) > 0)
                                {

                                    var p = entities.Phones.FirstOrDefault(x => x.MainPartyId == store.MainPartyId && x.PhoneType == (byte)PhoneType.Whatsapp);
                                    if (p != null)
                                    {
                                        entities.Phones.DeleteObject(p);
                                        entities.SaveChanges();
                                    }

                                    var whatsappPhone = whatsappGsm.Split(phSplitter);
                                    var whatsappNew = new global::MakinaTurkiye.Entities.Tables.Common.Phone
                                    {
                                        active = 1,
                                        MainPartyId = store.MainPartyId,
                                        PhoneAreaCode = whatsappPhone[1],
                                        PhoneCulture = "+" + whatsappPhone[0],
                                        PhoneType = (byte)PhoneType.Whatsapp,
                                        PhoneNumber = whatsappPhone[2]
                                    };
                                    _phoneService.InsertPhone(whatsappNew);
                                }

                            }
                            catch (Exception ex)
                            {


                            }

                            #endregion
                            updated = true;
                        }
                    }
                }
            }
            ViewBag.Updated = updated;
            return View();
        }
        private int GetNumericalValueFromExcel(ICell cell)
        {
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        return Convert.ToInt32(cell.NumericCellValue);

                    case CellType.String:
                        return Convert.ToInt32(cell.StringCellValue);
                    default:
                        return 0;
                }
            }
            else
                return 0;
        }
        private string GetStringValueFromExcel(ICell cell)
        {
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.String:
                        return cell.StringCellValue;
                    default:
                        return "";
                }
            }
            else
                return "";
        }
        private string GetText(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text;
            return "";
        }
        public ActionResult EditShowcase()
        {

            var storeShowcaseItems = entities.Stores.Where(c => c.StoreShowcase == true).ToList();

            return View(storeShowcaseItems);
        }

        [HttpPost]
        public ActionResult EditShowcase(int MainPartyId)
        {
            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == MainPartyId);
            store.StoreShowcase = false;
            entities.SaveChanges();

            return RedirectToAction("EditShowcase");
        }

        public ActionResult EditStore(int id)
        {
            PAGEID = PermissionPage.FirmaDuzenle;

            Session["ImageDelete"] = false;

            var model = new StoreModel();
            var whatsapLogs = _whatsapplogService.GetWhatpLogs();
            var storeWhatsappClick = whatsapLogs.GroupBy(x => x.MainPartyId).Select(g => new { mainpartyId = g.Key, totalClick = g.Sum(i => i.ClickCount) }).FirstOrDefault(x => x.mainpartyId == id);
            if (storeWhatsappClick != null)
                model.WhatsappClickCount = storeWhatsappClick.totalClick;
            else
                model.WhatsappClickCount = 0;
            var curStore = entities.Stores.SingleOrDefault(c => c.MainPartyId == id);
            UpdateClass(curStore, model);

            TempData["type"] = model.StoreActiveType;
            curStore.StoreLogo = !String.IsNullOrEmpty(curStore.StoreLogo) ? ("" + curStore.StoreLogo.Replace(".", "_th.")) : "";
            ViewData["Store"] = true;
            var memberStore = entities.MemberStores.FirstOrDefault(x => x.StoreMainPartyId == curStore.MainPartyId);
            LinkHelper lHelper = new LinkHelper();
            var link = lHelper.Encrypt(memberStore.MemberMainPartyId.ToString());
            ViewData["LoginId"] = "https://www.makinaturkiye.com/membership/LogonAuto?validateId=" + link;

            var curAuth = entities.Users.FirstOrDefault(x => x.UserId == curStore.AuthorizedId);
            var curPortfoy = entities.Users.FirstOrDefault(x => x.UserId == curStore.PortfoyUserId);
            var userPermission = entities.PermissionUsers.FirstOrDefault(x => x.UserId == CurrentUserModel.CurrentManagement.UserId);
            var userGroup = entities.UserGroups.FirstOrDefault(x => x.UserGroupId == userPermission.UserGroupId);
            if (userGroup != null)
                model.GroupName = userGroup.GroupName;
            if (curAuth != null)
            {
                model.AuthName = curAuth.UserName;

            }
            else
            {
                var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 16 || g.UserGroupId == 20 select new { u.UserName, u.UserId };
                //var users = entities.Users.OrderBy(x => x.UserName).ToList();
                var users = users1.ToList();
                if (curStore.AuthorizedId == null || curStore.AuthorizedId == 0)
                    model.Users.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
                foreach (var item in users)
                {
                    var selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };
                    if (curStore.AuthorizedId == item.UserId)
                        selectListItem.Selected = true;
                    model.Users.Add(selectListItem);
                }

            }
            if (curPortfoy != null)
            {
                model.PortfoyUserName = curPortfoy.UserName;
            }
            else
            {
                var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 18 select new { u.UserName, u.UserId };
                //var users = entities.Users.OrderBy(x => x.UserName).ToList();
                var users = users1.ToList();
                if (curStore.PortfoyUserId == null || curStore.PortfoyUserId == 0)
                    model.PortfoyUsers.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
                foreach (var item in users)
                {
                    var selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };
                    if (curStore.PortfoyUserId == item.UserId)
                        selectListItem.Selected = true;
                    model.PortfoyUsers.Add(selectListItem);
                }
            }
            if (model.GroupName == "Administrator" || userGroup.UserGroupId == 18 || CurrentUserModel.CurrentManagement.UserName.Contains("Mehtap"))
            {
                if (model.Users.Count <= 0)
                {
                    var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 16 || g.UserGroupId == 20 select new { u.UserName, u.UserId };
                    //var users = entities.Users.OrderBy(x => x.UserName).ToList();
                    var users = users1.ToList();
                    if (curStore.AuthorizedId == null || curStore.AuthorizedId == 0)
                        model.Users.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
                    foreach (var item in users)
                    {
                        var selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };
                        if (curStore.AuthorizedId == item.UserId)
                            selectListItem.Selected = true;
                        model.Users.Add(selectListItem);
                    }
                }
                if (model.PortfoyUsers.Count <= 0)
                {
                    var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 18 select new { u.UserName, u.UserId };
                    //var users = entities.Users.OrderBy(x => x.UserName).ToList();
                    var users = users1.ToList();
                    if (curStore.PortfoyUserId == null || curStore.PortfoyUserId == 0)
                        model.PortfoyUsers.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
                    foreach (var item in users)
                    {
                        var selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };
                        if (curStore.PortfoyUserId == item.UserId)
                            selectListItem.Selected = true;
                        model.PortfoyUsers.Add(selectListItem);
                    }

                }
            }



            return View(model);
        }

        [HttpPost]
        public ActionResult EditStore(int id, StoreModel model, FormCollection coll)
        {
            if (model.AuthorizedId == 0)
            {
                TempData["ErrorAuth"] = "Portföy Yönetici Seçiniz";
                return RedirectToAction("EditStore", new { id = id });
            }
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(id);
            var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);
            int tempPacketId = 0;
            byte tempStoreActiveType = 0;
            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == id);
            tempPacketId = Convert.ToInt32(store.PacketId);
            tempStoreActiveType = Convert.ToByte(store.StoreActiveType);

            string imageName = store.StoreLogo;
            store.ReadyForSale = model.ReadyForSale;


            store.AuthorizedId = model.AuthorizedId;
            store.PortfoyUserId = model.PortfoyUserId;
            store.StoreActiveType = model.StoreActiveType;
            store.StoreCapital = model.StoreCapital;
            //store.StoreEMail = model.StoreEMail;
            store.StoreEmployeesCount = model.StoreEmployeesCount;
            store.StoreEndorsement = model.StoreEndorsement;
            store.StoreEstablishmentDate = model.StoreEstablishmentDate;
            store.StoreName = model.StoreName;
            store.StoreWeb = model.StoreWeb;
            store.StoreAbout = model.StoreAbout;
            store.StoreType = model.StoreType;
            store.PurchasingDepartmentEmail = model.PurchasingDepartmentEmail;
            store.PurchasingDepartmentName = model.PurchasingDepartmentName;
            store.StoreShowcase = model.StoreShowcase;
            store.PacketId = model.PacketId;
            store.SeoKeyword = model.SeoKeyword;
            store.SeoDescription = model.SeoDescription;
            store.SeoTitle = model.SeoTitle;
            store.StorePacketBeginDate = model.StorePacketBeginDate;
            store.IsProductAdded = model.IsProductAdded;
            store.StorePacketEndDate = model.StorePacketEndDate;
            //store.StoreUniqueShortName = model.StoreUniqueShortName;
            store.TaxOffice = model.TaxOffice;
            store.TaxNumber = model.TaxNumber;
            store.TradeRegistrNo = model.TradeRegistrNo;
            store.MersisNo = model.MersisNo;
            store.StoreShortName = model.StoreShortName;
            store.StoreUrlName = UrlBuilder.ToUrl(model.StoreUrlName);
            store.StoreActiveType = model.StoreActiveType;
            store.IsAllowProductSellUrl = model.IsAllowProductSellUrl;
            var packetfeauture = entities.PacketFeatures.Where(c => c.PacketId == model.PacketId && c.PacketFeatureTypeId == 3).FirstOrDefault();
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

            if (Session["ImageDelete"].ToBoolean())
            {
                //FileHelpers.Delete("/UserFiles/Images/StoreLogo/" + store.StoreLogo);
                //FileHelpers.Delete(AppSettings.StoreLogoThumb100x100Folder + store.StoreLogo);
                //FileHelpers.Delete(AppSettings.StoreLogoThumb300x200Folder + store.StoreLogo);
                //FileHelpers.Delete(AppSettings.StoreLogoThumb170x90Folder + store.StoreLogo);
                //FileHelpers.Delete(AppSettings.StoreLogoThumb200x100Folder + store.StoreLogo);
                //FileHelpers.Delete(AppSettings.StoreLogoThumb55x40Folder + store.StoreLogo);
                //FileHelpers.Delete(AppSettings.StoreLogoThumb75x75Folder + store.StoreLogo);
                //store.StoreLogo = String.Empty;
            }
            else
                store.StoreLogo = imageName;

            if (Request.Files.Count > 0 && Request.Files[0].ContentType != "application/octet-stream")
            {
                //var thumns = new Dictionary<string, string>();
                //thumns.Add(AppSettings.StoreLogoThumb110x110Folder, "110x110");
                //thumns.Add(AppSettings.StoreLogoThumb150x90Folder, "150x90");
                //thumns.Add(AppSettings.StoreLogoThumb170x90Folder, "170x90");
                //thumns.Add(AppSettings.StoreLogoThumb200x100Folder, "200x100");
                //thumns.Add(AppSettings.StoreLogoThumb55x40Folder, "55x40");
                //thumns.Add(AppSettings.StoreLogoThumb75x75Folder, "75x75");
                //thumns.Add(AppSettings.StoreLogoThumb100x100Folder, "100x100");
                //thumns.Add(AppSettings.StoreLogoThumb300x200Folder, "300x200");
                //imageName = FileHelpers.ImageResize("/UserFiles/Images/StoreLogo/", Request.Files[0], thumns);
                //store.StoreLogo = imageName;

                if (!string.IsNullOrEmpty(store.StoreName))
                {
                    string resizeStoreFolder = this.Server.MapPath(AppSettings.ResizeStoreLogoFolder);
                    string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;

                    List<string> thumbSizesForStoreLogo = new List<string>();
                    thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));

                    string newStoreLogoImageFilePath = resizeStoreFolder + store.MainPartyId.ToString() + "\\";
                    if (!Directory.Exists(newStoreLogoImageFilePath + "thumbs"))
                    {
                        var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, store.MainPartyId.ToString()));
                        di.CreateSubdirectory("thumbs");
                    }

                    // eski logoyu kopyala, varsa ustune yaz
                    string newStoreLogoImageFileName = store.StoreShortName.ToImageFileName() + "_logo.jpg";
                    string storeLogoImageFilePath = newStoreLogoImageFilePath + newStoreLogoImageFileName;
                    Request.Files[0].SaveAs(storeLogoImageFilePath);

                    bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                    newStoreLogoImageFilePath + "thumbs\\" + store.StoreShortName.ToImageFileName(), thumbSizesForStoreLogo);
                    store.StoreLogo = newStoreLogoImageFileName;
                    entities.SaveChanges();
                }

            }
            bool productCountCalculated = false;
            if (tempPacketId != model.PacketId || tempStoreActiveType != model.StoreActiveType)
            {
                var packet = (from p in entities.Packets where p.PacketId == model.PacketId select p).FirstOrDefault();
                var memberStoresId = entities.MemberStores.Where(c => c.StoreMainPartyId.Value == store.MainPartyId).Select(x => x.MemberMainPartyId).ToList();
                var productItems = entities.Products.Where(c => memberStoresId.Contains(c.MainPartyId) && c.ProductActiveType != 8).ToList();
                int sayi = productItems.Count;

                if (packet.IsStandart.Value)
                {
                    foreach (var item in productItems)
                    {
                        var product = entities.Products.SingleOrDefault(c => c.ProductId == item.ProductId);
                        if (product.ProductActive == true)
                        {
                            ProductCountCalc(product, false);
                        }
                        product.ProductActive = false;
                        product.ProductActiveType = (byte)ProductActiveType.Onaylanmadi;
                        productCountCalculated = true;
                        entities.SaveChanges();
                    }

                }
                #region mail onay

                //burada ilan pasif olduğunda veya siliniyor olduğunda categori içerisinde bulunan ilan sayılarını sıfır yap yda azaltıp arttır.
                var StoreProductStatu = (PacketStatu)model.StoreActiveType;
                string ps = TempData["type"].ToString();
                switch (StoreProductStatu)
                {
                    case PacketStatu.Inceleniyor:
                        foreach (var item in productItems)
                        {
                            var product = entities.Products.SingleOrDefault(c => c.ProductId == item.ProductId);
                            if (productCountCalculated == false)
                            {
                                if (product.ProductActive == true && product.ProductActiveType == 1)
                                {
                                    ProductCountCalc(product, false);
                                }

                            }
                            product.ProductActive = false;
                            product.ProductActiveType = (byte)ProductActiveType.Onaylanmadi;
                        }
                        entities.SaveChanges();
                        break;
                    case PacketStatu.Onaylandi:
                        if (packet.PacketPrice > 0)
                        {

                            foreach (var item in productItems)
                            {
                                var product = entities.Products.SingleOrDefault(c => c.ProductId == item.ProductId);
                                if (productCountCalculated == false)
                                {
                                    if (product.ProductActive == false && (product.ProductActiveType == (byte)ProductActiveType.CopKutusuYeni || product.ProductActiveType == (byte)ProductActiveType.Inceleniyor || product.ProductActiveType == (byte)ProductActiveType.Onaylanmadi || product.ProductActiveType == (byte)ProductActiveType.Silindi))
                                    {
                                        ProductCountCalc(product, false);
                                    }

                                }
                                product.ProductActive = true;
                                product.ProductActiveType = (byte)ProductActiveType.Onaylandi;
                            }

                            entities.SaveChanges();
                            foreach (var item in productItems)
                            {
                                entities.CheckProductSearch(item.ProductId);
                            }

                        }
                        if (ps != "2")
                        {
                            #region
                            try
                            {

                                //onaylandı bilgisi kullanıcıya gidiyor.***************************************
                                var messageMt = entities.MessagesMTs.FirstOrDefault(x => x.MessagesMTName == "firmauyeligionay");
                                var settings = ConfigurationManager.AppSettings;
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress(messageMt.Mail, messageMt.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                                mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                                mail.Subject = messageMt.MessagesMTTitle;                                              //Mail konusu
                                string template = messageMt.MessagesMTPropertie;
                                template = template.Replace("#firmaadi#", model.StoreName).Replace("#firmaadres#", model.Avenue).Replace("#firmatelefon#", model.InstitutionalPhoneNumber).Replace("#firmawebsite#", model.StoreWeb).Replace("#kullaniciadi#", model.StoreName);
                                mail.Body = template;                                                            //Mailin içeriği
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.Normal;
                                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                                sc.Credentials = new NetworkCredential(messageMt.Mail, messageMt.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                                sc.Send(mail);
                            }
                            catch (Exception ex)
                            {
                                TempData["Mail"] = ex.InnerException;
                            }
                            #endregion
                        }
                        break;
                    case PacketStatu.Onaylanmadi:
                        foreach (var item in productItems)
                        {
                            var product = entities.Products.SingleOrDefault(c => c.ProductId == item.ProductId);
                            if (productCountCalculated == false)
                            {
                                if (product.ProductActive == false && product.ProductActiveType == 1)
                                {
                                    ProductCountCalc(product, false);
                                }
                            }
                            product.ProductActive = false;
                            product.ProductActiveType = (byte)ProductActiveType.Onaylanmadi;
                        }


                        if (ps != "3")
                        {
                            #region
                            try
                            {
                                var settings = ConfigurationManager.AppSettings;
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye"); //Mailin kimden gittiğini belirtiyoruz
                                mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                                mail.Subject = "FİRMA BİLGİLERİ ONAYLANMADI";                                              //Mail konusu
                                string template = Resources.Email.uyelikonaylanmadi;
                                mail.Body = template;                                                            //Mailin içeriği
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.Normal;
                                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                                sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
                                sc.Send(mail);
                            }
                            catch (Exception ex)
                            {
                                TempData["Mail"] = ex.InnerException;
                            }
                            #endregion
                        }
                        entities.SaveChanges();
                        break;
                    case PacketStatu.Silindi:
                        foreach (var item in productItems)
                        {
                            var product = entities.Products.SingleOrDefault(c => c.ProductId == item.ProductId);
                            if (productCountCalculated == false)
                            {
                                if (product.ProductActive == false && product.ProductActiveType == 1)
                                {
                                    ProductCountCalc(product, false);
                                }
                            }
                            product.ProductActive = false;
                            product.ProductActiveType = (byte)ProductActiveType.Silindi;
                        }
                        if (ps != "4")
                        {
                            #region
                            try
                            {
                                //firma bilgileri silindiği zaman kullanıcıya bu bilgiler gidecek.
                                var settings = ConfigurationManager.AppSettings;
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye"); //Mailin kimden gittiğini belirtiyoruz
                                mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                                mail.Subject = "Üyelik Aktivasyon";                                              //Mail konusu
                                string template = "<html><body>" +
                           "<span style='color:black'>   <p>Firma Üyeliğiniz onayınız doğrultusunda silinmiştir. .</p>\r\n" +
                        "<p>" + store.StoreName + "</p>\r\n\r\n" +
                        "<p>Telefon:0212-255-71-50</p><br/> <img src=\"http://makinaturkiye.com/Content/Images/logo.png \"/ alt=\"Makina Türkiye\"/><br/><a href=\"http://www.makinaturkiye.com\" >www.makinaturkiye.com</a><span></body></html>";
                                mail.Body = template;                                                            //Mailin içeriği
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.Normal;
                                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                                sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
                                sc.Send(mail);
                                //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
                            }
                            catch (Exception ex)
                            {
                                TempData["Mail"] = ex.InnerException;
                            }
                            #endregion
                        }
                        entities.SaveChanges();
                        break;
                    default:
                        break;
                }

                #endregion
            }
            if (tempStoreActiveType == 4 && store.StoreActiveType != 4)
            {
                store.SingularViewCount = 0;
                store.ViewCount = 0;
                store.storerate = 0;
            }
            entities.SaveChanges();
            return RedirectToAction("EditStore", "Store", new { id = store.MainPartyId, page = "basarili" });
        }

        public ActionResult EditMember(int id)
        {

            var member = entities.MemberStores.FirstOrDefault(c => c.StoreMainPartyId == id);

            var model = new StoreModel();

            var storeeposta = entities.MainPartyIdEpostas.Where(c => c.MainPartyId == id).SingleOrDefault();
            if (storeeposta != null)
            {
                model.Eposta1 = storeeposta.Eposta1;
                model.Eposta2 = storeeposta.EPosta2;
                model.Email1Check = storeeposta.Eposta1check.ToBoolean();
                model.Email2Check = storeeposta.Eposta2check.ToBoolean();
                model.Ad1 = storeeposta.Ad1;
                model.Ad2 = storeeposta.Ad2;
                model.SoyAd1 = storeeposta.SoyAd1;
                model.SoyAd2 = storeeposta.SoyAd2;
            }
            else
            {
                model.Email1Check = false;
                model.Email2Check = false;
            }

            model.MemberEmail = member.Member.MemberEmail;
            model.MemberName = member.Member.MemberName;
            model.MemberNo = member.Member.MemberNo;
            model.MemberPassword = member.Member.MemberPassword;
            model.MemberSurname = member.Member.MemberSurname;
            model.MemberTitleType = member.Member.MemberTitleType.HasValue ? member.Member.MemberTitleType.Value : Convert.ToByte(0);
            model.MemberType = member.Member.MemberType.Value;
            model.ReceiveEmail = member.Member.ReceiveEmail.HasValue ? member.Member.ReceiveEmail.Value : false;
            model.BirthDate = member.Member.BirthDate;
            model.Gender = member.Member.Gender.HasValue ? member.Member.Gender.Value : false;
            model.Active = member.Member.Active.Value;
            model.MemberTitleType = member.Member.MemberTitleType.HasValue ? member.Member.MemberTitleType.Value : Convert.ToByte(0);

            ViewData["Member"] = true;

            return View(model);
        }


        [HttpPost]
        public ActionResult EditMember(int id, StoreModel model)
        {

            if (entities.MainPartyIdEpostas.Where(c => c.MainPartyId == id).SingleOrDefault() != null)
            {
                var maineposta = entities.MainPartyIdEpostas.Where(c => c.MainPartyId == id).SingleOrDefault();
                maineposta.Ad1 = model.Ad1;
                maineposta.Ad2 = model.Ad2;
                maineposta.SoyAd1 = model.SoyAd1;
                maineposta.SoyAd2 = model.SoyAd2;
                maineposta.Eposta1 = model.Eposta1;
                maineposta.EPosta2 = model.Eposta2;
                maineposta.Eposta1check = model.Email1Check;
                maineposta.Eposta2check = model.Email2Check;
            }
            else
            {
                var mainepost = new MainPartyIdEposta
                {
                    MainPartyId = id,
                    Ad1 = model.Ad1,
                    Ad2 = model.Ad2,
                    SoyAd1 = model.SoyAd1,
                    SoyAd2 = model.SoyAd2,
                    Eposta1 = model.Eposta1,
                    EPosta2 = model.Eposta2,
                    Eposta1check = model.Email1Check,
                    Eposta2check = model.Email2Check
                };
                entities.MainPartyIdEpostas.AddObject(mainepost);
            }
            var memberId = entities.MemberStores.FirstOrDefault(c => c.StoreMainPartyId == id).MemberMainPartyId;
            var member = entities.Members.SingleOrDefault(c => c.MainPartyId == memberId);

            member.MemberEmail = model.MemberEmail;
            member.MemberName = model.MemberName;
            member.MemberSurname = model.MemberSurname;
            member.Active = model.Active;
            member.MemberPassword = model.MemberPassword;
            member.ReceiveEmail = model.ReceiveEmail;
            member.Gender = model.Gender;

            if (model.BirthDate != null)
            {
                member.BirthDate = model.BirthDate;
            }

            entities.SaveChanges();

            var curMainParty = new Classes.MainParty();
            curMainParty.LoadEntity(memberId);
            curMainParty.MainPartyFullName = model.MemberName + " " + model.MemberSurname;
            curMainParty.Save();

            return RedirectToAction("Index", "Store");
        }

        public ActionResult Users(int id)
        {
            var store = _storeService.GetStoreByMainPartyId(id);
            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(id).Where(x => x.MemberStoreType == (byte)MemberStoreType.Helper).Select(x => x.MemberMainPartyId).ToList();
            var members = _memberService.GetMembersByMainPartyIds(mainPartyIds);
            FilterModel<StoreUserItem> model = new FilterModel<StoreUserItem>();
            model.PageDimension = 40;
            model.TotalRecord = members.Count;
            model.CurrentPage = 1;
            List<StoreUserItem> users = new List<StoreUserItem>();
            foreach (var item in members)
            {
                users.Add(new StoreUserItem
                {
                    MemberEmail = item.MemberEmail,
                    MemberMainPartyId = item.MainPartyId,
                    MemberName = item.MemberName,
                    MemberNo = item.MemberNo,
                    MemberSurname = item.MemberSurname,
                    Active = item.Active.Value
                });
            }
            model.Source = users;
            ViewData["Users"] = true;
            return View(model);
        }
        public ActionResult EditActivityType(int id)
        {

            var model = new StoreModel();

            var dataStoreActivityType = new Data.StoreActivityType();
            model.StoreActivityTypeItems = dataStoreActivityType.GetStoreActivityTypeItemsByMainPartyId(id).AsCollection<StoreActivityTypesModel>();

            var curActivityType = new Classes.ActivityType();
            model.ActivityTypeItems = curActivityType.GetDataTable().AsCollection<ActivityTypeModel>();

            ViewData["ActivityType"] = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditActivityType(int id, StoreModel model, FormCollection coll)
        {

            string[] acType = coll["ActivityTypeIdItems"].Split(',');
            var datastoreactivity = new Data.StoreActivityType();
            datastoreactivity.ActivityTypeDeleteByMainPartyId(id);

            if (acType != null)
            {
                for (int i = 0; i < acType.Length; i++)
                {
                    if (acType.GetValue(i).ToString() != "false")
                    {
                        var curStoreActivityType = new Classes.StoreActivityType
                        {
                            StoreId = id,
                            ActivityTypeId = acType.GetValue(i).ToByte()
                        };
                        curStoreActivityType.Save();
                    }
                }
            }

            return RedirectToAction("Index", "Store");
        }

        public ActionResult EditActivityCategory(int id)
        {

            var model = new StoreModel();

            var dataCategory = new Data.Category();
            model.StoreActivityCategory = entities.StoreActivityCategories.Where(c => c.MainPartyId == id);
            model.SectorItems = dataCategory.CategoryGetSectorItemsByCategoryParent(0).AsCollection<CategoryModel>();

            ViewData["RelActivityCategory"] = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditActivityCategory(int id, StoreModel model, FormCollection coll)
        {

            string[] stActivityCategory = coll["StoreActivityCategory"].Split(',');
            int ID = id;
            if (stActivityCategory != null)
            {
                var storeAcCategory = entities.StoreActivityCategories.Where(c => c.MainPartyId == id);
                if (storeAcCategory != null && storeAcCategory.Count() > 0)
                {
                    foreach (var item in storeAcCategory)
                    {
                        entities.StoreActivityCategories.DeleteObject(item);
                    }
                    entities.SaveChanges();
                }

                for (int i = 0; i < stActivityCategory.Length; i++)
                {
                    if (stActivityCategory.GetValue(i).ToString() != "false")
                    {
                        var storeActivityType = new StoreActivityCategory
                        {
                            MainPartyId = id,
                            CategoryId = stActivityCategory.GetValue(i).ToInt32(),
                        };
                        entities.StoreActivityCategories.AddObject(storeActivityType);
                        entities.SaveChanges();
                    }
                }
            }

            return RedirectToAction("EditActivityCategory", "Store", new { id = ID });
        }

        public ActionResult EditCommunication(int id)
        {

            var model = new StoreModel();

            var address = entities.Addresses.FirstOrDefault(c => c.MainPartyId == id);
            var phone = entities.Phones.Where(c => c.MainPartyId == id).ToList();
            var store = entities.Stores.FirstOrDefault(x => x.MainPartyId == id);

            model.PhoneItems = phone;
            model.Address = address;
            model.IsWhatsappNotUsing = false;
            if (store.IsWhatsappNotUsing != null)
                model.IsWhatsappNotUsing = Convert.ToBoolean(store.IsWhatsappNotUsing);


            List<Locality> localityItems = new List<Locality>() { new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
            List<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };

            if (address != null)
            {
                model.CountryId = address.CountryId.HasValue ? address.CountryId.Value : 0;

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
            }
            else
                model.CountryId = 0;


            var cityItems = entities.Cities.Where(c => c.CountryId == model.CountryId).ToList();
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            model.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
            model.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
            model.TownItems = new SelectList(townItems, "TownId", "TownName");

            ViewData["Communication"] = true;

            return View(model);
        }
        public ActionResult StoreContactInfo(int id)
        {

            var model = new StoreContactOrderModel();


            var phones = entities.Phones.Where(c => c.MainPartyId == id).ToList();
            var store = entities.Stores.FirstOrDefault(x => x.MainPartyId == id);
            var memberStore = entities.MemberStores.FirstOrDefault(x => x.StoreMainPartyId == id);

            var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);

            model.Email = member.MemberEmail;
            model.Phones = phones;
            model.IsProductAdded = store.IsProductAdded;
            model.StoreMainPartyId = store.MainPartyId;
            model.IsWhatsappUsing = false;
            model.StoreName = store.StoreName;
            model.MemberNameSurname = member.MemberName + " " + member.MemberSurname;
            model.Address = entities.Addresses.FirstOrDefault(x => x.MainPartyId == id);
            model.MemberMainPartyId = memberStore.MemberMainPartyId.Value;
            var memberDescriptions = entities.MemberDescriptions.Where(x => x.MainPartyId == memberStore.MemberMainPartyId && x.ConstantId != null).OrderByDescending(x => x.descId);
            var memberPayment = memberDescriptions.FirstOrDefault(x => x.Title == "Ödeme");
            var modelMemberdescPayment = new StoreMemberDescriptionItem { DescId = memberPayment.descId, Description = memberPayment.Description, Title = memberPayment.Title };
            var userFromPayment = entities.Users.FirstOrDefault(x => x.UserId == memberPayment.FromUserId);
            if (userFromPayment != null)
                modelMemberdescPayment.UserName = userFromPayment.UserName;
            modelMemberdescPayment.UserName = userFromPayment.UserName;
            model.StoreMemberDescriptions.Add(modelMemberdescPayment);

            foreach (var item in memberDescriptions.Skip(0).Take(2))
            {
                var modelMemberdesc = new StoreMemberDescriptionItem { DescId = item.descId, Description = item.Description, Title = item.Title };
                if (item.Date.HasValue)
                    modelMemberdesc.RecordDate = item.Date.Value;
                var userFrom = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                if (userFrom != null)
                    modelMemberdesc.UserName = userFrom.UserName;
                model.StoreMemberDescriptions.Add(modelMemberdesc);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCommunication(int id, StoreModel model, bool WhatsappConfirm)
        {

            bool hasAddress = false;

            var address = entities.Addresses.FirstOrDefault(c => c.MainPartyId == id);
            var store = entities.Stores.FirstOrDefault(x => x.MainPartyId == id);
            store.IsWhatsappNotUsing = model.IsWhatsappNotUsing;
            entities.SaveChanges();
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

            address.MainPartyId = id;

            if (model.AddressTypeId > 0)
                address.AddressTypeId = model.AddressTypeId;
            else
                address.AddressTypeId = null;

            address.Avenue = model.Avenue;
            if (model.CountryId > 0 && model.CountryId != 246)
            {
                address.Avenue = model.AvenueOtherCountries;
            }

            address.Street = model.Street;
            address.DoorNo = model.DoorNo;
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
            bool whatsapActive = true;
            var phoneItems = entities.Phones.Where(c => c.MainPartyId == id);
            if (phoneItems != null)
            {
                using (var trans = new TransactionScope())
                {
                    foreach (var item in phoneItems)
                    {
                        var phone = entities.Phones.SingleOrDefault(c => c.PhoneId == item.PhoneId);
                        if (phone.PhoneType == (byte)PhoneType.Whatsapp && phone.WhatsappActive == false)
                        {
                            whatsapActive = false;
                        }
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
                    MainPartyId = id,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode != "" ? model.InstitutionalPhoneAreaCode : "",
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
                    MainPartyId = id,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode2 != "0" ? model.InstitutionalPhoneAreaCode2 : "",
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
                    MainPartyId = id,
                    PhoneAreaCode = model.InstitutionalGSMAreaCode != "0" ? model.InstitutionalGSMAreaCode : "",
                    PhoneCulture = model.InstitutionalGSMCulture,
                    PhoneNumber = model.InstitutionalGSMNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                    GsmType = model.GsmType
                };
                entities.Phones.AddObject(phoneGsm);
                entities.SaveChanges();
                //if (GsmWhatsapp)
                //{
                //    var phoneGsm1 = new Phone
                //    {
                //        MainPartyId = id,
                //        PhoneAreaCode = model.InstitutionalGSMAreaCode,
                //        PhoneCulture = model.InstitutionalGSMCulture,
                //        PhoneNumber = model.InstitutionalGSMNumber,
                //        PhoneType = (byte)PhoneType.Whatsapp,
                //        GsmType = model.GsmType
                //    };
                //    entities.Phones.AddObject(phoneGsm1);
                //    entities.SaveChanges();
                //}   
            }
            if (store.IsWhatsappNotUsing != true)
            {
                if (model.InstitutionalWGSMNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalWGSMNumber))
                {
                    var phoneGsm = new Phone
                    {
                        MainPartyId = id,
                        PhoneAreaCode = model.InstitutionalWGSMAreaCode != "0" ? model.InstitutionalWGSMAreaCode : "",
                        PhoneCulture = model.InstitutionalWGSMCulture,
                        PhoneNumber = model.InstitutionalWGSMNumber,
                        PhoneType = (byte)PhoneType.Whatsapp
                    };
                    if (whatsapActive == false)
                    {
                        phoneGsm.WhatsappActive = WhatsappConfirm;
                    }

                    entities.Phones.AddObject(phoneGsm);
                    entities.SaveChanges();
                }

            }

            if (model.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalFaxNumber))
            {
                var phoneFax = new Phone
                {
                    MainPartyId = id,
                    PhoneAreaCode = model.InstitutionalFaxAreaCode != "0" ? model.InstitutionalFaxAreaCode : "",
                    PhoneCulture = model.InstitutionalFaxCulture,
                    PhoneNumber = model.InstitutionalFaxNumber,
                    PhoneType = (byte)PhoneType.Fax,
                    GsmType = null
                };
                entities.Phones.AddObject(phoneFax);
                entities.SaveChanges();
            }
            int ID = id;
            return RedirectToAction("EditCommunication", "Store", new { id = ID });
        }

        public ActionResult EditAbout(int id)
        {

            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == id);

            var storeModel = new StoreModel();

            storeModel.GeneralText = store.GeneralText;
            storeModel.FounderText = store.FounderText;
            storeModel.PhilosophyText = store.PhilosophyText;
            storeModel.HistoryText = store.HistoryText;

            ViewData["AboutUs"] = true;

            return View(storeModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditAbout(int storeId, string GeneralText, string HistoryText, string FounderText, string PhilosophyText)
        {

            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == storeId);

            store.GeneralText = GeneralText;
            store.FounderText = FounderText;
            store.HistoryText = HistoryText;
            store.PhilosophyText = PhilosophyText;
            entities.SaveChanges();

            return Json(true);
        }


        public ActionResult StoreProfileHomeDescription(int id)
        {
            StoreModel model = new StoreModel();
            ViewData["StoreProfileHomeDescription"] = true;
            var store = _storeService.GetStoreByMainPartyId(id);
            model.MainPartyId = id;
            model.StoreProfileHomeDescription = store.StoreProfileHomeDescription;

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult StoreProfileHomeDescription(StoreModel storeModel)
        {

            ViewData["StoreProfileHomeDescription"] = true;
            var store = _storeService.GetStoreByMainPartyId(storeModel.MainPartyId);
            store.StoreProfileHomeDescription = storeModel.StoreProfileHomeDescription;
            _storeService.UpdateStore(store);
            return View(storeModel);
        }
        [HttpPost]
        public ActionResult DealerAddForBranch(int storeId, string DealerNameForBranch)
        {
            var curStoreDealer = new StoreDealer
            {
                DealerName = DealerNameForBranch,
                DealerType = (byte)DealerType.Sube,
                MainPartyId = storeId,
            };
            entities.StoreDealers.AddObject(curStoreDealer);
            entities.SaveChanges();

            var dealerItems = from c in entities.StoreDealers.AsEnumerable()
                              where c.MainPartyId == storeId && c.DealerType == (byte)DealerType.Sube
                              select new
                              {
                                  StoreDealerId = c.StoreDealerId,
                                  DealerName = c.DealerName
                              };
            return Json(dealerItems);
        }

        [HttpPost]
        public ActionResult DealerAddForDealer(int storeId, string DealerNameForDealer)
        {
            var curStoreDealer = new StoreDealer
            {
                DealerName = DealerNameForDealer,
                DealerType = (byte)DealerType.Bayii,
                MainPartyId = storeId,
            };
            entities.StoreDealers.AddObject(curStoreDealer);
            entities.SaveChanges();

            var dealerItems = from c in entities.StoreDealers.AsEnumerable()
                              where c.MainPartyId == storeId && c.DealerType == (byte)DealerType.Bayii
                              select new
                              {
                                  StoreDealerId = c.StoreDealerId,
                                  DealerName = c.DealerName
                              };
            return Json(dealerItems);
        }

        [HttpPost]
        public ActionResult DealerAddForService(int storeId, string DealerNameForService)
        {
            var curStoreDealer = new StoreDealer
            {
                DealerName = DealerNameForService,
                DealerType = (byte)DealerType.YetkiliServis,
                MainPartyId = storeId,
            };
            entities.StoreDealers.AddObject(curStoreDealer);
            entities.SaveChanges();

            var dealerItems = from c in entities.StoreDealers.AsEnumerable()
                              where c.MainPartyId == storeId && c.DealerType == (byte)DealerType.YetkiliServis
                              select new
                              {
                                  StoreDealerId = c.StoreDealerId,
                                  DealerName = c.DealerName
                              };
            return Json(dealerItems);
        }

        public ActionResult EditDealers(int id)
        {
            var storeModel = new StoreModel();
            int storeMainPartyId = id;

            DealerType pageType = (DealerType)Request.QueryString["DealerType"].ToByte();
            switch (pageType)
            {
                case DealerType.Bayii:
                    ViewData["Dealer"] = true;

                    var dealerItems = entities.StoreDealers.Where(c => c.MainPartyId.Value == storeMainPartyId && c.DealerType == (byte)DealerType.Bayii).ToList();
                    dealerItems.Insert(0, new StoreDealer { StoreDealerId = 0, DealerName = "< Lütfen Seçiniz >" });
                    storeModel.DealerItemsForDealer = new SelectList(dealerItems, "StoreDealerId", "DealerName", 0);

                    var DealerIds = (from c in entities.StoreDealers where c.MainPartyId == storeMainPartyId && c.DealerType == (byte)DealerType.Bayii select c.StoreDealerId);
                    storeModel.DealerAddressItems = (from c in entities.Addresses where DealerIds.Contains(c.StoreDealerId.Value) select c).ToList();
                    break;

                case DealerType.YetkiliServis:
                    ViewData["Service"] = true;

                    var serviceItems = entities.StoreDealers.Where(c => c.MainPartyId.Value == storeMainPartyId && c.DealerType == (byte)DealerType.YetkiliServis).ToList();
                    serviceItems.Insert(0, new StoreDealer { StoreDealerId = 0, DealerName = "< Lütfen Seçiniz >" });
                    storeModel.DealerItemsForService = new SelectList(serviceItems, "StoreDealerId", "DealerName", 0);

                    var ServisIds = (from c in entities.StoreDealers where c.MainPartyId == storeMainPartyId && c.DealerType == (byte)DealerType.YetkiliServis select c.StoreDealerId);
                    storeModel.DealerAddressItems = (from c in entities.Addresses where ServisIds.Contains(c.StoreDealerId.Value) select c).ToList();
                    break;
                case DealerType.Sube:
                    ViewData["Branch"] = true;

                    var branchItems = entities.StoreDealers.Where(c => c.MainPartyId.Value == storeMainPartyId && c.DealerType == (byte)DealerType.Sube).ToList();
                    branchItems.Insert(0, new StoreDealer { StoreDealerId = 0, DealerName = "< Lütfen Seçiniz >" });
                    storeModel.DealerItemsForBranch = new SelectList(branchItems, "StoreDealerId", "DealerName", 0);

                    var SubeIds = (from c in entities.StoreDealers where c.MainPartyId == storeMainPartyId && c.DealerType == (byte)DealerType.Sube select c.StoreDealerId);
                    storeModel.DealerAddressItems = (from c in entities.Addresses where SubeIds.Contains(c.StoreDealerId.Value) select c).ToList();
                    break;
                default:
                    break;
            }

            var countryItems = entities.Countries.OrderBy(c => c.CountryOrder).ThenBy(n => n.CountryName).ToList();
            countryItems.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });
            storeModel.CountryItems = new SelectList(countryItems, "CountryId", "CountryName");

            return View(storeModel);
        }

        [HttpPost]
        public ActionResult EditDealers(AddressModel model, byte DealerTypeId, int storeId)
        {
            using (var trans = new TransactionScope())
            {
                var address = new Address
                {
                    MainPartyId = null,
                    Avenue = model.Avenue,
                    Street = model.Street,
                    DoorNo = model.DoorNo,
                    ApartmentNo = model.ApartmentNo,
                    AddressDefault = true,
                    StoreDealerId = model.StoreDealerId,
                    AddressTypeId = null,
                    CountryId = model.CountryId
                };

                if (model.CountryId > 0)
                    address.CountryId = model.CountryId;
                else
                    address.CountryId = null;

                if (model.CityId > 0)
                    address.CityId = model.CityId;
                else
                    address.CityId = null;

                if (model.LocalityId > 0)
                    address.LocalityId = model.LocalityId;
                else
                    address.LocalityId = null;

                if (model.TownId > 0)
                    address.TownId = model.TownId;
                else
                    address.TownId = null;

                entities.Addresses.AddObject(address);
                entities.SaveChanges();

                if (!string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber))
                {
                    var phone = new Phone
                    {
                        AddressId = address.AddressId,
                        MainPartyId = null,
                        PhoneAreaCode = model.InstitutionalPhoneAreaCode,
                        PhoneCulture = model.InstitutionalPhoneCulture,
                        PhoneNumber = model.InstitutionalPhoneNumber,
                        PhoneType = (byte)PhoneType.Phone,
                    };
                    entities.Phones.AddObject(phone);
                }

                if (!string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber2))
                {
                    var phone = new Phone
                    {
                        AddressId = address.AddressId,
                        MainPartyId = null,
                        PhoneAreaCode = model.InstitutionalPhoneAreaCode2,
                        PhoneCulture = model.InstitutionalPhoneCulture2,
                        PhoneNumber = model.InstitutionalPhoneNumber2,
                        PhoneType = (byte)PhoneType.Phone,
                    };
                    entities.Phones.AddObject(phone);
                }

                if (!string.IsNullOrWhiteSpace(model.InstitutionalFaxNumber))
                {
                    var phone = new Phone
                    {
                        AddressId = address.AddressId,
                        MainPartyId = null,
                        PhoneAreaCode = model.InstitutionalFaxAreaCode,
                        PhoneCulture = model.InstitutionalFaxCulture,
                        PhoneNumber = model.InstitutionalFaxNumber,
                        PhoneType = (byte)PhoneType.Fax,
                    };
                    entities.Phones.AddObject(phone);
                }

                if (!string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber))
                {
                    var phone = new Phone
                    {
                        AddressId = address.AddressId,
                        MainPartyId = null,
                        PhoneAreaCode = model.InstitutionalGSMAreaCode,
                        PhoneCulture = model.InstitutionalGSMCulture,
                        PhoneNumber = model.InstitutionalGSMNumber,
                        PhoneType = (byte)PhoneType.Gsm,
                    };
                    entities.Phones.AddObject(phone);
                }

                if (!string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber2))
                {
                    var phone = new Phone
                    {
                        AddressId = address.AddressId,
                        MainPartyId = null,
                        PhoneAreaCode = model.InstitutionalGSMAreaCode2,
                        PhoneCulture = model.InstitutionalGSMCulture2,
                        PhoneNumber = model.InstitutionalGSMNumber2,
                        PhoneType = (byte)PhoneType.Gsm,
                    };
                    entities.Phones.AddObject(phone);
                }

                entities.SaveChanges();
                trans.Complete();
            }

            DealerType pageType = (DealerType)DealerTypeId;
            switch (pageType)
            {
                case DealerType.Bayii:
                    return RedirectToAction("EditDealers", "Store", new { DealerType = (byte)DealerType.Bayii, id = storeId });
                case DealerType.YetkiliServis:
                    return RedirectToAction("EditDealers", "Store", new { DealerType = (byte)DealerType.YetkiliServis, id = storeId });
                case DealerType.Sube:
                    return RedirectToAction("EditDealers", "Store", new { DealerType = (byte)DealerType.Sube, id = storeId });
                default:
                    return RedirectToAction("EditDealers", "Store", new { DealerType = (byte)DealerType.Bayii, id = storeId });
            }
        }

        [HttpPost]
        public ActionResult DeleteAddress(int AddressId, DealerType type, int storeId)
        {
            var address = entities.Addresses.SingleOrDefault(c => c.AddressId == AddressId);
            var phoneItems = address.Phones.ToList();
            foreach (var phoneItem in phoneItems)
            {
                entities.Phones.DeleteObject(phoneItem);
            }
            entities.Addresses.DeleteObject(address);
            entities.SaveChanges();

            var storeModel = new StoreModel();
            IList<Address> addressItems = null;

            switch (type)
            {
                case DealerType.Bayii:
                    var DealerIds = (from c in entities.StoreDealers where c.MainPartyId == storeId && c.DealerType == (byte)DealerType.Bayii select c.StoreDealerId);
                    addressItems = (from c in entities.Addresses where DealerIds.Contains(c.StoreDealerId.Value) select c).ToList();
                    break;
                case DealerType.YetkiliServis:
                    var ServicesIds = (from c in entities.StoreDealers where c.MainPartyId == storeId && c.DealerType == (byte)DealerType.YetkiliServis select c.StoreDealerId);
                    addressItems = (from c in entities.Addresses where ServicesIds.Contains(c.StoreDealerId.Value) select c).ToList();
                    break;
                case DealerType.Sube:
                    var BranchIds = (from c in entities.StoreDealers where c.MainPartyId == storeId && c.DealerType == (byte)DealerType.Sube select c.StoreDealerId);
                    addressItems = (from c in entities.Addresses where BranchIds.Contains(c.StoreDealerId.Value) select c).ToList();
                    break;
                default:
                    break;
            }

            return View("DealerAddressItems", addressItems);
        }

        public ActionResult EditBrand(int id)
        {
            ViewData["Brand"] = true;
            var model = new StoreModel();

            model.StoreBrandItems = entities.StoreBrands.Where(c => c.MainPartyId == id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBrand(FormCollection coll, string StoreBrandName, string BrandDescription, int storeId)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreBrandImageFolder, file, 106, FileHelpers.ThumbnailType.Width);

                    var curStoreBrand = new StoreBrand()
                    {
                        BrandPicture = fileName,
                        MainPartyId = storeId,
                        BrandName = StoreBrandName,
                        BrandDescription = BrandDescription,
                    };
                    entities.StoreBrands.AddObject(curStoreBrand);
                    entities.SaveChanges();
                }
            }
            return RedirectToAction("EditBrand");
        }

        [HttpPost]
        public ActionResult DeleteStoreBrand(int StoreBrandId, int storeId)
        {
            var storeBrand = entities.StoreBrands.SingleOrDefault(c => c.StoreBrandId == StoreBrandId);

            FileHelpers.Delete(AppSettings.DealerBrandImageFolder + storeBrand.BrandPicture);
            FileHelpers.Delete(AppSettings.DealerBrandImageFolder + FileHelper.ImageThumbnailName(storeBrand.BrandPicture));

            entities.StoreBrands.DeleteObject(storeBrand);
            entities.SaveChanges();

            var items = entities.StoreBrands.Where(c => c.MainPartyId == storeId);

            return View("StoreBrand", items);
        }

        [HttpPost]
        public ActionResult DeleteDealerBrand(int DealerBrandId, int storeId)
        {
            var dealerBrand = entities.DealerBrands.SingleOrDefault(c => c.DealerBrandId == DealerBrandId);

            FileHelpers.Delete(AppSettings.DealerBrandImageFolder + dealerBrand.DealerBrandPicture);
            FileHelpers.Delete(AppSettings.DealerBrandImageFolder + FileHelper.ImageThumbnailName(dealerBrand.DealerBrandPicture));

            entities.DealerBrands.DeleteObject(dealerBrand);
            entities.SaveChanges();

            var items = entities.DealerBrands.Where(c => c.MainPartyId == storeId);

            return View("DealerBrand", items);
        }

        public ActionResult EditDealership(int id)
        {
            ViewData["Dealership"] = true;
            var model = new StoreModel();

            model.DealerBrandItems = entities.DealerBrands.Where(c => c.MainPartyId == id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDealership(FormCollection coll, string BrandName, int storeId)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.DealerBrandImageFolder, file, 50, FileHelpers.ThumbnailType.Width);

                    var curDealerBrand = new DealerBrand()
                    {
                        DealerBrandPicture = fileName,
                        MainPartyId = storeId,
                        DealerBrandName = BrandName,
                    };

                    entities.DealerBrands.AddObject(curDealerBrand);
                    entities.SaveChanges();
                }
            }
            return RedirectToAction("EditDealership");
        }

        [HttpPost]
        public ActionResult DeleteStoreImage(int id, int storeId)
        {
            var picture = entities.Pictures.SingleOrDefault(c => c.PictureId == id);
            if (picture != null)
            {
                FileHelpers.Delete(AppSettings.StoreDealerImageFolder + picture.PicturePath);
                FileHelpers.Delete(AppSettings.StoreDealerImageFolder + FileHelper.ImageThumbnailName(picture.PicturePath));

                entities.DeleteObject(picture);
                entities.SaveChanges();
            }

            var model = new StoreModel();

            model.StoreDealerItems = entities.StoreDealers.Where(c => c.MainPartyId == storeId).ToList();

            var DealerIds = (from c in entities.StoreDealers where c.MainPartyId == storeId select c.StoreDealerId);
            model.PictureItems = (from c in entities.Pictures where DealerIds.Contains(c.StoreDealerId.Value) select c).ToList();

            return View("StoreImages", model);
        }

        public ActionResult EditStoreImages(int id)
        {
            ViewData["StoreImages"] = true;

            var model = new StoreModel();

            model.StoreDealerItems = entities.StoreDealers.Where(c => c.MainPartyId.Value == id).ToList();

            var DealerIds = (from c in entities.StoreDealers where c.MainPartyId == id select c.StoreDealerId);
            model.PictureItems = (from c in entities.Pictures where DealerIds.Contains(c.StoreDealerId.Value) select c).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditStoreImages(FormCollection coll)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreDealerImageFolder, file, 170, FileHelpers.ThumbnailType.Width);

                    var curPicture = new Picture()
                    {
                        PicturePath = fileName,
                        StoreDealerId = Request.Files.AllKeys.GetValue(i).ToString().Replace("#", "").ToInt32(),
                        ProductId = null,
                        MainPartyId = null,
                        PictureName = String.Empty
                    };
                    entities.Pictures.AddObject(curPicture);
                    entities.SaveChanges();
                }
            }
            return RedirectToAction("EditStoreImages");
        }

        public ActionResult EditProfilePicture(int id)
        {
            var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == id);

            ViewData["ProfilePicture"] = true;
            return View(store);
        }

        public void ConvertExcel()
        {
            string[] parcalidegerler;
            parcalidegerler = Session["StoreExel"].ToString().Split(':');
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
            System.Web.HttpContext.Current.Response.Charset = "windows-1254"; //ISO-8859-13 ISO-8859-9  windows-1254

            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string excelname = DateTime.Now.ToShortDateString().ToString();
            System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", excelname + ".xls"));

            string sep = "";
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Firma İsmi", sep));
            sep = "\t";
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Yetkili Adı", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Cep Telefonu1", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Cep Telefonu2", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Telefon1", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Telefon2", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Email", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Adres", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Web Adresi", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Üyelik Tipi", sep));
            System.Web.HttpContext.Current.Response.Write(string.Format("{0}Açıklama", sep));
            System.Web.HttpContext.Current.Response.Write("\n");
            for (int i = 0; i < parcalidegerler.Length; i++)
            {
                int storeid = parcalidegerler[i].ToInt32();

                try
                {
                    var store = entities.Stores.Where(c => c.MainPartyId == storeid).SingleOrDefault();

                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", store.StoreName, sep));

                    var memberid = entities.MemberStores.Where(c => c.StoreMainPartyId == store.MainPartyId).SingleOrDefault().MemberMainPartyId.Value;

                    string membernamesurname = "";
                    if (memberid != 0)
                    {
                        var member = entities.Members.Where(c => c.MainPartyId == memberid).SingleOrDefault();
                        membernamesurname = member.MemberName + " " + member.MemberSurname;
                    }
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", membernamesurname, sep));
                    var Phonelist = entities.Phones.Where(c => c.MainPartyId == store.MainPartyId).ToList();
                    string cep1 = "";
                    string cep2 = "";
                    string tel1 = "";
                    string tel2 = "";
                    if (Phonelist != null)
                    {
                        var cepvar1 = Phonelist.Where(c => c.PhoneType == (byte)PhoneType.Gsm).ToList().FirstOrDefault();
                        if (cepvar1 != null)
                        {
                            cep1 = cepvar1.PhoneCulture + " " + cepvar1.PhoneAreaCode + " " + cepvar1.PhoneNumber;
                        }
                        var cepvar2 = Phonelist.Where(c => c.PhoneType == (byte)PhoneType.Gsm).ToList().LastOrDefault();
                        if (cepvar2 != null)
                        {
                            cep2 = cepvar2.PhoneCulture + " " + cepvar2.PhoneAreaCode + " " + cepvar2.PhoneNumber;
                        }
                        var telvar1 = Phonelist.Where(c => c.PhoneType == (byte)PhoneType.Phone).ToList().FirstOrDefault();
                        if (telvar1 != null)
                        {
                            tel1 = telvar1.PhoneCulture + " " + telvar1.PhoneAreaCode + " " + telvar1.PhoneNumber;
                        }
                        var telvar2 = Phonelist.Where(c => c.PhoneType == (byte)PhoneType.Phone).ToList().LastOrDefault();
                        if (telvar2 != null)
                        {
                            tel2 = telvar2.PhoneCulture + " " + telvar2.PhoneAreaCode + " " + telvar2.PhoneNumber;
                        }
                    }

                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", cep1, sep));
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", cep2, sep));
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", tel1, sep));
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", tel2, sep));
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", store.StoreEMail, sep));

                    var address = entities.Addresses.FirstOrDefault(c => c.MainPartyId == store.MainPartyId);

                    string adres = NeoSistem.MakinaTurkiye.Management.Models.EnumModels.AddressEdit(address);
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", adres, sep));
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", store.StoreWeb, sep));
                    string membertype = store.Packet.PacketName;
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", membertype, sep));
                    string sayfa = "";
                    IList<MemberDescription> md = (from c in entities.MemberDescriptions
                                                   where c.MainPartyId == memberid
                                                   orderby c.Date descending
                                                   select c).ToList();
                    if (md != null)
                    {
                        foreach (var item in md)
                        {

                            sayfa = sayfa + item.Date + "tarihli açıklama :" + item.Description;
                        }
                    }
                    sayfa = Server.HtmlDecode(sayfa);
                    sayfa = System.Text.RegularExpressions.Regex.Replace(sayfa, @"<(.|\n)*?>", string.Empty);
                    sayfa = sayfa.Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    System.Web.HttpContext.Current.Response.Write(string.Format("{0}{1}", sayfa, sep));
                    System.Web.HttpContext.Current.Response.Write("\n");
                }
                catch
                {

                }
            }
            System.Web.HttpContext.Current.Response.End();
        }

        public ActionResult DownloadAsExcel()
        {
            ConvertExcel();
            Session["StoreExel"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult AddExel(int StoreId)
        {
            if (Session["StoreExel"] == null)
            {
                Session["StoreExel"] = StoreId;
            }
            else
            {
                Session["StoreExel"] = Session["StoreExel"] + ":" + StoreId;
            }
            return Json(true);
        }

        [HttpPost]
        public ActionResult EditProfilePicture(int id, FormCollection coll)
        {
            HttpPostedFileBase file = Request.Files["ProfilePicture"];

            if (file.ContentLength > 0)
            {
                string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreProfilePicture, file, 800, FileHelpers.ThumbnailType.Width);

                var store = entities.Stores.SingleOrDefault(c => c.MainPartyId == id);
                store.StorePicture = fileName;
                entities.SaveChanges();
            }
            return RedirectToAction("EditProfilePicture", new { id = id });
        }

        public ActionResult ImageDelete(ControlModel model, bool deleted)
        {
            model.ImageDeleted = !deleted;
            model.IsImage = true;
            model.Text = model.ImageDeleted ? "Resim silinmek için işaretlendi." : String.Empty;

            Session["ImageDelete"] = !deleted;

            return View("ImageDelete", model);
        }

        [HttpPost]
        public ActionResult ActiveSelectedStore(string CheckItem)
        {
            string[] storeIdItems = CheckItem.Split(',');

            if (storeIdItems != null)
            {
                using (var trans = new TransactionUI())
                {
                    try
                    {
                        for (int i = 0; i < storeIdItems.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(storeIdItems.GetValue(i).ToString()))
                            {
                                var curMember = new Classes.Member();
                                curMember.LoadEntity(storeIdItems.GetValue(i).ToInt32());
                                curMember.Active = true;
                                curMember.Save();
                            }
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }

            int total = 0;

            collection =
               collection = dataStore.Search(ref total, PAGEDIMENSION, 1, "", STARTCOLUMN, ORDER).AsCollection<StoreModel>();

            var filterItems = new FilterModel<StoreModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return Redirect("/Product/Index");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateStoreAbout(string GeneralText, string HistoryText, string FounderText, string PhilosophyText)
        {
            int storeId = Session["MainPartyId"].ToInt32();

            var dataStore = new Data.Store();
            dataStore.StoreUpdateAbout(storeId, FounderText, GeneralText, HistoryText, PhilosophyText);
            return Json(true);
        }

        public JsonResult Cities(int id)
        {
            var cityItems = entities.Cities.Where(c => c.CountryId == id).OrderBy(c => c.CityOrder).ThenBy(n => n.CityName).ToList();
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(cityItems, "CityId", "CityName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Localities(int id)
        {
            var dataAddress = new Data.Address();
            var items = dataAddress.LocalityGetItemByCityId(id).AsCollection<LocalityModel>().OrderBy(c => c.LocalityName).ToList();
            items.Insert(0, new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });

            var localityItems = new SelectList(items, "LocalityId", "LocalityName");

            return Json(localityItems, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Towns(int id)
        {
            var townItems = entities.Towns.Where(c => c.LocalityId.Value == id).OrderBy(c => c.TownName).ToList();
            townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(townItems, "TownId", "TownName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaCode(string CityId)
        {
            var curCity = new Classes.City();
            curCity.LoadEntity(CityId);

            return Content(curCity.AreaCode);
        }

        public ActionResult CultureCode(string CountryId)
        {
            var curCountry = new Classes.Country();
            curCountry.LoadEntity(CountryId);

            return Content(curCountry.CultureCode);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var mainParty = new Classes.MainParty();
            return Json(new { m = mainParty.Delete(id) });
        }
        [HttpPost]
        public ActionResult UploadStorePicture(FormCollection coll)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreDealerImageFolder, file, 170, FileHelpers.ThumbnailType.Width);

                    var curPicture = new Classes.Picture()
                    {
                        PicturePath = fileName,
                        MainPartyId = Request.Files.AllKeys.GetValue(i).ToString().Replace("#", "").ToInt32(),
                        ProductId = null,
                        PictureName = String.Empty,
                    };
                    curPicture.Save();
                }
            }
            return RedirectToAction(string.Format("/Edit/{0}#tabs-3", Session["MainPartyId"].ToString()));
        }
        [HttpPost]
        public ActionResult InsertBrandPicture(FormCollection coll, string BrandName)
        {
            int mainPartyId = Session["MainPartyId"].ToInt32();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.DealerBrandImageFolder, file, 50, FileHelpers.ThumbnailType.Width);

                    var curDealerBrand = new Classes.DealerBrand()
                    {
                        DealerBrandPicture = fileName,
                        MainPartyId = mainPartyId,
                        DealerBrandName = BrandName,
                    };
                    curDealerBrand.Save();
                }
            }
            return RedirectToAction(string.Format("/Edit/{0}#tabs-6", Session["MainPartyId"].ToString()));
        }
        [HttpPost]
        public ActionResult InsertStoreBrandPicture(FormCollection coll, string StoreBrandName, string BrandDescription)
        {
            int mainPartyId = Session["MainPartyId"].ToInt32();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreBrandImageFolder, file, 106, FileHelpers.ThumbnailType.Width);
                    var curStoreBrand = new Classes.StoreBrand()
                    {
                        BrandPicture = fileName,
                        MainPartyId = mainPartyId,
                        BrandName = StoreBrandName,
                        BrandDescription = BrandDescription,
                    };
                    curStoreBrand.Save();
                }
            }
            return RedirectToAction("Edit", "Store", new { id = Session["MainPartyId"].ToString(), target = "9" });
        }
        public ActionResult LoginLogs(int? page)
        {
            int pageSize = 30;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            page = page == null ? 1 : (int)page;
            var loginLogsDistintc = _loginLogService.GetAllLoginLog().OrderByDescending(x => x.LoginLogId).Select(x => x.StoreMainPartyId).Distinct().ToList();

            int totalRecord = loginLogsDistintc.Count;
            var loginLogs = loginLogsDistintc.Skip(skipRows).Take(pageSize).ToList();
            var listeModel = new List<global::MakinaTurkiye.Entities.Tables.Logs.LoginLog>();
            foreach (var item in loginLogs)
            {
                var login = _loginLogService.GetAllLoginLog().LastOrDefault(x => x.StoreMainPartyId == item);

                listeModel.Add(login);

            }
            FilterModel<global::MakinaTurkiye.Entities.Tables.Logs.LoginLog> model = new FilterModel<global::MakinaTurkiye.Entities.Tables.Logs.LoginLog>();

            model.Source = listeModel;
            model.CurrentPage = page.Value;
            model.TotalRecord = totalRecord;

            model.PageDimension = pageSize;
            return View(model);
        }

        [HttpPost]
        public JsonResult loginLogDelete(int id)
        {
            var loginLog = _loginLogService.GetLoginLogByLoginLogId(id);
            var loginLogs = _loginLogService.GetAllLoginLog().Where(x => x.StoreMainPartyId == loginLog.StoreMainPartyId).ToList();
            foreach (var item in loginLogs)
            {
                _loginLogService.DeleteLoginLog(item);
            }

            return Json(true);
        }


        public string StoreShortNameCreate()
        {
            var stores = entities.Stores.Where(x => x.StoreShortName.Contains("-"));
            var count = stores.ToList().Count;
            foreach (var item in stores.ToList())
            {

                if (item.StoreShortName.Contains("-"))
                {
                    string[] storeName = item.StoreName.Split(' ');
                    string storeShortName = "";
                    if (storeName.Length > 2)
                    {
                        storeShortName = storeName[0] + " " + storeName[1] + " " + storeName[2];
                    }
                    else if (storeName.Length > 1)
                    {
                        storeShortName = storeName[0] + " " + storeName[1];
                    }
                    else
                    {
                        storeShortName = storeName[0];
                    }
                    if (storeShortName != "")
                    {
                        item.StoreShortName = storeShortName;
                        entities.SaveChanges();

                    }
                }

            }
            return "Tamamlandı!!!";
        }
        public ActionResult WhatsappStore()
        {
            WhatsappStoreModel model = new WhatsappStoreModel();

            var whatsappStores = _whatsapplogService.GetWhatpLogs().GroupBy(x => x.MainPartyId).Select(g => new { mainpartyId = g.Key, totalClick = g.Sum(i => i.ClickCount) });
            var whatsapClickCountTotal = (from w in _whatsapplogService.GetWhatpLogs() select w.ClickCount).Sum();
            model.TotalPage = Convert.ToInt32(Math.Ceiling((decimal)whatsappStores.ToList().Count / 30));
            model.CurrentPage = 1;
            whatsappStores = whatsappStores.OrderByDescending(x => x.totalClick).Skip(0).Take(30).ToList();
            List<StoreWhatsappListItemModel> list = new List<StoreWhatsappListItemModel>();
            foreach (var item in whatsappStores)
            {
                var store = _storeService.GetStoreByMainPartyId(item.mainpartyId);
                list.Add(new StoreWhatsappListItemModel { MainPartyId = item.mainpartyId, StoreName = store.StoreName, TotalCount = item.totalClick.ToString() });

            }
            model.StoreWhatsappListItems = list;
            ViewData["TotalClick"] = whatsapClickCountTotal;
            return View(model);
        }
        [HttpGet]
        public PartialViewResult GetWhatsappStore(int CurrentPage)
        {
            var model = new WhatsappStoreModel();
            int take = 30;

            var whatsappStores = _whatsapplogService.GetWhatpLogs().GroupBy(x => x.MainPartyId).Select(g => new { mainpartyId = g.Key, totalClick = g.Sum(i => i.ClickCount) });
            model.TotalPage = Convert.ToInt32(Math.Ceiling((decimal)whatsappStores.ToList().Count / 30));
            int skip = CurrentPage * take - take;
            model.CurrentPage = CurrentPage;
            whatsappStores = whatsappStores.OrderByDescending(x => x.totalClick).Skip(skip).Take(take).ToList();
            List<StoreWhatsappListItemModel> list = new List<StoreWhatsappListItemModel>();
            foreach (var item in whatsappStores)
            {
                var store = _storeService.GetStoreByMainPartyId(item.mainpartyId);
                list.Add(new StoreWhatsappListItemModel { MainPartyId = item.mainpartyId, StoreName = store.StoreName, TotalCount = item.totalClick.ToString() });

            }
            var whatsapClickCountTotal = (from w in _whatsapplogService.GetWhatpLogs() select w.ClickCount).Sum();
            ViewData["TotalClick"] = whatsapClickCountTotal;
            model.StoreWhatsappListItems = list;
            return PartialView("_WhatsappStoreListItem", model);
        }
        public string UpdateStoreUrlName()
        {
            var stores = (from s in entities.Stores group s by s.StoreUrlName into grp where grp.Count() > 1 select grp.Key).ToList();
            foreach (var item in stores)
            {
                if (item != null)
                {
                    string storeUrlName = item.ToString();
                    var storeList = (from s in entities.Stores where s.StoreUrlName == storeUrlName select s).ToList();
                    for (int i = 1; i < storeList.ToList().Count; i++)
                    {
                        using (var entites1 = new MakinaTurkiyeEntities())
                        {
                            var store1 = (from s in entites1.Stores where s.StoreUrlName == item select s).FirstOrDefault();
                            if (store1 != null)
                            {
                                store1.StoreUrlName = store1.StoreUrlName + +i;
                                entites1.SaveChanges();
                            }

                        }

                    }
                }
            }
            return "ok";
        }
        public ActionResult StoreDetailInformation(int id)
        {
            var store = _storeService.GetStoreByMainPartyId(id);
            StoreDetailInformationModel model = new StoreDetailInformationModel();
            model.StoreInformationModel.StoreMainPartyId = store.MainPartyId;

            var packet = _packetService.GetPacketByPacketId(store.PacketId);
            if (packet.PacketName.Contains("Gold"))
            {
                model.StoreInformationModel.PacketEndDate = store.StorePacketEndDate.ToDateTime();
            }

            model.StoreInformationModel.PacketName = packet.PacketName;
            model.StoreInformationModel.PacketType = packet.PacketPrice.ToString();
            var user = entities.Users.FirstOrDefault(x => x.UserId == store.AuthorizedId);
            if (user != null)
            {
                model.StoreInformationModel.SalesManagerName = user.UserName;
            }
            var portfoyUser = entities.Users.FirstOrDefault(x => x.UserId == store.PortfoyUserId);
            if (portfoyUser != null)
                model.StoreInformationModel.PortfoyName = portfoyUser.UserName;
            model.StoreInformationModel.StoreEmail = store.StoreEMail;
            model.StoreInformationModel.StoreFullName = store.StoreName;
            model.StoreInformationModel.StoreName = store.StoreShortName;
            model.StoreInformationModel.StoreUrl = "https://www.makinaturkiye.com/" + store.StoreUrlName;
            model.StoreInformationModel.StoreWebUrl = store.StoreWeb;
            model.StoreInformationModel.TaxNumber = store.TaxNumber;
            model.StoreInformationModel.TaxOffice = store.TaxOffice;
            model.StoreInformationModel.ViewCount = store.ViewCount.ToInt32();
            model.StoreInformationModel.StoreNo = store.StoreNo;
            model.StoreInformationModel.StoreShortDetail = store.StoreAbout;
            model.StoreInformationModel.StoreSingularViewCount = store.SingularViewCount.ToInt32();
            model.StoreInformationModel.StoreLogo = store.StoreLogo;
            string otherInfo = "";
            otherInfo = otherInfo + store.StoreEstablishmentDate + " Kuruluş yılı,";
            var storeCount = entities.Constants.FirstOrDefault(x => x.ConstantId == store.StoreEmployeesCount);
            if (storeCount != null)
                otherInfo = otherInfo + storeCount.ConstantName + ", ";
            var endersoment = entities.Constants.FirstOrDefault(x => x.ConstantId == store.StoreEndorsement);
            if (endersoment != null)
                otherInfo = otherInfo + endersoment.ConstantName + " Yıllık Ciro";
            var storeCapital = entities.Constants.FirstOrDefault(x => x.ConstantId == store.StoreCapital);
            if (storeCapital != null)
                otherInfo = otherInfo + ", " + storeCapital.ConstantName + " Sermaye";

            model.StoreInformationModel.StoreOtherInfo = otherInfo;

            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
            var member = _memberService.GetMemberByMainPartyId(Convert.ToInt32(memberStore.MemberMainPartyId));
            model.MemberInformationModel.MemberName = member.MemberName;
            model.MemberInformationModel.MemberNo = member.MemberNo;
            model.MemberInformationModel.MemberSurname = member.MemberSurname;
            var storeeposts = entities.MainPartyIdEpostas.Where(c => c.MainPartyId == id);
            model.MemberInformationModel.MemberEmails.Add(member.MemberEmail);
            foreach (var item in storeeposts)
            {
                if (!string.IsNullOrEmpty(item.Eposta1))
                    model.MemberInformationModel.MemberEmails.Add(item.Eposta1);
                if (!string.IsNullOrEmpty(item.EPosta2))
                    model.MemberInformationModel.MemberEmails.Add(item.EPosta2);
            }
            StoreContactInfoModel storeContact = new StoreContactInfoModel();
            var adress = _adressService.GetFisrtAddressByMainPartyId(member.MainPartyId);
            if (adress == null)
                adress = _adressService.GetFisrtAddressByMainPartyId(store.MainPartyId);
            if (adress != null)
                storeContact.StoreAddress = AdressExtenstionNew.GetFullAddress(adress);


            var phones1 = _phoneService.GetPhonesByMainPartyId(store.MainPartyId);
            foreach (var item in phones1)
            {
                var phoneModel = new PhoneModel();

                phoneModel.PhoneAreaCode = item.PhoneAreaCode;
                phoneModel.PhoneCulture = item.PhoneCulture;
                phoneModel.PhoneNumber = item.PhoneNumber;
                phoneModel.PhoneType = Convert.ToByte(item.PhoneType);
                storeContact.StorePhones.Add(phoneModel);
            }
            model.StoreContactInfoModel = storeContact;
            var storeActivityTypes = from s in entities.StoreActivityTypes join c in entities.ActivityTypes on s.ActivityTypeId equals c.ActivityTypeId where s.StoreId == id select new { c.ActivityName };
            model.StoreActivityTypes = string.Join(",", storeActivityTypes.Select(x => x.ActivityName).ToList());


            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();

            var products = _productService.GetAllProductsByMainPartyIds(mainPartyIds);

            if (products.Count > 0)
            {
                model.ActiveProductCount = products.Where(x => x.ProductActive == true).ToList().Count();
                model.PasiveProductCount = products.Where(x => x.ProductActive != true).ToList().Count();
                var productSingualrViewCount = products.Select(x => x.SingularViewCount).Sum();
                var productViewCount = products.Select(x => x.ViewCount).Sum();
                model.SingularProductViewCount = Convert.ToInt64(productSingualrViewCount);
                model.ProductViewCount = Convert.ToInt64(productViewCount);
            }
            var whatsapLogs = _whatsapplogService.GetWhatpLogs();
            var storeWhatsappClick = whatsapLogs.GroupBy(x => x.MainPartyId).Select(g => new { mainpartyId = g.Key, totalClick = g.Sum(i => i.ClickCount) }).FirstOrDefault(x => x.mainpartyId == id);
            if (storeWhatsappClick != null)
                model.WhatsappClickCount = storeWhatsappClick.totalClick;
            else
                model.WhatsappClickCount = 0;


            var memberDescriptions = entities.MemberDescriptions.Where(x => x.MainPartyId == member.MainPartyId && x.ConstantId != null).OrderByDescending(x => x.descId);
            foreach (var item in memberDescriptions.Skip(0).Take(4))
            {
                var modelMemberdesc = new StoreMemberDescriptionItem { DescId = item.descId, Description = item.Description, Title = item.Title };
                if (item.Date.HasValue)
                    modelMemberdesc.RecordDate = item.Date.Value;
                var userFrom = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                if (userFrom != null)
                    modelMemberdesc.UserName = userFrom.UserName;
                model.StoreMemberDescriptionItems.Add(modelMemberdesc);
            }
            model.MemberMainPartyId = member.MainPartyId.ToString();

            var storeSeoNotifications = _storeSeoNotificationService.GetStoreSeoNotificationsByStoreMainPartyId(store.MainPartyId).OrderByDescending(x => x.StoreSeoNotificationId).Skip(0).Take(8);
            foreach (var storeSeoNotification in storeSeoNotifications)
            {
                var fromUser = entities.Users.FirstOrDefault(x => x.UserId == storeSeoNotification.FromUserId);
                model.StoreSeoNotificationItems.Add(new StoreMemberDescriptionItem
                {
                    DescId = storeSeoNotification.StoreSeoNotificationId,
                    Description = storeSeoNotification.Text,
                    RecordDate = storeSeoNotification.CreatedDate,
                    UserName = fromUser.UserName
                });
            }
            var storeUpdate = _storeService.GetStoreUpdatedByMainPartyId(store.MainPartyId);
            if (storeUpdate != null)
                model.StoreInformationModel.StoreUpdatedDate = storeUpdate.UpdatedDate;
            ViewData["StoreInformation"] = true;
            return View(model);
        }
        public ActionResult WithoutDescriptionStore(string page)
        {
            var entities = new MakinaTurkiyeEntities();
            var datetime = new DateTime(2018, 10, 20);
            int pageSize = 60;
            int Page = 1;
            if (!string.IsNullOrEmpty(page)) Page = Convert.ToInt32(page);

            var storesWithout = from s in entities.Stores
                                join ms in entities.MemberStores on s.MainPartyId
      equals ms.StoreMainPartyId
                                where s.StoreActiveType == 2 && !(
                                from mb in
                                entities.MemberDescriptions
                                join ms1 in entities.MemberStores on mb.MainPartyId equals ms1.MemberMainPartyId
                                where ms1.StoreMainPartyId == s.MainPartyId
                                select new { s.MainPartyId }
                                ).Any()
                                orderby s.MainPartyId descending
                                select new { s.StoreName, s.MainPartyId, s.StoreRecordDate };

            var stores1 = storesWithout.Select(x => x.MainPartyId).Distinct();
            int pageNumbers = stores1.ToList().Count / pageSize;
            var from = Page * pageSize - pageSize;
            var stores = stores1.OrderByDescending(x => x).Skip(from).Take(pageSize);
            ViewData["pageNumbers"] = pageNumbers;
            List<StoreModel> storeModels = new List<StoreModel>();
            foreach (var item in stores)
            {
                var store = _storeService.GetStoreByMainPartyId(item);
                var modelItem = new StoreModel { StoreName = store.StoreName, MainPartyId = item };
                if (store.StoreRecordDate != null)
                    modelItem.StoreRecordDate = store.StoreRecordDate.ToDateTime();
                storeModels.Add(modelItem);
            }
            return View(storeModels);
        }

        public ActionResult New(byte newType)
        {
            var storeNews = _storeNewService.GetAllStoreNews(newType);
            int page = 1;
            int pageDimension = 20;
            FilterModel<StoreNewItem> model = new FilterModel<StoreNewItem>();
            model.TotalRecord = storeNews.Count;
            model.PageDimension = pageDimension;
            model.CurrentPage = page;
            storeNews = storeNews.OrderByDescending(x => x.UpdateDate).Skip(page * pageDimension - pageDimension).Take(pageDimension).ToList();
            var storeNewList = new List<StoreNewItem>();
            foreach (var item in storeNews)
            {
                var store = _storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                var imagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, "100x100");
                storeNewList.Add(new StoreNewItem
                {
                    Active = item.Active,
                    RecordDate = item.RecordDate,
                    ImagePath = imagePath,
                    StoreName = store.StoreName,
                    StoreNewId = item.StoreNewId,
                    Title = item.Title,
                    UpdateDate = item.UpdateDate,
                    ViewCount = item.ViewCount
                });
            }
            model.Source = storeNewList;
            return View(model);
        }

        [HttpPost]
        public ActionResult New(int page, byte newType)
        {
            var storeNews = _storeNewService.GetAllStoreNews(newType);

            int pageDimension = 20;
            FilterModel<StoreNewItem> model = new FilterModel<StoreNewItem>();
            model.TotalRecord = storeNews.Count;
            model.PageDimension = pageDimension;
            model.CurrentPage = page;
            storeNews = storeNews.OrderByDescending(x => x.UpdateDate).Skip(page * pageDimension - pageDimension).Take(pageDimension).ToList();
            var storeNewList = new List<StoreNewItem>();

            foreach (var item in storeNews)
            {
                var store = _storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                var imagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, "100x100");
                storeNewList.Add(new StoreNewItem
                {
                    Active = item.Active,
                    ImagePath = imagePath,
                    ViewCount = item.ViewCount,
                    RecordDate = item.RecordDate,
                    StoreName = store.StoreName,
                    StoreNewId = item.StoreNewId,
                    Title = item.Title,
                    UpdateDate = item.UpdateDate
                });
            }
            model.Source = storeNewList;
            return PartialView("_StoreNewItem", model);

        }
        public ActionResult CreateNew(byte newType)
        {
            StoreNewItem model = new StoreNewItem();
            model.NewType = newType;
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateNew(StoreNewItem model)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
            {
                if (string.IsNullOrEmpty(model.Title))
                    ModelState.AddModelError("Title", "Başlık alanı boş geçilemez");
                if (string.IsNullOrEmpty(model.Content))
                {
                    ModelState.AddModelError("Content", "İçerik alanı boş geçilemez");
                }
            }
            else
            {
                string storeNo = Request.Form["StoreNo"].ToString();
                var store = entities.Stores.FirstOrDefault(x => x.StoreNo == storeNo);
                if (store == null)
                {
                    ModelState.AddModelError("StoreNo", "Lütfen kayıtlı firma numarası giriniz");
                }
                else
                {
                    var storeNew = new global::MakinaTurkiye.Entities.Tables.Stores.StoreNew();
                    storeNew.Active = true;
                    storeNew.Content = model.Content;
                    storeNew.NewType = model.NewType;
                    storeNew.StoreMainPartyId = store.MainPartyId;
                    storeNew.Title = model.Title;
                    storeNew.RecordDate = DateTime.Now;
                    storeNew.UpdateDate = DateTime.Now;
                    _storeNewService.InsertStoreNew(storeNew);
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        string filename = "";
                        if (ImageContentType.Any(x => x == file.ContentType) && file.ContentLength > 0)
                        {

                            string oldfile = file.FileName;
                            string mapPath = this.Server.MapPath(AppSettings.StoreNewImageFolder);
                            string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                            filename = storeNew.Title.ToImageFileName() + storeNew.StoreNewId + "_haber" + uzanti;
                            var targetFile = new FileInfo(mapPath + filename);
                            string storeNewImage = mapPath + filename;
                            file.SaveAs(storeNewImage);

                            var imageizes = AppSettings.StoreNewImageSize.Split(';');
                            foreach (var item in imageizes)
                            {
                                int width = Convert.ToInt32(item.Split('x')[0]);
                                int height = Convert.ToInt32(item.Split('x')[1]);

                                Image img = ImageProcessHelper.resizeImageBanner(width, height, storeNewImage);
                                ImageProcessHelper.SaveJpeg(storeNewImage, img, 100, "_haber", "_" + item);
                            }

                        }
                        storeNew.ImageName = filename;
                        _storeNewService.UpdateStoreNew(storeNew);
                    }
                    return RedirectToAction("New", new { @newType = model.NewType });
                }
            }
            return View(model);
        }
        public ActionResult EditNew(int id)
        {
            var storeNew = _storeNewService.GetStoreNewByStoreNewId(id);
            var store = _storeService.GetStoreByMainPartyId(storeNew.StoreMainPartyId);

            StoreNewItem model = new StoreNewItem();
            model.Active = storeNew.Active;
            model.RecordDate = storeNew.RecordDate;
            model.StoreName = store.StoreName;
            model.StoreNewId = storeNew.StoreNewId;
            model.Content = storeNew.Content;
            model.Title = storeNew.Title;
            model.ImagePath = ImageHelper.GetStoreNewImagePath(storeNew.ImageName, StoreNewImageSize.px100x100.ToString());
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditNew(StoreNewItem model)
        {
            var storeNew = _storeNewService.GetStoreNewByStoreNewId(model.StoreNewId);
            storeNew.Title = model.Title;
            storeNew.Content = model.Content;
            if (model.Active && storeNew.Active == false)
            {
                ConfirmNewMail(storeNew);
            }
            storeNew.Active = model.Active;



            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (ImageContentType.Any(x => x == file.ContentType) && file.ContentLength > 0)
                {
                    var mapPathOld = Server.MapPath(AppSettings.StoreNewImageFolder);
                    var fileOld = mapPathOld + storeNew.ImageName;
                    var imageizes = AppSettings.StoreNewImageSize.Split(';');
                    foreach (var item in imageizes)
                    {
                        var fileOldAnother = fileOld.Replace("haber", item);
                        if (System.IO.File.Exists(fileOld))
                        {
                            System.IO.File.Delete(fileOld);
                        }
                        if (System.IO.File.Exists(fileOldAnother))
                        {
                            System.IO.File.Delete(fileOldAnother);
                        }
                    }

                    string oldfile = file.FileName;
                    string mapPath = this.Server.MapPath(AppSettings.StoreNewImageFolder);
                    string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                    string filename = storeNew.Title.ToImageFileName() + storeNew.StoreNewId + "_haber" + uzanti;
                    var targetFile = new FileInfo(mapPath + filename);
                    string storeNewImage = mapPath + filename;
                    file.SaveAs(storeNewImage);
                    foreach (var item in imageizes)
                    {
                        int width = Convert.ToInt32(item.Split('x')[0]);
                        int height = Convert.ToInt32(item.Split('x')[1]);

                        Image img = ImageProcessHelper.resizeImageBanner(width, height, storeNewImage);
                        ImageProcessHelper.SaveJpeg(storeNewImage, img, 100, "_haber", "_" + item);
                    }

                    storeNew.ImageName = filename;
                }
            }

            _storeNewService.UpdateStoreNew(storeNew);


            return RedirectToAction("EditNew", new { id = model.StoreNewId });
        }
        public void ConfirmNewMail(global::MakinaTurkiye.Entities.Tables.Stores.StoreNew storeNew)
        {
            try
            {
                var mailTemplate = entities.MessagesMTs.FirstOrDefault(x => x.MessagesMTName == "haberonaylandi");
                var store = _storeService.GetStoreByMainPartyId(storeNew.StoreMainPartyId);
                var storeProfileNewLink = UrlBuilder.GetStoreNewsUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                string newLink = UrlBuilder.GetStoreNewUrl(storeNew.StoreNewId, storeNew.Title);
                string content = mailTemplate.MailContent;
                content = content.Replace("#firmaadi#", store.StoreName).Replace("#firmahaberanasayfa#", storeProfileNewLink).Replace("#haberlink#", newLink);
                var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeNew.StoreMainPartyId);
                var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);
                var mailHelper = new MailHelper(mailTemplate.MessagesMTTitle, content, mailTemplate.Mail, member.MemberEmail, mailTemplate.MailPassword, mailTemplate.MailSendFromName);
                mailHelper.Send();
            }
            catch (Exception ex)
            {


            }
        }
        [HttpPost]
        public JsonResult ChangeNewActive(string CheckItem, int set)
        {
            CheckItem = CheckItem.Substring(0, CheckItem.Length - 1);
            string[] itemNews = CheckItem.Split(',');
            bool confirm = false;
            if (set == 1) confirm = true;
            foreach (var item in itemNews)
            {
                var storeNew = _storeNewService.GetStoreNewByStoreNewId(Convert.ToInt32(item));

                storeNew.Active = confirm;
                if (confirm == true)
                {
                    ConfirmNewMail(storeNew);
                }
                _storeNewService.UpdateStoreNew(storeNew);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void ProductCountCalc(Product curProduct, bool add)
        {
            List<TopCategory> topCategories = new List<TopCategory>();
            if (curProduct.ModelId.HasValue)
            {
                topCategories = entities.spCategoryTopCategoriesByCategoryId(curProduct.ModelId.Value).ToList();

            }
            else if (curProduct.SeriesId.HasValue)
            {
                topCategories = entities.spCategoryTopCategoriesByCategoryId(curProduct.SeriesId.Value).ToList();

            }
            else if (curProduct.BrandId.HasValue)
            {
                topCategories = entities.spCategoryTopCategoriesByCategoryId(curProduct.BrandId.Value).ToList();

            }
            else if (curProduct.CategoryId.HasValue)
            {
                topCategories = entities.spCategoryTopCategoriesByCategoryId(curProduct.CategoryId.Value).ToList();

            }

            if (topCategories.Count > 0)
            {
                foreach (var item in topCategories)
                {
                    var category = entities.Categories.SingleOrDefault(c => c.CategoryId == item.CategoryId);
                    if (category != null)
                    {
                        if (add)
                        {
                            category.ProductCount = category.ProductCount + 1;
                            if (category.CategoryType == (byte)CategoryType.Sector)
                            {
                                if (curProduct.ProductStatu == "72")
                                    category.ProductCountAll = category.ProductCountAll + 1;
                                else if (curProduct.ProductStatu == "73")
                                    category.ProductCountNew = category.ProductCountNew + 1;
                                else if (curProduct.ProductStatu == "201")
                                    category.ProductCountNew = category.ProductCountNew + 1;
                            }
                        }
                        else
                        {
                            category.ProductCount = category.ProductCount - 1;
                            if (category.CategoryType == (byte)CategoryType.Sector)
                            {
                                if (curProduct.ProductStatu == "72")
                                    category.ProductCountAll = category.ProductCountAll - 1;
                                else if (curProduct.ProductStatu == "73")
                                    category.ProductCountNew = category.ProductCountNew - 1;
                                else if (curProduct.ProductStatu == "201")
                                    category.ProductCountNew = category.ProductCountNew - 1;
                            }

                        }

                        entities.SaveChanges();
                    }
                }
            }
        }


        public ActionResult BuyPacket(int id)
        {
            ViewData["BuyPacket"] = true;
            BuyPacketModel model = new BuyPacketModel();
            int packetId = 29;
            var store = _storeService.GetStoreByMainPartyId(id);
            model.TaxNo = store.TaxNumber;
            model.TaxOffice = store.TaxOffice;
            var packet = _packetService.GetAllPacket().FirstOrDefault(x => x.PacketId == packetId);
            float taxDiscount = ((float)packet.PacketPrice - ((float)packet.PacketPrice / (1.18f)));
            decimal packetPrice = packet.PacketPrice - Convert.ToDecimal(taxDiscount);
            model.PacketPrice = Convert.ToInt32(packetPrice);
            model.PacketDay = packet.PacketDay;
            return View(model);
        }
        [HttpPost]
        public JsonResult BuyPacket(BuyPacketModel model)
        {
            int packetId = 29;
            string installmentTextMail = "";
            string mailTemplateName = "havalbildirimmail";
            if (model.OrderType == (byte)Ordertype.HavaleTaksit)
                mailTemplateName = "havaletaksitbilidirimmail";
            else if (model.OrderType == (byte)Ordertype.KrediKarti)
                mailTemplateName = "kredikartisatis";
            else if (model.OrderType == (byte)Ordertype.KrediKartiVade)
                mailTemplateName = "kredikartivadebildirim";

            var packet = _packetService.GetPacketByPacketId(packetId);
            decimal packetPriceWithoutTax = packet.PacketPrice - (decimal)((float)packet.PacketPrice - ((float)packet.PacketPrice / (1.18f)));
            decimal amount = 0;
            decimal packetPrice = 0;
            if (model.DiscountType != "0")
            {
                amount = Convert.ToDecimal(model.DiscountAmount);
                if (model.DiscountType == "1")
                {
                    packetPrice = packetPriceWithoutTax - (packetPriceWithoutTax * (amount / 100));
                    packetPrice = Convert.ToDecimal((float)packetPrice * 1.18f);
                }
                else
                {
                    packetPrice = packetPriceWithoutTax - amount;
                    packetPrice = Convert.ToDecimal((float)packetPrice * 1.18f);
                }
            }
            else
            {
                packetPrice = packet.PacketPrice;
            }
            var store = _storeService.GetStoreByMainPartyId(model.MainPartyId);
            var adress = _adressService.GetFisrtAddressByMainPartyId(model.MainPartyId);
            string orderNo = "S" + model.MainPartyId;
            string orderCode = Guid.NewGuid().ToString("N").Substring(0, 5);
            var packetStartDate = DateTime.Now;
            if (store.StorePacketEndDate > packetStartDate && store.PacketId == 29)
            {
                packetStartDate = store.StorePacketEndDate.Value;
            }
            var order = new global::MakinaTurkiye.Entities.Tables.Checkouts.Order
            {
                MainPartyId = model.MainPartyId,
                Address = AdressExtenstionNew.GetFullAddress(adress),
                PacketId = packetId,
                OrderCode = orderCode,
                OrderNo = orderNo,
                TaxNo = model.TaxNo,
                TaxOffice = model.TaxOffice,
                PacketStatu = (byte)PacketStatu.Inceleniyor,
                OrderType = Convert.ToByte(model.OrderType),
                RecordDate = DateTime.Now,
                PacketStartDate = packetStartDate,
                PacketDay = model.PacketDay
            };
            if (store.AuthorizedId != null)
            {
                order.AuthorizedId = store.AuthorizedId.Value;
            }
            order.AccountId = 4;
            order.OrderPrice = packetPrice;
            _orderService.InsertOrder(order);

            #region storeDiscount
            if (model.DiscountType != "0")
            {
                var storeDiscount = new global::MakinaTurkiye.Entities.Tables.Stores.StoreDiscount();
                if (model.DiscountType == "2")
                    storeDiscount.DiscountAmount = amount;
                else
                    storeDiscount.DiscountPercentage = Convert.ToDecimal(model.DiscountAmount);
                storeDiscount.OrderId = order.OrderId;
                storeDiscount.StoreMainPartyId = model.MainPartyId;
                storeDiscount.UserId = CurrentUserModel.CurrentManagement.UserId;
                storeDiscount.RecordDate = DateTime.Now;
                _storeDiscountService.InsertStoreDiscount(storeDiscount);
            }
            #endregion

            List<DateTime> installmentdates = new List<DateTime>();

            var firstDate = DateTime.Now;

            DateTimeFormatInfo usDtfi = new CultureInfo("tr-TR", false).DateTimeFormat;

            if (model.OrderType == (byte)Ordertype.HavaleTaksit)
            {
                string[] installmentsDates = model.Dates.Substring(0, model.Dates.Length - 1).Split(',');
                int c = 1;
                foreach (var item in installmentsDates)
                {

                    var orderInstallment = new global::MakinaTurkiye.Entities.Tables.Checkouts.OrderInstallment();
                    orderInstallment.OrderId = order.OrderId;
                    orderInstallment.PayDate = DateTime.ParseExact(item, "dd.MM.yyyy", null);
                    orderInstallment.Amunt = order.OrderPrice / model.Installment;
                    orderInstallment.IsPaid = false;
                    orderInstallment.RecordDate = DateTime.Now;
                    _orderInstallmentService.InsertOrderInstallment(orderInstallment);
                    installmentTextMail = installmentTextMail + " " + orderInstallment.PayDate.ToString("dd/MMM/yyyy") + "<br/>";
                    installmentdates.Add(orderInstallment.PayDate);
                    if (c == 1)
                        firstDate = DateTime.ParseExact(item, "dd.MM.yyyy", null);
                    c++;
                }
            }
            if (!string.IsNullOrEmpty(model.PayDate))
            {
                firstDate = Convert.ToDateTime(model.PayDate);
            }
            var orderDescription = new global::MakinaTurkiye.Entities.Tables.Checkouts.OrderDescription();

            orderDescription.Description = string.IsNullOrEmpty(model.Description) ? "Vade Tarihi" : model.Description;
            orderDescription.OrderId = order.OrderId;
            orderDescription.RecordDate = DateTime.Now;
            orderDescription.PayDate = firstDate;
            _orderService.InsertOrderDescription(orderDescription);

            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);

            var mailAdress = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value).MemberEmail;
            string Email = mailAdress;
            MessagesMT mailMessage = new MessagesMT();
            Account account = new Account();
            using (var entities = new MakinaTurkiyeEntities())
            {
                mailMessage = entities.MessagesMTs.First(x => x.MessagesMTName == mailTemplateName);
                account = entities.Accounts.FirstOrDefault(x => x.AccountId == order.AccountId);
            }
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(Email);                                                              //Mailin kime gideceğini belirtiyoruz
            mail.Subject = mailMessage.MessagesMTTitle;                                              //Mail konusu
            string template = mailMessage.MessagesMTPropertie;
            if (model.OrderType == (byte)Ordertype.Havale)
            {
                template = template.Replace("#firmadi#", store.StoreName).Replace("#kullaniciadi#", store.StoreName).Replace("#pakettipi#", packet.PacketName).Replace("#tutar#", order.OrderPrice.ToDecimal().ToString("N") + " TL").Replace("#bankahesapbilgileri#", account.BankName + "-" + account.AccountNo);
                order.PayDate = firstDate;
                _orderService.UpdateOrder(order);
            }
            else if (model.OrderType == (byte)Ordertype.HavaleTaksit)
            {
                order.PayDate = installmentdates[0];
                template = template.Replace("#firmadi#", store.StoreName).Replace("#kullaniciadi#", store.StoreName).Replace("#pakettipi#", packet.PacketName).Replace("#tutar#", order.OrderPrice.ToDecimal().ToString("N") + " TL").Replace("#bankahesapbilgileri#", account.BankName + "-" + account.AccountNo);
                template = template.Replace("#taksitsayi#", model.Installment.ToString()).Replace("#taksittarih#", installmentTextMail);
                _orderService.UpdateOrder(order);

            }
            else if (model.OrderType == (byte)Ordertype.KrediKartiVade)
            {
                order.PayDate = firstDate;
                template = template.Replace("#firmadi#", store.StoreName).Replace("#kullaniciadi#", store.StoreName).Replace("#pakettipi#", packet.PacketName).Replace("#tutar#", order.OrderPrice.ToDecimal().ToString("N") + " TL").Replace("#bankahesapbilgileri#", account.BankName + "-" + account.AccountNo);
                template = template.Replace("#odemetarih#", firstDate.ToString("dd/mm/yyyy"));
                _orderService.UpdateOrder(order);
            }
            else if (order.OrderType == (byte)Ordertype.KrediKarti)
            {
                template = template.Replace("#firmadi#", store.StoreName).Replace("#kullaniciadi#", store.StoreName).Replace("#pakettipi#", packet.PacketName).Replace("#tutar#", order.OrderPrice.ToDecimal().ToString("N") + " TL").Replace("#bankahesapbilgileri#", account.BankName + "-" + account.AccountNo);

            }
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
            sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
            sc.Send(mail);


            #region bilgimakina
            MailMessage mailb = new MailMessage();
            MessagesMT mailTmpInf = new MessagesMT();
            using (var entitites = new MakinaTurkiyeEntities())
            {
                mailTmpInf = entitites.MessagesMTs.First(x => x.MessagesMTName == "bilgimakinasayfası");
            }


            mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
            mailb.To.Add("makinaturkiye@makinaturkiye.com");
            mailb.Subject = "Paket Satın Alma " + store.StoreName;
            //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
            //templatet = messagesmttemplate.MessagesMTPropertie;
            string bilgimakinaicin = store.StoreName + " isimli firma " + packet.PacketName + " paketi için admin panelinden satın alma gerçekleşmiştir.";
            mailb.Body = bilgimakinaicin;
            mailb.IsBodyHtml = true;
            mailb.Priority = MailPriority.Normal;
            SmtpClient scr1 = new SmtpClient();
            scr1.Port = 587;
            scr1.Host = "smtp.gmail.com";
            scr1.EnableSsl = true;
            scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
            scr1.Send(mailb);
            #endregion

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StoreSector(int id)
        {
            ViewData["StoreSector"] = true;
            var sectors = _categoryService.GetMainCategories();
            StoreSectorModel model = new StoreSectorModel();
            model.MainPartyId = id;
            foreach (var item in sectors)
            {
                var storeSector = _storeSectorService.GetStoreSectorByStoreMainPartyIdAndCategoryId(id, item.CategoryId);

                var sectorItem = new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                };
                if (storeSector != null)
                    sectorItem.Selected = true;
                model.SectorCategories.Add(sectorItem);

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult StoreSector(StoreSectorModel model, int[] SectorCategoriesForm)
        {

            var storeSectors = _storeSectorService.GetStoreSectorsByMainPartyId(model.MainPartyId);
            foreach (var item in storeSectors)
            {
                _storeSectorService.DeleteStoreSector(item);
            }
            foreach (var item in SectorCategoriesForm)
            {
                var storeSector = new global::MakinaTurkiye.Entities.Tables.Stores.StoreSector();
                storeSector.StoreMainPartyId = model.MainPartyId;
                storeSector.CategoryId = item;
                storeSector.RecordDate = DateTime.Now;
                _storeSectorService.InsertStoreSector(storeSector);
            }
            TempData["success"] = true;
            return RedirectToAction("StoreSector", new { id = model.MainPartyId });
        }

        public string ExportStoreGsmPhones()
        {

            FileHelper fileHelper = new FileHelper();

            var query = from s in entities.Stores
                        join ms in entities.MemberStores
                        on s.MainPartyId equals ms.StoreMainPartyId
                        join p in entities.Phones on ms.StoreMainPartyId equals p.MainPartyId
                        where p.PhoneType == (byte)PhoneType.Gsm
                        select p;



            string filename = "makinaturkiye-firma_telefon-listesi";
            var list = new List<string>();

            foreach (var item in query)
            {
                if (item.PhoneCulture != null)
                {
                    if (item.PhoneCulture.Substring(0, 1) != "+")
                    {
                        item.PhoneCulture = "+" + item.PhoneCulture;
                    }
                }
                else
                    item.PhoneCulture = "+90";

                var phoneNumber = item.PhoneCulture + item.PhoneAreaCode + item.PhoneNumber;
                list.Add(phoneNumber);

            }


            fileHelper.ExportExcel<string>(list, filename);
            return "exported";
        }


        public ActionResult DeleteStore(int storeMainPartyId)
        {
            //var stores = entities.Stores.Where(x => x.StoreActiveType == 4).OrderBy(x => x.MainPartyId).Skip((page * 100) - 100).Take(100);
            var stores = entities.Stores.Where(x => x.MainPartyId == storeMainPartyId);

            var basePath = "C:/Web/s.makinaturkiye.com";
            foreach (var item in stores)
            {

                var storeLogoPath = basePath + "/Store/" + item.MainPartyId;
                FileHelpers.DeleteFullPath(storeLogoPath);
                var bannerFile = "/UserFiles/StoreBanner/" + item.StoreBanner;
                FileHelpers.Delete(bannerFile);
                var storeCatologFodler = basePath + "/StoreCatolog/" + item.MainPartyId;
                FileHelpers.DeleteFullPath(storeCatologFodler);
                var storeCatologs = entities.StoreCatologFiles.Where(x => x.StoreMainPartyId == item.MainPartyId); //store catolog
                foreach (var catolog in storeCatologs)
                {
                    entities.StoreCatologFiles.DeleteObject(catolog);
                }
                var storeNews = entities.StoreNews.Where(x => x.StoreMainPartyId == item.MainPartyId); //storenews
                foreach (var storeNew in storeNews)
                {
                    var newFilePath = basePath + AppSettings.StoreNewImageFolder + storeNew.ImageName;
                    var newImageSizes = AppSettings.StoreNewImageSize.Split(';');
                    foreach (var imgSize in newImageSizes)
                    {
                        var filePath = AppSettings.StoreNewImageSize + storeNew.ImageName.Replace("_haber", imgSize);
                        FileHelpers.Delete(filePath);

                    }
                    FileHelpers.Delete(newFilePath);
                    entities.StoreNews.DeleteObject(storeNew);
                }
                var addresses = entities.Addresses.Where(x => x.MainPartyId == item.MainPartyId); //address
                foreach (var address in addresses)
                {
                    var addressChangehistories = entities.AddressChangeHistories.Where(x => x.ADdressId == address.AddressId);
                    foreach (var addressChangehistory in addressChangehistories)
                    {
                        entities.AddressChangeHistories.DeleteObject(addressChangehistory);
                    }
                    entities.Addresses.DeleteObject(address);
                }
                var phones = entities.Phones.Where(x => x.MainPartyId == item.MainPartyId);//phones
                foreach (var phone in phones)
                {
                    var phoneChangehistories = entities.PhoneChangeHistories.Where(x => x.PhoneId == phone.PhoneId);
                    foreach (var phoneChangehistory in phoneChangehistories)
                    {
                        entities.PhoneChangeHistories.DeleteObject(phoneChangehistory);
                    }
                    entities.Phones.DeleteObject(phone);
                }

                var storeCertificates = entities.StoreCertificates.Where(x => x.MainPartyId == item.MainPartyId);//storecertificates
                foreach (var storeCertificate in storeCertificates)
                {

                    var certificatePictures = entities.Pictures.Where(x => x.MainPartyId == item.MainPartyId && x.StoreImageType == 3);
                    foreach (var pic in certificatePictures)
                    {
                        var certificateImages = "/UserFiles/Images/StoreCertificateImageFolder/" + pic.PicturePath;
                        var thm = "/UserFiles/Images/StoreCertificateImageFolder/" + pic.PicturePath.Replace("_certificate", "500x800");
                        FileHelpers.Delete(certificateImages);
                        FileHelpers.Delete(thm);
                        entities.Pictures.DeleteObject(pic);
                    }
                    var certificateTypeProducts = entities.CertificateTypeProducts.Where(x => x.StoreCertificateId == storeCertificate.StoreCertificateId);
                    foreach (var certificateTypeProduct in certificateTypeProducts)
                    {
                        entities.CertificateTypeProducts.DeleteObject(certificateTypeProduct);
                    }
                    entities.StoreCertificates.DeleteObject(storeCertificate);
                }
                var dealers = entities.DealerBrands.Where(x => x.MainPartyId == item.MainPartyId);
                foreach (var dealer in dealers)
                {
                    entities.DealerBrands.DeleteObject(dealer);
                }

                var storeActivities = entities.StoreActivityTypes.Where(x => x.StoreId == item.MainPartyId);
                foreach (var storeActivity in storeActivities)
                {
                    entities.StoreActivityTypes.DeleteObject(storeActivity);
                }
                var storeActivityCategories = entities.StoreActivityCategories.Where(x => x.MainPartyId == item.MainPartyId);
                foreach (var storeActivityCategory in storeActivityCategories)
                {
                    entities.StoreActivityCategories.DeleteObject(storeActivityCategory);
                }
                var storeSliderImages = entities.Pictures.Where(x => x.MainPartyId == item.MainPartyId && x.StoreImageType == 2);//store sliderimages
                foreach (var slImage in storeSliderImages)
                {
                    var imagePath = "/UserFiles/Images/StoreSliderImage/" + slImage.PicturePath;
                    FileHelpers.Delete(imagePath);
                    FileHelpers.Delete(imagePath.Replace("_slider", "-800x300"));
                    entities.Pictures.DeleteObject(slImage);
                }

                var storeImages = entities.Pictures.Where(x => x.MainPartyId == item.MainPartyId && x.StoreImageType == 1);//storeimages
                foreach (var sImage in storeImages)
                {
                    var imagePath = "/UserFiles/Images/StoreImage/" + sImage.PicturePath;
                    FileHelpers.Delete(imagePath);
                    entities.Pictures.DeleteObject(sImage);
                }
                var storeChangeHistories = entities.StoreChangeHistories.Where(x => x.MainPartyId == item.MainPartyId);
                foreach (var storeChangeHistory in storeChangeHistories)
                {
                    entities.StoreChangeHistories.DeleteObject(storeChangeHistory);
                }
                var storeInfoNumberShows = entities.StoreInfoNumberShows.Where(x => x.StoreMainPartyID == item.MainPartyId);
                foreach (var storeInfoNumberShow in storeInfoNumberShows)
                {
                    entities.StoreInfoNumberShows.DeleteObject(storeInfoNumberShow);
                }
                var memberSettings = entities.MemberSettings.Where(x => x.StoreMainPartyId == item.MainPartyId);//membersettings
                foreach (var memberSetting in memberSettings)
                {
                    entities.MemberSettings.DeleteObject(memberSetting);
                }

                var favoriteStores = entities.FavoriteStores.Where(x => x.StoreMainPartyId == item.MainPartyId); //favorite stores
                foreach (var favoriteStore in favoriteStores)
                {
                    entities.FavoriteStores.DeleteObject(favoriteStore);
                }
                var storeSectors = entities.StoreSectors.Where(x => x.StoreMainPartyId == item.MainPartyId);
                foreach (var storeSector in storeSectors)
                {
                    entities.StoreSectors.DeleteObject(storeSector);
                }

                var storeStatisitcs = entities.StoreStatistics.Where(x => x.StoreId == item.MainPartyId); //storestatistics
                foreach (var storeStatisitc in storeStatisitcs)
                {
                    entities.StoreStatistics.DeleteObject(storeStatisitc);
                }

                var memberStores = entities.MemberStores.Where(x => x.StoreMainPartyId == item.MainPartyId);
                foreach (var memberStore in memberStores)
                {
                    var member = entities.Members.FirstOrDefault(x => x.MainPartyId == memberStore.MemberMainPartyId);
                    member.MemberType = (byte)MemberType.FastIndividual;

                    var memberDescriptions = entities.MemberDescriptions.Where(x => x.MainPartyId == memberStore.MemberMainPartyId);
                    foreach (var memberDescription in memberDescriptions)
                    {
                        entities.MemberDescriptions.DeleteObject(memberDescription);
                    }
                    var baseMemberDescriptions = entities.BaseMemberDescriptions.Where(x => x.MainPartyId == memberStore.MemberMainPartyId);
                    foreach (var baseMemberDescription in baseMemberDescriptions)
                    {
                        entities.BaseMemberDescriptions.DeleteObject(baseMemberDescription);
                    }
                    entities.MemberStores.DeleteObject(memberStore);
                }

                var storeSeoNotifications = _storeSeoNotificationService.GetStoreSeoNotificationsByStoreMainPartyId(item.MainPartyId);
                foreach (var storeSeoNotification in storeSeoNotifications)
                {
                    _storeSeoNotificationService.DeleteStoreSeoNotification(storeSeoNotification);
                }
                #region products
                var products = entities.Products.Where(x => x.MainPartyId == memberStores.FirstOrDefault().MemberMainPartyId);
                foreach (var product in products)
                {
                    product.ProductActive = false;
                    product.ProductActiveType = (byte)ProductActiveTypeEnum.CopKutusuYeni;
                }
                #endregion
                var storeDealers = entities.StoreDealers.Where(x => x.MainPartyId == item.MainPartyId);
                foreach (var storeDealer in storeDealers)
                {

                    var adresses = entities.Addresses.Where(x => x.StoreDealerId == storeDealer.StoreDealerId);
                    foreach (var address in adresses)
                    {
                        var phonesDealers = entities.Phones.Where(x => x.AddressId == address.AddressId);
                        foreach (var phonesDealer in phonesDealers)
                        {
                            entities.Phones.DeleteObject(phonesDealer);
                        }

                        entities.Addresses.DeleteObject(address);
                    }
                    var dealerPictures = entities.Pictures.Where(x => x.StoreDealerId == storeDealer.StoreDealerId);
                    foreach (var dealerPicture in dealerPictures)
                    {
                        FileHelpers.Delete(AppSettings.StoreDealerImageFolder + dealerPicture.PicturePath);
                        FileHelpers.Delete(AppSettings.StoreDealerImageFolder + FileHelper.ImageThumbnailName(dealerPicture.PicturePath));

                        entities.Pictures.DeleteObject(dealerPicture);
                    }
                    entities.StoreDealers.DeleteObject(storeDealer);
                }
                var orders = entities.Orders.Where(x => x.MainPartyId == item.MainPartyId);
                foreach (var order in orders)
                {
                    entities.Orders.DeleteObject(order);
                }

                var creditCardLogStores = entities.CreditCardLogs.Where(x => x.MainPartyID == item.MainPartyId);
                foreach (var creditCardLogStore in creditCardLogStores)
                {
                    entities.CreditCardLogs.DeleteObject(creditCardLogStore);

                }
                entities.Stores.DeleteObject(item);
            }
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult StoreInfoChecked(int id)
        {
            var storeUpdate = _storeService.GetStoreUpdatedByMainPartyId(id);
            if (storeUpdate == null)
            {
                storeUpdate = new global::MakinaTurkiye.Entities.Tables.Stores.StoreUpdated();
                storeUpdate.UpdatedDate = DateTime.Now;
                storeUpdate.MainPartyId = id;
                _storeService.InsertStoreUpdated(storeUpdate);
            }
            else
            {
                storeUpdate.UpdatedDate = DateTime.Now;
                _storeService.UpdateStoreUpdated(storeUpdate);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }

}

