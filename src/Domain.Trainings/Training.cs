﻿using System.Collections.Generic;
using ProcentCqrs.Infrastructure.System;

namespace ProcentCqrs.Domain.Trainings
{
    public class Training
    {
        private int _id;
        private string _name;
        private IList<TrainingAssignment> _trainees = new List<TrainingAssignment>();

        protected Training() { }

        public Training(string name)
        {
            _name = name;
        }

        public virtual TrainingAssignment AssignTrainee(Trainee trainee)
        {
            var newAssignment = new TrainingAssignment(this, trainee, ApplicationTime.Current);
            return newAssignment;
        }
    }
}