namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Core;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Mvc;
    using Validation;

    public class MemberModel
    {
        public bool hasStore { get; set; }
        public int MainPartyId { get; set; }

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
        public DateTime BirthDate { get; set; }

        [DisplayName("Sektör E-Posta")]
        public bool ReceiveEmail { get; set; }

        public int MemberMainPartyId { get; set; }
        public int StoreMainPartyId { get; set; }

        public IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.Entities.Category> SectorItems { get; set; }

        public IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.Entities.Category> ParentItems(int CategoryId)
        {
            var entities = new MakinaTurkiyeEntities();
            var ids = (from c in entities.Categories where c.CategoryParentId == CategoryId select c.CategoryId);
            return from c in entities.Categories where ids.Contains(c.CategoryParentId.Value) select c;
        }

        public ICollection<RelMainPartyCategoryModel> MainPartyRelatedSectorItems { get; set; }
        public ICollection<CategoryModel> CategoryItems { get; set; }
        public string MemberTypeText { get; set; }
        public int FastMemberShipType { get; set; }
        public string MainPartyFullName { get; set; }
        public DateTime MainPartyRecordDate { get; set; }
        public string Active_Text { get; set; }

        public string StoreName { get; set; }
        public string StoreEMail { get; set; }

        public ICollection<CategoryModel> CategoryParentItemsByCategoryId(int CategoryId)
        {
            var dataCategory = new Data.Category();
            return dataCategory.GetCategoryParentByCategoryId(CategoryId, (byte)MainCategoryType.Ana_Kategori).AsCollection<CategoryModel>();
        }

        [DisplayName("İlçe")]
        public int LocalityId { get; set; }

        [DisplayName("Mahalle / Köy")]
        public int TownId { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }

        [DisplayName("Bölge")]
        public string LocalityName { get; set; }


        [DisplayName("Adres Id")]
        public int AddressId { get; set; }

        [DisplayName("Ülke")]
        public int CountryId { get; set; }

        [DisplayName("Varsayılan Adres")]
        public bool AddressDefault { get; set; }

        [DisplayName("Telefon Id")]
        public int PhoneId { get; set; }

        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }

        [DisplayName("Telefon Tipi")]
        public byte PhoneType { get; set; }


        [DisplayName("Cadde")]
        public string Avenue { get; set; }

        [DisplayName("Sokak")]
        public string Street { get; set; }

        [DisplayName("Kapı No")]
        public string DoorNo { get; set; }

        [DisplayName("Bina No")]
        public string ApartmentNo { get; set; }

        [DisplayName("Adres Tipi")]
        public byte AddressTypeId { get; set; }

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

        [DisplayName("Fax")]
        public string InstitutionalFaxNumber { get; set; }
        public string InstitutionalFaxCulture { get; set; }
        public string InstitutionalFaxAreaCode { get; set; }

        public SelectList AddressTypeItems
        {
            get
            {
                var curAddressType = new Classes.AddressType();
                var addressItems = curAddressType.GetDataTable().AsCollection<AddressTypeModel>().ToList();
                addressItems.Insert(0, new AddressTypeModel { AddressTypeId = 0, AddressTypeName = "< Lütfen Seçiniz >" });
                return new SelectList(addressItems, "AddressTypeId", "AddressTypeName", 0);
            }
        }

        private SelectList myCityItems;
        public SelectList CityItems
        {
            get
            {
                if (myCityItems == null || myCityItems.Count() <= 0)
                {
                    var dataAddress = new Data.Address();
                    var cityItems = dataAddress.CityGetItemByCountryId(AppSettings.Turkiye).AsCollection<CityModel>().ToList();
                    cityItems.Insert(0, new CityModel { CityId = 0, CityName = "< Lütfen Seçiniz >" });
                    myCityItems = new SelectList(cityItems, "CityId", "CityName");
                }
                return myCityItems;
            }
            set { myCityItems = value; }
        }


        private SelectList myCountryItems;
        public SelectList CountryItems
        {
            get
            {
                if (myCountryItems == null || myCountryItems.Count() <= 0)
                {
                    var curCountry = new Classes.Country();
                    myCountryItems = new SelectList(curCountry.GetDataTable().DefaultView, "CountryId", "CountryName", 0);
                }
                return myCountryItems;
            }
            set { myCountryItems = value; }
        }

        private SelectList myLocalityItems;
        public SelectList LocalityItems
        {
            get
            {
                if (myLocalityItems == null || myLocalityItems.Count() == 0)
                {
                    return new SelectList(new[] { new { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } }, "LocalityId", "LocalityName", 1);
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
                if (myTownItems == null || myTownItems.Count() == 0)
                {
                    myTownItems = new SelectList(new[] { new { TownId = 0, TownName = "< Lütfen Seçiniz >" } }, "TownId", "TownName", 1);
                }
                return myTownItems;
            }
            set { myTownItems = value; }
        }

        public IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.Entities.Phone> PhoneItems { get; set; }
        public byte GsmType { get; set; }

        public NeoSistem.MakinaTurkiye.Management.Models.Entities.Address Address { get; set; }
    }
}