﻿using System.Web.Mvc;

namespace MakinaTurkiye.Localization.Helpers
{
    public static partial class HtmlHelperExtensions
    {
        /// <summary>
        /// Razor template using @Html.Localize("id")
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString Localize(this HtmlHelper helper, string id, string Culture, string DefaultDeger = "")
        {
            return new MvcHtmlString(Localization.Localize(id, Culture, DefaultDeger));
        }

        /// <summary>
        /// Razor template using @Html.Localize("id")
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static MvcHtmlString Localize(this HtmlHelper helper, Inline lang)
        {
            return new MvcHtmlString(Localization.Localize(lang));
        }

        /// <summary>
        /// or Razor template using @Html.Loc("id")
        /// </summary>
        /// <param id="helper"></param>
        /// <param id="id"></param>
        /// <returns></returns>
        public static MvcHtmlString Loc(this HtmlHelper helper, string id)
        {
            return new MvcHtmlString(Localization.Localize(id));
        }

        /// <summary>
        /// or Razor template using @Html.Loc("id")
        /// </summary>
        /// <param id="helper"></param>
        /// <param id="id"></param>
        /// <returns></returns>
        public static MvcHtmlString Get(this HtmlHelper helper, string id)
        {
            return new MvcHtmlString(Localization.Localize(id));
        }
    }
}