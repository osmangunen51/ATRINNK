namespace MakinaTurkiye.Api.View
{
    public class UpdatePersonalAddressInfo
    {
        public Country Country { get; set; }
        public City City { get; set; }
        public Locality Locality { get; set; }
        public Town Town { get; set; }
        public string Avenue { get; set; }
        public string ApartmentNo { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string GsmCountryCode { get; set; }
        public string GsmAreaCode { get; set; }
        public string Gsm { get; set; }
        public string WhatsappGsmCountryCode { get; set; }
        public string WhatsappGsmAreaCode { get; set; }
        public string WhatsappGsm { get; set; }
        public string Phone1CountryCode { get; set; }
        public string Phone1AreaCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2CountryCode { get; set; }
        public string Phone2AreaCode { get; set; }
        public string Phone2 { get; set; }
        public string FaxCountryCode { get; set; }
        public string FaxAreaCode { get; set; }
        public string Fax { get; set; }
    }
}