namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class DealerBrand:BaseEntity
    {
        public int DealerBrandId { get; set; }
        public int MainPartyId { get; set; }
        public string DealerBrandName { get; set; }
        public string DealerBrandPicture { get; set; }

        public virtual Store Store { get; set; }
    }
}
