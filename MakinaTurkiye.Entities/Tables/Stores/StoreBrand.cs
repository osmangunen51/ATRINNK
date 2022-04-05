namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreBrand : BaseEntity
    {
        public int StoreBrandId { get; set; }
        public int MainPartyId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandPicture { get; set; }
    }
}
