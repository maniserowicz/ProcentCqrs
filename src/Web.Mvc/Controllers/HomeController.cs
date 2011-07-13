using System.Web.Mvc;
using ProcentCqrs.Web.Mvc.MvcExtensions;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class HomeController : CqrsController
    {
        public virtual ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }
    }
}