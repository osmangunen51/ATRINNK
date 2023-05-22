using NeoSistem.Trinnk.Core.Web.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Helper
{
    public static class ImageHelpers
    {
        public static string CDNhost { get { return "//s.trinnk.com"; } }

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
                string[] name = imageName.Split('.');

                switch (imageSize)
                {
                    case ImageSize.px200x150:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-200x150.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    case ImageSize.px400x300:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-400x300.{2}", productID, name[0], name[1]);
                        if (FileHelpers.HasFile(imagePath))
                            builder.Attributes.Add("src", imagePath);
                        break;
                    case ImageSize.px500x375:
                        imagePath = string.Format(CDNhost + "/Product/{0}/thumbs/{1}-500X375.{2}", productID, name[0], name[1]);
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


        public static string GetStoreImage(int storeId, string logo, string size)
        {
            try
            {
                if (size == "300")
                {
                    return string.Format(CDNhost + "/Store/{0}/thumbs/{1}-{2}x200.jpg", storeId,
                                                         logo.Replace("_logo", "").Replace(".jpg", ""), size);
                }
                else
                {
                    if (logo != null)
                    {
                        return string.Format(CDNhost + "/Store/{0}/thumbs/{1}-{2}x{2}.jpg", storeId,
                 logo.Replace("_logo", "").Replace(".jpg", ""), size);
                    }
                    return "";
                }


            }
            catch
            {
                return "";
            }
        }

        public enum ImageSize
        {
            px400x300,
            px900x675,
            px200x150,
            px500x375,
            NoImage
        }
    }

}