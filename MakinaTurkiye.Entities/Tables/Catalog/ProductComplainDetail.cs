namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductComplainDetail : BaseEntity
    {
        //private ICollection<ProductComplainType> _productComplainTypes;
        //private ICollection<ProductComplain> _productComplains;

        public ProductComplainDetail()
        {
            //this.ProductComplainType = new ProductComplainType();
            //this.ProductComplain = new ProductComplain();
        }

        public int ProductComplainDetailId { get; set; }
        public int ProductComplainId { get; set; }
        public int ProductComplainTypeId { get; set; }

        //public virtual ICollection<ProductComplainType> ProductComplainTypes
        //{
        //    get { return _productComplainTypes ?? (_productComplainTypes = new List<ProductComplainType>()); }
        //    protected set { _productComplainTypes = value; }
        //}

        //public virtual ICollection<ProductComplain> ProductComplains
        //{
        //    get { return _productComplains ?? (_productComplains = new List<ProductComplain>()); }
        //    protected set { _productComplains = value; }
        //}

        public virtual ProductComplain ProductComplain { get; set; }
        public virtual ProductComplainType ProductComplainType { get; set; }
    }
}
