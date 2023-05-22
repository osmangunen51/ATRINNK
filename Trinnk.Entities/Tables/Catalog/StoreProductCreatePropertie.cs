namespace Trinnk.Entities.Tables.Catalog
{
    public class StoreProductCreatePropertie : BaseEntity
    {
        public int StoreProductCreatePropertieId { get; set; }
        public int? ConstantType { get; set; }
        public string Title { get; set; }
    }
}
