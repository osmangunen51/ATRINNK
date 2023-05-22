using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Home
{
    public class MTHomeStoreModel
    {
        public MTHomeStoreModel()
        {
            TopProductPictures = new List<StoreProductPictureModel>();
        }
        public int MainPartyId { get; set; }
        public string StoreName { get; set; }
        public string StoreLogo { get; set; }
        public string StoreUrl { get; set; }
        public string StoreAbout { get; set; }
        public List<StoreProductPictureModel> TopProductPictures { get; set; }
    }
    //sub class
    public class StoreProductPictureModel
    {
        public string PictureName { get; set; }
        public string PicturePath { get; set; }
        public string ProductUrl { get; set; }
    }
}