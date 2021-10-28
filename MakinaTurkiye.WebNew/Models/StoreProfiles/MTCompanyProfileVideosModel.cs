using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTCompanyProfileVideosModel
    {
        public MTCompanyProfileVideosModel()
        {
            this.MTCompanyProfileVideosModelsSub = new List<MTCompanyProfileVideosModel>();//for nesteed loops  
        }
        public int VideoId { get; set; }
        public string VideoImagePath { get; set; }
        public string VideoPath { get; set; }

        public IList<MTCompanyProfileVideosModel> MTCompanyProfileVideosModelsSub { get; set; }


    }
}