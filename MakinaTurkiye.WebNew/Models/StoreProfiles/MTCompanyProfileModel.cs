using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTCompanyProfileModel
    {
        public MTCompanyProfileModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.MTPopularProductsModels = new List<MTPopularProductsModel>();
            this.MTStoreAboutModel = new MTStoreAboutModel();
            this.MTStoreImageAndVideosModel = new MTStoreImageAndVideosModel();
            this.MTStoreMemberAndMapModel = new MTStoreMemberAndMapModel();
            this.SliderImages = new List<string>();
            this.Certificates = new List<MTCertificateItem>();
           
        }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public List<string> SliderImages { get; set; }
        public  MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public  IList<MTPopularProductsModel> MTPopularProductsModels { get; set; }
        public  MTStoreAboutModel MTStoreAboutModel { get; set; }

        public MTStoreImageAndVideosModel MTStoreImageAndVideosModel { get; set; }
        public MTStoreMemberAndMapModel MTStoreMemberAndMapModel { get; set; }
        public List<MTCertificateItem> Certificates { get; set; }



    }
}