namespace ProcentCqrs.Domain.Core.Events.Trainings
{
    public class TraineeWasAssignedToTraining : IEvent
    {
        public readonly int TrainingId;
        public readonly int UserId;

        public TraineeWasAssignedToTraining(int trainingId, int userId)
        {
            TrainingId = trainingId;
            UserId = userId;
        }
    }
}