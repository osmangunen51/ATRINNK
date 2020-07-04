namespace NeoSistem.MakinaTurkiye.Core.Web
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class ViewPage : System.Web.Mvc.ViewPage
  {
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      var baseController = this.ViewContext.Controller as Controller;
      if(baseController != null) {
        View = baseController.ViewModel;
      }
      CompressCss();
    }

    public dynamic View { get; set; }

    protected virtual void CompressCss()
    {
      foreach(Control control in Page.Header.Controls) {
        HtmlControl c = control as HtmlControl;
        if(c != null &&
          c.Attributes["type"] != null &&
          c.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase)) {

          if(!c.Attributes["href"].StartsWith("http://")) {
            string url = ResolveCssUrl(c.Attributes["href"]);
            c.Attributes["href"] = url;
            c.EnableViewState = false;
          }
        }
      }
    }

    public virtual void AddJavaScriptInclude(string url, bool addDeferAttribute)
    {
      HtmlGenericControl script = new HtmlGenericControl("script");
      script.Attributes["type"] = "text/javascript";
      script.Attributes["src"] = ResolveScriptUrl(url);
      if(addDeferAttribute) {
        script.Attributes["defer"] = "defer";
      }

      Page.Header.Controls.Add(script);
    }

    public virtual string ResolveScriptUrl(string url)
    {
      return "/NSResource.axd?path=" + HttpUtility.UrlEncode(url);
    }

    public virtual string ResolveCssUrl(string url)
    {
      return "/nscss.axd?name=" + HttpUtility.UrlEncode(url);
    }
  }

  public class ViewPage<TModel> : System.Web.Mvc.ViewPage<TModel>
  {
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      var baseController = this.ViewContext.Controller as Controller;
      if(baseController != null) {
        View = baseController.ViewModel;
      }
      //TODO: V2 icin gecici olarak kapatildi
      //CompressCss();
    }


    public dynamic View { get; set; }


    protected virtual void CompressCss()
    {
      foreach(Control control in Page.Header.Controls) {
        HtmlControl c = control as HtmlControl;
        if(c != null &&
          c.Attributes["type"] != null &&
          c.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase)) {

          if(!c.Attributes["href"].StartsWith("http://")) {
            string url = ResolveCssUrl(c.Attributes["href"]);
            c.Attributes["href"] = url;
            c.EnableViewState = false;
          }
        }
      }
    }

    public virtual void AddJavaScriptInclude(string url, bool addDeferAttribute)
    {
      HtmlGenericControl script = new HtmlGenericControl("script");
      script.Attributes["type"] = "text/javascript";
      script.Attributes["src"] = ResolveScriptUrl(url);
      if(addDeferAttribute) {
        script.Attributes["defer"] = "defer";
      }

      Page.Header.Controls.Add(script);
    }

    public virtual string ResolveScriptUrl(string url)
    {
      return "/NSResource.axd?path=" + HttpUtility.UrlEncode(url);
    }

    public virtual string ResolveCssUrl(string url)
    {
      return "/nscss.axd?name=" + HttpUtility.UrlEncode(url);
    }
  }
}
