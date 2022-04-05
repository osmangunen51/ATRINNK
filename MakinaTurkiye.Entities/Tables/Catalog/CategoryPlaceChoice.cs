namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class CategoryPlaceChoice : BaseEntity
    {
        public int CategoryPlaceChoiceId { get; set; }
        public int CategoryId { get; set; }
        public byte CategoryPlaceType { get; set; }
        public byte? Order { get; set; }
        public bool IsProductCategory { get; set; }

        public virtual Category Category { get; set; }
    }
}
