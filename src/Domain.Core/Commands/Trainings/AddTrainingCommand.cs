namespace ProcentCqrs.Domain.Core.Commands.Trainings
{
    public class AddTrainingCommand : ICommand
    {
        public readonly string Name;

        public AddTrainingCommand(string name)
        {
            Name = name;
        }
    }
}