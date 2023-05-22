using Trinnk.Entities.Tables.Common;
using Trinnk.Entities.Tables.Videos;
using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Areas.Account.Models.Advert;
using NeoSistem.Trinnk.Web.Models.Products;
using NeoSistem.Trinnk.Web.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Models
{

    public class ProductModel
    {
        public ProductModel()
        {
            this.ProductPriceTypes = new List<Constant>();
            this.MTProductPropertieModel = new MTProductPropertieModel();
            this.MTProductCatologItems = new List<MTProductCatologItem>();
            this.ConstantItems = new List<ConstantModel>();
            this.CertificateTypes = new List<SelectListItem>();
        }

        public int ProductId { get; set; }
        public int MainPartyId { get; set; }

        [DisplayName("Video Açıklama")]
        public string VideoTitle { get; set; }

        [DisplayName("Menşei")]
        public int MenseiId { get; set; }

        [DisplayName("Teslim Durumu")]
        public int OrderStatus { get; set; }

        [DisplayName("Ülke")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [DisplayName("İlçe")]
        public int LocalityId { get; set; }

        [DisplayName("Semt")]
        public int DistrictId { get; set; }

        [DisplayName("Mahalle / Köy")]
        public int TownId { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }

        public int? ModelId { get; set; }
        public int? BrandId { get; set; }
        public int? SeriesId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTreeName { get; set; }

        [DisplayName("Ürün Adı")]
        [RequiredValidation]
        [Required(ErrorMessage = "Zorunlu alan")]
        public string ProductName { get; set; }

        [DisplayName("Ürün Satışı")]
        public bool ReadyforSale { get; set; }

        [DisplayName("Ürün No")]
        public string ProductNo { get; set; }

        [DisplayName("Fiyat")]
        public decimal? ProductPrice { get; set; }

        public decimal? ProductPriceBegin { get; set; }
        public decimal? ProductPriceLast { get; set; }

        public byte? ProductPriceType { get; set; }
        public byte ProductFormat { get; set; }
        public string StoreName { get; set; }

        public DateTime? MainPartyRecordDate { get; set; }
        [DisplayName("Açıklama")]
        [RequiredValidation]
        public string ProductDescription { get; set; }

        public byte ProductBuyType { get; set; }
        public string ProductImage { get; set; }
        public string TotalPrice { get; set; }
        [DisplayName("Ürün Kayıt Tarihi")]
        public DateTime ProductRecordDate { get; set; }

        [DisplayName("Vitrin")]
        public bool ProductShowcase { get; set; }

        [DisplayName("İlan Başlangıç Tarihi")]
        public DateTime ProductAdvertBeginDate { get; set; }

        [DisplayName("İlan Bitiş Tarihi")]
        public DateTime ProductAdvertEndDate { get; set; }

        public bool Active { get; set; }

        [DisplayName("Ürün Tipi")]
        public string ProductType { get; set; }

        [DisplayName("Satış Durumu")]
        public string ProductSalesType { get; set; }

        [DisplayName("Onay Durumu")]
        public byte ProductActiveType { get; set; }

        [DisplayName("İlan Durumu")]
        public bool ProductActive { get; set; }

        [DisplayName("Ürün Durumu")]
        public string ProductStatu { get; set; }

        [DisplayName("Döviz Kuru")]
        public byte CurrencyId { get; set; }

        public string CurrencyName { get; set; }


        [DisplayName("Diğer Marka")]
        public string OtherBrand { get; set; }
        [DisplayName("Diğer Model")]
        public string OtherModel { get; set; }

        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SeriesName { get; set; }
        public string CategoryName { get; set; }

        [DisplayName("Kısa Detay")]
        public string BriefDetail { get; set; }

        [DisplayName("Model Yılı")]
        public int? ModelYear { get; set; }
        public string BriefDetailText { get; set; }
        public byte ProductOption { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductSalesTypeText { get; set; }

        public string SalesDepartmentName { get; set; }
        public string SalesDepartmentEmail { get; set; }
        public string PurchasingDepartmentName { get; set; }
        public string PurchasingDepartmentEmail { get; set; }

        public long ViewCount { get; set; }
        public long SingularViewCount { get; set; }
        public decimal productrate { get; set; }
        public int DropDownModelDate { get; set; }
        public string CurrentListedPage { get; set; }
        public string CurrentProductActiveType { get; set; }
        public string CurrentProductActive { get; set; }
        public DateTime ProductLastUpdate { get; set; }
        public decimal? ProductPriceWithDiscount { get; set; }
        public byte? DiscountType { get; set; }
        public decimal? DiscountAmount { get; set; }
        public IEnumerable<Constant> ProductPriceTypes { get; set; }
        public IEnumerable<Constant> TheOriginItems
        {
            //get
            //{
            //    //IList<Constant> theoriginitems = null;
            //    //using (var entities = new TrinnkEntities())
            //    //{
            //    //    theoriginitems = entities.Constants.Where(c => c.ConstantType == 16).ToList();
            //    //    theoriginitems.Insert(0, new Constant { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
            //    //}

            //    //return theoriginitems.OrderBy(c => c.ConstantName).OrderBy(c => c.ConstantId);

            //}
            get; set;
        }
        public IEnumerable<Constant> SiparisList
        {
            //get
            //{
            //    //IList<Constant> itemsiparis = null;
            //    //using (var entities = new TrinnkEntities())
            //    //{
            //    //    itemsiparis = entities.Constants.Where(c => c.ConstantType == 17).ToList();
            //    //    itemsiparis.Insert(0, new Constant { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
            //    //}

            //    //return itemsiparis.OrderBy(c => c.ConstantName).OrderBy(c => c.ConstantId);
            //}
            get; set;
        }

        public SelectList ModelDate
        {
            get
            {
                return new SelectList(new[]
                {
            new { Value = 0, Text= "< Lütfen Seçiniz >" },
            new { Value = 1990, Text= "1990" },
            new { Value = 1991, Text= "1991" },
            new { Value = 1992, Text= "1992" },
            new { Value = 1993, Text= "1993" },
            new { Value = 1994, Text= "1994" },
            new { Value = 1995, Text= "1995" },
            new { Value = 1996, Text= "1996" },
            new { Value = 1997, Text= "1997" },
            new { Value = 1998, Text= "1998" },
            new { Value = 1999, Text= "1999" },
            new { Value = 2000, Text= "2000" },
            new { Value = 2001, Text= "2001" },
            new { Value = 2002, Text= "2002" },
            new { Value = 2003, Text= "2003" },
            new { Value = 2004, Text= "2004" },
            new { Value = 2005, Text= "2005" },
            new { Value = 2006, Text= "2006" },
            new { Value = 2007, Text= "2007" },
            new { Value = 2008, Text= "2008" },
            new { Value = 2009, Text= "2009" },
            new { Value = 2010, Text= "2010" },
            new { Value = 2011, Text= "2011" },
            new { Value = 2012, Text= "2012" },
         }
                , "Value", "Text", 0);
            }
        }

        public short ProductPublicationDate { get; set; }
        public byte ProductPublicationDateType { get; set; }

        public SelectList CityItems { get; set; }

        public string CityName { get; set; }
        public string LocalityName { get; set; }

        public SelectList MenseiItems { get; set; }

        public SelectList CountryItems { get; set; }

        public SelectList LocalityItems
        {
            get;
            set;
        }

        public SelectList TownItems
        {
            get;
            set;
        }

        public List<PictureModel> PictureList { get; set; }

        public string GetCategoryName(int categoryId)
        {
            var category = new Classes.Category();
            bool hasRecord = category.LoadEntity(categoryId);
            if (hasRecord)
            {
                return category.CategoryName;
            }
            return "";
        }

        //public MainParty GetMainParty(int memberId)
        //{
        //    MainParty mainparty = new MainParty();
        //    using (var entities = new TrinnkEntities())
        //    {
        //        mainparty = entities.MainParties.SingleOrDefault(c => c.MainPartyId == memberId);
        //    }

        //    if (mainparty != null)
        //    {
        //        return mainparty;
        //    }
        //    return new MainParty();
        //}

        //public Classes.Store GetStore(int MainPartyId)
        //{
        //    var store = new Classes.Store();
        //    bool hasModel = store.LoadEntity(MainPartyId);
        //    if (hasModel)
        //    {
        //        return store;
        //    }
        //    return new Classes.Store();
        //}

        //public string GetPicture(int ProductId)
        //{
        //    Picture picture;
        //    using (var entities = new TrinnkEntities())
        //    {
        //        picture = entities.Pictures.FirstOrDefault(c => c.ProductId == ProductId);
        //    }

        //    if (picture != null)
        //    {
        //        return picture.PicturePath;
        //    }
        //    return "";
        //}

        public string ProductFeatures { get; set; }

        public string MainPicture { get; set; }

        //public string GetCategory(int id)
        //{
        //    if (id != 0)
        //    {
        //        var curCategory = new Classes.Category();
        //        bool hasRecord = curCategory.LoadEntity(id);
        //        if (hasRecord)
        //            return curCategory.CategoryName;
        //        return "";
        //    }
        //    return "";
        //}

        public ICollection<ConstantModel> ConstantItems
        {
            //get
            //{
            //    //var curConstant = new Classes.Constant();
            //    //return curConstant.GetDataTable().AsCollection<ConstantModel>();

            //}
            get; set;
        }

        public string CategoryProductGroupName { get; set; }

        public SelectList CurrencyItems
        {
            //get
            //{
            //    var curCurrency = new Classes.Currency();
            //    var currencyItems = curCurrency.GetDataTable().AsCollection<CurrencyModel>().ToList();
            //    currencyItems.Insert(0, new CurrencyModel { CurrencyId = 0, CurrencyName = "< Seçiniz >" });
            //    return new SelectList(currencyItems, "CurrencyId ", "CurrencyName");
            //}
            get; set;
        }

        public Address StoreAddress { get; set; }
        public ICollection<PhoneModel> PhoneList { get; set; }
        public StoreModel Store { get; set; }
        public bool HasStore { get; set; }

        [DisplayName("Yeni Ürün Resmi")]
        public string NewProductPicture { get; set; }

        [DisplayName("Yeni Ürün Videosu")]
        public string NewProductVideo { get; set; }

        public ICollection<PictureModel> ProductPictureItems { get; set; }
        public IList<Video> VideoItems { get; set; }

        public int ProductOdd { get; set; }

        public LeftMenuModel LeftMenu { get; set; }

        public string ProductUrl { get; set; }

        public string SimilarUrl { get; set; }

        public string WarrantyPeriod { get; set; }

        public string UnitType { get; set; }

        public bool Doping { get; set; }
        public int Sort { get; set; }
        public bool Kdv { get; set; }
        public bool Fob { get; set; }
        public bool HasVideo { get; set; }
        public MTProductPropertieModel MTProductPropertieModel { get; set; }
        public List<MTProductCatologItem> MTProductCatologItems { get; set; }

        public bool AllowSellUrl { get; set; }
        //public string Keywords { get; set; }
        public string ProductSellUrl { get; set; }

        public List<SelectListItem> CertificateTypes { get; set; }

        public byte? MinumumAmount { get; set; }
    }
}