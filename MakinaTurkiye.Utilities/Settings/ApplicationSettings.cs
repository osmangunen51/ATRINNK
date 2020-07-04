using System;
using System.Configuration;

namespace MakinaTurkiye.Utilities.Settings
{

    public class ApplicationSettings
    {

        public static byte MainPageNewsCount
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["MainPageNewsCount"]); }
        }

        public static int SectorId
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["SectorId"]); }
        }

        public static byte MainPageStoreCount
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["MainPageStoreCount"]); }
        }

        public static byte MainPageCategoryTopRecordCount
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["MainPageCategoryTopRecordCount"]); }
        }

        public static int CategoryProductSearchType
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["CategoryProductSearchType"]); }
        }

        public static string ProductImageFolder
        {
            get { return ConfigurationManager.AppSettings["ProductImageFolder"].ToString(); }
        }

        public static string ProductThumbSizes
        {
            get { return ConfigurationManager.AppSettings["ProductThumbSizes"].ToString(); }
        }

        public static string StoreLogoFolder
        {
            get { return ConfigurationManager.AppSettings["StoreLogoFolder"].ToString(); }
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
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["Turkiye"]); }
        }

        public static int Istanbul
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["İstanbul"]); }
        }

        public static string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["SiteUrl"].ToString(); }
        }
        public static string VideoUrlBase
        {
            get { return ConfigurationManager.AppSettings["VideoUrlBase"].ToString(); }
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
    }
}
