using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.IO;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;

namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
    public static class ImageHelpers
    {
        //test zorunlu

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

        //public static string GetStoreImage(this Store store, string size)
        //{
        //    return GetStoreImage(store.MainPartyId, store.StoreLogo, size);
        //}

        public static MvcHtmlString GetProductImage(this HtmlHelper helper, int? productID, string imageName, ImageSize imageSize, IDictionary<string, object> htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("img");
            string imagePath = string.Empty;

            if (productID.HasValue)
            {
                if (htmlAttributes != null)
                    builder.MergeAttributes<string, object>(htmlAttributes);

                if (string.IsNullOrEmpty(imageName))
                    return MvcHtmlString.Create(string.Empty);

                if (!imageName.Contains(".jpg"))
                    imageName = imageName + ".jpg";

               // imageName = imageName.Replace(".jpg", ".webp");

                string[] name = imageName.Split('.');


                switch (imageSize)
                {
                    case ImageSize.px100:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-100X.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    case ImageSize.px100x75:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-100x75.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    case ImageSize.px160x120:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-160x120.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;

                    case ImageSize.px180x135:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-180x135.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    case ImageSize.px400x300:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-400x300.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    case ImageSize.px900x675:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-900x675.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    default:
                        return MvcHtmlString.Create(string.Empty);
                }
            }
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public enum ImageSize
        {

            px100,
            px100x75,
            px160x120,
            px180x135,
            px400x300,
            px900x675
        }


        public static string EncodeAsFileName(this string fileName)
        {
            return Regex.Replace(fileName, "[" + Regex.Escape(
                    new string(Path.GetInvalidFileNameChars())) + "]", "");
        }

        public static MvcHtmlString EntityImage(this HtmlHelper helper, string src, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttributes<string, object>(htmlAttributes);
            src = string.Format("/entityImg.axd?s={1}&path={3}&Id={2}&name={0}", EncodeAsFileName(src.Replace(" ", "-")), builder.Attributes["size"], builder.Attributes["Id"], builder.Attributes["path"]);

            builder.Attributes.Remove("size");
            builder.Attributes.Add("imageType", "__THUMBCACHEIMAGE");
            builder.MergeAttribute("src", src, true);
            string buildStr = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(buildStr);
        }

        public static MvcHtmlString EntityImage(this HtmlHelper htmlHelper, string src)
        {
            return EntityImage(htmlHelper, src, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString EntityImage(this HtmlHelper htmlHelper, string src, object htmlAttributes)
        {
            return EntityImage(htmlHelper, src, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }
    }
}