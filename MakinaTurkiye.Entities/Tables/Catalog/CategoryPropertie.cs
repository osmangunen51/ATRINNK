namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class CategoryPropertie:BaseEntity
    {
        public int CategoryPropertieId { get; set; }
        public int CategoryId { get; set; }
        public int PropertieId { get; set; }

    }
}
