using NeoSistem.Trinnk.Web.Models.Videos;

namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
{
    public class MTStoreProfileHeaderModel
    {
        public MTStoreProfileHeaderModel()
        {
            this.MTStoreProfileMenuActivePage = new MTStoreProfileMenuActivePage();
            this.MTStoreProfileMenuHasModel = new MTStoreProfileMenuHasModel();
            this.StoreVideo = new MTVideoModel();

        }
        public int MainPartyId { get; set; }
        public string StoreName { get; set; }
        public string StoreShortName { get; set; }
        public string StoreLogoPath { get; set; }
        public string StoreAbout { get; set; }
        public bool HasFavorite { get; set; }
        public bool HasPromotionVideo { get; set; }
        public string PromotionVideoPath { get; set; }
        public string StoreUrl { get; set; }
        public string StoreBanner { get; set; }


        public MTCategoryModel Categories { get; set; }
        public MTStoreProfileMenuActivePage MTStoreProfileMenuActivePage { get; set; }
        public MTStoreProfileMenuHasModel MTStoreProfileMenuHasModel { get; set; }
        public MTVideoModel StoreVideo { get; set; }

    }
}