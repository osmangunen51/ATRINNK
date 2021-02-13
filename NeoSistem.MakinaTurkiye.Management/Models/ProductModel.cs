
namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using Validation;

    [Bind(Exclude = "ProductId")]
    public class ProductModel
    {
        public ProductModel()
        {
            this.ProductPriceTypes = new List<global::MakinaTurkiye.Entities.Tables.Common.Constant>();
            this.CategorySectors = new List<SelectListItem>();
        }
        [DisplayName("Ürün Satışı")]
        public bool ReadyforSale { get; set; }

        [DisplayName("Aktif Durumu")]
        public bool ProductActive { get; set; }

        [DisplayName("Money Condition")]
        public bool MoneyCondition { get; set; }

        public DateTime ProductLastViewDate { get; set; }

        public string CategoryProductGroupName { get; set; }

        public string UserName { get; set; }

        public int ProductId { get; set; }

  
            

        [DisplayName("Menşei")]
        public int? MenseiId { get; set; }

        [DisplayName("Teslim Durumu")]
        public int? OrderStatus { get; set; }

        [DisplayName("Model Adı")]
        public int? ModelId { get; set; }

        public bool? ChoicedForCategoryIndex
        {
            get;
            set;
        }
        public bool IsProductHomePage { get; set; }
        [DisplayName("Marka Adı")]
        public int? BrandId { get; set; }

        public string BrandNameTitle { get; set; }
        

        [DisplayName("Seri Adı")]
        public int? SeriesId { get; set; }

        [DisplayName("Kategori Adı")]
        public int CategoryId { get; set; }

        public IList<Video> VideoItems { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }

        [DisplayName("Üye")]
        public int MemberId { get; set; }

        [DisplayName("Ülke")]
        public int? CountryId { get; set; }

        [DisplayName("Şehir")]
        public int? CityId { get; set; }

        [DisplayName("Posta Kodu / Semt")]
        public int? DistrictId { get; set; }

        [DisplayName("Semt")]
        public int? LocalityId { get; set; }

        [DisplayName("Mahalle / Köy")]
        public int? TownId { get; set; }

        public string CategoryTreeName { get; set; }

        [DisplayName("Ürün No")]
        public string ProductNo { get; set; }

        [DisplayName("Ürün Adı")]
        [RequiredValidation]
        public string ProductName { get; set; }

        [DisplayName("Fiyat")]
        public decimal? ProductPrice { get; set; }

        [DisplayName("Ürün Tipi")]
        public string ProductType { get; set; }

        [DisplayName("Açıklama")]
        [RequiredValidation]
        public string ProductDescription { get; set; }

        [DisplayName("Ürün Kayıt Tarihi")]
        public DateTime ProductRecordDate { get; set; }

        [DisplayName("Son Güncelleme Tarihi")]
        public DateTime ProductLastUpdate { get; set; }

        [DisplayName("Vitrin")]
        public bool ProductShowcase { get; set; }

        [DisplayName("İlan Başlangıç Tarihi")]
        public DateTime? ProductAdvertBeginDate { get; set; }

        [DisplayName("İlan Bitiş Tarihi")]
        public DateTime? ProductAdvertEndDate { get; set; }

        [DisplayName("Ürün Durumu")]
        public string ProductStatu { get; set; }

        [DisplayName("Satış Detayı")]
        public string ProductSalesType { get; set; }

        [DisplayName("Onay Durumu")]
        public byte? ProductActiveType { get; set; }

        [DisplayName("Diğer Marka")]
        public string OtherBrand { get; set; }

        [DisplayName("Diğer Model")]
        public string OtherModel { get; set; }

        [DisplayName("Model Yılı")]
        public int ModelYear { get; set; }

        public string NameBrand { get; set; }
        public string NameModel { get; set; }
        public string BrandContentTitle { get; set; }
        public string ModelContentTitle { get; set; }
        public string NameSeries { get; set; }

        public byte MemberType { get; set; }
        public int StoreMainPartyId { get; set; }

        public string MainPartyFullName { get; set; }

        [DisplayName("Görüntülenme")]
        public long ViewCount { get; set; }

        [DisplayName("Tekil Görüntülenme")]
        public long SingularViewCount { get; set; }

        [DisplayName("Kısa Detay")]
        public string BriefDetail { get; set; }

        [DisplayName("Son Görüntülenme")]
        public DateTime? LastViewDate { get; set; }

        [DisplayName("Resim Yükle")]
        public string NewProductPicture { get; set; }

        [DisplayName("Video Yükle")]
        public string NewProductVideo { get; set; }

        [DisplayName("Video Adı")]
        public string VideoTitle { get; set; }

        public SelectList TownItems { get; set; }

        public string WarrantyPeriod { get; set; }
        public string UnitType { get; set; }

        [DisplayName("Doping")]
        public bool Doping { get; set; }

        public int PictureCount { get; set; }
        public bool? Kdv { get; set; }
        public bool? Fob { get; set; }
        public byte ?ProductPriceType { get; set; }

   
        public decimal? ProductPriceBegin { get; set; }
        public decimal? ProductPriceLast { get; set; }

        public bool HasVideo { get; set; }
        public bool IsAdvanceEdit { get; set; }
        public bool IsNewProduct { get; set; }

        public DateTime? ProductDopingEndDate { get; set; }
        public DateTime? ProductDopingBeginDate { get; set; }

        public DateTime? ProductHomeBeginDate { get; set; }
        public DateTime? ProductHomeEndDate { get; set; }

        public IList<global::MakinaTurkiye.Entities.Tables.Common.Constant> ProductPriceTypes { get; set; }
        public IEnumerable<Constant> TheOriginItems
        {
            get
            {
                IList<Constant> theoriginitems = null;
                using (var entities = new MakinaTurkiyeEntities())
                {
                    theoriginitems = entities.Constants.Where(c => c.ConstantType == 16).ToList();
                    theoriginitems.Insert(0, new Constant { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
                }

                return theoriginitems.OrderBy(c => c.ConstantName).OrderBy(c => c.ConstantId);
            }
        }
        public IEnumerable<Constant> SiparisList
        {
            get
            {
                IList<Constant> itemsiparis = null;
                using (var entities = new MakinaTurkiyeEntities())
                {
                    itemsiparis = entities.Constants.Where(c => c.ConstantType == 17).ToList();
                    itemsiparis.Insert(0, new Constant { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
                }

                return itemsiparis.OrderBy(c => c.ConstantName).OrderBy(c => c.ConstantId);
            }
        }

        public ICollection<ConstantModel> ConstantItems
        {
            get
            {
                var curConstant = new Classes.Constant();
                return curConstant.GetDataTable().AsCollection<ConstantModel>();
            }
        }

        public SelectList CountryItems { get; set; }

        public SelectList CityItems { get; set; }

        public SelectList LocalityItems { get; set; }

        public string MainCategoryName
        {
            get
            {
                var curCategory = new Classes.Category();
                curCategory.LoadEntity(CategoryId);
                return curCategory.CategoryName;
            }
        }

        public string FirstCategoryName { get; set; }

        public string BrandCategoryName
        {
            get
            {
                var curCategory = new Classes.Category();
                curCategory.LoadEntity(BrandId);
                return curCategory.CategoryName;
            }
        }

        public string ModelCategoryName
        {
            get
            {
                var curCategory = new Classes.Category();
                curCategory.LoadEntity(ModelId);
                return curCategory.CategoryName;
            }
        }

        public string SerieCategoryName
        {
            get
            {
                var curCategory = new Classes.Category();
                curCategory.LoadEntity(SeriesId);
                return curCategory.CategoryName;
            }
        }
        public List<SelectListItem> CategorySectors { get; set; }

        public ICollection<PictureModel> ProductPictureItems { get; set; }

        [DisplayName("Döviz Kuru")]
        public byte? CurrencyId { get; set; }
        public string CurrencyName { get; set; }


        public int VideoCount { get; set; }

        public SelectList CurrencyItems
        {
            get
            {
                var curCurrency = new Classes.Currency();
                var currencyItems = curCurrency.GetDataTable().AsCollection<CurrencyModel>().ToList();
                currencyItems.Insert(0, new CurrencyModel { CurrencyId = 0, CurrencyName = "< Seçiniz >" });
                return new SelectList(currencyItems, "CurrencyId ", "CurrencyName");
            }
        }

        public string CategoryName { get; set; }
        public string CategoryContentTitle { get; set; }
        public string ProductSellUrl { get; set; }
        public bool AllowProductSellUrl { get; set; }
        public string Keywords { get; set; }
        public int ? ProductHomePageId { get; set; }
        public byte ? DiscountType { get; set; }
        public decimal? ProductPriceWithDiscount { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}