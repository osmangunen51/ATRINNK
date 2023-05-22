namespace NeoSistem.Trinnk.Core.Web
{
    using global::Trinnk.Core;
    using Properties;
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Web.Mvc;

    public class Controller : System.Web.Mvc.AsyncController
    {
        public dynamic ViewModel { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            ViewModel = new ExpandoObject();
            base.Initialize(requestContext);
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


        public bool SendMail(MailMessage mailMessage, NetworkCredential NetworkCredential=null)
        {
            bool Status = false;
            try
            {
                if (NetworkCredential==null)
                {
                    NetworkCredential = new NetworkCredential(AppSettings.MailUserName, AppSettings.MailPassword);
                }
                using (SmtpClient sc = new SmtpClient())
                {
                    sc.Port = AppSettings.MailPort;                                                                   
                    sc.Host = AppSettings.MailHost;                                                      
                    sc.EnableSsl = AppSettings.MailSsl;                                                  
                    sc.Credentials = NetworkCredential;
                    sc.Send(mailMessage);
                    Status = true;
                }
            }
            catch (Exception Hata)
            {
                Status = false;
                TempData["StoreEmailError"]=Hata.Message;
            }
            return Status;
        }

        protected virtual ActionResult PostView<TModel>(string viewName, Func<TModel, ActionResult> method, TModel model)
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

            return View(viewName, model);
        }

        protected virtual ActionResult PostView<TModel>(Func<TModel, ActionResult> method, TModel model)
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

            return View(model);
        }

        protected virtual ActionResult PostView<TVar, TModel>(string viewName, Func<TModel, TVar, ActionResult> method, TModel model, TVar variable)
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

            return View(viewName, model);
        }

        protected virtual ActionResult PostView<TVar, TModel>(Func<TModel, TVar, ActionResult> method, TModel model, TVar variable)
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

            return View(model);
        }

    }
}
