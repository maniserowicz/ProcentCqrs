namespace ProcentCqrs.Domain.Users
{
    public class User
    {
        private int _id;

        private string _email;
        private string _firstName;
        private string _lastName;

        public User(string email, string firstName, string lastName)
        {
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}