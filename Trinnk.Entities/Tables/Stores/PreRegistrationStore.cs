using System;

namespace Trinnk.Entities.Tables.Stores
{
    public class PreRegistrationStore : BaseEntity
    {
        public int PreRegistrationStoreId { get; set; }
        public string StoreName { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public DateTime RecordDate { get; set; }
        public string WebUrl { get; set; }
        public string City { get; set; }
        public string ContactNameSurname { get; set; }
        public string ContactPhoneNumber { get; set; }

        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? LocalityId { get; set; }
        public int? TownId { get; set; }

    }
}
