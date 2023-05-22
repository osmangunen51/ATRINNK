using System;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Helper
{
    public class OpRedirectResult : RedirectResult
    {
        public OpRedirectResult(string url)
          : base(url)
        {

        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (context.IsChildAction)
            {
                throw new InvalidOperationException("Error: IsCHILD");
            }
            string url = this.Url;
            context.Controller.TempData.Keep();
            context.HttpContext.Response.Redirect(url, false);
        }
    }
}