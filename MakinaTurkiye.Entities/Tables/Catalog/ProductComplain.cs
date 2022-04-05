using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductComplain : BaseEntity
    {
        private ICollection<ProductComplainDetail> _productComplainDetails { get; set; }

        public int ProductComplainId { get; set; }
        public int ProductId { get; set; }
        public int MemberMainPartyId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsMember { get; set; }

        public virtual Product Product { get; set; }
        //public virtual Member Member { get; set; }

        public virtual ICollection<ProductComplainDetail> ProductComplainDetails
        {
            get { return _productComplainDetails ?? (_productComplainDetails = new List<ProductComplainDetail>()); }
            protected set { _productComplainDetails = value; }
        }

    }
}
