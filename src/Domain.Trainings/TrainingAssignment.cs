using System;

namespace ProcentCqrs.Domain.Trainings
{
    public class TrainingAssignment
    {
        private int _id;
        private Training _training;
        private Trainee _trainee;
        private DateTime _assignmentDate;

        protected TrainingAssignment() { }

        public TrainingAssignment(Training training, Trainee trainee, DateTime assignmentDate)
        {
            _training = training;
            _trainee = trainee;
            _assignmentDate = assignmentDate;
        }
    }
}