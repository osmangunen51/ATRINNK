
namespace NeoSistem.MakinaTurkiye.Web.Models.UtilityModel
{
    using EnterpriseEntity.Extensions;
    using System.Configuration;

    public class AppSettings
    {

        public static byte MainPageNewsCount
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MainPageNewsCount"].ToByte(); }
        }

        public static int SectorId
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SectorId"].ToInt32(); }
        }


        public static byte MainPageStoreCount
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MainPageStoreCount"].ToByte(); }
        }

        public static byte MainPageCategoryTopRecordCount
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MainPageCategoryTopRecordCount"].ToByte(); }
        }

        public static int CategoryProductSearchType
        {
            get { return ConfigurationManager.AppSettings["CategoryProductSearchType"].ToInt32(); }
        }

        public static string ProductImageFolder
        {
            get { return ConfigurationManager.AppSettings["ProductImageFolder"].ToString(); }
        }
        public static string CategoryIconImageFolder
        {
            get { return ConfigurationManager.AppSettings["CategoryIconImageFolder"].ToString(); }
        }

        public static string ProductThumbSizes
        {
            get { return ConfigurationManager.AppSettings["ProductThumbSizes"].ToString(); }
        }

        public static string StoreImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreImageFolder"].ToString(); }
        }
        public static string StoreSliderImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreSliderImageFolder"].ToString(); }
        }
        public static string StoreCertificateImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreCertificateImageFolder"].ToString(); }
        }

        public static string StoreLogoFolder
        {
            get { return ConfigurationManager.AppSettings["StoreLogoFolder"].ToString(); }
        }
        public static string StoreBannerFolder
        {
            get { return ConfigurationManager.AppSettings["StoreBannerFolder"].ToString(); }
        }
        public static string StoreLogoThumb300x200Folder
        {
            get { return ConfigurationManager.AppSettings["StoreLogoThumb300x200Folder"].ToString(); }
        }

        public static string StoreLogoThumb100x100Folder
        {
            get { return ConfigurationManager.AppSettings["StoreLogoThumb100x100Folder"].ToString(); }
        }


        public static string ResizeStoreLogoFolder
        {
            get { return ConfigurationManager.AppSettings["ResizeStoreLogoFolder"].ToString(); }
        }

        public static string StoreLogoThumbSizes
        {
            get { return ConfigurationManager.AppSettings["StoreLogoThumbSizes"]; }
        }
        public static string StoreNewImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreNewImageFolder"]; }
        }
        public static string StoreNewImageSize
        {
            get { return ConfigurationManager.AppSettings["StoreNewImageSize"]; }
        }


        public static string StoreCatologFolder
        {
            get { return ConfigurationManager.AppSettings["StoreCatologFolder"]; }
        }
        public static string ProductCatologFolder
        {
            get { return ConfigurationManager.AppSettings["ProductCatologFolder"]; }
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


        public static string BannerImagesFolder
        {
            get { return ConfigurationManager.AppSettings["BannerImagesFolder"].ToString(); }
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


        public static string MessageFileFolder
        {
            get { return ConfigurationManager.AppSettings["MessageFileFolder"].ToString(); }
        }

        public static string NewsImageFolder
        {
            get { return ConfigurationManager.AppSettings["NewsImageFolder"].ToString(); }
        }

        public static string StoreDealerImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreDealerImageFolder"].ToString(); }
        }

        public static string StoreProfilePicture
        {
            get { return ConfigurationManager.AppSettings["StoreProfilePicture"].ToString(); }
        }

        public static string DealerBrandImageFolder
        {
            get { return ConfigurationManager.AppSettings["DealerBrandImageFolder"].ToString(); }
        }

        public static string StoreBrandImageFolder
        {
            get { return ConfigurationManager.AppSettings["StoreBrandImageFolder"].ToString(); }
        }

        public static string VideoFolder
        {
            get { return ConfigurationManager.AppSettings["VideoFolder"].ToString(); }
        }

        public static string NewVideosFolder
        {
            get { return ConfigurationManager.AppSettings["NewVideosFolder"].ToString(); }
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

        public static int Turkiye
        {
            get { return ConfigurationManager.AppSettings["Turkiye"].ToInt32(); }
        }

        public static int Istanbul
        {
            get { return ConfigurationManager.AppSettings["İstanbul"].ToInt32(); }
        }

        public static string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["SiteUrl"].ToString(); }

        }
        public static string SiteUrlWithoutLastSlash
        {
            get { return ConfigurationManager.AppSettings["SiteUrlWithoutLastSlash"].ToString(); }
        }
        public static string VideoUrlBase
        {
            get { return ConfigurationManager.AppSettings["VideoUrlBase"].ToString(); }

        }
        public static string StoreAllUrl
        {
            get { return ConfigurationManager.AppSettings["StoreAllUrl"].ToString(); }

        }
        public static string SiteAllCategoryUrl
        {
            get { return ConfigurationManager.AppSettings["SiteAllCategoryUrl"].ToString(); }
        }

        public static string SiteAllNewUrl
        {
            get { return ConfigurationManager.AppSettings["SiteAllNewUrl"].ToString(); }
        }

        public static string RssFirmViewedCount
        {
            get { return ConfigurationManager.AppSettings["RssFirmViewedCount"].ToString(); }
        }

        public static string RssProductViewedCount
        {
            get { return ConfigurationManager.AppSettings["RssProductViewedCount"].ToString(); }
        }

        public static string SaveAnyOtherFile
        {
            get { return ConfigurationManager.AppSettings["SaveAnyOtherFile"].ToString(); }
        }
        public static string IyzicoApiKey
        {
            get { return ConfigurationManager.AppSettings["IyzicoApiKey"].ToString(); }
        }
        public static string IyzicoSecureKey
        {
            get { return ConfigurationManager.AppSettings["IyzicoSecureKey"].ToString(); }
        }
        public static string IyzicoApiUrl
        {
            get { return ConfigurationManager.AppSettings["IyzicoApiUrl"].ToString(); }
        }

        public static string PaytrApiMerchantSalt
        {
            get { return ConfigurationManager.AppSettings["PaytrApiMerchantSalt"].ToString(); }
        }
        public static string PaytrApiMerchantId
        {
            get { return ConfigurationManager.AppSettings["PaytrApiMerchantId"].ToString(); }
        }

        public static string PaytrApiMerchantKey
        {
            get { return ConfigurationManager.AppSettings["PaytrApiMerchantKey"].ToString(); }
        }
        public static string PaytrApiEmail
        {
            get { return ConfigurationManager.AppSettings["PaytrApiEmail"].ToString(); }
        }

    }
}