using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class NotificationModel
    {

        public int ID { get; set; }
        public string MemberName { get; set; }
        public string StoreName { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int MainPartyId { get; set; }
        public string Description { get; set; }
        public int StoreMainPartyId { get; set; }
        public string SalesPersonName { get; set; }
        public string FromUserName { get; set; }
        public bool IsFirst { get; set; }
        public bool IsImmediate { get; set; }
        public string StoreCityName { get; set; } = "";
        public string PreRegistrationStoreCityName { get; set; } = "";

        public string LocalityName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string PreLocalityName { get; set; }
        public string PreCityName { get; set; }
        public string PreCountryName { get; set; }

    }
}