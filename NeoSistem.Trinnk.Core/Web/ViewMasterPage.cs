using System;
using System.Web;

namespace NeoSistem.Trinnk.Core.Web
{
    public class ViewMasterPage : System.Web.Mvc.ViewMasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var baseController = this.ViewContext.Controller as Controller;
            if (baseController != null)
            {
                View = baseController.ViewModel;
            }
        }

        public dynamic View { get; set; }

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
