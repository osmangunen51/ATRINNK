using System;

namespace MakinaTurkiye.Api.View
{
    public class StoreConctactInformation
    {
        public int MainPartyId { get; set; }
        public int selectedCountryID { get; set; }
        public int selectedCityID { get; set; }
        public int selectedLocalityID { get; set; }
        public int selectedTownID { get; set; }
        public String cadde { get; set; }
        public String sokak { get; set; }
        public String posta { get; set; }
        public int addressId { get; set; }
    }
}