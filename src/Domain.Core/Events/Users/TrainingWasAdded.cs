namespace ProcentCqrs.Domain.Core.Events.Users
{
    public class UserWasAdded : IEvent
    {
        public readonly int Id;
        public readonly string Email;
        public readonly string FirstName;
        public readonly string LastName;

        public UserWasAdded(int id, string email, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}