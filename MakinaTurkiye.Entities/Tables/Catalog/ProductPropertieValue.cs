namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductPropertieValue:BaseEntity
    {
        public int ProductPropertieId { get; set; }
        public int PropertieId { get; set; }
        public int ProductId { get; set; }
        public byte PropertieType { get; set; }
        public string Value { get; set; }
    }
}
