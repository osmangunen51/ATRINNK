namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.EnterpriseEntity.Business;
    using NeoSistem.MakinaTurkiye.Web.Models.Validation;

    //[PropertiesMustMatch("MemberPassword", "MemberPasswordAgain", ErrorMessage = "Yeni şifre ve onay şifre eşleşmiyor.")]
    //[PropertiesMustMatch("MemberEmail", "MemberEmailAgain", ErrorMessage = "E-Posta adresleri eşleşmiyor")]
    public class MembershipModel
    {
        public string mailTitle { get; set; }

        public string mailDescription { get; set; }

        public bool ReceiveEmail { get; set; }

        public int MainPartyId { get; set; }

        public string MainPartyFullName { get; set; }
        public byte MainPartyType { get; set; }
        public bool Active { get; set; }
        public DateTime MainPartyRecordDate { get; set; }


        [DisplayName("Üyelik Tipi")]
        public byte MemberType { get; set; }

        [DisplayName("Adınız")]
        [RequiredValidation]
        public string MemberName { get; set; }

        [DisplayName("Soyadınız")]
        [RequiredValidation]
        public string MemberSurname { get; set; }

        [DisplayName("E-Posta Adresiniz")]
        [DataType(DataType.EmailAddress)]
        [RequiredValidation]
        public string MemberEmail { get; set; }

        [DisplayName("E-Posta Adresiniz (Tekrar)")]
        [DataType(DataType.EmailAddress)]
        public string MemberEmailAgain { get; set; }

        [DisplayName("Şifreniz")]
        [DataType(DataType.Password)]
        public string MemberPassword { get; set; }

        [DisplayName("Şifre (Tekrar)")]
        [DataType(DataType.Password)]
        public string MemberPasswordAgain { get; set; }

        [DisplayName("Cinsiyet")]
        public bool Gender { get; set; }

        [DisplayName("Doğum Tarihiniz")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Üyelim Durumu")]
        public bool MemberStatu { get; set; }

        [DisplayName("Satın Alma Departmanı Ad Soyad")]
        public string PurchasingDepartmentName { get; set; }

        [DisplayName("Satın Alma Departmanı E-Posta")]
        public string PurchasingDepartmentEmail { get; set; }
        [Required]
        [DisplayName("İlçe")]
        public int LocalityId { get; set; }
        [Required]
        [DisplayName("Mahalle / Köy")]
        public int TownId { get; set; }
        [Required]
        [DisplayName("Şehir")]
        public int CityId { get; set; }
        //[Required]
        [DisplayName("Bölge")]
        public string LocalityName { get; set; }


        [DisplayName("Adres Id")]
        public int AddressId { get; set; }
        [Required]
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

        [DisplayName("Firma Ünvanı")]
        public string StoreName { get; set; }
        [DisplayName("Firma Adı")]
        public string StoreShortName { get; set; }

        [DisplayName("Firma Url")]
        public string StoreUrlName { get; set; }

        [DisplayName("Firma Web Adresi")]
        public string StoreWeb { get; set; }

        [DisplayName("Logo")]
        public string StoreLogo { get; set; }

        public byte StoreActiveType { get; set; }

        [DisplayName("Paket Başlama Tarihi")]
        public DateTime StorePacketBeginDate { get; set; }

        [DisplayName("Paket Bitiş Tarihi")]
        public DateTime StorePacketEndDate { get; set; }

        [DisplayName("Mağaza Hakkında")]
        public string StoreAbout { get; set; }

        public DateTime StoreRecordDate { get; set; }

        [DisplayName("Kuruluş Yılı")]
        public int? StoreEstablishmentDate { get; set; }

        [DisplayName("Mağaza Durumu")]
        public byte StoreActivityType { get; set; }

        [DisplayName("Şirket Sermayesi")]
        public byte StoreCapital { get; set; }
        public string StoreCapitalName { get; set; }

        [DisplayName("Çalışan Sayısı")]
        public byte StoreEmployeesCount { get; set; }
        public string StoreEmployeesCountName { get; set; }

        [DisplayName("Yıllık Ciro")]
        public byte StoreEndorsement { get; set; }
        public string StoreEndorsementName { get; set; }

        [DisplayName("Vergi Dairesi")]
        public string TaxOffice { get; set; }

        [DisplayName("Vergi Numarası")]
        public string TaxNumber { get; set; }
        [DisplayName("Firma Yetkili Ünvanı")]
        public byte StoreAuthorizedTitleType { get; set; }

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
        public string GsmWhatsappAreaCode { get; set; }
        public string GsmWhatsappCulture { get; set; }
        public string GsmWhatsappNumber { get; set; }

        public bool IsGsmWhatsapp { get; set; }
        public byte GsmType { get; set; }

        public byte GsmType2 { get; set; }

        private string[] myActivityName;
        public string[] ActivityName
        {
            get
            {
                if (myActivityName == null)
                {
                    myActivityName = new string[] { };
                }
                return myActivityName;
            }
            set { myActivityName = value; }
        }

        private string[] myStoreRelatedCategory;
        public string[] StoreRelatedCategory
        {
            get
            {
                if (myStoreRelatedCategory == null)
                {
                    myStoreRelatedCategory = new string[] { };
                }
                return myStoreRelatedCategory;
            }
            set { myStoreRelatedCategory = value; }
        }

        private string[] myStoreActivityCategory;
        public string[] StoreActivityCategory
        {
            get
            {
                if (myStoreActivityCategory == null)
                {
                    myStoreActivityCategory = new string[] { };
                }
                return myStoreActivityCategory;
            }
            set { myStoreActivityCategory = value; }
        }


        private string[] myMemberRelatedSectorItems;
        public string[] MemberRelatedSectorItems
        {
            get
            {
                if (myMemberRelatedSectorItems == null)
                {
                    myMemberRelatedSectorItems = new string[] { };
                }
                return myMemberRelatedSectorItems;
            }
            set { myMemberRelatedSectorItems = value; }
        }

        [DisplayName("Şirket Türü")]
        public byte StoreType { get; set; }

        [DisplayName("Cadde")]
        public string Avenue { get; set; }

        public string AvenueOtherCountries { get; set; }

        [DisplayName("Sokak")]
        public string Street { get; set; }
        [DisplayName("Posta Kodu")]
        public string PostCode { get; set; }

        [DisplayName("Kapı No")]
        public string DoorNo { get; set; }

        [DisplayName("Bina No")]
        public string ApartmentNo { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        [DisplayName("Yetkili")]
        public byte MemberTitleType { get; set; }
        [Required]
        [DisplayName("Adres Tipi")]
        public byte AddressTypeId { get; set; }

        public string AddressTypeName { get; set; }

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

        public SelectList CurrencyItems
        {
            get
            {
                var curCurrency = new Classes.Currency();
                var currencyItems = curCurrency.GetDataTable().AsCollection<CurrencyModel>().ToList();
                return new SelectList(currencyItems, "CurrencyId", "CurrencyName");
            }
        }

        public SelectList StoreTypeItems
        {
            get
            {
                var dataConstant = new Data.Constant();
                var storeTypeItems = dataConstant.ConstantGetByConstantType((byte)ConstantType.StoerType).AsCollection<ConstantModel>().ToList();
                storeTypeItems.Insert(0, new ConstantModel { ConstantId = 0, ConstantName = "< Seçiniz >", ConstantType = 0 });
                return new SelectList(storeTypeItems, "ConstantId", "ConstantName");
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

        public SelectList StoreAuthorizedTitleTypeItems
        {
            get
            {
                var dataConstant = new Data.Constant();
                var storeAuthorizedTitleTypeItems = dataConstant.ConstantGetByConstantType((byte)ConstantType.MemberTitleType).AsCollection<ConstantModel>().ToList();
                storeAuthorizedTitleTypeItems.Insert(0, new ConstantModel { ConstantId = 0, ConstantName = "< Seçiniz >", ConstantType = 0 });
                return new SelectList(storeAuthorizedTitleTypeItems, "ConstantId", "ConstantName");
            }
        }

        public IEnumerable<string> GetActivityNames()
        {
            using (var transaction = new TransactionUI())
            {
                var activity = new Classes.ActivityType();
                bool hasRecord = false;

                foreach (var item in this.ActivityName)
                {
                    hasRecord = activity.LoadEntity(item.ToByte());
                    if (hasRecord)
                    {
                        yield return activity.ActivityName;
                    }
                }
            }
        }

        public IEnumerable<string> GetRelatedCategory()
        {
            using (var transaction = new TransactionUI())
            {
                var category = new Classes.Category();
                bool hasRecord = false;

                foreach (var item in this.StoreActivityCategory)
                {
                    if (item != "false")
                    {
                        hasRecord = category.LoadEntity(item.ToInt32());
                        if (hasRecord)
                        {
                            yield return category.CategoryName;
                        }
                    }
                }

            }
        }

    }
}
