using System;

namespace ProcentCqrs.Domain.Core.Commands.Users
{
    public class RenameUserCommand : ICommand
    {
        public readonly int UserId;
        public readonly string FirstName;
        public readonly string LastName;

        public RenameUserCommand(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}