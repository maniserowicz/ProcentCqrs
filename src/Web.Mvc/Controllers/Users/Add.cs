using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProcentCqrs.Domain.Core.Commands.Users;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class UsersController
    {
        [HttpGet]
        public virtual ActionResult RenderAdd()
        {
            return PartialView(MVC.Users.Views.AddUserPanel);
        }

        [HttpPost]
        public virtual ActionResult Add(UsersAddModel model)
        {
            if (ModelState.IsValid == false)
            {
                throw new ArgumentException();
            }

            _commandSender.Send(new AddUserCommand(model.Email, model.FirstName, model.LastName));

            return RedirectToAction(MVC.Users.Index());
        }

        public class UsersAddModel
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public virtual string Email { get; set; }
            [Required]
            public virtual string FirstName { get; set; }
            [Required]
            public virtual string LastName { get; set; }
        }
    }
}
