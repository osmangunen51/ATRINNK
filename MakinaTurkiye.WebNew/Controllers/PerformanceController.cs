using System.Threading.Tasks;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    public class PerformanceController: System.Web.Mvc.AsyncController
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Index2()
        {
            return View();
        }
    }
}