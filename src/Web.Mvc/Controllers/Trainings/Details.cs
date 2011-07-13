using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProcentCqrs.Web.Mvc.Controllers
{
    public partial class TrainingsController
    {
        [HttpGet]
        public virtual ActionResult Details(int id)
        {
            var training = _db.TrainingForTrainingsDetails.FindById(id);
            var users = _db.UserForTrainingsDetails.QueryByTrainingId(id)
                .ToList<UserForTrainingsDetails>();

            var model = new TrainingsDetailsModel
            {
                TrainingInfo = training,
                Users = users,
            };

            return View(model);
        }

        public class TrainingsDetailsModel
        {
            public virtual TrainingForTrainingsDetails TrainingInfo { get; set; }
            public virtual IEnumerable<UserForTrainingsDetails> Users { get; set; }
        }

        public class TrainingForTrainingsDetails
        {
            public virtual int Id { get; set; }
            public virtual string Name { get; set; }
            public virtual int UsersCount { get; set; }
        }

        public class UserForTrainingsDetails
        {
            public virtual int Id { get; set; }
            public virtual string Email { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual DateTime AssignmentDate { get; set; }
        }
    }
}