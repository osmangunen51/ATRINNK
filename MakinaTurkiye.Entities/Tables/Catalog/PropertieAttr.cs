namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class PropertieAttr : BaseEntity
    {
        public int PropertieAttrId { get; set; }
        public int PropertieId { get; set; }
        public string AttrValue{ get; set; }
        public byte? DisplayOrder { get; set; }

    }
}
