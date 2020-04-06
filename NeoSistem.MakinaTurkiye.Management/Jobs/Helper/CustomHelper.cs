namespace NeoSistem.MakinaTurkiye.Management.Helper
{
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class CustomHelper
  {
    public static MvcHtmlString FilterTextBox(this HtmlHelper helper, string name, bool isDate = false, bool clear = true)
    {
      var builder = new StringBuilder("<table style=\"width: 100%; border-collapse: separate\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"width: 100%; padding-right: 5px; position:relative; border: solid 1px #CCC; background-color: #FFF\">");
      builder.AppendFormat("<input id=\"{0}\" name=\"{0}\" model=\"{0}\" class=\"Search {1}\" style=\"width: 85%; border: none;\" />", name, isDate ? "date" : "");
      if(clear) { 
        builder.AppendFormat("<span class=\"ui-icon ui-icon-close searchClear\" onclick=\"clearSearch('{0}');\"> </span>", name);
      }
      builder.Append("</td></tr></tbody></table>");
      return MvcHtmlString.Create(builder.ToString());
    }
    public static MvcHtmlString FilterDropDown(this HtmlHelper helper, string name, SelectList items)
    {
      var builder = new StringBuilder("<table style=\"width: 100%; border-collapse: separate\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"width: 100%;  position:relative;  \">");
      builder.AppendFormat("<select id=\"{0}\" name=\"{0}\" class=\"dropdown\" onchange=\"Search();\" >", name);
      foreach(var item in items) {
        builder.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", item.Value, item.Text, item.Selected ? "selected" : "");
      }
      builder.Append("</select></td></tr></tbody></table>");
      return MvcHtmlString.Create(builder.ToString());
    }

    public static MvcHtmlString FilterCheckBox(this HtmlHelper helper, string name, string text, bool check = true, object htmlAttributes = null)
    {
      var atts = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
      var builder = new StringBuilder("<input type=\"checkbox\" style=\"float:left\" class=\"columnFilter\" ");
      foreach(var item in atts) {
        builder.AppendFormat(" {0}=\"{1}\" ", item.Key, item.Value);
      }
      builder.AppendFormat(" id=\"{0}\" name=\"{0}\" {1} /><div style=\"float: left; height: 13px; padding-top: 3px;\">", name, check ? "checked" : "");

      builder.AppendFormat("<label for=\"{0}\">{1}</label></div>", name, text);
      return MvcHtmlString.Create(builder.ToString());
    }

    public static MvcHtmlString SpanCheckBox(this HtmlHelper helper, string name, string text, bool check = true, object htmlAttributes = null)
    {
      var atts = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
      var builder = new StringBuilder("<input type=\"checkbox\" style=\"float:left\" ");
      foreach(var item in atts) {
        builder.AppendFormat(" {0}=\"{1}\" ", item.Key, item.Value);
      }
      builder.AppendFormat(" id=\"{0}\" name=\"{0}\" {1} /><div style=\"float: left; height: 13px; padding-top: 3px;\">", name, check ? "checked" : "");

      builder.AppendFormat("<label for=\"{0}\">{1}</label></div>", name, text);
      return MvcHtmlString.Create(builder.ToString());
    }
  }

}