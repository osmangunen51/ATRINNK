using System;

namespace MakinaTurkiye.Entities.Tables.Users
{
    public class UserInformation : BaseEntity
    {
        public int UserInformationId { get; set; }
        public int UserId { get; set; }
        public string NameSurname { get; set; }
        public string IdentityNumber { get; set; }
        public int UserGroupId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Gender { get; set; }
        public bool MarialStatus { get; set; }
        public byte NumberOfChildren { get; set; }
        public bool DriverLicense { get; set; }
        public string Education { get; set; }
        public string BankAccountNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string WhoSecondPhoneNumber { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
