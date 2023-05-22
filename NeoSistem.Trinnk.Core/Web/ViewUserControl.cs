namespace NeoSistem.Trinnk.Core.Web
{
    using System;

    public class ViewUserControl : System.Web.Mvc.ViewUserControl
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

    }
    public class ViewUserControl<TModel> : System.Web.Mvc.ViewUserControl<TModel>
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

    }
}
