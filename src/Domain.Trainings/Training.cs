using System.Collections.Generic;

namespace ProcentCqrs.Domain.Trainings
{
    public class Training
    {
        private int _id;
        private string _name;
        private IList<TrainingAssignment> _trainees = new List<TrainingAssignment>();

        public Training(string name)
        {
            _name = name;
        }
    }
}