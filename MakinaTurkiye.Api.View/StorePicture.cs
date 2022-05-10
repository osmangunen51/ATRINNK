using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class StorePictureItem
    {
        public int PictureId { get; set; } = 0;
        public string Value { get; set; }
    }

    public class StorePicture
    {
        public List<StorePictureItem> List { get; set; } = new List<StorePictureItem>();
        public int MainPartyId { get; set; } = 0;
    }
}