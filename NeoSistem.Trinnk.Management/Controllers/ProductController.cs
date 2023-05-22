using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
{
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using global::Trinnk.Core;
    using global::Trinnk.Services.Catalog;
    using global::Trinnk.Services.Common;
    using global::Trinnk.Services.Media;
    using global::Trinnk.Services.Members;
    using global::Trinnk.Services.Stores;
    using global::Trinnk.Services.Videos;
    using global::Trinnk.Utilities.HttpHelpers;
    using Trinnk.Core.Web.Helpers;
    using Models;
    using NeoSistem.Trinnk.Management.Models.Catolog;
    using NeoSistem.Trinnk.Management.Models.Entities;
    using NeoSistem.Trinnk.Management.Models.ProductModels;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    public class ProductController : BaseController
    {

        #region Constants

        const string STARTCOLUMN = "ProductId";
        const string ORDER = "Desc";
        const string SessionPage = "product_PAGEDIMENSION";

        #endregion

        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IVideoService _videoService;
        private readonly IConstantService _constantService;
        private readonly IPictureService _pictureService;
        private readonly IProductComplainService _productComplainService;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly IStoreService _storeService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMemberService _memberService;
        private readonly IProductCommentService _productCommentService;
        private readonly IProductHomePageService _productHomePageService;
        private readonly ICategoryPlaceChoiceService _categoryPlaceChoiceService;
        private readonly ICertificateTypeService _certificateTypeService;
        private readonly IDeletedProductRedirectService _deletedProductRedirectService;
        #endregion

        #region Ctor

        public ProductController(ICategoryService categoryService,
            IProductCommentService productCommentService,
            IProductService productService, IVideoService videoService,
            IConstantService constantService, IPictureService pictureService,
            IProductComplainService productComplainService,
            IFavoriteProductService favoriteProductService, IStoreService storeService,
            IMemberStoreService memberStoreService, IMemberService memberService,
            IProductHomePageService productHomePageService,
            ICategoryPlaceChoiceService categoryPlaceChoiceService,
            ICertificateTypeService certificateTypeService, IDeletedProductRedirectService deletedProductRedirectService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
            this._videoService = videoService;
            this._constantService = constantService;
            this._pictureService = pictureService;
            this._productComplainService = productComplainService;
            this._favoriteProductService = favoriteProductService;
            this._storeService = storeService;
            this._memberStoreService = memberStoreService;
            this._memberService = memberService;
            this._productCommentService = productCommentService;
            this._productHomePageService = productHomePageService;
            this._categoryPlaceChoiceService = categoryPlaceChoiceService;
            this._certificateTypeService = certificateTypeService;
            this._deletedProductRedirectService = deletedProductRedirectService;

        }

        #endregion

        int PAGEDIMENSION = 20;

        static Data.Product dataProduct = null;

        static ICollection<ProductModel> collection = null;

        #region Methods

        public ActionResult ActiveType(int id)
        {
            return RedirectToAction("Index", new { ActiveType = id });
        }

        public ActionResult Active(int id)
        {
            return RedirectToAction("Index", new { Active = id });
        }
        //excel ekleme
        public ActionResult ReadExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadExcel(FormCollection coll)
        {
            //kayıt başarılımı değilse hangi satırda hata gerçekleşti onu yazdır ve eğer o satırda hata olduysa o satırdan itibaren tekrar başlaması gerekli

            HttpPostedFileBase file = Request.Files["ExcelProduct"];
            string fileName = FileHelpers.SaveExcel(AppSettings.SaveAnyOtherFile, file);
            string dosyayolu = AppSettings.SaveAnyOtherFile + fileName;
            string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;",
                      Server.MapPath(dosyayolu));

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sayfa1$]", conn);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            var ogrenciler = from o in dt.AsEnumerable()
                             select new
                             {
                                 ProductName = o.Field<string>("ProductName"),
                                 ProductPrice = o.Field<double>("ProductPrice"),
                                 ProductDescription = o.Field<string>("ProductDescription"),
                                 Picture1 = o.Field<string>("Picture1"),
                                 ModelAdi = o.Field<string>("ModelAdi"),
                                 MarkaAdi = o.Field<string>("MarkaAdi"),
                                 ModelNo = o.Field<int>("ModelNo"),
                                 MarkaNo = o.Field<int>("MarkaNo")
                             };
            int[] Marka = { 0 };
            int[] Model = { 0 };
            int[] MarkaCategoryId = { 0 };
            int[] ModelCategoryId = { 0 };
            foreach (var item in ogrenciler)
            {
                if (Marka.Contains(item.MarkaNo) == true)
                {
                    //bu marka kullanılmış yani aynı yere eklemek gerek
                    if (Model.Contains(item.ModelNo) == true)
                    {
                        //int sıra=MarkaCategoryId[Marka]

                        //modelde kullanılmış burda categoride model ve marka açmaya gerek yok varolanı kullanacağız
                    }
                    else
                    {
                        //marka kullanılmış ancak model yok burada yeni bir model için kategori oluşturulacak marka aynı
                    }

                }
                else
                {
                    //marka için kategori oluşturulacak model içinde oluşturulmalı
                    Marka[Marka.Length + 1] = item.MarkaNo;
                    Model[Marka.Length + 1] = item.ModelNo;
                    var categoryMarka = new Category
                    {
                        Active = true,
                        RecordCreatorId = 99,
                        RecordDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        LastUpdaterId = 99,
                        CategoryType = (byte)CategoryType.Brand,
                        CategoryOrder = 0,
                        CategoryName = item.MarkaAdi,
                        CategoryParentId = 147274,
                        MainCategoryType = (byte)MainCategoryType.Ana_Kategori,
                        CategoryContentTitle = item.MarkaAdi
                    };


                    entities.Categories.AddObject(categoryMarka);
                    int markaid = categoryMarka.CategoryId;
                    MarkaCategoryId[Marka.Length] = markaid;
                    var categoryModel = new Category
                    {
                        Active = true,
                        RecordCreatorId = 99,
                        RecordDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        LastUpdaterId = 99,
                        CategoryType = (byte)CategoryType.Model,
                        CategoryOrder = 0,
                        CategoryName = item.MarkaAdi,
                        CategoryParentId = markaid,
                        MainCategoryType = (byte)MainCategoryType.Ana_Kategori,
                        CategoryContentTitle = item.MarkaAdi
                    };
                    entities.Categories.AddObject(categoryModel);
                    ModelCategoryId[Model.Length] = categoryModel.CategoryId;
                    entities.SaveChanges();


                }


            }

            return View();
        }

        public ActionResult Index(string page)
        {


            //var date = DateTime.Today;
            //var datetimeYesterday = DateTime.Today.AddDays(-1);
            //var dateTimeTomorrow = DateTime.Today.AddDays(1);
            //var productsNearDpoing = (from p in entities.Products where p.ProductDopingEndDate < dateTimeTomorrow && p.ProductDopingEndDate > datetimeYesterday && p.ProductDopingEndDate != null select new {p.ProductId }).ToList();



            PAGEID = PermissionPage.Ilanlar;
            int Page = 1;
            if (!string.IsNullOrEmpty(page)) Page = Convert.ToInt32(page);
            try
            {
                Session["ContentWidth"] = "1500px";
                if (Request.QueryString["ActiveType"].ToInt32() == 3)
                {
                    PAGEDIMENSION = 100;
                    Session[SessionPage] = PAGEDIMENSION;
                }
                if (Session[SessionPage] == null)
                {
                    Session[SessionPage] = PAGEDIMENSION;
                }

                int total = 0;
                dataProduct = new Data.Product();

                string whereClause = string.Empty;

                if (Request.QueryString["StoreMainPartyId"] != null)
                {
                    int storeMainPartyId = Request.QueryString["StoreMainPartyId"].ToInt32();
                    var memberMainPartyIds = entities.MemberStores.Where(c => c.StoreMainPartyId == storeMainPartyId).Select(x => x.MemberMainPartyId);

                    whereClause = "Where P.MainPartyId in ( " + string.Join(",", memberMainPartyIds) + " ) ";

                    if (Request.QueryString["ActiveType"] != null)
                    {
                        whereClause = whereClause + " And ProductActiveType = " + Request.QueryString["ActiveType"].ToInt32();

                    }
                }
                else
                {
                    if (Request.QueryString["ActiveType"] != null)
                    {
                        whereClause = " Where ProductActiveType = " + Request.QueryString["ActiveType"].ToInt32();
                        if (Request.QueryString["ActiveType"].ToInt32() == 0)
                        {
                            whereClause = whereClause + " And ProductActive <>0 "; //İncelenen ilanlara pasif ilanlar gelmeyecek
                        }
                    }
                    else if (Request.QueryString["Active"] != null)
                    {
                        whereClause = " Where ProductActive = " + Request.QueryString["Active"].ToInt32();
                    }
                    else
                    {
                        whereClause = " where ProductActiveType !=" + (byte)ProductActiveType.CopKutusuYeni;
                    }

                }



                collection = dataProduct.Search(ref total, (int)Session[SessionPage], Page, whereClause, STARTCOLUMN, ORDER).AsCollection<ProductModel>();
                var model = new FilterModel<ProductModel>
                {
                    CurrentPage = Page,
                    TotalRecord = total,
                    Order = ORDER,
                    OrderName = STARTCOLUMN,
                    Source = collection
                };

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(ProductModel model, string OrderName, string Order, int? Page, int PageDimension)
        {
            if (string.IsNullOrEmpty(OrderName))
            {
                OrderName = "ProductId";
            }
            try
            {
                dataProduct = dataProduct ?? new Data.Product();

                var whereClause = new StringBuilder("Where");

                string likeClaue = " {0} LIKE N'%{1}%' ";
                string equalClause = " {0} = {1} ";
                bool op = false;


                if (!string.IsNullOrWhiteSpace(model.ProductNo) && model.ProductNo != "#")
                {
                    whereClause.AppendFormat(likeClaue, "ProductNo", model.ProductNo);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.ProductName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "ProductName", model.ProductName);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.FirstCategoryName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CMain.CategoryName", model.FirstCategoryName);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.NameBrand))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CBrand.CategoryName", model.NameBrand);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.NameModel))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CModel.CategoryName", model.NameModel);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.NameSeries))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CSeries.CategoryName", model.NameSeries);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.OtherBrand))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "OtherBrand", model.OtherBrand);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.OtherModel))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "OtherModel", model.OtherModel);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.UserName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "MainPartyFullName", model.UserName);
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

                if (model.ProductActiveType.HasValue)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(equalClause, "ProductActiveType", model.ProductActiveType);
                    op = true;
                }

                if (model.StoreMainPartyId > 0)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    var member = entities.MemberStores.FirstOrDefault(c => c.StoreMainPartyId == model.StoreMainPartyId);

                    whereClause.AppendFormat(equalClause, "P.MainPartyId", member.MemberMainPartyId);
                    op = true;
                }


                if (model.ProductPrice > 0M)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(equalClause, "ProductPrice", model.ProductPrice);
                    op = true;
                }

                if (model.ProductRecordDate != new DateTime())
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    string dateEqual = "Cast(ProductRecordDate as date) = Cast('{0}' as date) ";
                    whereClause.AppendFormat(dateEqual, model.ProductRecordDate.ToString("yyyMMdd"));
                }

                if (model.ProductLastViewDate != new DateTime())
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    string dateEqual = "Cast(ProductLastViewDate as date) = Cast('{0}' as date) ";
                    whereClause.AppendFormat(dateEqual, model.ProductLastViewDate.ToString("yyyMMdd"));
                }



                if (whereClause.ToString() == "Where")
                {
                    whereClause.Clear();
                }
                int total = 0;

                Session[SessionPage] = PageDimension;
                collection =
                  dataProduct.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<ProductModel>();

                var filterItems = new FilterModel<ProductModel>
                {
                    CurrentPage = Page ?? 1,
                    TotalRecord = total,
                    Order = Order,
                    OrderName = OrderName,
                    Source = collection,
                    PageDimension = PageDimension
                };

                return View("ProductList", filterItems);
            }
            catch (Exception error)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult ExportProducts(ProductModel model, string OrderName, string Order, int? Page, int? PageDimension)
        {
            ICollection<ProductModel> data = new ProductModel[] { };
            List<MTProductExcelItem> list = new List<MTProductExcelItem>();
            dataProduct = dataProduct ?? new Data.Product();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'%{1}%' ";
            string equalClause = " {0} = {1} ";
            bool op = false;


            if (!string.IsNullOrWhiteSpace(model.ProductNo) && model.ProductNo != "#")
            {
                whereClause.AppendFormat(likeClaue, "ProductNo", model.ProductNo);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "ProductName", model.ProductName);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.FirstCategoryName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "CMain.CategoryName", model.FirstCategoryName);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.NameBrand))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "CBrand.CategoryName", model.NameBrand);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.NameModel))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "CModel.CategoryName", model.NameModel);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.NameSeries))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "CSeries.CategoryName", model.NameSeries);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.OtherBrand))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "OtherBrand", model.OtherBrand);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.OtherModel))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "OtherModel", model.OtherModel);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "MainPartyFullName", model.UserName);
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

            if (model.ProductActiveType.HasValue)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "ProductActiveType", model.ProductActiveType);
                op = true;
            }

            if (model.StoreMainPartyId > 0)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                var member = entities.MemberStores.FirstOrDefault(c => c.StoreMainPartyId == model.StoreMainPartyId);

                whereClause.AppendFormat(equalClause, "P.MainPartyId", member.MemberMainPartyId);
                op = true;
            }


            if (model.ProductPrice > 0M)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "ProductPrice", model.ProductPrice);
                op = true;
            }

            if (model.ProductRecordDate != new DateTime())
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = "Cast(ProductRecordDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.ProductRecordDate.ToString("yyyMMdd"));
            }

            if (model.ProductLastViewDate != new DateTime())
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = "Cast(ProductLastViewDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.ProductLastViewDate.ToString("yyyMMdd"));
            }



            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }
            int total = 0;

            Session[SessionPage] = PageDimension;
            collection =
              dataProduct.Search(ref total, PageDimension.Value, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<ProductModel>();

            foreach (var item in collection)
            {
                MTProductExcelItem product = new MTProductExcelItem();
                product.ProductDescription = item.ProductDescription;
                product.UrunAdi = item.ProductName;
                product.UrunNo = item.ProductNo;
                product.UrunId = item.ProductId;
                product.Keywords = !string.IsNullOrEmpty(item.Keywords) ? item.Keywords : "";
                if (item.MenseiId.HasValue)
                {
                    var constantMensei = _constantService.GetConstantByConstantId((short)item.MenseiId.Value);
                    if (constantMensei != null)
                    {
                        product.Mensei = constantMensei.ConstantName;
                    }
                    else
                        product.Mensei = "";
                }
                else
                    product.Mensei = "";

                int briefId;
                if (int.TryParse(product.ProductBriefDetail, out briefId))
                {
                    briefId = Convert.ToInt32(item.BriefDetail);
                    var brief = _constantService.GetConstantByConstantId((short)briefId);
                    product.ProductBriefDetail = brief != null ? brief.ConstantName : "";
                }
                else
                {
                    product.ProductBriefDetail = "";
                }
                product.WarrantyPeriod = !string.IsNullOrEmpty(item.WarrantyPeriod) ? item.WarrantyPeriod + " Yıl" : "";
                product.ProductPrice = item.ProductPrice.HasValue ? Convert.ToDouble(item.ProductPrice.Value.ToString("F", CultureInfo.InvariantCulture)) : 0;
                string kdv = "";
                if (item.Kdv.HasValue)
                    kdv = item.Kdv.Value == true ? "Kdv Dahil" : "Kdv Haric";

                product.Kdv = kdv;
                list.Add(product);
            }
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("sheet1");

            var items = list.Select(
                o => new
                {
                    o.UrunId,
                    o.UrunNo,
                    o.UrunAdi,
                    ProductDescription = !string.IsNullOrEmpty(o.ProductDescription) ? o.ProductDescription : "",
                    o.ProductBriefDetail,
                    o.WarrantyPeriod,
                    o.ProductPrice,
                    o.Mensei,
                    o.Kdv,
                    o.Keywords
                }).ToList();

            var properties = new[] { "UrunId", "UrunNo", "UrunAdi", "ProductDescription", "ProductBriefDetail", "WarrantyPeriod", "ProductPrice", "Mensei", "Kdv", "Keywords" };
            var headers = new[] { "UrunId", "UrunNo", "UrunAdi", "ProductDescription", "ProductBriefDetail", "WarrantyPeriod", "ProductPrice", "Mensei", "Kdv", "Keywords" };

            var headerRow = sheet.CreateRow(0);

            // create header
            for (int i = 0; i < properties.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
            }

            // fill content
            for (int i = 0; i < items.Count; i++)
            {
                var rowIndex = i + 1;
                var row = sheet.CreateRow(rowIndex);
                try
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        var cell = row.CreateCell(j);
                        var o = items[i];
                        cell.SetCellValue(o.GetType().GetProperty(properties[j]).GetValue(o, null).ToString());
                    }
                }
                catch (Exception ex)
                {
                    continue;

                }

            }

            using (var stream = new MemoryStream())
            {
                workbook.Write(stream);
                stream.Close();
                return File(stream.ToArray(), "application/vnd.ms-excel", "urun_listesi.xls");
            }
        }


        [HttpPost]
        public ActionResult DeleteVideo(int ProductId, int VideoId)
        {
            var video = entities.Videos.SingleOrDefault(c => c.VideoId == VideoId);

            FileHelpers.Delete(AppSettings.VideoFolder + video.VideoPath + ".mp4");
            FileHelpers.Delete(AppSettings.VideoThumbnailFolder + video.VideoPicturePath);

            entities.Videos.DeleteObject(video);
            entities.SaveChanges();


            var product = _productService.GetProductByProductId(ProductId);
            if (product != null)
            {
                var videos = _videoService.GetVideosByProductId(ProductId);
                if (videos.Count > 0)
                {
                    if (!product.HasVideo)
                    {
                        product.HasVideo = true;
                        _productService.UpdateProduct(product);
                    }
                }
                else
                {
                    if (product.HasVideo)
                    {
                        product.HasVideo = false;
                        _productService.UpdateProduct(product);
                    }
                }
            }


            try
            {
                entities.CheckProductSearch(ProductId);
            }
            catch
            {

            }


            var videoModel = entities.Videos.Where(c => c.ProductId == ProductId).ToList();
            var videoProductModel = entities.Products.Where(c => c.ProductId == ProductId).SingleOrDefault();
            //HasVideo Control
            if (videoModel.Count > 0)
            {
                if (videoProductModel.HasVideo != true)
                    videoProductModel.HasVideo = true;
                entities.SaveChanges();
            }
            else
            {
                if (videoProductModel.HasVideo != false)
                    videoProductModel.HasVideo = false;
                entities.SaveChanges();
            }
            try
            {
                entities.CheckProductSearch(ProductId);
            }
            catch
            {

            }

            return View("VideoList", videoModel);
        }

        public ActionResult DoVideoShowcase(int ProductId, int VideoId)
        {
            var video = entities.Videos.SingleOrDefault(c => c.VideoId == VideoId);
            if (video != null)
            {
                video.ShowOnShowcase = true;
                entities.SaveChanges();
            }

            var videoModel = entities.Videos.Where(c => c.ProductId == ProductId).ToList();
            return View("VideoList", videoModel);
        }

        public ActionResult Tooltip(int productId)
        {


            var model = collection.SingleOrDefault(m => m.ProductId == productId);
            return View("TooltipContent", model);
        }


        public ActionResult UpdateProductByExcelFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProductByExcelFile(HttpPostedFileBase file)
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
                            string value = sheet.GetRow(row).GetCell(0).StringCellValue; //Here for sample , I just save the value in "value" field, Here you can write your custom logics...
                            int productId = Convert.ToInt32(sheet.GetRow(row).GetCell(0).StringCellValue);
                            string productName = sheet.GetRow(row).GetCell(2).StringCellValue;
                            string productDescription = sheet.GetRow(row).GetCell(3).StringCellValue;
                            string productBriefDetail = sheet.GetRow(row).GetCell(4).StringCellValue;
                            string warriantPeriodText = sheet.GetRow(row).GetCell(5).StringCellValue;

                            decimal productPrice = 0;
                            string keywords = sheet.GetRow(row).GetCell(9).StringCellValue;
                            var cell = sheet.GetRow(row).GetCell(6);
                            IFormulaEvaluator evaluator = hssfwb.GetCreationHelper().CreateFormulaEvaluator();
                            if (cell != null)
                            {
                                switch (cell.CellType)
                                {
                                    case CellType.Numeric:
                                        productPrice = Convert.ToDecimal(cell.NumericCellValue);
                                        break;
                                    case CellType.String:
                                        productPrice = Convert.ToDecimal(cell.StringCellValue);
                                        break;
                                }
                            }
                            string mensei = sheet.GetRow(row).GetCell(7).StringCellValue;

                            int warriantPeriod = 0;
                            if (!string.IsNullOrEmpty(warriantPeriodText))
                            {
                                warriantPeriod = Convert.ToInt32(warriantPeriodText.Split(' ')[0]);
                            }
                            var constantProductBrief = entities.Constants.FirstOrDefault(x => x.ConstantName == productBriefDetail);

                            var constantMensei = entities.Constants.FirstOrDefault(x => x.ConstantName == mensei);

                            var product = _productService.GetProductByProductId(productId);
                            if (product != null)
                            {
                                product.ProductName = productName;
                                product.ProductDescription = productDescription;
                                product.MenseiId = constantMensei != null ? constantMensei.ConstantId : product.MenseiId;
                                product.WarrantyPeriod = warriantPeriod != 0 ? warriantPeriod.ToString() : product.WarrantyPeriod;
                                product.ProductPrice = productPrice != 0 ? Convert.ToDecimal(productPrice) : product.ProductPrice;
                                if (!string.IsNullOrEmpty(keywords))
                                    product.Keywords = keywords;

                                if (product.ProductPrice.HasValue && product.ProductPrice.Value > 0 && product.ProductPriceType != (byte)ProductPriceType.Price)
                                {
                                    product.ProductPriceType = (byte)ProductPriceType.Price;
                                }
                                product.BriefDetail = constantProductBrief != null ? constantProductBrief.ConstantId.ToString() : "";
                                _productService.UpdateProduct(product);
                                updated = true;
                            }

                        }
                    }
                }
            }
            ViewBag.Updated = updated;
            return View();
        }
        public IList<PictureModel> PictureItems
        {
            get
            {
                if (Session["PictureItems"] == null)
                {
                    Session["PictureItems"] = new List<PictureModel>();
                }
                return Session["PictureItems"] as List<PictureModel>;
            }
            set { Session["PictureItems"] = value; }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var product = new Classes.Product();
            var curProduct = entities.Products.SingleOrDefault(c => c.ProductId == id);


            if (curProduct != null && curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi)
            {
                ProductCountCalc(curProduct, false);
            }
            curProduct.ProductActiveType = (byte)ProductActiveType.CopKutusuYeni;
            curProduct.ProductActive = false;
            curProduct.ProductLastUpdate = DateTime.Now;
            entities.SaveChanges();
            return Json(new { m = true });
        }

        [HttpPost]
        public JsonResult ProductColumn(string id, string productidforupdate, string columncategory)
        {
            if (columncategory == "product")
            {
                if (!string.IsNullOrEmpty(id) || !string.IsNullOrEmpty(productidforupdate))
                {
                    int productid = Int32.Parse(id);
                    var product = entities.Products.Where(c => c.ProductId == productid).SingleOrDefault();
                    product.ProductName = productidforupdate;


                }

            }
            else if (columncategory == "brand" || columncategory == "serie" || columncategory == "model")
            {
                if (!string.IsNullOrEmpty(id) || !string.IsNullOrEmpty(productidforupdate))
                {
                    int categoryid = Int32.Parse(id);
                    var categoryforupdate = entities.Categories.Where(c => c.CategoryId == categoryid).SingleOrDefault();
                    categoryforupdate.CategoryName = productidforupdate;
                    entities.SaveChanges();

                    if (categoryforupdate.CategoryType == (byte)CategoryType.Brand)
                    {
                        var products = entities.Products.Where(x => x.BrandId == categoryid);
                        foreach (var item2 in products)
                        {
                            try
                            {
                                entities.CheckProductSearch(item2.ProductId);
                            }
                            catch
                            {

                            }
                        }
                    }
                    else if (categoryforupdate.CategoryType == (byte)CategoryType.Series)
                    {
                        var products = entities.Products.Where(x => x.SeriesId == categoryid);
                        foreach (var item2 in products)
                        {
                            try
                            {
                                entities.CheckProductSearch(item2.ProductId);
                            }
                            catch
                            {

                            }
                        }
                    }
                    else if (categoryforupdate.CategoryType == (byte)CategoryType.Model)
                    {
                        var products = entities.Products.Where(x => x.ModelId == categoryid);
                        foreach (var item2 in products)
                        {
                            try
                            {
                                entities.CheckProductSearch(item2.ProductId);
                            }
                            catch
                            {

                            }
                        }
                    }

                }

            }
            else if (columncategory == "modelNon")
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(productidforupdate))
                {
                    int productId = Int32.Parse(id);

                    var product = entities.Products.FirstOrDefault(x => x.ProductId == productId);

                    var curCategoryc = new Category
                    {
                        Active = true,
                        CategoryName = productidforupdate,
                        CategoryParentId = product.SeriesId != null ? product.SeriesId.Value : product.BrandId.Value,
                        CategoryOrder = 1,
                        CategoryType = (byte)CategoryType.Model,
                        RecordDate = DateTime.Now,
                        RecordCreatorId = 99,
                        LastUpdateDate = DateTime.Now,
                        LastUpdaterId = 99,
                        ProductCount = 0,
                        MainCategoryType = 1,
                        Title = "",
                        Keywords = "",
                        Description = "",
                        CategoryContentTitle = productidforupdate
                    };
                    entities.Categories.AddObject(curCategoryc);
                    entities.SaveChanges();
                    var topCategories = _categoryService.GetSPTopCategories(curCategoryc.CategoryId);
                    var topCategoriesNames = topCategories.Select(x => x.CategoryContentTitle);

                    curCategoryc.CategoryPath = String.Join(" - ", topCategoriesNames);
                    curCategoryc.CategoryPathUrl = GetCategoryPathUrl(curCategoryc);
                    entities.SaveChanges();

                    int categoryModelId = curCategoryc.CategoryId;
                    string updatableCategory = "";
                    if (product.ModelId != null)
                    {
                        updatableCategory += product.ModelId + ".";
                    }
                    updatableCategory += categoryModelId + "." + product.BrandId + ".";
                    product.OtherModel = String.Empty;
                    product.ModelId = categoryModelId;
                    product.CategoryTreeName = product.CategoryTreeName + categoryModelId + ".";
                    product.ProductLastUpdate = DateTime.Now;
                    entities.SaveChanges();


                }
            }

            else if (columncategory == "brandNon")
            {
                Data.Category dataCategory = new Data.Category();

                string updatableCategory = "";
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(productidforupdate))
                {
                    int productId = Convert.ToInt32(id);
                    var curProduct = entities.Products.FirstOrDefault(x => x.ProductId == productId);

                    var category = new Category
                    {
                        Active = true,
                        CategoryName = productidforupdate,
                        CategoryParentId = curProduct.CategoryId,
                        CategoryOrder = 1,
                        CategoryType = (byte)CategoryType.Brand,
                        RecordDate = DateTime.Now,
                        RecordCreatorId = 99,
                        LastUpdateDate = DateTime.Now,
                        LastUpdaterId = 99,
                        ProductCount = 0,
                        MainCategoryType = 1,
                        Title = "",
                        Keywords = "",
                        Description = "",
                        CategoryContentTitle = productidforupdate
                    };
                    entities.Categories.AddObject(category);

                    entities.SaveChanges();
                    category.CategoryPathUrl = GetCategoryPathUrl(category);
                    var topCategories = _categoryService.GetSPTopCategories(category.CategoryId);
                    var topCategoriesNames = topCategories.Select(x => x.CategoryContentTitle).ToList();

                    category.CategoryPath = String.Join(" - ", topCategoriesNames);


                    var model = entities.Categories.FirstOrDefault(x => x.CategoryId == curProduct.ModelId);
                    if (model != null)
                    {
                        model.CategoryParentId = category.CategoryId;
                        entities.SaveChanges();
                    }

                    int categoryId = category.CategoryId;
                    if (curProduct.ModelId != null)
                    {
                        updatableCategory = curProduct.ModelId + ".";
                    }
                    updatableCategory += categoryId + ".";
                    curProduct.OtherBrand = String.Empty;
                    curProduct.BrandId = categoryId;
                    curProduct.CategoryTreeName = curProduct.CategoryTreeName + categoryId + ".";


                    entities.SaveChanges();
                    if (model != null)
                    {
                        var products = entities.Products.Where(x => x.ModelId == model.CategoryId);
                        string updatableCategoryNew = "";
                        foreach (var item in products)
                        {
                            if (item.ModelId != null)
                            {
                                updatableCategoryNew = curProduct.ModelId + ".";
                            }
                            updatableCategoryNew += categoryId + ".";
                            item.OtherBrand = String.Empty;
                            item.BrandId = categoryId;
                            item.CategoryTreeName = item.CategoryTreeName + categoryId + ".";


                        }
                        entities.SaveChanges();
                        foreach (var item2 in products)
                        {
                            try
                            {
                                entities.CheckProductSearch(item2.ProductId);
                            }
                            catch
                            {

                            }
                        }

                        dataCategory.UpdateProductCountOnCategorys(updatableCategoryNew);

                    }
                }

            }
            entities.SaveChanges();
            return Json(true);

        }

        public ActionResult Edit(int id, string check)
        {
            PAGEID = PermissionPage.IlanDuzenle;

            var product = new Classes.Product();
            if (check == "true")
            {
                ViewBag.check = "true";
            }
            if (product.LoadEntity(id))
            {
                var categories = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)CategoryPlaceType.HomeChoicesed, true);

                var productvalues = entities.Products.Where(c => c.ProductId == id).SingleOrDefault();
                var model = new ProductModel();
                UpdateClass(product, model);

                model.OtherBrand = product.OtherBrand;
                model.OtherModel = product.OtherModel;
                model.MenseiId = productvalues.MenseiId;
                model.OrderStatus = productvalues.OrderStatus;
                model.WarrantyPeriod = productvalues.WarrantyPeriod;
                model.UnitType = productvalues.UnitType;
                model.Doping = productvalues.Doping;
                model.Keywords = product.Keywords;
                model.ProductSellUrl = productvalues.ProductSellUrl;
                model.MinumumOrderAmount = productvalues.MinumumAmount;

                var dataPicture = new Data.Picture();
                model.ProductPictureItems = dataPicture.GetItemsByProductId(id).AsCollection<PictureModel>();
                model.VideoItems = entities.Videos.Where(c => c.ProductId == id).ToList();
                model.MoneyCondition = productvalues.MoneyCondition == null ? false : productvalues.MoneyCondition == true ? true : false;
                var curCountry = new Classes.Country();
                model.CountryItems = new SelectList(curCountry.GetDataTable().DefaultView, "CountryId", "CountryName", 0);
                model.HasVideo = product.HasVideo;
                model.Kdv = product.Kdv;
                model.Fob = product.Fob;

                var dataAddress = new Data.Address();

                if (product.CountryId == null)
                {
                    product.CountryId = AppSettings.Turkiye;
                }

                var cityItems = dataAddress.CityGetItemByCountryId(product.CountryId.Value).AsCollection<CityModel>().ToList();
                cityItems.Insert(0, new CityModel { CityId = 0, CityName = "< Lütfen Seçiniz >" });

                var productHomePage = _productHomePageService.GetProductHomePageByProductId(product.ProductId);

                if (productHomePage != null)
                {
                    model.IsProductHomePage = productHomePage.Active != null ? true : false;
                    if (productHomePage.BeginDate.HasValue)
                        model.ProductHomeBeginDate = productHomePage.BeginDate.Value;
                    if (productHomePage.EndDate.HasValue)
                        model.ProductHomeEndDate = productHomePage.EndDate.Value;
                }
                List<LocalityModel> localityItems = new List<LocalityModel>() { new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
                if (product.CityId != null)
                {
                    localityItems = dataAddress.LocalityGetItemByCityId(product.CityId.Value).AsCollection<LocalityModel>().ToList();
                }

                List<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };
                if (product.LocalityId.HasValue)
                {
                    townItems = entities.Towns.Where(c => c.LocalityId == product.LocalityId).ToList();
                    //townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });

                    //townItems = dataAddress.TownGetItemByDistrictId(product.LocalityId.Value).AsCollection<TownModel>().ToList();
                    //townItems = entities.Towns.Where(c => c.LocalityId == product.LocalityId.Value).ToList();
                }
                if (product.MainPartyId != null)
                {
                    model.StoreMainPartyId = Convert.ToInt32(product.MainPartyId);
                }
                model.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
                model.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
                model.TownItems = new SelectList(townItems, "TownId", "TownName");
                model.ProductPriceTypes = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProductPriceType);
                model.ChoicedForCategoryIndex = product.ChoicedForCategoryIndex;

                Session["ProductStatu"] = product.ProductActiveType;

                model.IsAdvanceEdit = false;
                var categorySectors = _categoryService.GetMainCategories();
                model.CategorySectors.Add(new SelectListItem { Text = "Seçiniz", Selected = true, Value = "0" });
                foreach (var item in categorySectors)
                {
                    model.CategorySectors.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryId.ToString() });
                }

                model.AllowProductSellUrl = CheckPermissionProductSellUrl(model);


                return View(model);
            }
            return RedirectToAction("Index");
        }
        public bool CheckPermissionProductSellUrl(ProductModel product)
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.StoreMainPartyId);
            if (memberStore != null)
            {
                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                return store.IsAllowProductSellUrl.HasValue ? store.IsAllowProductSellUrl.Value : false;
            }
            return false;
        }

        public void CreateImageForStoreLogo(string format)
        {
            var store = entities.Stores.ToList();
            string resizeStoreFolder = this.Server.MapPath(AppSettings.ResizeStoreLogoFolder);
            string storeLogoThumbSize = "120x80";

            List<string> thumbSizesForStoreLogo = new List<string>();
            thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));


            foreach (var storeitem in store)
            {
                string resizeStoreLogoImageFilePath = resizeStoreFolder + storeitem.MainPartyId.ToString() + "\\";

                if (Directory.Exists(resizeStoreLogoImageFilePath + "thumbs"))
                {
                    string storeLogoImageFileName = storeitem.StoreName.ToImageFileName() + "_logo.jpg";
                    string storeLogoImageFileSavePath = resizeStoreLogoImageFilePath + storeLogoImageFileName;

                    bool thumbResult = ImageProcessHelper.ImageResize(storeLogoImageFileSavePath,
           resizeStoreLogoImageFilePath + "thumbs\\" + storeitem.StoreName.ToImageFileName(), thumbSizesForStoreLogo);


                }



            }





        }

        public void CreateImageForPopulerProduct(string format)
        {
            if (string.IsNullOrEmpty(format) && format.IndexOf('x') == -1)
            {
                format = "160x120";

            }


            List<string> thumbSizes1 = new List<string>();
            thumbSizes1.Add(format);

            NeoSistem.Trinnk.Management.Helper.ProductImageHelpers productImageHelpersq = new NeoSistem.Trinnk.Management.Helper.ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes1);

            TrinnkEntities entitiesa = new TrinnkEntities();

            var productList = entitiesa.Products.Where(c => c.MoneyCondition == true).ToList();

            foreach (var item in productList)
            {

                List<PictureModel> pictureModela = productImageHelpersq.SaveProductImageEdit2(item);


            }

        }

        public void CreateImageForAllProduct(string format)
        {
            if (string.IsNullOrEmpty(format) && format.IndexOf('x') == -1)
            {
                format = "160x120";

            }


            List<string> thumbSizes1 = new List<string>();
            thumbSizes1.Add(format);

            NeoSistem.Trinnk.Management.Helper.ProductImageHelpers productImageHelpersq = new NeoSistem.Trinnk.Management.Helper.ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes1);

            TrinnkEntities entitiesa = new TrinnkEntities();

            var productList = entitiesa.Products.ToList();

            foreach (var item in productList)
            {

                List<PictureModel> pictureModela = productImageHelpersq.SaveProductImageEdit2(item);


            }

        }

        public ActionResult CreateHomePageImage(string format)
        {
            CreateImageForAllProduct(format);



            return View();
        }

        [HttpPost]
        public ActionResult CreateHomePageImage(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult CreateStoreLogo(string format)
        {
            CreateImageForStoreLogo(format);



            return View();
        }

        [HttpPost]
        public ActionResult CreateStoreLogo(int id)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ProductModel model, FormCollection coll, string productPriceType)
        {
            byte? productActiveType = null;
            int? mainPartyId = null;
            var curProduct = entities.Products.SingleOrDefault(c => c.ProductId == id);
            //inputTagName == "NewProductPicture"

            if (model.IsNewProduct)
            {
                var pictures = entities.Pictures.Where(x => x.ProductId == id);
                foreach (var item in pictures)
                {
                    string PictureName = item.PicturePath;
                    entities.Pictures.DeleteObject(item);
                    List<string> thumbSizes = new List<string>();
                    thumbSizes.AddRange(AppSettings.ProductThumbSizes.Replace("*", "").Split(';'));
                    FileHelpers.Delete(AppSettings.ProductImageFolder + id + "/" + PictureName);
                    foreach (string thumb in thumbSizes)
                    {

                        string imagetype = PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf("."));//örnek .jpeg
                        string imagename = PictureName.Remove(PictureName.Length - PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf(".")).Length);
                        FileHelpers.Delete(AppSettings.ProductImageFolder + id + "/" + "thumbs/" + imagename + "-" + thumb + imagetype);
                    }
                }
                entities.SaveChanges();

                var videos = entities.Videos.Where(x => x.ProductId == id);
                foreach (var item in videos)
                {
                    FileHelpers.Delete(AppSettings.VideoFolder + item.VideoPath + ".mp4");
                    FileHelpers.Delete(AppSettings.VideoThumbnailFolder + item.VideoPicturePath);
                    entities.Videos.DeleteObject(item);
                    entities.SaveChanges();
                }
                var product = _productService.GetProductByProductId(id);



                var productComplains = _productComplainService.GetAllProductComplain().Where(x => x.ProductId == id);
                foreach (var item in productComplains)
                {
                    _productComplainService.DeleteProductComplain(item);
                }
                var favorites = entities.FavoriteProducts.Where(x => x.ProductId == id);
                foreach (var item in favorites)
                {
                    entities.FavoriteProducts.DeleteObject(item);
                }
                entities.SaveChanges();


            }

            #region EditImageUpload
            //foreach (string inputTagName in Request.Files)
            //{

            if (Request.Files.AllKeys.Contains("NewProductPicture") && Request.Files["NewProductPicture"].ContentLength > 0)
            {
                List<string> thumbSizes = new List<string>();
                thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

                NeoSistem.Trinnk.Management.Helper.ProductImageHelpers productImageHelpers = new NeoSistem.Trinnk.Management.Helper.ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes);
                List<PictureModel> pictureModel = productImageHelpers.SaveProductImageEdit(Request, id, model.ProductName);

                TrinnkEntities entities = new TrinnkEntities();
                int counter = 1;
                foreach (PictureModel item in pictureModel)
                {


                    var modelpicture = new Picture()
                    {
                        PictureName = "",
                        PicturePath = item.PicturePath,
                        ProductId = id,
                        PictureOrder = counter

                    };

                    entities.Pictures.AddObject(modelpicture);
                    counter++;
                    entities.SaveChanges();
                }
            }
            if (Request.Files.AllKeys.Contains("NewProductPicture") && Request.Files["NewProductVideo"].ContentLength > 0)
            {
                HttpPostedFileBase file = Request.Files["NewProductVideo"];
                VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                DateTime timesplit;


                if (DateTime.TryParse(vModel.Duration, out timesplit))
                {

                }
                else
                {
                    timesplit = DateTime.Now.Date;
                }


                var video = new Video
                {
                    Active = true,
                    VideoPath = vModel.newFileName,
                    VideoSize = null,
                    VideoPicturePath = vModel.newFileName + ".jpg",
                    VideoTitle = model.VideoTitle,
                    VideoRecordDate = DateTime.Now,
                    SingularViewCount = 0,
                    ProductId = id,
                    VideoMinute = (byte?)timesplit.Minute,
                    VideoSecond = (byte?)timesplit.Second

                };
                entities.Videos.AddObject(video);

                curProduct.HasVideo = true;
            }
            //}
            #endregion

            #region EditProductInfo

            if (model.CountryId.HasValue)
                curProduct.CountryId = model.CountryId.Value;

            if (model.CityId != 0)
                curProduct.CityId = model.CityId.Value;

            if (model.LocalityId != 0)
                curProduct.LocalityId = model.LocalityId.Value;

            if (model.TownId != 0)
                curProduct.TownId = model.TownId.Value;

            if (model.CurrencyId > 0)
                curProduct.CurrencyId = model.CurrencyId;
            else
                curProduct.CurrencyId = null;
            curProduct.MoneyCondition = model.MoneyCondition;

            if (model.MoneyCondition)
            {

                try
                {
                    CreateImageForPopulerProduct("180x135");
                }
                catch (Exception)
                {


                }

            }

            curProduct.ProductName = model.ProductName;
            curProduct.ProductLastUpdate = DateTime.Now;
            curProduct.ProductType = model.ProductType;
            curProduct.MenseiId = model.MenseiId;
            curProduct.OrderStatus = model.OrderStatus;
            curProduct.WarrantyPeriod = model.WarrantyPeriod;
            curProduct.UnitType = model.UnitType;
            curProduct.ProductPriceType = Convert.ToByte(productPriceType);
            curProduct.Kdv = false;
            curProduct.Fob = false;
            curProduct.Keywords = model.Keywords;
            curProduct.HasVideo = _videoService.GetVideosByProductId(id).Any();
            curProduct.MinumumAmount = model.MinumumOrderAmount;

            if (curProduct.ProductPriceType == (byte)ProductPriceType.Price)
            {

                if (coll["pricePropertie"].ToString() == "kdvdahil")
                {
                    curProduct.Kdv = true;
                }
                else if (coll["pricePropertie"].ToString() == "fob")
                {
                    curProduct.Fob = true;
                }

                curProduct.DiscountType = model.DiscountType;
                if (curProduct.DiscountType != 0)
                {
                    curProduct.DiscountAmount = model.DiscountAmount;
                    curProduct.ProductPriceWithDiscount = Convert.ToDecimal(coll["TotalPrice"]);
                    curProduct.ProductPriceForOrder = Convert.ToDecimal(coll["TotalPrice"]);
                }
                else
                {
                    curProduct.DiscountAmount = 0;
                    curProduct.ProductPriceWithDiscount = 0;
                    curProduct.ProductPriceForOrder = model.ProductPrice;
                }
                curProduct.ProductPrice = model.ProductPrice;
                curProduct.ProductPrice = model.ProductPrice;
                curProduct.ProductPriceBegin = 0;
                curProduct.ProductPriceLast = 0;
            }
            else if (curProduct.ProductPriceType == (byte)ProductPriceType.PriceRange)
            {
                if (coll["pricePropertie"].ToString() == "kdvdahil")
                    curProduct.Kdv = true;
                else if (coll["pricePropertie"].ToString() == "fob")
                    curProduct.Fob = true;

                curProduct.ProductPriceBegin = model.ProductPriceBegin;
                curProduct.ProductPriceLast = model.ProductPriceLast;
                curProduct.ProductPriceForOrder = model.ProductPriceBegin;


            }
            else
            {
                curProduct.ProductPrice = 0;
                curProduct.ProductPriceBegin = 0;
                curProduct.ProductPriceLast = 0;
                curProduct.ProductPriceForOrder = 99999999999;
            }

            curProduct.ProductSellUrl = model.ProductSellUrl;



            //if (model.Doping && !curProduct.Doping)
            //{
            //    curProduct.productrate += 1000;
            //}
            if (!model.Doping && curProduct.Doping)
            {

                curProduct.ProductRateWithDoping -= 1000;
                curProduct.ProductDopingBeginDate = null;
                curProduct.ProductDopingEndDate = null;
            }



            if (model.Doping)
            {
                curProduct.ProductDopingBeginDate = DateTime.Now;

                if (model.ProductDopingBeginDate != null)
                    curProduct.ProductDopingBeginDate = model.ProductDopingBeginDate;
                if (model.ProductDopingEndDate != null)
                    curProduct.ProductDopingEndDate = model.ProductDopingEndDate;
            }
            if (model.Doping && !curProduct.Doping)
            {
                curProduct.ProductRateWithDoping += 1000;
                try
                {
                    SendMailForProductDoping(curProduct);
                }
                catch (Exception ex)
                {

                }

            }
            curProduct.Doping = model.Doping;
            string ProductSalesType = String.Empty;
            if (coll["ProductSalesType"] != null)
            {
                string[] acProductSalesType = coll["ProductSalesType"].Split(',');
                if (acProductSalesType != null)
                {
                    for (int i = 0; i < acProductSalesType.Length; i++)
                    {
                        if (acProductSalesType.GetValue(i).ToString() != "false")
                        {
                            if (string.IsNullOrEmpty(ProductSalesType))
                                ProductSalesType = acProductSalesType.GetValue(i).ToString();
                            else
                                ProductSalesType = ProductSalesType + "," + acProductSalesType.GetValue(i).ToString();
                        }
                    }
                }
            }

            curProduct.ProductSalesType = ProductSalesType;
            curProduct.ProductStatu = model.ProductStatu;

            string BriefDetail = String.Empty;
            if (coll["BriefDetail"] != null)
            {
                string[] acBriefDetail = coll["BriefDetail"].Split(',');
                if (acBriefDetail != null)
                {
                    for (int i = 0; i < acBriefDetail.Length; i++)
                    {
                        if (acBriefDetail.GetValue(i).ToString() != "false")
                        {
                            if (string.IsNullOrEmpty(BriefDetail))
                                BriefDetail = acBriefDetail.GetValue(i).ToString();
                            else
                                BriefDetail = BriefDetail + "," + acBriefDetail.GetValue(i).ToString();
                        }
                    }
                }
            }
            curProduct.BriefDetail = BriefDetail;
            curProduct.ProductDescription = model.ProductDescription;
            curProduct.ProductShowcase = model.ProductShowcase;
            if (!IsDate(model.ProductAdvertBeginDate))
                curProduct.ProductAdvertBeginDate = null;
            else
                curProduct.ProductAdvertBeginDate = model.ProductAdvertBeginDate;

            if (!IsDate(model.ProductAdvertEndDate))
                curProduct.ProductAdvertEndDate = null;
            else
                curProduct.ProductAdvertEndDate = model.ProductAdvertEndDate;

            if (!(!string.IsNullOrWhiteSpace(curProduct.OtherBrand) || !string.IsNullOrWhiteSpace(curProduct.OtherModel)))
            {
                curProduct.ProductActiveType = model.ProductActiveType;
                productActiveType = model.ProductActiveType;
                curProduct.ProductActive = model.ProductActive;
            }


            curProduct.OtherBrand = model.OtherBrand;
            curProduct.OtherModel = model.OtherModel;
            curProduct.ModelYear = model.ModelYear;
            if (model.ChoicedForCategoryIndex != null)
                curProduct.ChoicedForCategoryIndex = model.ChoicedForCategoryIndex;
            mainPartyId = curProduct.MainPartyId.Value;


            if (model.IsAdvanceEdit)
            {
                if (model.ModelId == 0) model.ModelId = null;
                if (model.SeriesId == 0) model.ModelId = null;
                if (model.BrandId == 0) model.BrandId = null;
                curProduct.ModelId = model.ModelId;
                curProduct.CategoryId = model.CategoryId;
                curProduct.BrandId = model.BrandId;
                curProduct.SeriesId = model.SeriesId;
                //other brand and model adding to categories
                if (!string.IsNullOrEmpty(model.OtherBrand))
                {
                    var categoryBrand = new Category();
                    categoryBrand.Active = true;
                    categoryBrand.CategoryName = model.OtherBrand;
                    categoryBrand.CategoryParentId = model.CategoryId;
                    categoryBrand.CategoryType = (byte)CategoryType.Brand;
                    categoryBrand.LastUpdateDate = DateTime.Now;
                    categoryBrand.ProductCount = 1;
                    categoryBrand.Title = "";
                    categoryBrand.Description = "";
                    categoryBrand.Keywords = "";
                    categoryBrand.RecordDate = DateTime.Now;
                    categoryBrand.RecordCreatorId = 99;
                    categoryBrand.CategoryOrder = 0;
                    categoryBrand.MainCategoryType = (byte)MainCategoryType.Ana_Kategori;
                    categoryBrand.CategoryContentTitle = model.OtherBrand;


                    entities.Categories.AddObject(categoryBrand);
                    entities.SaveChanges();
                    categoryBrand.CategoryPathUrl = GetCategoryPathUrl(categoryBrand);
                    var topCategories1 = _categoryService.GetSPTopCategories(categoryBrand.CategoryId);
                    var topCategoriesNames = topCategories1.Select(x => x.CategoryContentTitle).ToList();

                    categoryBrand.CategoryPath = String.Join(" - ", topCategoriesNames);
                    entities.SaveChanges();

                    curProduct.BrandId = categoryBrand.CategoryId;
                    curProduct.OtherBrand = "";

                }
                if (!string.IsNullOrEmpty(model.OtherModel))
                {
                    var categoryModel = new Category();
                    categoryModel.Active = true;
                    categoryModel.CategoryName = model.OtherModel;
                    categoryModel.CategoryParentId = curProduct.BrandId;
                    categoryModel.CategoryType = (byte)CategoryType.Model;
                    categoryModel.LastUpdateDate = DateTime.Now;
                    categoryModel.ProductCount = 1;
                    categoryModel.Title = "";
                    categoryModel.Description = "";
                    categoryModel.Keywords = "";
                    categoryModel.RecordDate = DateTime.Now;
                    categoryModel.CategoryOrder = 0;
                    categoryModel.RecordCreatorId = 99;
                    categoryModel.MainCategoryType = (byte)MainCategoryType.Ana_Kategori;
                    categoryModel.CategoryContentTitle = model.OtherBrand;
                    entities.Categories.AddObject(categoryModel);
                    entities.SaveChanges();

                    categoryModel.CategoryPathUrl = GetCategoryPathUrl(categoryModel);
                    var topCategories1 = _categoryService.GetSPTopCategories(categoryModel.CategoryId);
                    var topCategoriesNames = topCategories1.Select(x => x.CategoryContentTitle).ToList();

                    categoryModel.CategoryPath = String.Join(" - ", topCategoriesNames);
                    entities.SaveChanges();

                    curProduct.ModelId = categoryModel.CategoryId;
                    curProduct.OtherModel = "";

                }
                var topCategories = new List<global::Trinnk.Entities.StoredProcedures.Catalog.TopCategoryResult>();
                if (curProduct.SeriesId.HasValue)
                    topCategories = _categoryService.GetSPTopCategories(Convert.ToInt32(curProduct.SeriesId)).ToList();
                else if (curProduct.ModelId.HasValue)
                    topCategories = _categoryService.GetSPTopCategories(Convert.ToInt32(curProduct.ModelId)).ToList();
                else if (curProduct.BrandId.HasValue)
                    topCategories = _categoryService.GetSPTopCategories(Convert.ToInt32(curProduct.BrandId)).ToList();
                else if (curProduct.BrandId.HasValue)
                    topCategories = _categoryService.GetSPTopCategories(Convert.ToInt32(curProduct.CategoryId)).ToList();
                string categoryTreeName = "";
                foreach (var item in topCategories)
                {
                    categoryTreeName = categoryTreeName + item.CategoryId.ToString() + ".";
                }
                curProduct.CategoryTreeName = categoryTreeName;
            }
            if (model.IsNewProduct)
            {
                curProduct.ViewCount = 0;
                curProduct.SingularViewCount = 0;
                curProduct.ProductRecordDate = DateTime.Now;
                curProduct.LastViewDate = DateTime.Now;
            }
            if (model.IsAdvanceEdit || model.IsNewProduct)
            {
                curProduct.ProductActiveType = (byte)ProductActiveType.Inceleniyor;
            }

            curProduct.ProductLastUpdate = DateTime.Now;
            entities.SaveChanges();
            #endregion

            #region EditFirmConfirm
            if (model.IsAdvanceEdit != true)
            {
                if (Session["ProductStatu"].ToByte() == 0)
                {
                    string mailDesc = "";
                    string ilanonay = "";
                    if (productActiveType == (byte)ProductActiveType.Onaylandi)
                    {
                        mailDesc = "Ürün bilgileriniz kriterlerimize uygun olup ilanınız onaylanmıştır.";
                        ilanonay = "ilanınız onaylanmıştır";
                    }
                    else if (productActiveType == (byte)ProductActiveType.Onaylanmadi)
                    {
                        mailDesc = "Ürün bilgileriniz kriterlerimize uygun olmadığından dolayı ilanınız onaylanmamıştır";
                        ilanonay = "ilanınız onaylanmamıştır";
                    }
                    else if (productActiveType == (byte)ProductActiveType.Silindi)
                    {
                        mailDesc = "Ürün bilgileriniz kriterlerimize uygun olmadığından dolayı ilanınız dondurulmuştur.";
                        ilanonay = "ilanınız silinmiştir";
                    }
                    else if (productActiveType == (byte)ProductActiveType.Inceleniyor)
                    {
                        mailDesc = "İlaniniz pasif duruma getirilmiştir.";
                        ilanonay = "ilanınız inceleniyor";
                    }

                    var member = entities.Members.SingleOrDefault(c => c.MainPartyId == mainPartyId);

                    #region mail
                    try
                    {
                        #region kullaniciicin

                        var curCategory = new NeoSistem.Trinnk.Classes.Category();
                        curCategory.LoadEntity(curProduct.CategoryId);
                        string productUrl = Helpers.ProductUrl(curProduct.ProductId, curProduct.ProductName);

                        var settings = ConfigurationManager.AppSettings;
                        MailMessage mail = new MailMessage();
                        MessagesMT mailT = entities.MessagesMTs.First(x => x.MessagesMTName == "ilanekle");
                        mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = "Trinnk.com Bilgilendirme-" + ilanonay;                                              //Mail konusu
                        string template = mailT.MessagesMTPropertie;
                        template = template.Replace("#AdSoyad#", member.MemberName + " " + member.MemberSurname).Replace("#Urunlinki#", productUrl);
                        mail.Body = template;                                                            //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        this.SendMail(mail);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        TempData["Mail"] = ex.InnerException;
                    }
                }

            }
            else
            {

            }

            #endregion
            #endregion
            #region producthomepage

            var productHomePage = _productHomePageService.GetProductHomePageByProductId(id);
            if (productHomePage == null && model.IsProductHomePage)
            {
                productHomePage = new global::Trinnk.Entities.Tables.Catalog.ProductHomePage();
                var topCategory = _categoryService.GetSPTopCategories(curProduct.CategoryId.Value).FirstOrDefault();
                var productHomePages = _productHomePageService.GetProductHomePagesByCategoryId(topCategory.CategoryId);
                int productHomePageLimit = 12;
                if (productHomePages.Count >= productHomePageLimit)
                {
                    TempData["ProductHomePageMessage"] = "Bu ürün kategorisinde " + productHomePageLimit + " adet ürün bulunduğu için daha fazla ekeleme yapamazsınız";
                }
                else
                {
                    productHomePage.CategoryId = topCategory.CategoryId;
                    productHomePage.ProductId = curProduct.ProductId;
                    productHomePage.RecordDate = DateTime.Now;
                    productHomePage.BeginDate = model.ProductHomeBeginDate.Value;
                    productHomePage.EndDate = model.ProductHomeEndDate.Value;
                    productHomePage.Type = (byte)HomePageProductType.Free;
                    TimeSpan value = DateTime.Now.Subtract(model.ProductHomeBeginDate.Value);
                    if (value.Days <= 0)
                        productHomePage.Active = true;
                    _productHomePageService.InsertProductHomePage(productHomePage);
                }
            }
            else if (productHomePage != null && !model.IsProductHomePage)
            {
                _productHomePageService.DeleteProductHomePage(productHomePage);
            }
            else if (productHomePage != null && model.IsProductHomePage)
            {
                productHomePage.BeginDate = model.ProductHomeBeginDate.Value;
                productHomePage.EndDate = model.ProductHomeEndDate.Value;
                TimeSpan value = DateTime.Now.Subtract(model.ProductHomeBeginDate.Value);

                if (value.Days <= 0)
                    productHomePage.Active = true;
                _productHomePageService.UpdateProductHomePage(productHomePage);
            }
            #endregion
            if (curProduct.ProductActiveType != null)
            {
                if (Session["ProductStatu"].ToByte() != curProduct.ProductActiveType.Value.ToByte())
                {
                    if (productActiveType == (byte)ProductActiveType.Onaylandi)
                    {
                        ProductCountCalc(curProduct, true);
                    }
                    else if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi && productActiveType == (byte)ProductActiveType.Onaylanmadi)
                    {
                        if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi)
                        {
                            ProductCountCalc(curProduct, false);
                        }
                    }
                    else if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi && productActiveType == (byte)ProductActiveType.Silindi)
                    {
                        if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi)
                        {
                            ProductCountCalc(curProduct, false);
                        }
                    }
                    else if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi && productActiveType == (byte)ProductActiveType.Inceleniyor)
                    {
                        if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi)
                        {
                            ProductCountCalc(curProduct, false);
                        }
                    }
                    else if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi && productActiveType == (byte)ProductActiveType.CopKutusuYeni)
                    {
                        if (Session["ProductStatu"].ToByte() == (byte)ProductActiveType.Onaylandi)
                        {
                            ProductCountCalc(curProduct, false);
                        }
                    }
                }
            }
            try
            {
                entities.CheckProductSearch(id);
            }
            catch
            {

            }

            Session.Contents.Remove("ProductStatu");
            return RedirectToAction("Edit", new { id = id, check = "true" });

        }
        public void SendMailForProductDoping(Product curProduct)
        {

            string productUrl = Helpers.ProductUrl(curProduct.ProductId, curProduct.ProductName);
            string dopingBeginDate = curProduct.ProductAdvertBeginDate.ToDateTime().ToString("dd.MM.yyyy");
            string endDate = curProduct.ProductDopingEndDate.ToDateTime().ToString("dd.MM.yyyy");
            string dopingDates = string.Format("{0}-{1}", dopingBeginDate, endDate);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(curProduct.MainPartyId.Value);
            var member = _memberService.GetMemberByMainPartyId(curProduct.MainPartyId.Value);
            string memberemail = member.MemberEmail;
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            string storename = store.StoreName;
            var settings = ConfigurationManager.AppSettings;
            MailMessage mail = new MailMessage();
            MessagesMT mailT = entities.MessagesMTs.First(x => x.MessagesMTName == "dopingverildi");


            mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(memberemail);                                                              //Mailin kime gideceğini belirtiyoruz
            mail.Subject = mailT.MessagesMTTitle;                                            //Mail konusu
            string template = mailT.MessagesMTPropertie;
            template = template.Replace("#firmaadi#", storename).Replace("#urunlink#", productUrl).Replace("#tarih#", dopingDates).Replace("#urunadi#", curProduct.ProductName).Replace("#ilanno#", curProduct.ProductNo);
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            this.SendMail(mail);

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

        public static bool IsDate(Object obj)
        {
            if (obj != null)
            {
                string strDate = obj.ToString();
                try
                {
                    DateTime dt = DateTime.Parse(strDate);
                    if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                        return true;
                    return false;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public string LimitText(string text, short limit)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Length > limit)
                {
                    return text.Substring(0, limit);
                }
                return text;
            }
            return "";
        }

        [HttpPost]
        public ActionResult SendForStore(string CheckItem, byte type, int StoreId)
        {
            var store = entities.Stores.Where(c => c.MainPartyId == StoreId).SingleOrDefault();
            int MainPartyId = entities.MemberStores.Where(c => c.StoreMainPartyId == StoreId).SingleOrDefault().MemberMainPartyId.ToInt32();
            string[] producIdItems = CheckItem.Split(',');

            if (producIdItems != null && store != null)
            {
                using (var trans = new TransactionScope())
                {
                    try
                    {
                        for (int i = 0; i < producIdItems.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(producIdItems.GetValue(i).ToString()))
                            {
                                int productId = producIdItems.GetValue(i).ToInt32();
                                var curProduct = entities.Products.SingleOrDefault(c => c.ProductId == productId);
                                if (type != curProduct.ProductActiveType.Value.ToByte())
                                {
                                    //çöp ilanlar taşınıyorsa rate ve view countları sıfırla
                                    if (curProduct.ProductActiveType == (byte)ProductActiveType.CopKutusuYeni)
                                    {
                                        curProduct.productrate = 0;
                                        curProduct.SingularViewCount = 0;
                                        curProduct.ViewCount = 0;
                                    }
                                    if (type == 4)
                                    {
                                        curProduct.MainPartyId = MainPartyId;
                                    }

                                }
                            }
                        }

                        entities.SaveChanges();

                    }
                    catch (Exception)
                    {
                    }
                    for (int i = 0; i < producIdItems.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(producIdItems.GetValue(i).ToString()))
                        {
                            int productid = producIdItems.GetValue(i).ToInt32();
                            try
                            {
                                entities.CheckProductSearch(productid);
                            }
                            catch
                            { }
                        }
                    }
                    trans.Complete();
                }
            }

            int total = 0;

            string whereClause = string.Empty;
            if (ViewData["ProductStatu"].ToInt32() > 0)
            {
                whereClause = "Where ProductStatu = " + ViewData["ProductStatu"].ToInt32();
            }

            collection =
               collection = dataProduct.Search(ref total, PAGEDIMENSION, 1, whereClause, STARTCOLUMN, ORDER).AsCollection<ProductModel>();

            var filterItems = new FilterModel<ProductModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return Redirect("/Product/Index");
        }

        [HttpPost]
        public ActionResult ConfirmMember(string CheckItem, byte type)
        {
            string[] producIdItems = CheckItem.Split(',');
            string categoryforcount = "";
            Data.Category dataCategory = new Data.Category();
            if (producIdItems != null)
            {
                //using (var trans = new TransactionScope())
                //{
                try
                {
                    for (int i = 0; i < producIdItems.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(producIdItems.GetValue(i).ToString()))
                        {
                            int productId = producIdItems.GetValue(i).ToInt32();
                            var curProduct = entities.Products.SingleOrDefault(c => c.ProductId == productId);
                            curProduct.ProductLastUpdate = DateTime.Now;
                            if (curProduct.ModelId != null)
                            {
                                categoryforcount = curProduct.ModelId + ".";
                            }
                            if (curProduct.BrandId != null)
                            {
                                categoryforcount += curProduct.BrandId + ".";
                            }
                            if (curProduct.SeriesId != null)
                            {
                                categoryforcount += curProduct.SeriesId + ".";
                            }
                            if (curProduct.CategoryId != null)
                            {
                                categoryforcount += curProduct.CategoryId + ".";
                            }
                            if (type != curProduct.ProductActiveType.Value.ToByte())
                            {

                                if (type == (byte)ProductActiveType.Onaylandi)
                                {
                                    ProductCountCalc(curProduct, true);
                                }
                                else if (curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi && type == (byte)ProductActiveType.Onaylanmadi)
                                {
                                    if (curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi)
                                    {
                                        ProductCountCalc(curProduct, false);
                                    }
                                }
                                else if (curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi && type == (byte)ProductActiveType.Silindi)
                                {
                                    if (curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi)
                                    {
                                        ProductCountCalc(curProduct, false);
                                    }
                                }
                                else if (curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi && type == (byte)ProductActiveType.Inceleniyor)
                                {
                                    if (curProduct.ProductActiveType == (byte)ProductActiveType.Onaylandi)
                                    {
                                        ProductCountCalc(curProduct, false);
                                    }
                                }

                                if (type == 1)
                                {
                                    if (!(!string.IsNullOrWhiteSpace(curProduct.OtherBrand) || !string.IsNullOrWhiteSpace(curProduct.OtherModel)))
                                    {
                                        curProduct.ProductActiveType = (byte)ProductActiveType.Onaylandi;
                                        curProduct.ProductActive = true;

                                    }
                                }
                                else if (type == 2)
                                {
                                    curProduct.ProductActiveType = (byte)ProductActiveType.CopKutusuYeni;
                                    curProduct.MainPartyId = 59580;
                                    curProduct.ProductActive = false;
                                    ProductCountCalc(curProduct, false);

                                }
                                else if (type == 3)
                                {
                                    curProduct.ProductActiveType = (byte)ProductActiveType.Onaylanmadi;
                                    curProduct.ProductActive = false;
                                }
                                else if (type == 4)
                                {
                                    curProduct.MainPartyId = 53017;
                                    curProduct.ProductActiveType = (byte)ProductActiveType.Onaylanmadi;
                                    curProduct.ProductActive = false;
                                }
                                else
                                {
                                    curProduct.ProductActiveType = (byte)ProductActiveType.Silindi;
                                    curProduct.ProductActive = false;
                                }
                            }
                        }
                    }
                    entities.SaveChanges();


                }
                catch (Exception)
                {
                }
                try
                {
                    dataCategory.UpdateProductCountOnCategorys(categoryforcount);
                }
                catch (Exception)
                {
                }

                //trans.Complete();
                //}

                for (int i = 0; i < producIdItems.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(producIdItems.GetValue(i).ToString()))
                    {
                        int productid = producIdItems.GetValue(i).ToInt32();
                        try
                        {
                            entities.CheckProductSearch(productid);
                        }
                        catch
                        { }
                    }
                }

            }

            int total = 0;

            string whereClause = string.Empty;
            if (ViewData["ProductStatu"].ToInt32() > 0)
            {
                whereClause = "Where ProductStatu = " + ViewData["ProductStatu"].ToInt32();
            }

            collection =
               collection = dataProduct.Search(ref total, PAGEDIMENSION, 1, whereClause, STARTCOLUMN, ORDER).AsCollection<ProductModel>();

            var filterItems = new FilterModel<ProductModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return Redirect("/Product/Index");
        }

        [HttpPost]
        public JsonResult BrandInsert(int ProductId)
        {
            Data.Category dataCategory = new Data.Category();

            string updatableCategory = "";
            //var updateCountCategory=dataProduct
            var curProduct = entities.Products.SingleOrDefault(c => c.ProductId == ProductId);

            if (!string.IsNullOrWhiteSpace(curProduct.OtherBrand))
            {
                var category = new Category
                {
                    Active = true,
                    CategoryName = curProduct.OtherBrand,
                    CategoryParentId = curProduct.CategoryId,
                    CategoryOrder = 1,
                    CategoryType = (byte)CategoryType.Brand,
                    RecordDate = DateTime.Now,
                    RecordCreatorId = 99,
                    LastUpdateDate = DateTime.Now,
                    LastUpdaterId = 99,
                    ProductCount = 0,
                    MainCategoryType = 1,
                    Title = "",
                    Keywords = "",
                    Description = "",
                    CategoryContentTitle = curProduct.OtherBrand
                };
                entities.Categories.AddObject(category);

                entities.SaveChanges();
                category.CategoryPathUrl = GetCategoryPathUrl(category);
                var topCategories = _categoryService.GetSPTopCategories(category.CategoryId);
                var topCategoriesNames = topCategories.Select(x => x.CategoryContentTitle).ToList();

                category.CategoryPath = String.Join(" - ", topCategoriesNames);


                var model = entities.Categories.FirstOrDefault(x => x.CategoryId == curProduct.ModelId);
                if (model != null)
                {
                    model.CategoryParentId = category.CategoryId;
                    entities.SaveChanges();
                }

                int categoryId = category.CategoryId;
                if (curProduct.ModelId != null)
                {
                    updatableCategory = curProduct.ModelId + ".";
                }
                updatableCategory += categoryId + ".";
                curProduct.OtherBrand = String.Empty;
                curProduct.BrandId = categoryId;
                curProduct.CategoryTreeName = curProduct.CategoryTreeName + categoryId + ".";


                entities.SaveChanges();
                if (model != null)
                {
                    var products = entities.Products.Where(x => x.ModelId == model.CategoryId && x.ProductId != curProduct.ProductId);
                    string updatableCategoryNew = "";
                    foreach (var item in products)
                    {
                        if (item.ModelId != null)
                        {
                            updatableCategoryNew = curProduct.ModelId + ".";
                        }
                        updatableCategoryNew += categoryId + ".";
                        item.OtherBrand = String.Empty;
                        item.BrandId = categoryId;
                        item.CategoryTreeName = item.CategoryTreeName + categoryId + ".";


                    }
                    entities.SaveChanges();
                    foreach (var item2 in products)
                    {
                        try
                        {
                            entities.CheckProductSearch(item2.ProductId);
                        }
                        catch
                        {

                        }
                    }

                    dataCategory.UpdateProductCountOnCategorys(updatableCategoryNew);
                }

            }
            dataCategory.UpdateProductCountOnCategorys(updatableCategory);
            try
            {
                entities.CheckProductSearch(ProductId);
            }
            catch
            {

            }
            return Json(true);
        }

        [HttpPost]
        public ActionResult SeriesInsert(int ProductId)
        {
            Data.Category dataCategory = new Data.Category();
            string updatableCategory = "";
            using (var trans = new TransactionScope())
            {
                var curProduct = entities.Products.SingleOrDefault(c => c.ProductId == ProductId);

                if (!string.IsNullOrEmpty(curProduct.OtherBrand))
                {
                    var curCategory = new Category
                    {
                        Active = true,
                        CategoryName = curProduct.OtherBrand,
                        CategoryParentId = curProduct.CategoryId,
                        CategoryOrder = 1,
                        CategoryType = (byte)CategoryType.Brand,
                        RecordDate = DateTime.Now,
                        RecordCreatorId = 99,
                        LastUpdateDate = DateTime.Now,
                        LastUpdaterId = 99,
                        ProductCount = 0,
                        MainCategoryType = 1,
                        Title = "",
                        Keywords = "",
                        Description = "",
                        CategoryContentTitle = curProduct.OtherBrand
                    };
                    entities.Categories.AddObject(curCategory);
                    entities.SaveChanges();
                    curCategory.CategoryPathUrl = GetCategoryPathUrl(curCategory);
                    var topCategories1 = _categoryService.GetSPTopCategories(curCategory.CategoryId);
                    var topCategoriesNames = topCategories1.Select(x => x.CategoryContentTitle).ToList();

                    curCategory.CategoryPath = String.Join(" - ", topCategoriesNames);
                    entities.SaveChanges();

                    int categoryBrandId = curCategory.CategoryId;
                    if (curProduct.ModelId != null)
                    {
                        updatableCategory = curProduct.ModelId + ".";
                    }
                    updatableCategory += categoryBrandId + ".";
                    curProduct.OtherBrand = String.Empty;
                    curProduct.BrandId = categoryBrandId;
                    curProduct.CategoryTreeName = curProduct.CategoryTreeName + categoryBrandId + ".";

                    entities.SaveChanges();
                }

                trans.Complete();
            }
            dataCategory.UpdateProductCountOnCategorys(updatableCategory);
            try
            {
                entities.CheckProductSearch(ProductId);
            }
            catch
            {

            }
            return Json(true);
        }

        [HttpPost]
        public ActionResult ModelInsert(int ProductId)
        {
            Data.Category dataCategory = new Data.Category();
            string updatableCategory = "";
            using (var trans = new TransactionScope())
            {
                var product = entities.Products.SingleOrDefault(c => c.ProductId == ProductId);

                if (!string.IsNullOrWhiteSpace(product.OtherBrand))
                {
                    var curCategory = new Category
                    {
                        Active = true,
                        CategoryName = product.OtherBrand,
                        CategoryParentId = product.CategoryId,
                        CategoryOrder = 1,
                        CategoryType = (byte)CategoryType.Brand,
                        RecordDate = DateTime.Now,
                        RecordCreatorId = 99,
                        LastUpdateDate = DateTime.Now,
                        LastUpdaterId = 99,
                        ProductCount = 0,
                        MainCategoryType = 1,
                        Title = "",
                        Keywords = "",
                        Description = "",
                        CategoryContentTitle = product.OtherBrand,

                    };
                    entities.Categories.AddObject(curCategory);
                    entities.SaveChanges();
                    var topCategories = _categoryService.GetSPTopCategories(curCategory.CategoryId);

                    var topCategoriesNames = topCategories.Select(x => x.CategoryContentTitle).ToList();
                    curCategory.CategoryPath = String.Join(" - ", topCategoriesNames);
                    curCategory.CategoryPathUrl = GetCategoryPathUrl(curCategory);

                    int categoryBrandId = curCategory.CategoryId;
                    if (product.BrandId != null)
                    {
                        updatableCategory = product.BrandId + ".";
                    }
                    updatableCategory += categoryBrandId + ".";

                    product.OtherBrand = String.Empty;
                    product.BrandId = categoryBrandId;
                    product.CategoryTreeName = product.CategoryTreeName + categoryBrandId + ".";

                    entities.SaveChanges();
                }

                if (product.BrandId.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(product.OtherModel))
                    {
                        var curCategoryc = new Category
                        {
                            Active = true,
                            CategoryName = product.OtherModel,
                            CategoryParentId = product.SeriesId != null ? product.SeriesId.Value : product.BrandId.Value,
                            CategoryOrder = 1,
                            CategoryType = (byte)CategoryType.Model,
                            RecordDate = DateTime.Now,
                            RecordCreatorId = 99,
                            LastUpdateDate = DateTime.Now,
                            LastUpdaterId = 99,
                            ProductCount = 0,
                            MainCategoryType = 1,
                            Title = "",
                            Keywords = "",
                            Description = "",
                            CategoryContentTitle = product.OtherModel
                        };
                        entities.Categories.AddObject(curCategoryc);
                        entities.SaveChanges();
                        var topCategories = _categoryService.GetSPTopCategories(curCategoryc.CategoryId);
                        var topCategoriesNames = topCategories.Select(x => x.CategoryContentTitle);

                        curCategoryc.CategoryPath = String.Join(" - ", topCategoriesNames);
                        curCategoryc.CategoryPathUrl = GetCategoryPathUrl(curCategoryc);
                        entities.SaveChanges();

                        int categoryModelId = curCategoryc.CategoryId;
                        if (product.ModelId != null)
                        {
                            updatableCategory += product.ModelId + ".";
                        }
                        updatableCategory += categoryModelId + "." + product.BrandId + ".";
                        product.OtherModel = String.Empty;
                        product.ModelId = categoryModelId;
                        product.CategoryTreeName = product.CategoryTreeName + categoryModelId + ".";
                        product.ProductLastUpdate = DateTime.Now;
                        entities.SaveChanges();
                    }
                }
                trans.Complete();
            }
            dataCategory.UpdateProductCountOnCategorys(updatableCategory);
            try
            {
                entities.CheckProductSearch(ProductId);
            }
            catch
            {

            }
            return Json(true);
        }

        private string GetCategoryPathUrl(Trinnk.Management.Models.Entities.Category category)
        {
            var categoryUrlName = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
            string categoryUrl = "";
            if (category.CategoryType == (byte)CategoryType.Sector)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.ProductGroup)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.Category)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.Brand)
            {
                var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetCategoryUrl(categoryParent.CategoryId, categoryUrlName, category.CategoryId, category.CategoryName);
            }
            else if (category.CategoryType == (byte)CategoryType.Model)
            {
                var categoryBrand = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryBrand.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetModelUrl(category.CategoryId, category.CategoryName, categoryBrand.CategoryName, categoryUrlName, categoryParent.CategoryId);
            }
            else if (category.CategoryType == (byte)CategoryType.Series)
            {
                var categoryModel = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                var categoryBrand = _categoryService.GetCategoryByCategoryId(categoryModel.CategoryParentId.Value);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryBrand.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetSerieUrl(category.CategoryId, category.CategoryName, categoryBrand.CategoryName, categoryUrlName);
            }
            return categoryUrl;
        }
        [HttpPost]
        public ActionResult ProductReadyForSale(int ProductId)
        {
            using (var trans = new TransactionScope())
            {
                var product = entities.Products.SingleOrDefault(c => c.ProductId == ProductId);

                if (product.ReadyforSale == null)
                {
                    product.ReadyforSale = true;
                    entities.SaveChanges();

                }
                else if (product.ReadyforSale == true)
                {
                    product.ReadyforSale = false;
                    entities.SaveChanges();
                }
                else if (product.ReadyforSale == false)
                {
                    product.ReadyforSale = true;
                    entities.SaveChanges();
                }
                trans.Complete();
            }

            return Json(true);
        }

        [HttpPost]
        public ActionResult DeletePicture(int ProductId, int PictureId, string PictureName)
        {

            var picture = entities.Pictures.SingleOrDefault(c => c.PictureId == PictureId);
            entities.Pictures.DeleteObject(picture);
            entities.SaveChanges();

            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Replace("*", "").Split(';'));
            FileHelpers.Delete(AppSettings.ProductImageFolder + ProductId + "/" + PictureName);
            foreach (string thumb in thumbSizes)
            {

                string imagetype = PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf("."));//örnek .jpeg
                string imagename = PictureName.Remove(PictureName.Length - PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf(".")).Length);

                FileHelpers.Delete(AppSettings.ProductImageFolder + ProductId + "/" + "thumbs/" + imagename + "-" + thumb + imagetype);
            }


            var dataPicture = new Data.Picture();
            var pictureModel = dataPicture.GetItemsByProductId(ProductId).AsCollection<PictureModel>();
            return View("PictureList", pictureModel);
        }

        [HttpPost]
        public ActionResult mainImage(string idArray, int ProductId, int PictureId, string PictureName)
        {
            var dataPicture = new Data.Picture();
            var pictureModel = dataPicture.GetItemsByProductId(ProductId).AsCollection<PictureModel>();
            List<PictureModel> newpictures = new List<PictureModel>();

            foreach (var row in pictureModel)
            {
                if (row.PicturePath == PictureName)
                {
                    newpictures.Add(row);

                    foreach (var ro in pictureModel)
                    {
                        if (ro.PicturePath != PictureName)
                        {
                            newpictures.Add(ro);
                        }
                    }
                }
            }

            int count = 1;
            foreach (var v in newpictures)
            {
                entities.ExecuteStoreCommand("UPDATE Picture SET PictureOrder=" + count + " WHERE PictureId=" + v.PictureId);
                ++count;
            }


            return View("PictureList", newpictures);
        }

        [HttpPost]
        public JsonResult UpdatePictureOrder(string idArray)
        {
            int count = 1;
            string[] arr = idArray.Split(',');
            foreach (var id in arr)
            {
                entities.ExecuteStoreCommand("UPDATE Picture SET PictureOrder=" + count + " WHERE PictureId=" + id);
                ++count;
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult Cities(int id)
        {
            var dataAddress = new Data.Address();
            var cityItems = dataAddress.CityGetItemByCountryId(id).AsCollection<CityModel>().ToList();
            cityItems.Insert(0, new CityModel { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            var items = cityItems.Select(delegate (CityModel c)
            {
                return new { Value = c.CityId, Text = c.CityName };
            });
            return Json(items);
        }

        [HttpPost]
        public JsonResult Localities(int id)
        {
            var dataAddress = new Data.Address();

            var localityItems = dataAddress.LocalityGetItemByCityId(id).AsCollection<LocalityModel>().OrderBy(c => c.LocalityName).ToList();
            localityItems.Insert(0, new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
            var items = localityItems.Select(delegate (LocalityModel c)
            {
                return new { Value = c.LocalityId, Text = c.LocalityName };
            });
            return Json(items);
        }

        [HttpPost]
        public JsonResult Towns(int id)
        {
            var dataAddress = new Data.Address();
            var townItems = dataAddress.TownGetItemByDistrictId(id).AsCollection<TownModel>().OrderBy(c => c.TownName).ToList();
            townItems.Insert(0, new TownModel { TownId = 0, TownName = "< Lütfen Seçiniz >" });

            var items = townItems.Select(delegate (TownModel c)
            {
                return new { Value = c.TownId, Text = c.TownName };
            });

            return Json(items);
        }

        public ActionResult EditShowcase()
        {

            var productShowcaseItems = entities.Products.Where(c => c.ProductShowcase == true).ToList();

            return View(productShowcaseItems);
        }

        [HttpPost]
        public ActionResult EditShowcase(int ProductId)
        {
            var product = entities.Products.SingleOrDefault(c => c.ProductId == ProductId);

            product.ProductShowcase = false;
            entities.SaveChanges();

            return RedirectToAction("EditShowcase");
        }

        //public string ProductNameChange()
        //{
        //    var productDistinct = entities.Products.GroupBy(g => g.ProductName).Where(w => w.Count() > 1)
        // .Select(s =>
        //     new
        //     {
        //         s.FirstOrDefault().ProductName
        //     }).Distinct().ToList();

        //    foreach (var item in productDistinct)
        //    {
        //        var products = entities.Products.Where(x=>x.ProductName==item.ProductName);
        //        string tempProductName="";
        //        int tempSayi = 0;
        //        foreach (var item1 in products.ToList())
        //        {
        //            var modelName = (from p in entities.Products join m in entities.Categories on p.ModelId equals m.CategoryId where p.ProductId == item1.ProductId select new { m.CategoryName }).FirstOrDefault();
        //            string modelname = "";
        //            if(modelName!=null)
        //             modelname = modelName.CategoryName;
        //            var brandname = (from p in entities.Products join m in entities.Categories on p.BrandId equals m.CategoryId where p.ProductId == item1.ProductId select new { m.CategoryName }).FirstOrDefault();
        //             string brandnme="";
        //            if(brandname!=null)
        //             brandnme = brandname.CategoryName;
        //            //var productUpdate = entities.Products.FirstOrDefault(x=>x.ProductId==item1.ProductId);
        //            //var productUpdates = _productService.GetProductByProductId(item1.ProductId);
        //            if(brandnme!="")
        //            {
        //                if (!item1.ProductName.Contains(brandnme))
        //                {
        //                    item1.ProductName = item1.ProductName + " " + brandnme + " " + modelname;
        //                    if (tempProductName == item1.ProductName)
        //                    {
        //                        tempSayi++;
        //                        item1.ProductName += " " + tempSayi;
        //                    }
        //                    tempProductName = item1.ProductName;
        //                    entities.SaveChanges();
        //                }
        //            }
        //        }
        //    }
        //    return "";
        //}
        public ActionResult DopingShowcase()
        {
            ProductDopingModel model = new ProductDopingModel();
            var products = entities.Products.Where(p => p.Doping == true).ToList();
            model.Stores.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });
            foreach (var product in products)
            {
                string storename = "";
                string brandName = string.Empty, modelName = string.Empty, seriesName = string.Empty;
                if (product.BrandId.HasValue)
                {
                    Category productBrand = entities.Categories.FirstOrDefault(c => c.CategoryId == product.BrandId);
                    brandName = productBrand.CategoryName;
                }
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
                if (memberStore != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);


                    if (store != null)
                    {
                        storename = store.StoreShortName;
                        if (model.Stores.Where(x => x.Text == store.StoreShortName).ToList().Count == 0)
                            model.Stores.Add(new SelectListItem { Text = store.StoreShortName, Value = store.MainPartyId.ToString() });
                    }
                }
                //if (product.ModelId.HasValue)
                //{
                //    Category productModel = entities.Categories.FirstOrDefault(c => c.CategoryId == product.ModelId);
                //    modelName = productModel.CategoryName;
                //}
                //if (product.SeriesId.HasValue)
                //{
                //    Category productSeries = entities.Categories.FirstOrDefault(c => c.CategoryId == product.SeriesId);
                //    seriesName = productSeries.CategoryName;
                //}
                var categories = _categoryService.GetBreadCrumbCategoriesByCategoryId(product.CategoryId.Value);
                string categoryBreadCrumb = string.Empty;
                foreach (var category in categories)
                {
                    categoryBreadCrumb = categoryBreadCrumb + category.CategoryName + ">";
                }
                if (categoryBreadCrumb.Length > 0)
                {
                    categoryBreadCrumb = categoryBreadCrumb.Remove(categoryBreadCrumb.Length - 1, 1);
                }


                model.DopingModels.Add(new ProductDopingListModel
                {
                    ProductId = product.ProductId,
                    ProductNo = product.ProductNo,
                    BrandName = brandName,
                    ProductName = product.ProductName,
                    CategoryBreadCrumb = categoryBreadCrumb,
                    CategoryId = product.CategoryId.Value,
                    ProductDopingBeginDate = product.ProductDopingBeginDate,
                    ProductDopingEndDate = product.ProductDopingEndDate,
                    StoreShortName = storename

                });
            }

            var breadCrumbCategories = model.DopingModels.Select(p => new { p.CategoryId, p.CategoryBreadCrumb }).Distinct().ToList();
            model.AvairableCategories.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });
            foreach (var category in breadCrumbCategories)
            {
                model.AvairableCategories.Add(new SelectListItem { Text = category.CategoryBreadCrumb, Value = category.CategoryId.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DopingShowcase(int categoryId, int StoreMainPartyId)
        {
            ProductDopingModel model = new ProductDopingModel();
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(StoreMainPartyId);
            string storename = "";
            if (StoreMainPartyId != 0)
            {
                var store = _storeService.GetStoreByMainPartyId(StoreMainPartyId);
                if (store != null)
                {
                    storename = store.StoreShortName;
                }
            }
            var products = new List<Product>();
            if (categoryId != 0)
                products = entities.Products.Where(p => p.Doping == true && p.CategoryId == categoryId).OrderByDescending(x => x.ProductAdvertEndDate).ToList();
            else products = entities.Products.Where(x => x.Doping == true).OrderByDescending(x => x.ProductAdvertEndDate).ToList();
            if (StoreMainPartyId != 0)
                products = products.Where(x => x.MainPartyId == memberStore.MemberMainPartyId.Value).OrderByDescending(x => x.ProductAdvertEndDate).ToList();

            foreach (var product in products)
            {

                string brandName = string.Empty, modelName = string.Empty, seriesName = string.Empty;
                if (product.BrandId.HasValue)
                {
                    Category productBrand = entities.Categories.FirstOrDefault(c => c.CategoryId == product.BrandId);
                    brandName = productBrand.CategoryName;
                }
                //if (product.ModelId.HasValue)
                //{
                //    Category productModel = entities.Categories.FirstOrDefault(c => c.CategoryId == product.ModelId);
                //    modelName = productModel.CategoryName;
                //}
                //if (product.SeriesId.HasValue)
                //{
                //    Category productSeries = entities.Categories.FirstOrDefault(c => c.CategoryId == product.SeriesId);
                //    seriesName = productSeries.CategoryName;
                //}

                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                if (store != null)
                    storename = store.StoreShortName;


                var categories = _categoryService.GetBreadCrumbCategoriesByCategoryId(product.CategoryId.Value);
                string categoryBreadCrumb = string.Empty;
                foreach (var category in categories)
                {
                    categoryBreadCrumb = categoryBreadCrumb + category.CategoryName + " > ";
                }
                categoryBreadCrumb = categoryBreadCrumb.Remove(categoryBreadCrumb.Length - 1, 1);

                model.DopingModels.Add(new ProductDopingListModel
                {
                    ProductId = product.ProductId,
                    ProductNo = product.ProductNo,
                    BrandName = brandName,
                    ProductName = product.ProductName,
                    CategoryBreadCrumb = categoryBreadCrumb,
                    CategoryId = product.CategoryId.Value,
                    ProductDopingBeginDate = product.ProductDopingBeginDate,
                    ProductDopingEndDate = product.ProductDopingEndDate,
                    StoreShortName = storename
                });
            }
            return View("DopingShowcaseList", model.DopingModels);
        }

        [HttpPost]
        public JsonResult ProductDopingDelete(int id)
        {
            var product = entities.Products.FirstOrDefault(x => x.ProductId == id);
            bool success = false;
            if (product != null)
            {
                product.Doping = false;
                product.ProductAdvertBeginDate = (DateTime?)null;
                product.ProductAdvertEndDate = (DateTime?)null;
                product.ProductRateWithDoping -= 1000;
                product.ProductLastUpdate = DateTime.Now;
                entities.SaveChanges();
                success = true;
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        //public string UpdateVideoName()
        //{
        //    var videoDistinct = entities.Videos.GroupBy(g => g.VideoTitle).Where(w => w.Count() > 1)
        // .Select(s =>
        //     new
        //     {
        //         s.FirstOrDefault().VideoTitle
        //     }).Distinct().ToList();

        //    foreach (var item in videoDistinct)
        //    {
        //        var videos = entities.Videos.Where(x => x.VideoTitle == item.VideoTitle);
        //        int tempSayi = 1;
        //        foreach (var item1 in videos.ToList())
        //        {
        //            item1.VideoTitle = item1.VideoTitle + " (" + tempSayi + ")";
        //            tempSayi++;
        //            entities.SaveChanges();
        //        }
        //    }
        //    return "";
        //}
        [HttpPost]
        public JsonResult ProductRateCalculate()
        {
            _productService.CalculateSPProductRate();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region AjaxMethods
        [HttpPost]
        public JsonResult GetCategoryByParentId(int id, byte type)
        {
            var list = new List<SelectListItem>();
            var categories = entities.Categories.Where(x => x.CategoryParentId == id && x.CategoryType == type).OrderBy(c => c.CategoryName).ToList();

            if (categories.Count > 0)
                list.Add(new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true });
            foreach (var item in categories)
            {
                list.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryId.ToString() });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult FavoriteProducts()
        {
            int pageCurrent = 1;
            int pageSize = 50;

            var favoritList = _favoriteProductService.GetAllFavoriteProduct(pageSize, pageCurrent);
            List<FavoriteListItemModel> collection = new List<FavoriteListItemModel>();

            foreach (var item in favoritList)
            {
                int mainPartyId = Convert.ToInt32(item.MainPartyId);
                int productId = Convert.ToInt32(item.ProductId);
                var product = _productService.GetProductByProductId(productId);
                if (product != null)
                {
                    var member = _memberService.GetMemberByMainPartyId(mainPartyId);

                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);

                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

                    var itemModel = new FavoriteListItemModel();
                    itemModel.AddedMemberMainPartyId = mainPartyId;
                    //itemModel.AddedStoreName = store.StoreName;
                    itemModel.AdedMemberName = member.MemberName + " " + member.MemberSurname;
                    itemModel.ProductId = item.ProductId.Value;
                    itemModel.ProductName = product.ProductName;
                    itemModel.ProductNo = product.ProductNo;
                    itemModel.ReceiverStoreMainPartyId = store.MainPartyId;
                    itemModel.ReceiverStoreName = store.StoreName;
                    itemModel.CreatedDate = item.CreatedDate;
                    collection.Add(itemModel);
                }

            }
            collection = collection.OrderByDescending(x => x.CreatedDate).ToList();
            var model = new FilterModel<FavoriteListItemModel>
            {
                CurrentPage = favoritList.PageIndex,
                TotalRecord = favoritList.TotalCount,
                PageDimension = 50,
                Source = collection
            };
            return View(model);
        }
        [HttpGet]
        public PartialViewResult GetFavoriteProducts(string page)
        {
            int pageCurrent = 1;
            int pageSize = 50;
            if (!string.IsNullOrEmpty(page)) pageCurrent = Convert.ToInt32(page);

            var favoritList = _favoriteProductService.GetAllFavoriteProduct(pageSize, pageCurrent);
            List<FavoriteListItemModel> collection = new List<FavoriteListItemModel>();

            foreach (var item in favoritList)
            {
                int mainPartyId = Convert.ToInt32(item.MainPartyId);
                int productId = Convert.ToInt32(item.ProductId);
                var product = _productService.GetProductByProductId(productId);
                if (product != null)
                {
                    var member = _memberService.GetMemberByMainPartyId(mainPartyId);
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    var itemModel = new FavoriteListItemModel();
                    itemModel.AddedMemberMainPartyId = mainPartyId;
                    //itemModel.AddedStoreName = store.StoreName;
                    itemModel.AdedMemberName = member.MemberName + " " + member.MemberSurname;
                    itemModel.ProductId = item.ProductId.Value;
                    itemModel.ProductName = product.ProductName;
                    itemModel.ProductNo = product.ProductNo;
                    itemModel.ReceiverStoreMainPartyId = store.MainPartyId;
                    itemModel.ReceiverStoreName = store.StoreName;
                    collection.Add(itemModel);
                }
            }
            var model = new FilterModel<FavoriteListItemModel>
            {
                CurrentPage = favoritList.PageIndex,
                TotalRecord = favoritList.TotalCount,
                PageDimension = pageSize,
                Source = collection
            };
            return PartialView("_FavoriteProductListItem", model);
        }

        public ActionResult Comment(string productId, string Reported)
        {
            int pageIndex = 1;
            int pageSize = 50;
            int ProductId = 0;
            if (!string.IsNullOrEmpty(productId))
                ProductId = Convert.ToInt32(productId);
            var productComments = _productCommentService.GetProductComments(pageSize, pageIndex, ProductId);
            if (Reported == "1")
            {
                productComments = _productCommentService.GetProductComments(pageSize, pageIndex, ProductId, true);
            }

            List<ProductCommentItem> source = new List<ProductCommentItem>();
            FilterModel<ProductCommentItem> model = new FilterModel<ProductCommentItem>();
            foreach (var item in productComments)
            {

                source.Add(new ProductCommentItem
                {
                    CommenText = item.CommentText,
                    MemberNameSurname = item.Member.MemberName + " " + item.Member.MemberSurname,
                    ProductCommentId = item.ProductCommentId,
                    ProductName = item.Product.ProductName,
                    ProductNo = item.Product.ProductNo,
                    Rate = item.Rate.Value,
                    RecorDate = item.RecordDate.ToString("dd/MM/yyyy HH:mm"),
                    ProductId = item.ProductId,
                    Reported = item.Reported,
                    MemberEmail = item.Member.MemberEmail,
                    Status = item.Status
                });
            }
            model.Source = source;
            model.CurrentPage = pageIndex;
            model.TotalRecord = productComments.TotalCount;
            model.PageDimension = pageSize;

            return View(model);
        }
        [HttpPost]
        public PartialViewResult Comment(int page, string productId, string Reported)
        {
            int pageSize = 50;
            int ProductId = 0;
            if (!string.IsNullOrEmpty(productId))
                ProductId = Convert.ToInt32(productId);
            var productComments = _productCommentService.GetProductComments(pageSize, page, ProductId);
            if (Reported == "1")
            {
                productComments = _productCommentService.GetProductComments(pageSize, page, ProductId, true);
            }
            List<ProductCommentItem> source = new List<ProductCommentItem>();
            FilterModel<ProductCommentItem> model = new FilterModel<ProductCommentItem>();
            foreach (var item in productComments)
            {

                source.Add(new ProductCommentItem
                {
                    CommenText = item.CommentText,
                    MemberNameSurname = item.Member.MemberName + " " + item.Member.MemberSurname,
                    ProductCommentId = item.ProductCommentId,
                    ProductName = item.Product.ProductName,
                    ProductNo = item.Product.ProductNo,
                    MemberEmail = item.Member.MemberEmail,
                    Rate = item.Rate.Value,
                    RecorDate = item.RecordDate.ToString("dd/mm/yyyy hh:MM"),
                    ProductId = item.ProductId,
                    Status = item.Status
                });
            }
            model.Source = source;
            model.CurrentPage = page;
            model.TotalRecord = productComments.TotalCount;
            model.PageDimension = pageSize;

            return PartialView("_CommentList", model);
        }
        [HttpGet]
        public ActionResult CommentStatus(int set, int id)
        {

            var productcomment = _productCommentService.GetProductCommentByProductCommentId(id);
            bool confirm = false;
            if (set == 1)
                confirm = true;

            productcomment.Status = confirm;
            _productCommentService.UpdateProductComment(productcomment);
            return RedirectToAction("Comment", "Product");
        }

        [HttpPost]
        public JsonResult MultiCommentConfirm(int set, string CheckItem)
        {
            string[] id = CheckItem.Substring(0, CheckItem.Length - 1).Split(',');
            bool confirm = false;
            if (set == 1)
                confirm = true;
            foreach (var item in id)
            {
                var productComment = _productCommentService.GetProductCommentByProductCommentId(Convert.ToInt32(item));
                productComment.Status = confirm;
                _productCommentService.UpdateProductComment(productComment);

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public string CreateImageNewSize(int fromPage, int takePage)
        {

            //var productImages = entities.Products.OrderByDescending(x => x.ProductId).Skip(fromPage).Take(takePage);
            var productImages = entities.Products.Where(x => x.ProductId == 126766);

            var thumbSizes = new List<string>();
            thumbSizes.Add("200x150");
            foreach (var item in productImages)
            {

                foreach (var item1 in item.Pictures)
                {

                    string newMainImageFilePath = AppSettings.ProductImageFolder + item.ProductId.ToString() + "\\";
                    var fileName = item1.PicturePath.Split('.')[0];
                    bool thumbResult = ImageProcessHelper.ImageResize(Server.MapPath(newMainImageFilePath) + item1.PicturePath,
            Server.MapPath(newMainImageFilePath) + "thumbs\\" + fileName, thumbSizes);
                }

            }

            return "ok";
        }

        public ActionResult HomeSectorProduct()
        {
            HomeSectorProductModel model = new HomeSectorProductModel();
            FilterModel<HomeSectorProductItem> filterModel = new FilterModel<HomeSectorProductItem>();
            List<HomeSectorProductItem> source = new List<HomeSectorProductItem>();
            int pageDimension = 20;
            var productHomePages = _productHomePageService.GetProductHomePages();
            int totalCount = 0;
            totalCount = productHomePages.Count;
            productHomePages = productHomePages.OrderByDescending(x => x.ProductHomePageId).Skip(0).Take(pageDimension).ToList();
            PrepareHomeSectorProductList(productHomePages, source);
            filterModel.Source = source;
            filterModel.TotalRecord = totalCount;
            filterModel.PageDimension = pageDimension;
            filterModel.CurrentPage = 1;
            model.HomeSectorProductItemsFilter = filterModel;

            var sectors = _categoryService.GetMainCategories().OrderBy(x => x.CategoryOrder).ThenBy(x => x.CategoryName);
            model.Sectors.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in sectors)
            {
                model.Sectors.Add(
                    new SelectListItem
                    {
                        Text = item.CategoryName,
                        Value = item.CategoryId.ToString()
                    });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult HomeSectorProduct(int PageIndex, int CategoryId, byte Type, byte Time)
        {
            int pageDimension = 20;
            var productHomePages = _productHomePageService.GetProductHomePages();
            if (CategoryId != 0)
            {
                productHomePages = productHomePages.Where(x => x.CategoryId == CategoryId).ToList();
            }
            if (Type != 0)
            {
                productHomePages = productHomePages.Where(x => x.Type == Type).ToList();
            }
            if (Time != 0)
            {
                if (Time == 1)
                    productHomePages = productHomePages.Where(x => x.EndDate.Value.Date < DateTime.Now.Date).ToList();
                else
                    productHomePages = productHomePages.Where(x => x.EndDate.Value.Date >= DateTime.Now.Date).ToList();

            }
            int totalCount = productHomePages.Count;
            productHomePages = productHomePages.OrderByDescending(x => x.ProductHomePageId).Skip(pageDimension * PageIndex - pageDimension).Take(pageDimension).ToList();

            FilterModel<HomeSectorProductItem> filterModel = new FilterModel<HomeSectorProductItem>();
            List<HomeSectorProductItem> source = new List<HomeSectorProductItem>();
            PrepareHomeSectorProductList(productHomePages, source);

            filterModel.Source = source;
            filterModel.TotalRecord = totalCount;
            filterModel.CurrentPage = PageIndex;
            filterModel.PageDimension = pageDimension;
            return PartialView("_HomeSectorProductList", filterModel);
        }

        private void PrepareHomeSectorProductList(IList<global::Trinnk.Entities.Tables.Catalog.ProductHomePage> productHomePages, List<HomeSectorProductItem> source)
        {
            foreach (var item in productHomePages)
            {
                var product = _productService.GetProductByProductId(item.ProductId);
                if (product != null)
                {
                    if (product.MainPartyId.HasValue)
                    {
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
                        var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

                        source.Add(new HomeSectorProductItem
                        {

                            Active = item.Active,
                            BeginDate = item.BeginDate.Value,
                            EndDate = item.EndDate.Value,
                            Id = item.ProductHomePageId,
                            ProductId = item.ProductId,
                            ProductName = product.ProductName,
                            ProductNo = product.ProductNo,
                            SectorName = item.Category.CategoryName,
                            StoreName = store.StoreName,
                            Type = item.Type.Value,
                            ProductHomeOrder = product.ProductHomePageOrder

                        });
                    }

                }

            }

        }
        [HttpPost]
        public JsonResult HomeSectorProductOrder(string productId, string order)
        {
            var productHomePage = _productHomePageService.GetProductHomePageByProductId(productId.ToInt32());
            int orderS = 0;
            var res = int.TryParse(order, out orderS);

            if (res)
            {
                order = order == "0" ? "255" : order;
                var product = _productService.GetProductByProductId(productHomePage.ProductId);
                product.ProductHomePageOrder = Convert.ToByte(order);
                _productService.UpdateProduct(product);
                var category = _categoryService.GetCategoryByCategoryId(productHomePage.CategoryId);
                var productHomePages = _productHomePageService.GetProductHomePagesByCategoryId(productHomePage.CategoryId);
                var products = _productService.GetProductsByProductIds(productHomePages.Select(x => x.ProductId).ToList());
                foreach (var item in products.Where(x => x.ProductHomePageOrder == null))
                {
                    item.ProductHomePageOrder = 255;
                    _productService.UpdateProduct(item);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CertificateTypes()
        {
            CertificateModel model = new CertificateModel();
            var certificateTypes = _certificateTypeService.GetCertificateTypes();
            foreach (var item in certificateTypes)
            {
                model.CertificateItems.Add(new CertificateItemModel
                {
                    CertificateTypeId = item.CertificateTypeId,
                    IconPath = AppSettings.CertificateTypeIconFolder + item.IconPath,
                    Active = item.Active,
                    Name = item.Name,
                    Order = item.Order
                });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CertificateTypes(CertificateModel model, HttpPostedFileBase icon)
        {
            var certificateType = new global::Trinnk.Entities.Tables.Catalog.CertificateType();
            if (!string.IsNullOrEmpty(model.CertificateItemModel.Name))
            {
                certificateType.Name = model.CertificateItemModel.Name;
                if (icon != null && icon.ContentLength > 0)
                {
                    string fileName = FileHelpers.Upload(AppSettings.CertificateTypeIconFolder, icon);
                    certificateType.IconPath = fileName;
                }
                certificateType.CreatedDate = DateTime.Now;
                certificateType.UpdatedDate = DateTime.Now;
                certificateType.Order = model.CertificateItemModel.Order;
                _certificateTypeService.InsertCertificateType(certificateType);
                TempData["Message"] = "Sertifika Başarıyla Eklendi";
            }
            else
            {
                TempData["Message"] = "Lütfen Sertifika İsmi Giriniz";
            }

            return RedirectToAction("CertificateTypes");

        }
        public ActionResult CertificateTypeConfirm(int id)
        {
            var certificateType = _certificateTypeService.GetCertificateTypeByCertificateTypeId(id);
            certificateType.Active = true;
            _certificateTypeService.UpdateCertificateType(certificateType);
            return RedirectToAction("CertificateTypes");
        }
        [HttpPost]
        public ActionResult DeleteCertificateType(int id)
        {
            var certificate = _certificateTypeService.GetCertificateTypeByCertificateTypeId(id);
            FileHelpers.Delete(AppSettings.CertificateTypeIconFolder + certificate.IconPath);
            _certificateTypeService.DeleteCertificateType(certificate);
            return Json(true, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ProductDelete(int id)
        {
            try
            {
                var product = _productService.GetProductByProductId(id);
                var item = product;
                var deletedProduct = new global::Trinnk.Entities.Tables.Catalog.DeletedProductRedirect();
                deletedProduct.CategoryId = item.CategoryId.HasValue ? item.CategoryId.Value : 0;
                deletedProduct.ProductId = item.ProductId;
                _deletedProductRedirectService.InsertDeletedProductRedirect(deletedProduct);
                var pictures = _pictureService.GetPicturesByProductId(item.ProductId, false);
                FileHelpers.DeleteFullPath(AppSettings.ProductImageFolder + item.ProductId);
                foreach (var picture in pictures)
                {
                    var thumbSizes = AppSettings.ProductThumbSizes.Replace("*", "").Split(';');
                    _pictureService.DeletePicture(picture);
                }
                var videos = _videoService.GetVideosByProductId(item.ProductId);
                foreach (var video in videos)
                {
                    FileHelpers.Delete(AppSettings.VideoFolder + video.VideoPath);
                    FileHelpers.Delete(AppSettings.VideoThumbnailFolder + video.VideoPicturePath);
                    _videoService.DeleteVideo(video);
                }
                _productService.DeleteProduct(item);
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }




        }


    }

}