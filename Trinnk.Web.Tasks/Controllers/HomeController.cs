using Trinnk.Utilities.Mvc;
using System.Web.Mvc;

namespace Trinnk.Web.Tasks.Controllers
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