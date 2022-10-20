using MakinaTurkiye.Utilities.Mvc;
using System.Web.Mvc;

namespace MakinaTurkiye.Web.Tasks.Controllers
{
    [AllowSameSite]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
    }
}