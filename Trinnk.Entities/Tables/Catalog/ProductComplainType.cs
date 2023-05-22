using System.Collections.Generic;

namespace Trinnk.Entities.Tables.Catalog
{
    public class ProductComplainType : BaseEntity
    {
        private ICollection<ProductComplainDetail> _productComplainDetails;
        public int ProductComplainTypeId { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<ProductComplainDetail> ProductComplainDetails
        {
            get { return _productComplainDetails ?? (_productComplainDetails = new List<ProductComplainDetail>()); }
            protected set { _productComplainDetails = value; }
        }




    }
}
