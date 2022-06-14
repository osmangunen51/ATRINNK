using System;

namespace NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations
{
    public class PreRegistrationItem
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public bool HasDescriptions { get; set; }
        public DateTime RecordDate { get; set; }
        public string WebUrl { get; set; }
        public bool IsInserted { get; set; }

        public string City { get; set; }

        public string ContactPhoneNumber { get; set; }
        public string ContactNameSurname { get; set; }

    }
}