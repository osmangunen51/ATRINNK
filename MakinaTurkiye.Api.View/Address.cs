using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
    public class Address
    {
        public int AddressId { get; set; }
        public int MainPartyId { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public Locality Locality { get; set; }
        public Town Town { get; set; }
        public string Avenue { get; set; }
        public string ApartmentNo { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }
        public bool? AdressDefault { get; set; }
        public int? StoreDealerId { get; set; }
        public string PostCode { get; set; }

    }
}
