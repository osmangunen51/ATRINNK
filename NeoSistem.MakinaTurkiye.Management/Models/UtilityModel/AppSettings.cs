
namespace NeoSistem.MakinaTurkiye.Web.Models.UtilityModel
{
    using EnterpriseEntity.Extensions;
    using System.Configuration;

    public class AppSettings
    {
        public static string ProductThumbSizes
        {
            get { return ConfigurationManager.AppSettings["ProductThumbSizes"].ToString(); }
        }
        public static string CategoryIconImageFolder
        {
            get { return ConfigurationManager.AppSettings["CategoryIconImageFolder"].ToString(); }
        }
        public static string BannerImagesFolder
        {
            get { return ConfigurationManager.AppSettings["BannerImagesFolder"].ToString(); }
        }
        public static string CategoryBannerImagesFolder
        {
            get { return ConfigurationManager.AppSettings["CategoryBannerImagesFolder"].ToString(); }
        }
        public static string BannerImagesThumbFolder
        {
            get { return ConfigurationManager.AppSettings["BannerImagesThumbFolder"].ToString(); }
        }
        public static string BannerGifFolder
        {
            get { return ConfigurationManager.AppSettings["BannerGifFolder"].ToString(); }
        }
        public static string BannerFlashFolder
        {
            get { return ConfigurationManager.AppSettings["BannerFlashFolder"].ToString(); }
        }


        public static string MakinaTurkiyeWebUrl
        {
            get { return ConfigurationManager.AppSettings["MakinaTurkiyeWebUrl"].ToString(); }
        }

        public static string MakinaTurkiyeAdminUrl
        {
            get { return ConfigurationManager.AppSettings["MakinaTurkiyeAdminUrl"].ToString(); }
        }

        public static string NewsImageFolder
        {
            get { return ConfigurationManager.AppSettings["NewsImageFolder"].ToString(); }
        }

        public static string StoreProfilePicture
        {
            get { return ConfigurationManager.AppSettings["StoreProfilePicture"].ToString(); }
        }
        public static string StoreNewImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreNewImageFolder"]; }
        }
        public static string StoreNewImageSize
        {
            get { return ConfigurationManager.AppSettings["StoreNewImageSize"]; }
        }
        public static string TempFolder
        {
            get { return ConfigurationManager.AppSettings["TempFolder"].ToString(); }
        }

        public static string ffmpegFolder
        {
            get { return ConfigurationManager.AppSettings["ffmpegFolder"].ToString(); }
        }

        public static string VideoThumbnailFolder
        {
            get { return ConfigurationManager.AppSettings["VideoThumbnailFolder"].ToString(); }
        }

        public static string VideoFolder
        {
            get { return ConfigurationManager.AppSettings["VideoFolder"].ToString(); }
        }
        public static string NewVideosFolder
        {
            get { return ConfigurationManager.AppSettings["NewVideosFolder"].ToString(); }
        }

        public static string DealerBrandImageFolder
        {
            get { return ConfigurationManager.AppSettings["DealerBrandImageFolder"].ToString(); }
        }

        public static string StoreBrandImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreBrandImageFolder"].ToString(); }
        }

        public static int Turkiye
        {
            get { return ConfigurationManager.AppSettings["Turkiye"].ToInt32(); }
        }

        public static int Istanbul
        {
            get { return ConfigurationManager.AppSettings["İstanbul"].ToInt32(); }
        }

        public static string StoreDealerImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreDealerImageFolder"].ToString(); }
        }

        public static string ProductImageFolder
        {
            get { return ConfigurationManager.AppSettings["ProductImageFolder"].ToString(); }
        }

        public static string BaseMenuImageFolder
        {
            get { return ConfigurationManager.AppSettings["BaseMenuImageFolder"].ToString(); }
        }

        //public static string StoreLogoThumb75x75Folder
        //{
        //  get { return ConfigurationManager.AppSettings["StoreLogoThumb75x75Folder"].ToString(); }
        //}
        //public static string StoreLogoThumb150x90Folder
        //{
        //  get { return ConfigurationManager.AppSettings["StoreLogoThumb150x90Folder"].ToString(); }
        //}
        //public static string StoreLogoThumb110x110Folder
        //{
        //  get { return ConfigurationManager.AppSettings["StoreLogoThumb110x110Folder"].ToString(); }
        //}
        //public static string StoreLogoThumb55x40Folder
        //{
        //  get { return ConfigurationManager.AppSettings["StoreLogoThumb55x40Folder"].ToString(); }
        //}
        //public static string StoreLogoThumb170x90Folder
        //{
        //  get { return ConfigurationManager.AppSettings["StoreLogoThumb170x90Folder"].ToString(); }
        //}
        //public static string StoreLogoThumb200x100Folder
        //{
        //  get { return ConfigurationManager.AppSettings["StoreLogoThumb200x100Folder"].ToString(); }
        //}


        public static string ResizeStoreLogoFolder
        {
            get { return ConfigurationManager.AppSettings["ResizeStoreLogoFolder"].ToString(); }
        }

        public static string StoreLogoThumbSizes
        {
            get { return ConfigurationManager.AppSettings["StoreLogoThumbSizes"]; }
        }



        public static string ProductImageThumb55x40Folder
        {
            get { return ConfigurationManager.AppSettings["ProductImageThumb55x40Folder"].ToString(); }
        }
        public static string ProductImageThumb240x160Folder
        {
            get { return ConfigurationManager.AppSettings["ProductImageThumb240x160Folder"].ToString(); }
        }
        public static string ProductImageThumb800x600Folder
        {
            get { return ConfigurationManager.AppSettings["ProductImageThumb800x600Folder"].ToString(); }
        }
        public static string ProductImageThumb75x75Folder
        {
            get { return ConfigurationManager.AppSettings["ProductImageThumb75x75Folder"].ToString(); }
        }
        public static string ProductImageThumb120x120Folder
        {
            get { return ConfigurationManager.AppSettings["ProductImageThumb120x120Folder"].ToString(); }
        }

        public static string SaveAnyOtherFile
        {
            get { return ConfigurationManager.AppSettings["SaveAnyOtherFile"].ToString(); }
        }
        public static string CertificateTypeIconFolder
        {
            get { return ConfigurationManager.AppSettings["CertificateTypeIconFolder"].ToString(); }
        }
    }
}