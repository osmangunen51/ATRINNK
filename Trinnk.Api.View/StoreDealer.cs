using System.Collections.Generic;

namespace Trinnk.Api.View
{
    public class DealerPhone
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string Number { get; set; }
        public byte? Type { get; set; }
    }

    public class DealerAddress
    {
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public int? LocalityID { get; set; }
        public int? TownID { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string Town { get; set; }

        public string cadde { get; set; }
        public string sokak { get; set; }
        public string posta { get; set; }
        public string BinaNo { get; set; }
        public string KapiNo { get; set; }
    }

    public class StoreDealerItem
    {
        public int? DealerId { get; set; } = 0;
        public string Name { get; set; } = "";
        public DealerAddress Address { get; set; } = new DealerAddress();
        public DealerPhone Tel1 { get; set; } = new DealerPhone() { Type = (byte)PhoneType.Phone };
        public DealerPhone Tel2 { get; set; } = new DealerPhone() { Type = (byte)PhoneType.Phone };
        public DealerPhone Fax { get; set; } = new DealerPhone() { Type = (byte)PhoneType.Fax };
        public DealerPhone Gsm { get; set; } = new DealerPhone() { Type = (byte)PhoneType.Gsm };
        public byte DealerType { get; set; } = 1;
    }

    public class StoreDealer
    {
        public List<StoreDealerItem> List { get; set; } = new List<StoreDealerItem>();
        public int MainPartyId { get; set; } = 0;
        public byte DealerType { get; set; } = 1;
    }

    public class StoreDealerDelete
    {
        public int Id { get; set; } = 0;
        public int MainPartyId { get; set; } = 0;
        public byte DealerType { get; set; } = 1;
    }
}