namespace MakinaTurkiye.Api.View
{
    public class MTHomeBannerModel
    {
        public int Index { get; set; }
        public string PicturePathPc { get; set; }
        public string PicturePathMobile { get; set; }
        public string PicturePathTablet { get; set; }
        public short BannerImageType { get; set; }
        public string Url { get; set; }
        public string ImageTag { get; set; }
    }
}