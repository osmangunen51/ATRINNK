using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class StoreInformation
    {
        public int selectedCountryID { get; set; }
        public int selectedCityID { get; set; }
        public int selectedLocalityID { get; set; }
        public int selectedTownID { get; set; }
        public String cadde { get; set; }
        public String sokak { get; set; }
        public String posta { get; set; }
        public int memberTitleID { get; set; }
        public String storeTitle { get; set; }
        public String storeName { get; set; }
        public String storeUrl { get; set; }
        public String storeWeb { get; set; }
        public int storeEndorseID { get; set; }
        public int storeCapID { get; set; }
        public int storeEmpCountID { get; set; }
        public int storeTypeID { get; set; }
        public String storeEstDate { get; set; }
        public List<int> storeActivitySelected { get; set; } = new List<int>();
    }
}