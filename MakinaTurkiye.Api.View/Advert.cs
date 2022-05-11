using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{

    public class AdvertItemPicture
    {
        public int PictureId { get; set; } = 0;
        public string Value { get; set; }
    }

    public class AdvertItem
    {
        public int ProductId { get; set; }=0;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ShortDetail { get; set; } = "";
        public List<AdvertItemPicture> Pictures { get; set; } = new List<AdvertItemPicture>();
        public int Order { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public byte? Type { get; set; } = 0;
        public byte? State { get; set; } = 0;
        public byte? Mensei { get; set; } = 0;
        public byte? SaleState { get; set; } = 0;
        public byte? Location { get; set; } = 0;
        public List<byte> Certifications { get; set; } = new List<byte>();
    }

    public class Advert
    {
        public int MainMartyId { get; set; } = 0;
        public AdvertItem AdvertItem { get; set; }=new AdvertItem();
    }
}
