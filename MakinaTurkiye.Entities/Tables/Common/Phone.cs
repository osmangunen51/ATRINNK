namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Phone : BaseEntity
    {
        public int PhoneId { get; set; }

        public int? MainPartyId { get; set; }
        public int? AddressId { get; set; }

        public string PhoneCulture { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public byte? PhoneType { get; set; }
        public byte? GsmType { get; set; }
        public int? active { get; set; }
        public string ActivationCode { get; set; }

        //public virtual Member Member { get; set; }

        public virtual Address Address { get; set; }
    }
}
