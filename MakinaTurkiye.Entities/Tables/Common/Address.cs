using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Address: BaseEntity
    {
        private ICollection<Phone> _phones;

        public int AddressId { get; set; }
        public int? MainPartyId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? LocalityId { get; set; }
        public int? TownId { get; set; }
        public byte? AddressTypeId { get; set; }
        public string Avenue { get; set; }
        public string ApartmentNo { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }
        public bool? AddressDefault { get; set; }
        public int? StoreDealerId { get; set; }
        public string PostCode { get; set; }

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Phone> Phones
        {
            get { return _phones ?? (_phones = new List<Phone>()); }
            protected set { _phones = value; }
        }
    }
}
