using System.Collections.Generic;
using System.Web.Mvc;
using ProcentCqrs.Web.Mvc.MvcExtensions;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class UsersController : CqrsController
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            var model = new UsersIndexModel
                            {
                                AllUsers = _db.UsersForUsersIndex.All()
                                    .ToList<UsersForUsersIndex>()
                            };

            return View(model);
        }

        public class UsersIndexModel
        {
            public virtual IEnumerable<UsersForUsersIndex> AllUsers { get; set; }
        }

        public class UsersForUsersIndex
        {
            public virtual int Id { get; set; }
            public virtual string Email { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
        }
    }
}