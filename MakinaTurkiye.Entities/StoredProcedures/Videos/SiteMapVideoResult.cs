namespace MakinaTurkiye.Entities.StoredProcedures.Videos
{
    public class SiteMapVideoResult
    {
        public int VideoId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string VideoPicturePath { get; set; }
        public string VideoPath { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }

    }
}
