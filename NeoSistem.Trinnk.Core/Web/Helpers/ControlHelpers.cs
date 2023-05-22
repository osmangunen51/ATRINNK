namespace NeoSistem.Trinnk.Core.Web.Helpers
{
    using NeoSistem.Trinnk.Core.Web.Controls;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class ControlHelpers
    {
        public static AdminPanel BeginPanel(this HtmlHelper helper)
        {
            helper.ViewContext.Writer.Write("<table cellpadding=\"0\" cellspacing=\"0\" style=\"font-size: 1px; margin-left: 10px;\"><tr><td class=\"topleft\"></td><td class=\"top\"></td><td class=\"topright\"></td></tr><tr><td class=\"left\"></td><td style=\"font-size: 13px\">");
            AdminPanel form = new AdminPanel(helper.ViewContext);
            return form;
        }
        public static string UActionLinkIsSub(string href, string Issub)
        {
            string siteLiveUrl = WebConfigurationManager.AppSettings["LiveSiteUrl"];
            string siteSubLiveUrl = "*http://{0}.trinnk.com";
            int MainPartyRef = 0;
            string url = "";
            if (Int32.TryParse(Issub, out MainPartyRef))
            {
                url = string.Format(siteSubLiveUrl, Issub) + href;
            }
            else
            {
                url = siteLiveUrl + href;
            }

            return url;
        }

        public static void EndPanel(this HtmlHelper helper)
        {
            helper.ViewContext.Writer.Write("<td class=\"right\"></td></tr><tr><td class=\"bottomleft\"></td><td class=\"bottom\"></td><td class=\"bottomright\"></td></tr></table>");
        }

        public static MvcHtmlString UActionLink(this HtmlHelper helper, string title, string href, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("a") { InnerHtml = title };
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("href", href, true);
            string buildStr = builder.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(buildStr);
        }

        public static MvcHtmlString UActionLink(this HtmlHelper htmlHelper, string title, string href)
        {
            return UActionLink(htmlHelper, title, href, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString UActionLink(this HtmlHelper htmlHelper, string title, string href, object htmlAttributes)
        {
            return UActionLink(htmlHelper, title, href, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }


        public static MvcHtmlString Image(this HtmlHelper helper, string src, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttributes<string, object>(htmlAttributes);
            if (builder.Attributes.ContainsKey("path"))
            {
                src = string.Format("/image.axd?picture={0}&path={1}", src, builder.Attributes["path"]);
            }
            else
            {
                src = string.Format("/image.axd?picture={0}", src);
            }
            builder.Attributes.Remove("path");
            builder.Attributes.Add("imageType", "__CACHEIMAGE");
            builder.MergeAttribute("src", src, true);
            string buildStr = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(buildStr);
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src)
        {
            return Image(htmlHelper, src, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, object htmlAttributes)
        {
            return Image(htmlHelper, src, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }


        public static MvcHtmlString Thumbnail(this HtmlHelper helper, string src, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttributes<string, object>(htmlAttributes);
            if (builder.Attributes.ContainsKey("path"))
            {
                src = string.Format("/timage.axd?p={0}&s={1}&path={2}", src, builder.Attributes["size"], builder.Attributes["path"]);
            }
            else
            {
                src = string.Format("/timage.axd?p={0}&s={1}", src, builder.Attributes["size"]);
            }
            builder.Attributes.Remove("path");
            builder.Attributes.Remove("size");
            builder.Attributes.Add("imageType", "__THUMBCACHEIMAGE");
            builder.MergeAttribute("src", src, true);
            string buildStr = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(buildStr);
        }

        public static MvcHtmlString Thumbnail(this HtmlHelper htmlHelper, string src)
        {
            return Thumbnail(htmlHelper, src, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString Thumbnail(this HtmlHelper htmlHelper, string src, object htmlAttributes)
        {
            return Thumbnail(htmlHelper, src, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString ImageButton(this HtmlHelper htmlHelper, string src, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("type", "image");
            builder.MergeAttribute("src", src, true);
            string buildStr = builder.ToString(TagRenderMode.SelfClosing);
            string action = "<a href=\"javascript:void();\" >{0}</a>";
            return MvcHtmlString.Create(String.Format(action, buildStr));
        }

        public static MvcHtmlString ImageButton(this HtmlHelper htmlHelper, string src)
        {
            return ImageButton(htmlHelper, src, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString ImageButton(this HtmlHelper htmlHelper, string src, object htmlAttributes)
        {
            return ImageButton(htmlHelper, src, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString OfficeButton(this HtmlHelper htmlHelper, string text, IDictionary<string, object> htmlAttributes)
        {
            string guidId = Guid.NewGuid().ToString("N");
            string inner = "<input type=\"submit\" id=\"_post{1}\" style=\"display: none;\" /><div class=\"cdBtnL{1}\"></div><div class=\"cdBtnM{1}\">{0}</div><div class=\"cdBtnR{1}\"></div>";
            TagBuilder builder = new TagBuilder("a") { InnerHtml = string.Format(inner, text, guidId) };
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("href", "javascript:void(0);", true);
            builder.MergeAttribute("writable:Id", "_post" + guidId);
            builder.MergeAttribute("class", "cdBtn");
            string buildStr = builder.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(buildStr);
        }

        public static MvcHtmlString OfficeButton(this HtmlHelper htmlHelper, string text)
        {
            return OfficeButton(htmlHelper, text, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString OfficeButton(this HtmlHelper htmlHelper, string text, object htmlAttributes)
        {
            return OfficeButton(htmlHelper, text, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString FileUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.FileHelper(ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData).Model, ExpressionHelper.GetExpressionText((LambdaExpression)expression), htmlAttributes);
        }

        public static MvcHtmlString FileUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.FileUploadFor<TModel, TProperty>(expression, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString FileUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.FileUploadFor<TModel, TProperty>(expression, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }


        private static MvcHtmlString FileHelper(this HtmlHelper htmlHelper, object model, string expression, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(htmlHelper, "file", expression, model, htmlAttributes);
        }

        private static MvcHtmlString InputHelper(HtmlHelper htmlHelper, string inputType, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            ModelState state = null;
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            TagBuilder builder = new TagBuilder("input");
            builder.MergeAttributes(htmlAttributes);
            builder.MergeAttribute("type", inputType);
            builder.MergeAttribute("name", name, true);
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);

            string str3 = (string)GetModelStateValue(htmlHelper, name, typeof(string));
            builder.MergeAttribute("value", str3 ?? str, true);

            builder.GenerateId(name);

            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out state) && (state.Errors.Count > 0))
            {
                builder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            string buildStr = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(buildStr);
        }

        private static object GetModelStateValue(HtmlHelper helper, string key, Type destinationType)
        {
            ModelState state = null;
            if (helper.ViewData.ModelState.TryGetValue(key, out state) && (state.Value != null))
            {
                return state.Value.ConvertTo(destinationType, null);
            }
            return null;
        }

    }
}