using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProcentCqrs.Domain.Core.Commands.Trainings;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class TrainingsController
    {
        [HttpGet]
        public virtual ActionResult RenderAssignUser(int trainingId)
        {
            var model = new TrainingsAssignUserModel()
            {
                TrainingId = trainingId,
            };
            return PartialView(MVC.Trainings.Views.AssignUserToTrainingPanel, model);
        }

        [HttpPost]
        public virtual ActionResult AssignUser(TrainingsAssignUserModel model)
        {
            if (ModelState.IsValid == false)
            {
                throw new ArgumentException();
            }

            _commandSender.Send(new AssignTraineeToTrainingCommand(model.TrainingId, model.UserId));

            return RedirectToAction(MVC.Trainings.Details(model.TrainingId));
        }

        public class TrainingsAssignUserModel
        {
            [Required]
            public virtual int UserId { get; set; }
            [Required]
            [HiddenInput(DisplayValue = false)]
            public virtual int TrainingId { get; set; }
        }
    }
}