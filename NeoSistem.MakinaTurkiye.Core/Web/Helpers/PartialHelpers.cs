namespace NeoSistem.MakinaTurkiye.Core.Web.Helpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class PartialHelpers
    {

        public static string BannerRender(this HtmlHelper helper, string src, int width, int height)
        {
            string view = String.Format("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" width=\"{0}\" height=\"{1}\"><PARAM name=wmode value=opaque><param name=\"Movie\" value=\"{2}\"><param name=\"Src\" value=\"{2}\"><param name=\"Quality\" value=\"High\"></object>", width, height, src);
            return Helpers.ToHtmlString(MvcHtmlString.Create(view), false);
        }

        public static string RenderScriptPartial(this HtmlHelper htmlHelper, string partialViewName)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext,
      partialViewName);
            if (result.View != null)
            {
                MvcHtmlString mvcString = htmlHelper.Partial(partialViewName, null, htmlHelper.ViewData);
                return Helpers.ToScriptString(mvcString);
            }
            return "";
        }

        public static string RenderScriptPartial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext, partialViewName);
            if (result.View != null)
            {
                MvcHtmlString mvcString = htmlHelper.Partial(partialViewName, null, viewData);
                return Helpers.ToScriptString(mvcString);
            }
            return "";
        }

        public static string RenderScriptPartial(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext, partialViewName);
            if (result.View != null)
            {
                MvcHtmlString mvcString = htmlHelper.Partial(partialViewName, model, htmlHelper.ViewData);
                return Helpers.ToScriptString(mvcString);
            }
            return "";
        }

        public static string RenderHtmlPartial(this HtmlHelper htmlHelper, string partialViewName)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext, partialViewName);

            if (result.View != null)
            {
                MvcHtmlString mvcString = htmlHelper.Partial(partialViewName, null, htmlHelper.ViewData);
                return Helpers.ToHtmlString(mvcString, true);
            }
            return "";
        }

        public static string RenderHtmlPartial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext, partialViewName);
            if (result.View != null)
            {
                MvcHtmlString mvcString = htmlHelper.Partial(partialViewName, null, viewData);
                return Helpers.ToHtmlString(mvcString, true);
            }
            return "";
        }

        public static string RenderHtmlPartial(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext, partialViewName);
            if (result.View != null)
            {
                MvcHtmlString mvcString = htmlHelper.Partial(partialViewName, model, htmlHelper.ViewData);
                return Helpers.ToHtmlString(mvcString, true);
            }
            return "";
        }
    }
}