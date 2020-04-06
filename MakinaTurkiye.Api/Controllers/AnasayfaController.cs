using System.Web.Mvc;

namespace MakinaTurkiye.Api.Controllers
{
    public class AnasayfaController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "MakinaTürkiye Servisi";
            return View();
        }
    }
}
