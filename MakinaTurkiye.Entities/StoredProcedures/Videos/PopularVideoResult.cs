namespace MakinaTurkiye.Entities.StoredProcedures.Videos
{
    public class PopularVideoResult
    {
        public int VideoId { get; set; }
        public string ProductName { get; set; }
        public string VideoPicturePath { get; set; }
        public string StoreName { get; set; }
        public string CategoryName { get; set; }
        public long SingularViewCount { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
    }
}
