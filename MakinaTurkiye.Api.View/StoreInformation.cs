using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class StoreInformation
    {
        public int? MainPartyId { get; set; }
        public int? selectedCountryID { get; set; }
        public int? selectedCityID { get; set; }
        public int? selectedLocalityID { get; set; }
        public int? selectedTownID { get; set; }
        public string cadde { get; set; }
        public string sokak { get; set; }
        public string posta { get; set; }
        public int? memberTitleID { get; set; }
        public string storeName { get; set; }
        public string storeUrl { get; set; }
        public string storeWeb { get; set; }
        public int? storeEndorseID { get; set; }
        public int? storeCapID { get; set; }
        public int? storeEmpCountID { get; set; }
        public int? storeTypeID { get; set; }
        public int? storeEstDate { get; set; }
        public List<int> storeActivitySelected { get; set; } = new List<int>();
        public int? addressId { get; set; }
    }
}