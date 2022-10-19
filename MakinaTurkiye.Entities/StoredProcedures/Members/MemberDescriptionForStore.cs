using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Members
{
    public class MemberDescriptionForStore
    {
        public int descId { get; set; }

        public string Description { get; set; }
        public int? MainPartyId { get; set; }

        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Int16? DescriptionDegree { get; set; }
        public int? Status { get; set; }
        public string StoreName { get; set; }
        public int? UserId { get; set; }
        public int? FromUserId { get; set; }
        public int? ConstandId { get; set; }
        public bool? IsFirst { get; set; }
        public int? PreRegistrationStoreId { get; set; }
        public bool? IsImmediate { get; set; }
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
