namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using MakinaTurkiye.Management.Models.Entities;
    using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
    using Properties;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [HandleError]
    public class BaseController : Core.Web.Controller
    {
        protected internal MakinaTurkiyeEntities entities = null;

        protected internal int PAGEID { get; set; }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!IsAccess(PAGEID))
            {
                if (!string.IsNullOrEmpty(Response.RedirectLocation))
                    return;
                Response.Redirect("/Home/Forbidden");
            }
        }

        protected bool IsAccess(int pageId)
        {
            if (pageId <= 0)
            {
                return true;
            }
            if (CurrentUserModel.CurrentManagement.Permissions != null)
            {
                return CurrentUserModel.CurrentManagement.Permissions.Any(c => c.PermissionId == pageId);
            }
            return false;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (CurrentUserModel.CurrentManagement.UserId == 0)
            {
                requestContext.HttpContext.Response.Redirect("/Account/Login?returnUrl=" + HttpUtility.UrlEncode(requestContext.HttpContext.Request.RawUrl));
            }

            base.Initialize(requestContext);
            entities = new MakinaTurkiyeEntities();
        }

        protected void UpdateClass<TFromClass, TToClass>(TFromClass fromClass, TToClass toClass)
        {
            var toType = typeof(TToClass);
            var fromType = typeof(TFromClass);

            var toProperties = toType.GetProperties();
            var fromProperties = fromType.GetProperties();

            foreach (var item in fromProperties)
            {
                var prop = toProperties.SingleOrDefault(p => p.Name == item.Name);
                if (prop != null)
                {
                    prop.SetValue(toClass, item.GetValue(fromClass, null), null);
                }
            }
        }

        protected virtual TResult PostView<TResult, TModel>(string viewName, string mastername, Func<TModel, TResult> method, TModel model) where TResult : ActionResult
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return method.Invoke(model);
                }
                else
                {
                    ModelState.AddModelError("", Resources.ValidationError);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", String.Format(Resources.RecordError, ex.Message));
            }

            ViewResult v = View();
            viewName = viewName ?? v.ViewName;
            mastername = mastername ?? v.MasterName;

            return View(viewName, mastername, model) as TResult;
        }

        protected virtual TResult PostView<TResult, TModel>(string viewName, Func<TModel, TResult> method, TModel model) where TResult : ActionResult
        {
            return PostView<TResult, TModel>(viewName, null, method, model);
        }

        protected virtual TResult PostView<TResult, TModel>(Func<TModel, TResult> method, TModel model) where TResult : ActionResult
        {
            return PostView<TResult, TModel>(null, null, method, model);
        }

        protected virtual ActionResult PostView<TModel>(string viewName, string mastername, Func<TModel, ActionResult> method, TModel model)
        {
            return PostView<ActionResult, TModel>(viewName, mastername, method, model);
        }

        protected virtual ActionResult PostView<TModel>(string viewName, Func<TModel, ActionResult> method, TModel model)
        {
            return PostView<TModel>(viewName, null, method, model);
        }

        protected virtual ActionResult PostView<TModel>(Func<TModel, ActionResult> method, TModel model)
        {
            return PostView<TModel>(null, method, model);
        }

        protected virtual TResult PostView<TResult, TVar, TModel>(string viewName, string mastername, Func<TModel, TVar, TResult> method, TModel model, TVar variable) where TResult : ActionResult
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return method.Invoke(model, variable);
                }
                else
                {
                    ModelState.AddModelError("", Resources.ValidationError);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", String.Format(Resources.RecordError, ex.Message));
            }

            return View(viewName, mastername, model) as TResult;
        }

        protected virtual TResult PostView<TResult, TVar, TModel>(string viewName, Func<TModel, TVar, TResult> method, TModel model, TVar value) where TResult : ActionResult
        {
            return PostView<TResult, TVar, TModel>(viewName, string.Empty, method, model, value);
        }

        protected virtual TResult PostView<TResult, TVar, TModel>(Func<TModel, TVar, TResult> method, TModel model, TVar value) where TResult : ActionResult
        {
            return PostView<TResult, TVar, TModel>(string.Empty, string.Empty, method, model, value);
        }

        protected virtual ActionResult PostView<TModel, TVar>(string viewName, string mastername, Func<TModel, TVar, ActionResult> method, TModel model, TVar value)
        {
            return PostView<ActionResult, TVar, TModel>(viewName, mastername, method, model, value);
        }

        protected virtual ActionResult PostView<TModel, TVar>(string viewName, Func<TModel, TVar, ActionResult> method, TModel model, TVar value)
        {
            return PostView(viewName, string.Empty, method, model, value);
        }

        protected virtual ActionResult PostView<TModel, TVar>(Func<TModel, TVar, ActionResult> method, TModel model, TVar value)
        {
            return PostView<TModel, TVar>(string.Empty, method, model, value);
        }


    }
}
