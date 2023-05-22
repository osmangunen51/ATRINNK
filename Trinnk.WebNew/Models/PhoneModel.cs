namespace NeoSistem.Trinnk.Web.Models
{

    public class PhoneModel
    {

        public int Index { get; set; }

        public int PhoneId { get; set; }

        public int MainPartyId { get; set; }

        public int StoreDealerId { get; set; }

        public string PhoneNumber { get; set; }

        public byte PhoneType { get; set; }

        public string PhoneCulture { get; set; }

        public string PhoneAreaCode { get; set; }

    }
}