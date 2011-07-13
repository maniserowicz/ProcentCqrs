using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProcentCqrs.Domain.Core.Commands.Trainings;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class TrainingsController
    {
        [HttpGet]
        public virtual ActionResult RenderAdd()
        {
            return PartialView(MVC.Trainings.Views.AddTrainingPanel);
        }

        [HttpPost]
        public virtual ActionResult Add(TrainingsAddModel model)
        {
            if (ModelState.IsValid == false)
            {
                throw new ArgumentException();
            }

            _commandSender.Send(new AddTrainingCommand(model.Name));

            return RedirectToAction(MVC.Trainings.Index());
        }

        public class TrainingsAddModel
        {
            [Required]
            public virtual string Name { get; set; }
        }
    }
}