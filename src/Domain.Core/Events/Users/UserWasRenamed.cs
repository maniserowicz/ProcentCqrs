namespace ProcentCqrs.Domain.Core.Events.Users
{
    public class UserWasRenamed : IEvent
    {
        public readonly int Id;
        public readonly string FirstName;
        public readonly string LastName;

        public UserWasRenamed(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}