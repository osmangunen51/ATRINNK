
namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Text;

    public class EnumModels
    {
        //test zor

        public static string UrlHttpEdit(string url)
        {
            if (!string.IsNullOrEmpty(url) && !url.Equals("http://"))
            {
                string http = "http://";
                string https = "https://";
                if (url.Contains(http) || url.Contains(https))
                {
                    return url;
                }
                else
                    return http + url;
            }
            return string.Empty;
        }

        public static string AddressEdit(Address model)
        {
            if (model == null)
            {
                return "";
            }
            var builder = new StringBuilder();

            builder.AppendFormat("{0} ", model.Avenue);

            if (!string.IsNullOrWhiteSpace(model.Street))
            {
                builder.AppendFormat("{0} ", model.Street);
            }

            if (!string.IsNullOrWhiteSpace(model.ApartmentNo))
            {
                builder.AppendFormat("No: {0} ", model.ApartmentNo);
            }

            if (!string.IsNullOrWhiteSpace(model.DoorNo))
            {
                builder.AppendFormat("/ {0} ", model.DoorNo);
            }
            builder.AppendFormat("{0} {1} {2}/{3}", model.Town != null ? model.Town.TownName : "", model.Locality != null ? model.Locality.LocalityName : "", model.City != null ? model.City.CityName : "-", model.Country != null ? model.Country.CountryName : "-");
            return builder.ToString();
        }

        public static string AddressEditForOrder(Address model)
        {
            var entities = new MakinaTurkiyeEntities();

            District districtItem = null;
            if (model == null)
            {
                return "";
            }
            var builder = new StringBuilder();

            if (model.Town != null)
            {
                builder.AppendFormat("{0} ", model.Town.TownName);
                int districtId = Convert.ToInt32(model.Town.DistrictId);
                districtItem = entities.Districts.FirstOrDefault(d => d.DistrictId == districtId);
            }

            builder.AppendFormat("{0} ", model.Avenue);

            if (!string.IsNullOrWhiteSpace(model.Street))
            {
                builder.AppendFormat("{0} ", model.Street);
            }

            if (!string.IsNullOrWhiteSpace(model.ApartmentNo))
            {
                builder.AppendFormat("No: {0} ", model.ApartmentNo);
            }

            if (!string.IsNullOrWhiteSpace(model.DoorNo))
            {
                builder.AppendFormat("/ {0} ", model.DoorNo);
            }

            builder.AppendFormat("{1} {0} {2} / {3}", model.Locality != null ? model.Locality.LocalityName : "", districtItem != null ? districtItem.ZipCode : "", model.City != null ? model.City.CityName : "-", model.Country != null ? model.Country.CountryName : "-");

            return builder.ToString();
        }
        public static string AddressChangeHistory(global::MakinaTurkiye.Entities.Tables.Common.AddressChangeHistory model)
        {
            var entities = new MakinaTurkiyeEntities();

            District districtItem = null;
            if (model == null)
            {
                return "";
            }
            var builder = new StringBuilder();

            if (model.Town != null)
            {
                builder.AppendFormat("{0} ", model.Town.TownName);
                int districtId = Convert.ToInt32(model.Town.DistrictId);
                districtItem = entities.Districts.FirstOrDefault(d => d.DistrictId == districtId);
            }

            builder.AppendFormat("{0} ", model.Avenue);

            if (!string.IsNullOrWhiteSpace(model.Street))
            {
                builder.AppendFormat("{0} ", model.Street);
            }

            if (!string.IsNullOrWhiteSpace(model.ApartmentNo))
            {
                builder.AppendFormat("No: {0} ", model.ApartmentNo);
            }

            if (!string.IsNullOrWhiteSpace(model.DoorNo))
            {
                builder.AppendFormat("/ {0} ", model.DoorNo);
            }

            builder.AppendFormat("{1} {0} {2} / {3}", model.Locality != null ? model.Locality.LocalityName : "", districtItem != null ? districtItem.ZipCode : "", model.City != null ? model.City.CityName : "-", model.Country != null ? model.Country.CountryName : "-");

            return builder.ToString();
        }
    }

    public enum Ordertype : byte
    {
        Havale = 1,
        KrediKarti = 2,
        KrediKartiVade = 3,
        HavaleTaksit = 4
    }
    public enum SeoDefinitionType : byte
    {
        Category = 1,
        StoreCategory = 3
    }
    public enum FastMembershipType : byte
    {
        Normal = 5,
        Facebook = 10,
        Phone = 20,
        ProductComplain = 30
    }
    public enum PacketStatu
    {
        Inceleniyor = 1,
        Onaylandi = 2,
        Onaylanmadi = 3,
        Silindi = 4
    }


    [Flags]
    public enum DisplayType : byte
    {
        Window,
        List,
        Text
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
    public enum MemberType
    {
        FastIndividual = 5,
        Individual = 10,
        Enterprise = 20
    }
    [Flags]
    public enum MailLogId
    {
        HızlıUyeler = 1,
        BireyselUyeler = 2,
        FirmaUyeleri = 3,
        TumUyeler = 4
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
        EWewsletter
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
        MainSlider = 13,
        CategorySlider = 14
    }
    public enum PropertieType : byte
    {
        Editor = 1,
        Text = 2,
        MutipleOption = 3
    }
    public enum BannerImageType
    {
        Pc = 0,
        Tablet = 1,
        Mobile = 2
    }
    public enum StoreNewType : byte
    {
        Normal = 1,
        SuccessStories = 2
    }
    [Flags]
    public enum StoreType : byte
    {
        Anonim = 1,
        Limited = 2
    }

    public enum GsmType
    {
        Vodafone = 1,
        Avea = 2,
        Turkcell = 3
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
    public enum DealerType
    {
        Bayii = 1,
        YetkiliServis = 2,
        Sube = 3
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
        MessageType = 13,
        StoreactivationType = 14,
        MemberType = 15,
        Mensei = 16,
        SiparisDurumu = 17,
        MemberMail = 18,
        Katsayilar = 19,
        Messages = 20,
        Birim = 21,
        FootorDegerleri = 22,
        DiscountPacketDescription = 23,
        ProductPriceType = 24,
        StoreSpecialMailFile = 25,
        StoreDescriptionType = 26,
        UserSpecialMailType = 27,
        ProblemType = 28,
        StoreProfileHomeDecriptionTemplate = 29,
        PaymentBank = 30,
        SeoDescriptionTitle = 31
    }
    public enum MobileMessageType : byte
    {
        Normal = 1,
        Whatsapp = 2
    }
    public enum ProductPriceType : byte
    {
        Price = 238,
        PriceRange = 239,
        PriceAsk = 240,
        PriceDiscuss = 241
    }
    [Flags]
    public enum CategoryType_tr : byte
    {
        Marka = 3,
        Seri = 4,
        Model = 5,
        Sektor = 0,
        Ana_Kategori = 2,
        Kategori = 1,
        Ürün_Grubu = 6,
        Alt_Kategori = 7,
        Yardım_Kategori = 8
    }
    [Flags]
    public enum MainCategoryType : byte
    {
        Ana_Kategori = 1,
        Yardim = 2
    }
    [Flags]
    public enum ProductActiveType : byte
    {
        Inceleniyor,
        Onaylandi,
        Onaylanmadi,
        Silindi,
        CopKutusuYeni = 8
    }

    [Flags]
    public enum ProductActive : byte
    {
        Pasif,
        Aktif
    }
    public enum RegistrationType : byte
    {
        Full,
        Pre
    }
    public enum MemberStoreType : byte
    {
        Owner = 1,
        Helper = 2
    }

    public enum MessageType : byte
    {
        Inbox = 0,
        Outbox = 1,
        RecyleBin = 2
    }
    public enum HomePageProductType : byte
    {
        Free = 1,
        Paid = 2
    }

    public enum ProductDiscountType : byte
    {
        Percentage = 1,
        Amount = 2
    }

    public enum UserFileTypes : byte
    {
        Photos = 1,
        IdentityCard = 2,
        Residence = 3,
        IdentityRegister = 4,
        FamilyStatus = 5,
        Military = 6,
        Healht = 7,
        Diploma = 8,
        CriminalReport = 9
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

    //public enum SeoDefinitionEntityType:byte
    //{
    //  Category=1
    //}



    public enum MemberDescriptionOrder : byte
    {
        Defaullt = 0,
        UpdatedDateAsc = 1,
        UpdatedDateDesc = 2
    }
    #region OldEnumType
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
    //public enum AccountFavoritePageType
    //{
    //  Store,
    //  Product
    //}

    //[Flags]
    //public enum ProductPayType : byte
    //{
    //  Onayda = 1,
    //  Odendi = 2,
    //  Onaylanmadi = 3,
    //}

    //public enum ProductSalesType : byte
    //{
    //  Takas = 1,
    //  Vade = 2
    //}

    //[Flags]
    //public enum ProductCargoType : byte
    //{
    //  Alici = 1,
    //  Satisi = 2,
    //}

    //[Flags]
    //public enum MemberTitleType : byte
    //{
    //  Admin,
    //  Marketer,
    //  TechnicalSupport
    //}

    //[Flags]
    //public enum ProductStatu : byte
    //{
    //  Onaylandi = 1,
    //  Onaylanmadi = 2,
    //  YeniGelen = 4,
    //  Satıldı = 8,
    //  Indirimde = 16,
    //  OnayBekleyen = 32
    //}

    public enum Packets : byte
    {
        Standart = 12
    }
    #endregion

}