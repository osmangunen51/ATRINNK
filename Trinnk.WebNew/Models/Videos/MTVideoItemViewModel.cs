namespace NeoSistem.Trinnk.Web.Models.Videos
{
    public class MTVideoItemViewModel
    {
        public MTVideoItemViewModel()
        {
            this.MTStoreAndProductDetailModel = new MTStoreAndProductDetailModel();
            this.MTOtherVideosModel = new MTOtherVideosModel();
            this.MTVideoCategoryItemModel = new MTVideoCategoryItemModel();
        }
        public string VideoPath { get; set; }
        public int VideoId { get; set; }
        public MTStoreAndProductDetailModel MTStoreAndProductDetailModel { get; set; }
        public MTOtherVideosModel MTOtherVideosModel { get; set; }
        public MTVideoCategoryItemModel MTVideoCategoryItemModel { get; set; }

    }
}