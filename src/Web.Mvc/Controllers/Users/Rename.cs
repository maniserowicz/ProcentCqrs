using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProcentCqrs.Domain.Core.Commands.Users;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class UsersController
    {
        [HttpGet]
        public virtual ActionResult RenderRename()
        {
            return PartialView(MVC.Users.Views.RenameUserDialog);
        }

        [HttpPost]
        public virtual ActionResult Rename(UsersRenameModel model)
        {
            if (ModelState.IsValid == false)
            {
                throw new ArgumentException();
            }

            _commandSender.Send(new RenameUserCommand(model.UserId, model.FirstName, model.LastName));

            return RedirectToAction(MVC.Users.Index());
        }

        public class UsersRenameModel
        {
            [Required]
            [HiddenInput(DisplayValue = false)]
            public virtual int UserId { get; set; }
            [Required]
            public virtual string FirstName { get; set; }
            [Required]
            public virtual string LastName { get; set; }
        }
    }
}
