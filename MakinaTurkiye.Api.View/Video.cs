using System;

namespace MakinaTurkiye.Api.View
{
    public class Video
    {
        public int VideoId { get; set; }
        public int? ProductId { get; set; }
        public int? StoreMainPartyId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoPath { get; set; }
        public string VideoPicturePath { get; set; }
        public long? VideoSize { get; set; }
        public DateTime? VideoRecordDate { get; set; }
        public bool? Active { get; set; }
        public long? SingularViewCount { get; set; }
        public byte? VideoMinute { get; set; }
        public byte? VideoSecond { get; set; }
        public bool? ShowOnShowcase { get; set; }
        public byte? Order { get; set; }
    }

    public class ProductVideo
    {
        public int VideoId { get; set; }
        public int? ProductId { get; set; }
        public string VideoTitle { get; set; }
        public string ProductName { get; set; }
        public string VideoPath { get; set; }
        public string VideoPicturePath { get; set; }
        public string VideoUrl { get; set; }
        public long? VideoSize { get; set; }
        public DateTime? VideoRecordDate { get; set; }
        public bool? Active { get; set; }
        public byte? VideoMinute { get; set; }
        public byte? VideoSecond { get; set; }
    }
    public class AddProductCatalog
    {
        public int StoreMainMartyId { get; set; } = 0;
        public int AdvertId { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
    }
    public class AddProductVideo
    {
        public int StoreMainMartyId { get; set; } = 0;
        public int AdvertId { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
    }

    public class AddProductPicture
    {
        public int StoreMainMartyId { get; set; } = 0;
        public int AdvertId { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
    }

}
