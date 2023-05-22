using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
{
    public class MTStoreImageModel
    {
        public MTStoreImageModel()
        {
            this.ImagePath = new List<string>();
            MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();

        }
        public string StoreName { get; set; }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public List<string> ImagePath { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
    }
}