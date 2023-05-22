using Trinnk.Core;
using System.Web;

namespace Trinnk.Utilities.ImageHelpers
{
    public static class ImageHelper
    {
        public static string GetStoreProfilePicture(string logo)
        {
            if (!string.IsNullOrWhiteSpace(logo) && logo.Contains("."))
                logo = logo.Substring(0, logo.LastIndexOf(".")) + "_th" + logo.Substring(logo.LastIndexOf("."));
            else
                logo = "";

            return string.Format("https://trinnk.com/UserFiles/Images/StoreProfilePicture/{0}", logo);
        }


        public static string GetVideoImagePath(string videoPictureName)
        {
            string imageUrl = string.Format("//s.trinnk.com/VideoThumb/{0}", videoPictureName);

            //#if !DEBUG
            //                 string fileBase = "/UserFiles/VideoThumb/" + videoPictureName;
            //            var absolutePath = HttpContext.Current.Server.MapPath(fileBase);
            //            if (System.IO.File.Exists(absolutePath))
            //            {
            //                return imageUrl;
            //            }
            //            else
            //            {
            //                return "https://dummyimage.com/245x165/dbdbdb/000000.jpg&text=Haz%C4%B1rlan%C4%B1yor";

            //            }
            //#endif



            return imageUrl;
        }

        public static string CDNhost
        {
            get
            {
                //#if DEBUG
                //                return "//s.trinnk.com";
                //#else
                //                    return "//s.trinnk.com";
                //#endif
                return "https://s.trinnk.com";
            }
        }

        public static string GetDealerShipPicture(string file)
        {
            if (string.IsNullOrEmpty(file))
                return string.Empty;            
            return $"{CDNhost}{AppSettings.DealerBrandImageFolder}{file}".Replace("UserFiles/","");

        }

        public static string GetBrandPicture(string file)
        {
            if (string.IsNullOrEmpty(file))
                return string.Empty;            
            return $"{CDNhost}{AppSettings.StoreBrandImageFolder}{file}".Replace("UserFiles/","");

        }

        public static string GetCompainyPicture(string file)
        {
            if (string.IsNullOrEmpty(file))
                return string.Empty;
            return $"{CDNhost}{AppSettings.StoreImageFolder}{file}".Replace("UserFiles/", "");

        }


        public static string GetStoreBanner(int storeId, string banner)
        {
            if (string.IsNullOrEmpty(banner))
                return string.Empty;
            return string.Format(CDNhost + "/StoreBanner/{0}.jpg",
                                             banner.Replace("_banner", "-1400x280").Replace(".jpg", ""));

        }
        public static string GetStoreImage(int storeId, string logo, string size)
        {
            if (string.IsNullOrEmpty(logo))
                return string.Empty;
            try
            {
                if (size == "300")
                {
                    return string.Format(CDNhost + "/Store/{0}/thumbs/{1}-{2}x200.jpg", storeId,
                                                         logo.Replace("_logo", "").Replace(".jpg", ""), size);
                }
                else if (size == "120")
                {

                    return string.Format(CDNhost + "/Store/{0}/thumbs/{1}-{2}x80.jpg", storeId,
                                                       logo.Replace("_logo", "").Replace(".jpg", ""), size);
                }
                else
                {
                    return string.Format(CDNhost + "/Store/{0}/thumbs/{1}-{2}x{2}.jpg", storeId,
                                     logo.Replace("_logo", "").Replace(".jpg", ""), size);
                }


            }
            catch
            {
                return string.Empty;
            }
        }


        public static string GetStoreLogoPath(int storeId, string storeLogo, int size)
        {
            if (string.IsNullOrEmpty(storeLogo))
            {
                return "//s.trinnk.com/no-image.png";
            }

            if (size == 300)
            {
                storeLogo = storeLogo.Replace("_logo", "");
                storeLogo = storeLogo.Replace(".jpg", "");
                storeLogo = storeLogo.Replace(".png", "");
                return string.Format("//s.trinnk.com/Store/{0}/thumbs/{1}-{2}x200.jpg", storeId, storeLogo, size);
            }
            if (size == 400)
            {
                storeLogo = storeLogo.Replace("_logo", "");
                storeLogo = storeLogo.Replace(".jpg", "");
                storeLogo = storeLogo.Replace(".png", "");
                return string.Format("//s.trinnk.com/Store/{0}/thumbs/{1}-{2}x300.jpg", storeId, storeLogo, size);
            }
            if (size == 500)
            {
                storeLogo = storeLogo.Replace("_logo", "");
                storeLogo = storeLogo.Replace(".jpg", "");
                storeLogo = storeLogo.Replace(".png", "");
                return string.Format("//s.trinnk.com/Store/{0}/thumbs/{1}-{2}x375.jpg", storeId, storeLogo, size);
            }
            else
            {
                return string.Format("//s.trinnk.com/Store/{0}/thumbs/{1}-{2}x{2}.jpg", storeId, storeLogo.Replace("_logo", "").Replace(".jpg", ""), size);
            }
        }
        public static string GetStoreLogoParh(int storeId, string storeLogo, int width, int height)
        {
            if (string.IsNullOrEmpty(storeLogo))
            {
                return "//s.trinnk.com/no-image.png";
            }
            return string.Format("//s.trinnk.com/Store/{0}/thumbs/{1}-{2}x{3}.jpg", storeId, storeLogo.Replace("_logo", "").Replace(".jpg", ""), width, height);
        }
        public static string GetStoreNewImagePath(string imageName, string imageSize)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                if (!string.IsNullOrEmpty(imageSize))
                {
                    imageSize = imageSize.Replace("px", "");
                    imageName = imageName.Replace("_haber", "");
                    return string.Format("//s.trinnk.com/StoreNewImage/{0}_{1}.{2}", imageName.Split('.')[0], imageSize, imageName.Split('.')[1]);
                }
                else
                {
                    return string.Format("//s.trinnk.com/StoreNewImage/{0}_haber.{1}", imageName.Split('.')[0], imageName.Split('.')[1]);
                }
            }
            else
                return string.Empty;


        }

        public static string GetProductImagePath(int productId, string productImageName, ProductImageSize imageSize)
        {
            if (string.IsNullOrEmpty(productImageName))
                return "//s.trinnk.com/no-image.png";

            try
            {
                if (!productImageName.Contains(".jpg"))
                    productImageName = productImageName + ".jpg";
                //productImageName = productImageName.Replace(".jpg", ".webp");
                string[] name = productImageName.Split('.');
                string picturePathIsExist = string.Empty;
                name = productImageName.Split('.');
                switch (imageSize)
                {
                    case ProductImageSize.px400x300:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-400x300.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px900x675:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-900x675.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px200x150:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-200x150.{2}", productId,
                           name[0], name[1]);
                    case ProductImageSize.px500x375:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-500x375.{2}", productId,
                        name[0], name[1]);
                    case ProductImageSize.NoImage:
                        return "//s.trinnk.com/no-image.png";
                    default:
                        return string.Empty;
                }
            }
            catch
            {
                return string.Empty;

            }

        }

        public static string GetProductImagePathForAccount(int productId, string productImageName, ProductImageSize imageSize)
        {
            if (string.IsNullOrEmpty(productImageName))
                return "//s.trinnk.com/no-image.png";

            try
            {
                if (!productImageName.Contains(".jpg"))
                    productImageName = productImageName + ".jpg";
                string[] name = productImageName.Split('.');
                string picturePathIsExist = string.Empty;
                switch (imageSize)
                {
                    case ProductImageSize.px400x300:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-400x300.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px900x675:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-900x675.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px200x150:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-200x150.{2}", productId,
                           name[0], name[1]);
                    case ProductImageSize.px500x375:
                        return string.Format("//s.trinnk.com/Product/{0}/thumbs/{1}-500x375.{2}", productId,
                        name[0], name[1]);
                    case ProductImageSize.NoImage:
                        return "//s.trinnk.com/no-image.png";
                    default:
                        return string.Empty;
                }
            }
            catch
            {
                return string.Empty;

            }

        }
        public static string GetLogo()
        {
            return string.Format("//www.trinnk.com/Content/V2/images/trinnk-dark.png");
        }
        public static string GetHomeSectorImagePath(string imagePath)
        {
            if (imagePath == null)
                return "";

            return string.Format("//s.trinnk.com/Images/CategoryHomePageImageFolder/{0}", imagePath);
        }


        public static string GetBannerImagePath(string bannerResource)
        {
            return string.Format("//s.trinnk.com/Banner/ImagesThumb/{0}", bannerResource);
        }
        public static string GetCategoryBannerImagePath(string bannerResource)
        {
            return string.Format("//s.trinnk.com//Banner/CategoryImages/{0}", bannerResource);
        }
        public static string GetCategoryIconPath(string bannerResource)
        {
            return string.Format("//s.trinnk.com/Images/CategoryIconImageFolder/{0}", bannerResource);
        }
    }
}
