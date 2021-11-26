
namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using EnterpriseEntity.Extensions.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public static class EnumModel
    {
        //test zorunlu


        public static Dictionary<string, int> GetMonth()
        {
            var monthItems = new Dictionary<string, int>();

            monthItems.Add("Ocak", 1);
            monthItems.Add("Şubat", 2);
            monthItems.Add("Mart", 3);
            monthItems.Add("Nisan", 4);
            monthItems.Add("Mayıs", 5);
            monthItems.Add("Haziran", 6);
            monthItems.Add("Temmuz", 7);
            monthItems.Add("Ağustos", 8);
            monthItems.Add("Eylül", 9);
            monthItems.Add("Ekim", 10);
            monthItems.Add("Kasım", 11);
            monthItems.Add("Aralık", 12);

            return monthItems;
        }
        public enum OrderPacketType : byte
        {
            Normal = 1,
            Doping = 2
        }

        public enum LanguageCode : byte
        {
            Tr = 1,
            En = 2
        }

        public enum LanguageCodeString 
        {
            tr,
            en
        }



        //public static string AddressEdit(Address model)
        //{
        //    Town townItem = null;
        //    Locality localityItem = null;
        //    City cityItem = null;
        //    Country countryItem = null;
        //    District districtItem = null;

        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        using (var trans = new TransactionScope())
        //        {
        //            if (model.TownId.HasValue)
        //            {
        //                townItem = entities.Towns.SingleOrDefault(c => c.TownId == model.TownId.Value);
        //                districtItem = entities.Districts.SingleOrDefault(c => c.DistrictId == townItem.DistrictId);
        //            }
        //            if (model.LocalityId.HasValue)
        //            {
        //                localityItem = entities.Localities.SingleOrDefault(c => c.LocalityId == model.LocalityId.Value);
        //            }
        //            if (model.CityId.HasValue)
        //            {
        //                cityItem = entities.Cities.SingleOrDefault(c => c.CityId == model.CityId.Value);
        //            }
        //            if (model.CountryId.HasValue)
        //            {
        //                countryItem = entities.Countries.SingleOrDefault(c => c.CountryId == model.CountryId.Value);
        //            }
        //            trans.Complete();
        //        }
        //    }

        //    if (model == null)
        //    {
        //        return "";
        //    }
        //    var builder = new StringBuilder();

        //    if (townItem != null)
        //    {
        //        builder.AppendFormat("{0} ", townItem.TownName);
        //    }

        //    builder.AppendFormat("{0} ", model.Avenue);

        //    if (!string.IsNullOrWhiteSpace(model.Street))
        //    {
        //        builder.AppendFormat("{0} ", model.Street);
        //    }

        //    if (!string.IsNullOrWhiteSpace(model.ApartmentNo))
        //    {
        //        builder.AppendFormat("No: {0} ", model.ApartmentNo);
        //    }

        //    if (!string.IsNullOrWhiteSpace(model.DoorNo))
        //    {
        //        builder.AppendFormat("/ {0} ", model.DoorNo);
        //    }

        //    builder.AppendFormat("{1} {0} {2} / {3}", localityItem != null ? localityItem.LocalityName : "", districtItem != null ? districtItem.ZipCode : "", cityItem != null ? cityItem.CityName : "-", countryItem != null ? countryItem.CountryName : "-");

        //    return builder.ToString();
        //}



        //public static string AddressEdit(AddressModel model)
        //{
        //    if (model == null)
        //    {
        //        return "";
        //    }
        //    var builder = new StringBuilder();

        //    builder.AppendFormat("{0} ", model.TownName);

        //    builder.AppendFormat("{0} ", model.Avenue);

        //    if (!string.IsNullOrWhiteSpace(model.Street))
        //    {
        //        builder.AppendFormat("{0} ", model.Street);
        //    }

        //    if (!string.IsNullOrWhiteSpace(model.ApartmentNo))
        //    {
        //        builder.AppendFormat("No: {0} ", model.ApartmentNo);
        //    }

        //    if (!string.IsNullOrWhiteSpace(model.DoorNo))
        //    {
        //        builder.AppendFormat("/ {0} ", model.DoorNo);
        //    }

        //    builder.AppendFormat("{0} {1} {2}/{3}", model.ZipCode, model.LocalityName, model.CityName, model.CountryName);


        //    return builder.ToString();
        //}

        public static string UrlHttpEdit(string url)
        {
            string http = "http://";
            if (url == null)
            {
                return "#";
            }
            if (url.Contains(http))
            {
                return url;
            }
            else
                return http + url;
        }

        //public static IList<Currency> CacheCurrencyItems()
        //{
        //    IList<Currency> currencyItems;
        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        currencyItems = entities.Currencies.ToList();
        //    }
        //    return currencyItems;
        //}

        public static int CharactersCount(string text, char character)
        {
            if (!string.IsNullOrEmpty(text))
            {
                int count = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text.Substring(i, 1) == character.ToString())
                    {
                        count++;
                    }
                }
                return count;
            }
            else
                return 0;
        }

        //public static string GetProductAllAddress(Product product)
        //{
        //    if (product == null)
        //        return string.Empty;
        //    StringBuilder sb = new StringBuilder();
        //    if (product.TownId.HasValue)
        //    {
        //        sb.AppendFormat("{0} ", product.Town.TownName);
        //    }

        //    if (product.LocalityId.HasValue)
        //    {
        //        sb.AppendFormat(" {0} ", product.Locality.LocalityName);
        //    }
        //    if (product.DistrictId.HasValue)
        //    {
        //        sb.AppendFormat(" {0} ", product.District.DistrictName);
        //    }

        //    if (product.CityId.HasValue)
        //    {
        //        sb.AppendFormat(" {0} ", product.City.CityName);
        //    }

        //    if (product.CountryId.HasValue)
        //    {
        //        sb.AppendFormat(" {0} ", product.Country.CountryName);
        //    }

        //    return sb.ToString();
        //}

        public static string GetDescription(this Enum value)
        {
            var descriptionAttribute = (DescriptionAttribute)value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(false)
                .Where(a => a is DescriptionAttribute)
                .FirstOrDefault();

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }

        public static int GetEnumFromDescription(string description, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }
            }
            return 0;
        }

    }

    public enum Ordertype
    {
        Havale = 1,
        KrediKarti = 2
    }

    public enum SingularViewCountType
    {
        Product,
        StoreProfile,
        Video
    }



    public enum EPacketFeatureType
    {
        ProcessCount = 1,
        Active = 2,
        Content = 3
    }
    public enum AdvancedSearchFilterType
    {
        City = 1,
        Locality = 2,
        Brand = 3,
        Model = 4,
        Serie = 5
    }
    public enum StoreDealerType
    {
        Branch = 3,
        Services = 2,
        Dealer = 1
    }

    //public enum SeoDefinitionType : byte
    //{
    //    Category = 1,
    //    StoreCategory = 3
    //}

    [Flags]
    public enum PageType : byte
    {
        General = 1,
        Category = 2,
        Product = 3,
        Store = 4,
        Brand = 5,
        Serie = 6,
        Model = 7,
        ProductGroup = 9,
        Help = 10,
        StoreCategory = 11,
        Sector = 12,
        VideoMainPage = 13,
        VideoCategoryPage = 14,
        VideoViewPage = 15,
        ProductSearchPage = 16,
        CategoryBrand = 17,
        CategoryBrandCountry = 18,
        CategoryBrandCity = 19,
        CategoryBrandLocality = 20,
        CategoryCountry = 21,
        CategoryCity = 22,
        CategoryLocality = 23,
        BrandCountry = 24,
        BrandCity = 25,
        BrandLocality = 26,
        Error = 27,
        HelpCategory = 28,
        StoreCategoryHome = 29,
        StoreProductPage = 30,
        StoreAboutPage = 31,
        StoreDepartmentPage = 32,
        StoreServiceNetwork = 33,
        StoreDealerPage = 34,
        StoreDealerNetwork = 35,
        StoreBrandPage = 36,
        StoreConnectionPage = 37,
        Uyelik = 38,
        VideoSearchPage = 39,
        StoreVideoPage = 40,
        HizliUyelik = 41,
        StoreProductCategoryPage = 42,
        SectorAll = 43,
        StoreImages = 44,
        StoreCatolog = 45,
        StoreNewHome = 46,
        StoreNewDetail = 47,
        StoreNews = 48,
        SuccessStories = 51,
        SuccesstoriesDetail = 52,
        ProductRequest = 53
    }

    [Flags]
    public enum DisplayType : int
    {
        [Description("Pencere")]
        Window = 1,
        [Description("Liste")]
        List = 2,
        [Description("Yazi")]
        Text = 3,
        [Description("Tablo")]
        Table = 4

    }


    [Flags]
    public enum AccountFavoritePageType
    {
        Store,
        Product
    }

    [Flags]
    public enum PhoneType : byte
    {
        Phone,
        Fax,
        Gsm,
        Whatsapp
    }

    [Flags]
    public enum PhoneType_TR
    {
        Telefon,
        Fax,
        Gsm
    }

    [Flags]
    public enum MemberType
    {
        /// <summary>
        /// Hızlı Üyelik
        /// </summary>
        FastIndividual = 5,
        /// <summary>
        ///Bireysel
        /// </summary>
        Individual = 10,
        /// <summary>
        /// Kurumsal
        /// </summary>
        Enterprise = 20
    }

    [Flags]
    public enum MemberType_Tr
    {
        Hızlı_Üyelik = 5,
        Bireysel_Üyelik = 10,
        Kurumsal_Üyelik = 20
    }

    [Flags]
    public enum MemberStatu : byte
    {
        Active,
        Passive,
        Deleted
    }

    [Flags]
    public enum Gender : byte
    {
        Mr,
        Ms
    }

    [Flags]
    public enum MainPartyType : byte
    {
        Firm,
        Member,
        FastMember
    }

    [Flags]
    public enum CategoryType : byte
    {
        Brand = 3,
        Series = 4,
        Model = 5,
        ProductGroup = 6,
        Sector = 0,
        Category = 1
    }
    public enum BannerImageType
    {
        Pc = 0,
        Tablet = 1,
        Mobile = 2
    }
    public enum StoreImageType
    {
        StoreImage = 1,
        StoreProfileSliderImage = 2
    }
    [Flags]
    public enum MainCategoryType : byte
    {
        Ana_Kategori = 1,
        Yardim = 2
    }

    [Flags]
    public enum BannerType : byte
    {
        CategoryBanner1 = 1,
        CategoryBanner2 = 2,
        CategoryBanner3 = 3,
        CategoryBanner4 = 4,
        CategoryBanner5 = 5,
        CategoryBanner6 = 6,
        MainPageRight = 7,
        MainPageBottom1 = 8,
        MainPageBottom2 = 9,
        MainPageBottom3 = 10,
        CategorySideLeft = 11,
        ProductSideLeft = 12,
        CategoryHorizontalBlankBanner = 13,
        CategorySliderBanner = 14
    }

    [Flags]

    public enum ProductActiveType : byte
    {
        Inceleniyor,
        Onaylandi,
        Onaylanmadi,
        Silindi,
        CopKutusunda = 6,
        Tumu = 7,
        CopKutusuYeni = 8
    }


    [Flags]

    public enum ProductStatus : byte
    {
        NewProduct = 72,
        SecondHand = 73
    }



    [Flags]
    public enum ProductActive : byte
    {
        Pasif,
        Aktif
    }

    //[Flags]
    //public enum ProductActiveType : byte
    //{
    //  Onaydaki,
    //  Aktif,
    //  Pasif,
    //  Onaylanmamis,
    //  Silinen,
    //  Tum,
    //  VitrinAktif,
    //  VitrinPasif,
    //  VitrinOnayBekleyen,
    //  VitrinOnaylanmamis,
    //}

    //public enum MobileMessageType : byte
    //{
    //    Normal = 1,
    //    Whatsapp = 2
    //}

    [Flags]
    public enum PurchaseRequestType : byte
    {
        Onaydaki,
        Aktif,
        Pasif,
        Onaylanmamis,
        Silinen,
    }
    public enum CategoryPlaceType
    {
        HomeLeftSide = 1,
        HomeCenter = 2,
        HomeChoicesed = 3,
        ProductGroup = 4
    }
    public enum HelpCategoryPlace
    {
        ForMember = 1,
        ForStore = 2,
        AccountHome = 3,
        StoreLogoUpdate = 4,
        PersonalAccount = 5
    }
    public enum ProductPriceType : byte
    {
        Price = 238,
        PriceRange = 239,
        PriceAsk = 240,
        PriceDiscuss = 241
    }
    public enum MessagePageType : byte
    {
        Inbox = 0,
        Send = 1,
        Outbox = 2,
        Banned = 3,
        RecyleBin = 4
    }

    public enum MessageType : byte
    {
        Inbox = 0,
        Outbox = 1,
        RecyleBin = 2
    }
    public enum FastMembershipType : byte
    {
        Normal = 5,
        Facebook = 10,
        Phone = 20,
        ProductComplain = 30,
        Google = 40
    }
    public enum LogType : byte
    {
        MemberShip = 5,
        Messaging = 10,

    }
    public enum StoreNewType : byte
    {
        Normal = 1,
        SuccessStories = 2
    }

    public enum StoreAuthorizedTitleType
    {
        Yonetici = 1,
        Pazarlamaci = 2,
        TeknikDestek = 3
    }

    public enum ProductPublicationDateType
    {
        Gun = 1,
        Ay = 2,
        Yil = 3
    }

    public enum DealerType
    {
        Bayii = 1,
        YetkiliServis = 2,
        Sube = 3
    }


    public enum GsmType
    {
        Vodafone = 1,
        Avea = 2,
        Turkcell = 3
    }

    public enum ConstantType
    {
        ProductPayType = 1,
        ProductSalesType = 2,
        ProductCargoType = 3,
        StoerType = 4,
        MoneyCondition = 5,
        MemberTitleType = 6,
        ProductType = 7,
        ProductStatu = 8,
        ProductBriefDetail = 9,
        EmployeesCount = 10,
        StoreCapital = 11,
        StoreEndorsement = 12,
        Birim = 21,
        FootorDegerleri = 22,
        DiscountPacketDescription = 23,
        ProductPriceType = 24
    }
    public enum PropertieType : byte
    {
        Editor = 1,
        Text = 2,
        MutipleOption = 3
    }
    public enum PacketStatu
    {
        Inceleniyor = 1,
        Onaylandi = 2,
        Onaylanmadi = 3,
        Silindi = 4
    }

    public enum MemberStoreType : byte
    {
        Owner = 1,
        Helper = 2
    }
    public enum PageOrderType : int
    {
        [Description("a-z")] AdanZ = 1,
        [Description("z-a")] ZdenA = 2,
        [Description("soneklenen")] SonEkleneneGore = 3,
        [Description("encokgoruntulenen")] EnCokGoruntulenen = 4
    }

    public enum ProductSearchType : int
    {
        [Description("tumurunler")]
        All = 0,
        [Description("sifir")]
        New = 72,
        [Description("ikinciel")]
        Used = 73
    }
    public enum ProductDiscountType : byte
    {
        Percentage = 1,
        Amount = 2
    }
    public enum ProductSearchTypeV2 : int
    {
        [Description("tumurunler")]
        All = 0,
        [Description("sifir")]
        New = 1,
        [Description("ikinciel")]
        Used = 2,
        [Description("hizmet")]
        Services = 3,
    }
    public enum LoginTabType : byte
    {
        Login = 1,
        Register = 2
    }



    #region OldEnums

    //[Flags]
    //public enum CategoryGroupType : byte
    //{
    //  None = 0,
    //  Makina = 1,
    //  Parca = 2,
    //  HamMadde = 3,
    //  SarfMalzeme = 4,
    //  Aksesuar = 5,
    //  Hizmet = 6,
    //  Yayinlar = 7
    //}

    //[Flags]
    //public enum MemberTitleType : byte
    //{
    //  Admin,
    //  Marketer,
    //  TechnicalSupport
    //}

    //[Flags]
    //public enum StoreType : byte
    //{
    //  Anonim,
    //  Limited,
    //}

    //[Flags]
    //public enum ProductStatu
    //{
    //  Onaylandi = 1,
    //  Onaylanmadi = 2,
    //  YeniGelen = 4,
    //  Satıldı = 8,
    //  Indirimde = 16,
    //  OnayBekleyen = 32
    //}
    #endregion

}