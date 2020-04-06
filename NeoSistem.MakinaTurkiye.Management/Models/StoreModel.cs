namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using NeoSistem.MakinaTurkiye.Management.Models.Validation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class StoreModel
    {
        public StoreModel()
        {
            this.Users = new List<SelectListItem>();
            this.PortfoyUsers = new List<SelectListItem>();

        }
        public int MainPartyId { get; set; }
        public int MemberMainPartyId { get; set; }
        public Member member { get; set; }
        [DisplayName("Firma Ünvanı")]
        [RequiredValidation, StringLengthValidation(200)]
        public string StoreName { get; set; }

        [DisplayName("Firma No")]
        public string StoreNo { get; set; }

        [DisplayName("Firma Ürün Satış")]
        public bool ReadyForSale { get; set; }

        //[DisplayName("E-Posta")]
        //[RequiredValidation, StringLengthValidation(320), EmailValidation]
        public string StoreEMail { get; set; }

        [DisplayName("Web Adresi")]
        public string StoreWeb { get; set; }

        [DisplayName("Logo")]
        public string StoreLogo { get; set; }

        [DisplayName("Paket Durumu")]
        public byte? StoreActiveType { get; set; }

        [DisplayName("Şirket Türü")]
        public byte StoreType { get; set; }

        [DisplayName("Paket")]
        public int PacketId { get; set; }

        [DisplayName("Paket Başlangıç Tarihi")]
        public DateTime? StorePacketBeginDate { get; set; }

        [DisplayName("Paket Bitiş Tarihi")]
        public DateTime? StorePacketEndDate { get; set; }

        [DisplayName("Kısa Detay")]
        [RequiredValidation]
        public string StoreAbout { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [RequiredValidation, DataType(DataType.DateTime)]
        public DateTime StoreRecordDate { get; set; }

        [DisplayName("Paket Adi")]
        public string PacketName { get; set; }

        public string StatuText { get; set; }

        [DisplayName("Ciro")]
        public byte StoreEndorsement { get; set; }

        [DisplayName("Üyelik Durumu")]
        public byte PacketStatu { get; set; }

        [DisplayName("Görüntülenme")]
        public long ViewCount { get; set; }

        [DisplayName("Tekil Görüntülenme")]
        public long SingularViewCount { get; set; }

        [DisplayName("Vitrin")]
        public bool StoreShowcase { get; set; }

        [DisplayName("VD")]
        public string TaxOffice { get; set; }

        [DisplayName("VN")]
        public string TaxNumber { get; set; }
        [DisplayName("Mersis N")]
        public string MersisNo { get; set; }
        [DisplayName("Ticaret Sicil N")]
        public string TradeRegistrNo { get; set; }
        public int AuthorizedId { get; set; }
        public int PortfoyUserId { get; set; }
        public string CurrencyName { get; set; }
        public string StoreUrlName { get; set; }
        public string StoreShortName { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> PortfoyUsers { get; set; }
        public bool IsWhatsappNotUsing { get; set; }
        public int WhatsappClickCount { get; set; }
        public bool WhatsappActive { get; set; }
        public string AuthName { get; set; }
        public string PortfoyUserName { get; set; }
        public string GroupName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyword { get; set; }
        public NeoSistem.MakinaTurkiye.Management.Models.Entities.Address Address { get; set; }

        public IEnumerable<Phone> PhoneItems { get; set; }

        public SelectList CurrencyItems
        {
            get
            {
                var curCurrency = new Classes.Currency();
                var currencyItems = curCurrency.GetDataTable().AsCollection<CurrencyModel>().ToList();
                currencyItems.Insert(0, new CurrencyModel { CurrencyId = 0, CurrencyName = "< Seçiniz >" });
                return new SelectList(currencyItems, "CurrencyId", "CurrencyName");
            }
        }


        public ICollection<CategoryModel> CategoryParentItemsByCategoryId(int CategoryId)
        {
            var dataCategory = new Data.Category();
            return dataCategory.GetCategoryParentByCategoryId(CategoryId, (byte)MainCategoryType.Ana_Kategori).AsCollection<CategoryModel>();
        }

        [DisplayName("Çalışan Sayısı")]
        public byte StoreEmployeesCount { get; set; }

        [DisplayName("Sermaye")]
        [RequiredValidation]
        public byte StoreCapital { get; set; }

        public SelectList StoreCapitalItems
        {
            get
            {
                var dataConstant = new Data.Constant();
                var storeCapitalItems = dataConstant.ConstantGetByConstantType((byte)ConstantType.StoreCapital).AsCollection<ConstantModel>().ToList();
                storeCapitalItems.Insert(0, new ConstantModel { ConstantId = 0, ConstantName = "< Seçiniz >", ConstantType = 0 });
                return new SelectList(storeCapitalItems, "ConstantId", "ConstantName");
            }
        }


        public SelectList StoreEndorsementItems
        {
            get
            {
                var dataConstant = new Data.Constant();
                var storeEndorsementItems = dataConstant.ConstantGetByConstantType((byte)ConstantType.StoreEndorsement).AsCollection<ConstantModel>().ToList();
                storeEndorsementItems.Insert(0, new ConstantModel { ConstantId = 0, ConstantName = "< Seçiniz >", ConstantType = 0 });
                return new SelectList(storeEndorsementItems, "ConstantId", "ConstantName");
            }
        }

        public SelectList EmployeesCountItems
        {
            get
            {
                var dataConstant = new Data.Constant();
                var employeesCount = dataConstant.ConstantGetByConstantType((byte)ConstantType.EmployeesCount).AsCollection<ConstantModel>().ToList();
                employeesCount.Insert(0, new ConstantModel { ConstantId = 0, ConstantName = "< Seçiniz >", ConstantType = 0 });
                return new SelectList(employeesCount, "ConstantId", "ConstantName");
            }
        }

        [DisplayName("Kuruluş Yılı")]
        public int? StoreEstablishmentDate { get; set; }

        [DisplayName("Şirket Kısa Açıklama")]
        [RequiredValidation, StringLengthValidation(500)]
        public string StoreDescription { get; set; }

        [DisplayName("Satın Alma Ad Soyad")]
        [RequiredValidation, StringLengthValidation(50)]
        public string PurchasingDepartmentName { get; set; }

        [DisplayName("Satın Alma E-Posta")]
        [RequiredValidation, StringLengthValidation(320)]
        public string PurchasingDepartmentEmail { get; set; }

        public SelectList StorePacketItems
        {
            get
            {
                var packet = new Classes.Packet();
                var list = packet.GetDataSet().Tables[0].AsCollection<PacketModel>();
                return new SelectList(list, "PacketId", "PacketName", this.PacketId);
            }
        }

        public string StorePacketName
        {
            get
            {
                var packet = new Classes.Packet();
                var packetColl = packet.GetDataSet().Tables[0].AsCollection<PacketModel>().Where(p => p.PacketId == PacketId);
                string packetName = "";

                if (packetColl.Count() > 0)
                {
                    packetName = packetColl.First().PacketName;
                }
                return packetName;
            }
        }

        public SelectList StoreTypeItems
        {
            get
            {
                var dataConstant = new Data.Constant();
                var model = dataConstant.ConstantGetByConstantType((byte)ConstantType.StoerType).AsCollection<ConstantModel>().ToList();
                model.Insert(0, new ConstantModel { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
                return new SelectList(model, "ConstantId", "ConstantName");
            }
        }

        public SelectList PacketItems
        {
            get
            {
                var entities = new MakinaTurkiyeEntities();
                var packetItems = entities.Packets.ToList();
                return new SelectList(packetItems, "PacketId", "PacketName");
            }
        }

        public SelectList StoreActiveTypeItems
        {
            get
            {
                return new SelectList(new[]
                {
           new
            {
              StoreActiveType = 1,
              StoreActiveTypeName = "İnceleniyor"
            },
            new
            {
              StoreActiveType = 2,
              StoreActiveTypeName = "Onaylandı"
            },
            new
            {
              StoreActiveType = 3,
              StoreActiveTypeName = "Onaylanmadı"
            }
            ,
            new
            {
              StoreActiveType = 4,
              StoreActiveTypeName = "Süresi Geçti"
            }
         }
                , "StoreActiveType", "StoreActiveTypeName", 1);
            }
        }

        public SelectList PacketStatuItems
        {
            get
            {
                return new SelectList(new[]
                {
            new
            {
              PacketStatuType = 0,
              PacketStatuName = "İnceleniyor"
            },
            new
            {
              PacketStatuType = 1,
              PacketStatuName = "Onaylandı"
            }
            ,
            new
            {
              PacketStatuType = 2,
              PacketStatuName = "Onaylanmadı"
            }
            ,
            new
            {
              PacketStatuType = 3,
              PacketStatuName = "Silindi"
            }
         }
                , "PacketStatuType", "PacketStatuName", 1);
            }
        }

        [DisplayName("Ülke")]
        public int CountryId { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }

        public IList<Address> DealerAddressItems { get; set; }
        public ICollection<ActivityTypeModel> ActivityTypeItems { get; set; }
        public ICollection<StoreActivityTypesModel> StoreActivityTypeItems { get; set; }
        public ICollection<CategoryModel> SectorItems { get; set; }
        public ICollection<CategoryModel> CategoryItems { get; set; }
        public ICollection<RelMainPartyCategoryModel> MainPartyRelatedCategoryItems { get; set; }
        public IEnumerable<StoreActivityCategory> StoreActivityCategory { get; set; }
        public IList<StoreDealer> StoreDealerItems { get; set; }
        public SelectList DealerItemsForBranch { get; set; }
        public SelectList DealerItemsForDealer { get; set; }
        public SelectList DealerItemsForService { get; set; }
        public IList<Picture> PictureItems { get; set; }

        public ICollection<CategoryModel> CategoryGroupParentItemsByCategoryId(int CategoryId)
        {
            var dataCategory = new Data.Category();
            return dataCategory.CategoryGetSectorItemsByCategoryParent(CategoryId).AsCollection<CategoryModel>();
        }

        public bool IsImage
        {
            get
            {
                return StoreLogo != String.Empty;
            }
        }

        public string MainPartyFullName { get; set; }

        public string GeneralText { get; set; }
        public string HistoryText { get; set; }
        public string FounderText { get; set; }
        public string PhilosophyText { get; set; }
        public string StoreProfileHomeDescription { get; set; }

        //public ICollection<PictureModel> StoreDealerPictureItems(int StoreDealerId)
        //{
        //  var dataPicture = new Data.Picture();
        //  return dataPicture.GetItemsByStoreDealerId(StoreDealerId).AsCollection<PictureModel>();
        //}

        public IEnumerable<AddressModel> DealerBayiiAddressItems { get; set; }
        public IEnumerable<AddressModel> DealerServisAddressItems { get; set; }
        public IEnumerable<AddressModel> DealerSubeAddressItems { get; set; }

        public IEnumerable<AddressModel> AddressItems { get; set; }

        public SelectList StoreDealerBayiiDDLItems { get; set; }
        public SelectList StoreDealerServisDDLItems { get; set; }
        public SelectList StoreDealerSubeDDLItems { get; set; }

        public IList<DealerBrand> DealerBrandItems { get; set; }
        public IList<StoreBrand> StoreBrandItems { get; set; }


        private SelectList myCountryItems;
        public SelectList CountryItems
        {
            get
            {
                var entities = new MakinaTurkiyeEntities();
                if (myCountryItems == null || myCountryItems.Count() <= 0)
                {
                    var curCountry = new Classes.Country();
                    var country = entities.Countries.OrderBy(c => c.CountryOrder).ThenBy(n => n.CountryName).ToList();
                    country.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });
                    myCountryItems = new SelectList(country, "CountryId", "CountryName", 0);
                }
                return myCountryItems;
            }
            set { myCountryItems = value; }
        }

        public string LocalityName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        private SelectList myCityItems;
        public SelectList CityItems
        {
            get
            {
                if (myCityItems == null || myCityItems.Count() <= 0)
                {
                    myCityItems = new SelectList(new[] { new { CityId = 0, CityName = "< Lütfen Seçiniz >" } }, "CityId", "CityName", 1);
                }
                return myCityItems;
            }
            set { myCityItems = value; }
        }

        private SelectList myLocalityItems;
        public SelectList LocalityItems
        {
            get
            {
                if (myLocalityItems == null || myLocalityItems.Count() <= 0)
                {
                    myLocalityItems = new SelectList(new[] { new { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } }, "LocalityId", "LocalityName", 1);
                }
                return myLocalityItems;
            }
            set { myLocalityItems = value; }
        }

        private SelectList myTownItems;
        public SelectList TownItems
        {
            get
            {
                if (myTownItems == null || myTownItems.Count() <= 0)
                {
                    myTownItems = new SelectList(new[] { new { TownId = 0, TownName = "< Lütfen Seçiniz >" } }, "TownId", "TownName", 1);
                }
                return myTownItems;
            }
            set { myTownItems = value; }
        }

        [DisplayName("Adres Tipi")]
        public byte AddressTypeId { get; set; }

        public string AvenueOtherCountries { get; set; }

        public SelectList AddressTypeItems
        {
            get
            {
                var entities = new MakinaTurkiyeEntities();
                var addressTypeItems = entities.AddressTypes.AsEnumerable().ToList();
                addressTypeItems.Insert(0, new AddressType { AddressTypeId = 0, AddressTypeName = "< Lütfen Seçiniz >" });

                return new SelectList(addressTypeItems, "AddressTypeId", "AddressTypeName", 0);
            }
        }

        public SelectList MemberTitleTypeItems
        {
            get
            {
                var entities = new MakinaTurkiyeEntities();
                var model = entities.Constants.Where(c => c.ConstantType == (byte)ConstantType.MemberTitleType).ToList();
                model.Insert(0, new Constant { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
                return new SelectList(model, "ConstantId", "ConstantName", 0);
            }
        }

        [DisplayName("İlçe")]
        public int LocalityId { get; set; }

        [DisplayName("Mahalla / Köy")]
        public int TownId { get; set; }

        [DisplayName("Cadde")]
        public string Avenue { get; set; }

        [DisplayName("Sokak")]
        public string Street { get; set; }

        [DisplayName("Kapı No")]
        public string DoorNo { get; set; }

        [DisplayName("Bina No")]
        public string ApartmentNo { get; set; }

        public string InstitutionalPhoneAreaCode { get; set; }
        public string InstitutionalPhoneAreaCode2 { get; set; }
        public string InstitutionalPhoneCulture { get; set; }
        public string InstitutionalPhoneCulture2 { get; set; }
        [DisplayName("Telefon (1)")]
        public string InstitutionalPhoneNumber { get; set; }
        [DisplayName("Telefon (2)")]
        public string InstitutionalPhoneNumber2 { get; set; }

        public string InstitutionalGSMAreaCode { get; set; }
        public string InstitutionalGSMAreaCode2 { get; set; }
        public string InstitutionalGSMCulture { get; set; }
        public string InstitutionalGSMCulture2 { get; set; }
        [DisplayName("GSM (1)")]
        public string InstitutionalGSMNumber { get; set; }
        [DisplayName("GSM (2)")]
        public string InstitutionalGSMNumber2 { get; set; }
        public byte GsmType { get; set; }
        [DisplayName("Fax")]
        public string InstitutionalFaxNumber { get; set; }
        public string InstitutionalFaxCulture { get; set; }
        public string InstitutionalFaxAreaCode { get; set; }

        public string InstitutionalWGSMNumber { get; set; }
        public string InstitutionalWGSMCulture { get; set; }
        public string InstitutionalWGSMAreaCode { get; set; }

        [DisplayName("Üyelik Tipi")]
        public byte MemberType { get; set; }

        [RequiredValidation, StringLengthValidation(20)]
        [DisplayName("Adi")]
        public string MemberName { get; set; }

        [DisplayName("Üye No")]
        public string MemberNo { get; set; }

        [RequiredValidation, StringLengthValidation(30)]
        [DisplayName("Soyadi")]
        public string MemberSurname { get; set; }

        [RequiredValidation, StringLengthValidation(320), EmailValidation]
        [DisplayName("E-Posta")]
        public string MemberEmail { get; set; }

        [RequiredValidation, StringLengthValidation(15)]
        [DisplayName("Parola")]
        public string MemberPassword { get; set; }

        [DisplayName("Yetki Tipi")]
        public byte MemberTitleType { get; set; }

        [DisplayName("Üye Durumu")]
        public bool Active { get; set; }

        [DisplayName("Cinsiyet")]
        public bool Gender { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Sektör E-Posta")]
        public bool ReceiveEmail { get; set; }

        public int MainPartyIdEposta { get; set; }

        public string Eposta1 { get; set; }
        public string Eposta2 { get; set; }
        public string Ad1 { get; set; }
        public string Ad2 { get; set; }
        public string SoyAd1 { get; set; }
        public string SoyAd2 { get; set; }
        public bool Email1Check { get; set; }
        public bool Email2Check { get; set; }
        public string LogingLink { get; set; }
        public MainPartyIdEposta EpostaMainPartyId { get; set; }
        public bool? IsAllowProductSellUrl { get; set; }

        [DisplayName("Firma Sub Domain İsmi")]
        [StringLengthValidation(50)]
        public string StoreUniqueShortName { get; set; }

    }
}