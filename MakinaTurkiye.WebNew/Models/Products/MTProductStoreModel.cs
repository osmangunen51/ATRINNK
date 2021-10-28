using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductStoreModel
    {
        public MTProductStoreModel()
        {
            this.PhoneModels = new List<MTStorePhoneModel>();
        }

        public string ProductNo { get; set; }
        public string MemberNo { get; set; }
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public string StoreLogoPath { get; set; }
        public bool IsFavoriteStore { get; set; }
        public int MainPartyId { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string LocalityName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StoreUrl { get; set; }
        public string TruncateStoreName { get; set; }//200 olacak şekilde
        public string StoreAllProductUrl { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public List<MTStorePhoneModel> PhoneModels { get; set; }
    }
}