using System.Web.Mvc;
using ProcentCqrs.Web.Mvc.MvcExtensions;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public class HomeController : CqrsController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}