namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Constant:BaseEntity
    {
        //private ICollection<Product> _productTypeProducts;
        //private ICollection<Product> _productStatuProducts;
        //private ICollection<Product> _briefDetailProducts;

        //private ICollection<Product> _menseiProducts;
        //private ICollection<Product> _orderStatusProducts;

        public short ConstantId { get; set; }
        public byte? ConstantType { get; set; }
        public string ConstantName { get; set; }
        public string ContstantPropertie { get; set; }
        public string ConstantTitle { get; set; }
        public int? Order { get; set; }


        //public virtual ICollection<Product> ProductTypeProducts
        //{
        //    get { return _productTypeProducts ?? (_productTypeProducts = new List<Product>()); }
        //    protected set { _productTypeProducts = value; }
        //}

        //public virtual ICollection<Product> ProductStatuProducts
        //{
        //    get { return _productStatuProducts ?? (_productStatuProducts = new List<Product>()); }
        //    protected set { _productStatuProducts = value; }
        //}

        //public virtual ICollection<Product> BriefDetailProducts
        //{
        //    get { return _briefDetailProducts ?? (_briefDetailProducts = new List<Product>()); }
        //    protected set { _briefDetailProducts = value; }
        //}

        //public virtual ICollection<Product> MenseiProducts
        //{
        //    get { return _menseiProducts ?? (_menseiProducts = new List<Product>()); }
        //    protected set { _menseiProducts = value; }
        //}

        //public virtual ICollection<Product> OrderStatusProducts
        //{
        //    get { return _orderStatusProducts ?? (_orderStatusProducts = new List<Product>()); }
        //    protected set { _orderStatusProducts = value; }
        //}
    }
}
