
namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Helper
{
    using System.Web.Mvc;

    public static class CustomHelper
    {
        public static ContentPanel BeginContent(this HtmlHelper helper, string title = "&nbsp;")
        {
            helper.ViewContext.Writer.Write("<div class=\"hesabim_orta_icerik\">");
            helper.ViewContext.Writer.Write("<div class=\"orta_baslik\">");
            helper.ViewContext.Writer.Write(title);
            helper.ViewContext.Writer.Write("</div>");

            ContentPanel form = new ContentPanel(helper.ViewContext);
            return form;
        }

        public static void EndContent(this HtmlHelper helper)
        {
            helper.ViewContext.Writer.Write("</div>");
        }

        public static ContentPanel BeginLeft(this HtmlHelper helper, string title = "")
        {
            helper.ViewContext.Writer.Write("<div class=\"hesabim_sol_menu\">");
            helper.ViewContext.Writer.Write("<div class=\"hesabim_baslik\">");
            helper.ViewContext.Writer.Write(title);
            helper.ViewContext.Writer.Write("</div>");

            ContentPanel form = new ContentPanel(helper.ViewContext);
            return form;
        }

        public static void EndLeft(this HtmlHelper helper)
        {
            helper.ViewContext.Writer.Write("</div>");
        }

    }
}