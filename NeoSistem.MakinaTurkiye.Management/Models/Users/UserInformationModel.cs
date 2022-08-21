namespace NeoSistem.MakinaTurkiye.Management.Models.Users
{
    public class UserInformationModel
    {
        public string NameSurname { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string StartWorkDate { get; set; }
        public string EndDate { get; set; }
        public string Adress { get; set; }
        public string BirthDate { get; set; }
        public byte Gender { get; set; }
        public bool MarialStatus { get; set; }
        public byte NumberOfChildren { get; set; }
        public bool DriverLicense { get; set; }
        public string Education { get; set; }
        public string BankAccountNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string WhoSecondPhoneNumber { get; set; }
    }
}