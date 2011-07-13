namespace ProcentCqrs.Domain.Core.Events.Trainings
{
    public class TrainingWasAdded : IEvent
    {
        public readonly int Id;
        public readonly string Name;

        public TrainingWasAdded(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}