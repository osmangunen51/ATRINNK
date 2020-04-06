

using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using MakinaTurkiye.Entities.Tables.Common;
using NeoSistem.EnterpriseEntity.Extensions.Data;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }

        public int StoreDealerId { get; set; }

        public int? MainPartyId { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public int LocalityId { get; set; }

        public int TownId { get; set; }

        public int DistrictId { get; set; }

        public byte AddressTypeId { get; set; }

        public string AddressTypeName { get; set; }

        public string PostCode { get; set; }

        public bool AddressDefault { get; set; }
        public string GsmWhatsappAreaCode { get; set; }
        public string GsmWhatsappCulture { get; set; }
        public string GsmWhatsappNumber { get; set; }

        public bool IsGsmWhatsapp { get; set; }
        public Phone GsmPhone { get; set; }
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

        public string Avenue { get; set; }
        public string ApartmentNo { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }

        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string LocalityName { get; set; }
        public string TownName { get; set; }
        public string DistrictName { get; set; }

        public string DealerName { get; set; }

        public string ZipCode { get; set; }
        public SelectList CityItems { get; set; }

        public SelectList CountryItems { get; set; }

        public SelectList LocalityItems { get; set; }

        public SelectList TownItems
        {
            get;
            set;
        }

        public IEnumerable<Phone> PhoneItems { get; set; }

        public IEnumerable<PhoneModel> MemberPhoneItemsForAddress
        {
            get
            {
                var dataPhone = new Data.Phone();
                return dataPhone.GetPhoneItemsByAddressId(AddressId).AsCollection<PhoneModel>();
            }
        }

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


        public string InstitutionalFaxNumber { get; set; }
        public string InstitutionalFaxCulture { get; set; }
        public string InstitutionalFaxAreaCode { get; set; }
        public byte GsmType { get; set; }

        public LeftMenuModel LeftMenu { get; set; }
    }
}