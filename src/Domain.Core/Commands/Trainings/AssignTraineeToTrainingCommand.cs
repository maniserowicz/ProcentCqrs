namespace ProcentCqrs.Domain.Core.Commands.Trainings
{
    public class AssignTraineeToTrainingCommand : ICommand
    {
        public readonly int TrainingId;
        public readonly int UserId;

        public AssignTraineeToTrainingCommand(int trainingId, int userId)
        {
            TrainingId = trainingId;
            UserId = userId;
        }
    }
}