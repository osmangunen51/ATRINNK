using MakinaTurkiye.Entities.Tables.Catalog;
using System;

namespace MakinaTurkiye.Entities.Tables.Videos
{
    public partial class Video:BaseEntity
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
        public byte ? Order { get; set; }


        public virtual Product Product { get; set; }

    }
}
