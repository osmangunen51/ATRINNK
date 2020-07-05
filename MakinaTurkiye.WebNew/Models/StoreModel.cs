
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Media;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Stores;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Models
{

    public class StoreModel
    {
        public StoreModel()
        {
            this.HelpList = new List<MTHelpModeltem>();
        }
        public LeftMenuModel LeftMenu { get; set; }
        public List<MTHelpModeltem> HelpList { get; set; }
        public IEnumerable<Picture> PictureItems { get; set; }
        public int MainPartyId { get; set; }

        public Store Store
        {
            get;
            set;
        }

        public LeftMenuModel LeftMenuModel { get; set; }

        public Member MemberItem
        {
            get;
            set;
        }

        public long SingularViewCount { get; set; }
        public DateTime StoreRecordDate { get; set; }

        //public IEnumerable<RelMainPartyCategory> StoreCategoryItems { get; set; }

        public IEnumerable<Phone> PhoneItems { get; set; }

        public ICollection<CategoryModel> CategoryItems { get; set; }

        //public SelectList SectorItems
        //{
        //    get
        //    {
        //        IList<Category> sectorItems;
        //        using (var entities = new MakinaTurkiyeEntities())
        //        {
        //            sectorItems = entities.Categories.Where(c => c.CategoryParentId == null).OrderBy(c => c.CategoryOrder).OrderBy(c => c.CategoryName).ToList();
        //        }
        //        return new SelectList(sectorItems, "CategoryId", "CategoryName");
        //    }
        //}

        public string GeneralText { get; set; }
        public string HistoryText { get; set; }
        public string FounderText { get; set; }
        public string PhilosophyText { get; set; }

        public IEnumerable<StoreDealer> StoreDealerItems { get; set; }

        [DisplayName("İlçe")]
        public int LocalityId { get; set; }

        [DisplayName("Semt")]
        public int DistrictId { get; set; }

        [DisplayName("Mahalla / Köy")]
        public int TownId { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }

        //[DisplayName("Bölge")]
        public string LocalityName { get; set; }

        [DisplayName("Adres Tipi")]
        public byte AddressTypeId { get; set; }

        [DisplayName("Adres Id")]
        public int AddressId { get; set; }

        [DisplayName("Ülke")]
        public int CountryId { get; set; }

        [DisplayName("Cadde")]
        public string Avenue { get; set; }

        [DisplayName("Sokak")]
        public string Street { get; set; }

        [DisplayName("Kapı No")]
        public string DoorNo { get; set; }

        [DisplayName("Bina No")]
        public string ApartmentNo { get; set; }

        public int StoreDealerId { get; set; }

        public SelectList DealerItemsForBranch { get; set; }
        public SelectList DealerItemsForDealer { get; set; }
        public SelectList DealerItemsForService { get; set; }
        public SelectList StoreDealerServisDDLItems { get; set; }
        public SelectList StoreDealerSubeDDLItems { get; set; }

        public IEnumerable<DealerBrand> DealerBrandItems { get; set; }
        public IEnumerable<StoreBrand> StoreBrandItems { get; set; }

        public SelectList CityItems
        {
            get
            {
                return new SelectList(new[] { new { CityId = 0, CityName = "< Lütfen Seçiniz >" } }, "CityId", "CityName", 1);
            }
        }

        public SelectList CountryItems { get; set; }

        public SelectList LocalityItems
        {
            get
            {
                return new SelectList(new[] { new { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } }, "LocalityId", "Localityname", 1);
            }
        }

        public SelectList TownItems
        {
            get
            {
                return new SelectList(new[] { new { TownId = 0, TownName = "< Lütfen Seçiniz >" } }, "TownId", "TownName", 1);
            }
        }

        public SelectList DistrictItems
        {
            get
            {
                return new SelectList(new[] { new { DistrictId = 0, DistrictNameZipCode = "< Lütfen Seçiniz >" } }, "DistrictId", "DistrictNameZipCode", 1);
            }
        }

        public SelectList AddressTypeItems
        {
            get
            {
                var curAddressType = new Classes.AddressType();
                return new SelectList(curAddressType.GetDataTable().DefaultView, "AddressTypeId", "AddressTypeName", 0);
            }
        }

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

        public IEnumerable<Address> AddressItems { get; set; }
        public IList<Address> DealerBayiiAddressItems { get; set; }
        public IList<Address> DealerServisAddressItems { get; set; }
        public IList<Address> DealerSubeAddressItems { get; set; }

        public IList<Address> DealerAddressItems { get; set; }

        public bool HasAboutUs { get; set; }
        public bool HasServices { get; set; }
        public bool HasBranch { get; set; }
        public bool HasBrand { get; set; }
        public bool HasDealer { get; set; }
        public bool HasDealership { get; set; }

        public City cityItem { get; set; }
        public Locality localityItem { get; set; }
        public string CityName { get; set; }

        public IList<StoreActivityCategory> StoreActivityCategory { get; set; }

        public string ActivityTypeText { get; set; }
        public string ProductCategoryText { get; set; }

        public string StoreTypeName { get; set; }
        public string StoreEmployeesCountName { get; set; }
        public string StoreEndorsementName { get; set; }
        public string StoreCapitalName { get; set; }

        public IEnumerable<ActivityType> ActivityItems { get; set; }
        public IEnumerable<StoreActivityType> StoreActivityItems { get; set; }

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
        public string StoreWeb { get; set; }
        public string StoreName { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string MemberTitle { get; set; }
        public string MemberEmail { get; set; }
        public string PhoneText { get; set; }

        public IList<Phone> StorePhoneItems
        {
            get
            {
                IList<Phone> phoneItems = new List<Phone>();
                string[] phones = PhoneText.Split('|');
                foreach (var item in phones)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        string[] items = item.Split(',');
                        var phone = new Phone
                        {
                            PhoneId = Convert.ToInt32(items.GetValue(0).ToString().Replace("PhoneId=", "")),
                            PhoneCulture = items.GetValue(3).ToString().Replace("PhoneCulture=", ""),
                            PhoneAreaCode = items.GetValue(4).ToString().Replace("PhoneAreaCode=", ""),
                            PhoneNumber = items.GetValue(5).ToString().Replace("PhoneNumber=", ""),
                            PhoneType = Convert.ToByte(items.GetValue(6).ToString().Replace("PhoneType=", "")),
                            GsmType = items.GetValue(7).ToString().Replace("GsmType=", "").ToString() != "" ? Convert.ToByte(items.GetValue(7).ToString().Replace("GsmType=", "").ToString()) : Convert.ToByte(0)
                        };
                        phoneItems.Add(phone);
                    }
                }
                return phoneItems;
            }
        }
        public string StoreLogoThumb
        {
            get
            {
                return !string.IsNullOrEmpty(this.StoreLogo) && this.StoreLogo.Contains(".") ? ("/UserFiles/Images/StoreLogo/" + this.StoreLogo.Replace(".", "_th.")) : "";
            }
        }


        public string StoreLogoFullPath
        {
            get
            {
                return !string.IsNullOrEmpty(this.StoreLogo) && this.StoreLogo.Contains(".") ? ("/UserFiles/Images/StoreLogo/" + this.StoreLogo) : "";
            }
        }

            public string StoreBannerThumb
        {
            get
            {
                return !string.IsNullOrEmpty(this.StoreLogo) && this.StoreLogo.Contains(".") ? ("/UserFiles/Images/StoreBanner/" + this.StoreLogo.Replace(".", "_th.")) : "";
            }
        }

        public string StoreBannerFullPath
        {
            get
            {
                return !string.IsNullOrEmpty(this.StoreLogo) && this.StoreLogo.Contains(".") ? ("/UserFiles/Images/StoreBanner/" + this.StoreLogo) : "";
            }
        }

        public string StoreLogo { get; set; }
        public string StoreBanner { get; set; }

        public string StoreAbout { get; set; }

    }
}