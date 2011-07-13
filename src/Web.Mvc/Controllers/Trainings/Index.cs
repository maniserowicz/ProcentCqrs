using System.Collections.Generic;
using System.Web.Mvc;
using ProcentCqrs.Web.Mvc.MvcExtensions;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class TrainingsController : CqrsController
    {
        public virtual ActionResult Index()
        {
            var model = new TrainingsIndexModel();

            return View(model);
        }

        public class TrainingsIndexModel
        {
            public virtual IEnumerable<TrainingsForTrainingsIndex> AllTrainings { get; set; }
        }

        public class TrainingsForTrainingsIndex
        {
            public virtual int Id { get; set; }
            public virtual string Name { get; set; }
            public virtual int UsersCount { get; set; }
        }
    }
}