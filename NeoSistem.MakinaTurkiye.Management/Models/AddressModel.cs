namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;

    [Bind(Exclude = "AddressId")]
    public class AddressModel
    {
        public string AddressFormName { get; set; }
        public string AddressListName { get; set; }

        public bool hasStore { get; set; }

        public int AddressId { get; set; }

        public int MainPartyId { get; set; }

        [DisplayName("Ülke")]
        public int CountryId { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }

        [DisplayName("İlçe")]
        public int LocalityId { get; set; }

        [DisplayName("Mahalle / Köy")]
        public int TownId { get; set; }

        public SelectList Cities
        {
            get
            {
                var cities = new Classes.City();
                var items = cities.GetDataSet().Tables[0].AsCollection<CityModel>().Where(c => c.CountryId == CountryId);
                return new SelectList(items, "CityId", "CityName", CityId);
            }
        }

        public SelectList Countries
        {
            get
            {
                var countries = new Classes.Country();
                var items = countries.GetDataSet().Tables[0].AsCollection<CountryModel>();
                return new SelectList(items, "CountryId", "CountryName", CountryId);
            }
        }

        [DisplayName("Varsayılan")]
        public bool AddressDefault { get; set; }

        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string LocalityName { get; set; }
        public string TownName { get; set; }

        [DisplayName("Cadde")]
        public string Avenue { get; set; }

        [DisplayName("Sokak")]
        public string Street { get; set; }

        [DisplayName("Apartman No")]
        public string ApartmentNo { get; set; }

        [DisplayName("Kapı No")]
        public string DoorNo { get; set; }

        [DisplayName("Adres Tipi")]
        public byte AddressTypeId { get; set; }

        public string AddressTypeName { get; set; }

        [DisplayName("Semt")]
        public string DistrictName { get; set; }

        [DisplayName("Posta Kodu")]
        public string ZipCode { get; set; }

        public SelectList AddressTypeItems
        {
            get
            {
                var curAddressType = new Classes.AddressType();
                return new SelectList(curAddressType.GetDataTable().DefaultView, "AddressTypeId", "AddressTypeName", 0);
            }
        }

        public SelectList TownItems { get; set; }

        public SelectList DistrictItems { get; set; }

        public SelectList CountryItems { get; set; }

        public SelectList CityItems { get; set; }

        public SelectList LocalityItems { get; set; }

        public IEnumerable<PhoneModel> MemberPhoneItemsForAddress
        {
            get
            {
                var dataPhone = new Data.Phone();
                return dataPhone.GetPhoneItemsByAddressId(AddressId).AsCollection<PhoneModel>();
            }
        }

        public string DealerName { get; set; }

        public string InstitutionalPhoneAreaCode { get; set; }
        public string InstitutionalPhoneAreaCode2 { get; set; }
        public string InstitutionalPhoneCulture { get; set; }
        public string InstitutionalPhoneCulture2 { get; set; }
        public string InstitutionalPhoneNumber { get; set; }
        public string InstitutionalPhoneNumber2 { get; set; }

        public string InstitutionalGSMAreaCode { get; set; }
        public string InstitutionalGSMAreaCode2 { get; set; }
        public string InstitutionalGSMCulture { get; set; }
        public string InstitutionalGSMCulture2 { get; set; }
        public string InstitutionalGSMNumber { get; set; }
        public string InstitutionalGSMNumber2 { get; set; }

        [DisplayName("Fax")]
        public string InstitutionalFaxNumber { get; set; }
        public string InstitutionalFaxCulture { get; set; }
        public string InstitutionalFaxAreaCode { get; set; }

        public int StoreDealerId { get; set; }
    }
}