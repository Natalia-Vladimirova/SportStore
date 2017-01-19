using System.Web.Mvc;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Добро пожаловать в Sport Store!";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
