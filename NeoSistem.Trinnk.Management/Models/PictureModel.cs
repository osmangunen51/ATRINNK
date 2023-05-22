namespace NeoSistem.Trinnk.Management.Models
{
    public class PictureModel
    {
        public int PictureId { get; set; }

        public int ProductId { get; set; }

        public string PictureName { get; set; }

        public string PicturePath { get; set; }

        public int PictureOrder { get; set; }
    }
}