using System.Web;

namespace MakinaTurkiye.Utilities.ImageHelpers
{
    public static class ImageHelper
    {
        public static string GetStoreProfilePicture(string logo)
        {
            if (!string.IsNullOrWhiteSpace(logo) && logo.Contains("."))
                logo= logo.Substring(0, logo.LastIndexOf(".")) + "_th" + logo.Substring(logo.LastIndexOf("."));
            else
                logo="";

            return string.Format("https://makinaturkiye.com/UserFiles/Images/StoreProfilePicture/{0}", logo);
        }


        public static string GetVideoImagePath(string videoPictureName)
        {
            string imageUrl = string.Format("//s.makinaturkiye.com/VideoThumb/{0}", videoPictureName);

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
                //                return "//s.makinaturkiye.com";
                //#else
                //                    return "//s.makinaturkiye.com";
                //#endif
                return "https://s.makinaturkiye.com";
            }
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


        public static string GetStoreLogoParh(int storeId, string storeLogo, int size)
        {
            if (string.IsNullOrEmpty(storeLogo))
            {
                return "//s.makinaturkiye.com/no-image.png";
            }

            if (size == 300)
            {

                return string.Format("//s.makinaturkiye.com/Store/{0}/thumbs/{1}-{2}x200.jpg", storeId, storeLogo.Replace("_logo", "").Replace(".jpg", ""), size);
            }
            else
            {
                return string.Format("//s.makinaturkiye.com/Store/{0}/thumbs/{1}-{2}x{2}.jpg", storeId, storeLogo.Replace("_logo", "").Replace(".jpg", ""), size);
            }
        }
        public static string GetStoreLogoParh(int storeId, string storeLogo, int width,int height)
        {
            if (string.IsNullOrEmpty(storeLogo))
            {
                return "//s.makinaturkiye.com/no-image.png";
            }



                return string.Format("//s.makinaturkiye.com/Store/{0}/thumbs/{1}-{2}x{3}.jpg", storeId, storeLogo.Replace("_logo", "").Replace(".jpg", ""), width,height);


        }
        public static string GetStoreNewImagePath(string imageName,string imageSize)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                if (!string.IsNullOrEmpty(imageSize))
                {
                    imageSize = imageSize.Replace("px", "");
                    imageName = imageName.Replace("_haber", "");
                    return string.Format("//s.makinaturkiye.com/StoreNewImage/{0}_{1}.{2}", imageName.Split('.')[0], imageSize, imageName.Split('.')[1]);
                }
                else
                {
                    return string.Format("//s.makinaturkiye.com/StoreNewImage/{0}_haber.{1}", imageName.Split('.')[0], imageName.Split('.')[1]);
                }
            }
            else
                return string.Empty;


        }

        public static string GetProductImagePath(int productId, string productImageName, ProductImageSize imageSize)
        {
            if (string.IsNullOrEmpty(productImageName))
                return "//s.makinaturkiye.com/no-image.png";

            try
            {
                if (!productImageName.Contains(".jpg"))
                    productImageName = productImageName + ".jpg";
                //productImageName = productImageName.Replace(".jpg", ".webp");
                string[] name = productImageName.Split('.');
                string picturePathIsExist = string.Empty;
                if (imageSize == ProductImageSize.px100)
                {

                    //System.IO.Path.Combine(, fileName);
                    picturePathIsExist = string.Format("/UserFiles/Product/{0}/thumbs/{1}-100X.{2}", productId, name[0],
                        name[1]);

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(picturePathIsExist)))
                    {
                        imageSize = ProductImageSize.px100;
                    }
                    else
                    {
                        imageSize = ProductImageSize.px100x75;
                    }
                }


                //string imageSizeName = imageSize.ToString();
                //if (imageSize == ProductImageSize.x160x120) imageSizeName = "160x120";
                //picturePathIsExist = string.Format("/UserFiles/Product/{0}/thumbs/{1}-" + imageSizeName.Replace("px", "") + ".{2}", productId, name[0], name[1]);
                //if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(picturePathIsExist)))
                //{

                //    picturePathIsExist = string.Format("/UserFiles/Product/{0}/thumbs/{1}-400x300.{2}", productId, name[0], name[1]);
                //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(picturePathIsExist)))
                //    {
                //        imageSize = ProductImageSize.px400x300;
                //    }
                //    else
                //    {
                //        imageSize = ProductImageSize.NoImage;
                //    }
                //}


                name = productImageName.Split('.');

                switch (imageSize)
                {
                    case ProductImageSize.px100:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-100X.{2}", productId, name[0],
                            name[1]);
                    case ProductImageSize.px100x75:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-100x75.{2}", productId, name[0],
                            name[1]);
                    case ProductImageSize.px400x300:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-400x300.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px900x675:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-900x675.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px180x135:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-180x135.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.x160x120:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-160x120.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px200x150:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-200x150.{2}", productId,
                           name[0], name[1]);
                    case ProductImageSize.px980x:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-980x.{2}", productId,
                           name[0], name[1]);
                    case ProductImageSize.px500x375:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-500x375.{2}", productId,
                        name[0], name[1]);
                    case ProductImageSize.pxx980:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-x980.{2}", productId,
                  name[0], name[1]);
                    case ProductImageSize.NoImage:
                        return "//s.makinaturkiye.com/no-image.png";
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
                return "//s.makinaturkiye.com/no-image.png";

            try
            {
                if (!productImageName.Contains(".jpg"))
                    productImageName = productImageName + ".jpg";
                string[] name = productImageName.Split('.');
                string picturePathIsExist = string.Empty;
                if (imageSize == ProductImageSize.px100)
                {

                    //System.IO.Path.Combine(, fileName);
                    picturePathIsExist = string.Format("/UserFiles/Product/{0}/thumbs/{1}-100X.{2}", productId, name[0],
                        name[1]);

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(picturePathIsExist)))
                    {
                        imageSize = ProductImageSize.px100;
                    }
                    else
                    {
                        imageSize = imageSize = ProductImageSize.px100x75;
                    }
                }
                switch (imageSize)
                {
                    case ProductImageSize.px100:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-100X.{2}", productId, name[0],
                            name[1]);
                    case ProductImageSize.px100x75:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-100x75.{2}", productId, name[0],
                            name[1]);
                    case ProductImageSize.px400x300:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-400x300.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px900x675:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-900x675.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px180x135:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-180x135.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.x160x120:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-160x120.{2}", productId,
                            name[0], name[1]);
                    case ProductImageSize.px200x150:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-200x150.{2}", productId,
                           name[0], name[1]);
                    case ProductImageSize.px980x:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-980x.{2}", productId,
                           name[0], name[1]);
                    case ProductImageSize.px500x375:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-500x375.{2}", productId,
                        name[0], name[1]);
                    case ProductImageSize.pxx980:
                        return string.Format("//s.makinaturkiye.com/Product/{0}/thumbs/{1}-x980.{2}", productId,
                  name[0], name[1]);
                    case ProductImageSize.NoImage:
                        return "//s.makinaturkiye.com/no-image.png";
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
            return string.Format("//www.makinaturkiye.com/Content/V2/images/makinaturkiye-dark.png");
        }
        public static string GetHomeSectorImagePath(string imagePath)
        {
            if (imagePath == null)
                return "";

            return string.Format("//s.makinaturkiye.com/Images/CategoryHomePageImageFolder/{0}", imagePath);
        }


        public static string GetBannerImagePath(string bannerResource)
        {
            return string.Format("//s.makinaturkiye.com/Banner/ImagesThumb/{0}", bannerResource);
        }
        public static string GetCategoryBannerImagePath(string bannerResource)
        {
            return string.Format("//s.makinaturkiye.com//Banner/CategoryImages/{0}", bannerResource);
        }
        public static string GetCategoryIconPath(string bannerResource)
        {
            return string.Format("//s.makinaturkiye.com/Images/CategoryIconImageFolder/{0}", bannerResource);
        }
    }
}
