using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
