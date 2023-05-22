using System.Web.Mvc;

namespace Trinnk.Api.Controllers
{
    public class AnasayfaController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Makina Türkiye Servisi";
            return View();
        }
    }
}