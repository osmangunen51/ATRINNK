namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Banner:BaseEntity
    {
        public int BannerId { get; set; }
        public int? CategoryId { get; set; }
        public string BannerAltTag { get; set; }
        public string BannerResource { get; set; }
        public string BannerLink { get; set; }
        public string BannerDescription { get; set; }
        public string BannerOrder { get; set;  }
        public byte? BannerType { get; set; }
        public short? BannerImageType { get; set; }
    
    }
}
