namespace ProcentCqrs.Domain.Core.Commands.Users
{
    public class AddUserCommand : ICommand
    {
        public readonly string Email;
        public readonly string FirstName;
        public readonly string LastName;

        public AddUserCommand(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}