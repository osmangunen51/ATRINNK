namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class PictureModel
    {
        public int PictureId { get; set; }
        public int ProductId { get; set; }
        public int StoreDealerId { get; set; }
        public string PictureName { get; set; }
        public string PicturePath { get; set; }
        public int PictureOrder { get; set; }
    }
}