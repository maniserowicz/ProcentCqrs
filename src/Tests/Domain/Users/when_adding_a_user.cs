using FakeItEasy;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Users;
using ProcentCqrs.Domain.Users.CommandHandlers;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;
using Xunit;

namespace ProcentCqrs.Tests.Domain.Users
{
    public class when_adding_a_user : CommandHandlerTest<AddUserHandler, AddUserCommand>
    {
        private string _firstName;
        private string _lastName;
        private string _email;

        protected override AddUserCommand CreateCommand()
        {
            _firstName = Randomizer.String();
            _lastName = Randomizer.String();
            _email = Randomizer.Email();

            return new AddUserCommand(_email, _firstName, _lastName);
        }

        private dynamic allUsers;

        protected override void Before_assertions()
        {
            allUsers = DbRead.Users.All().ToList();
        }

        [Fact]
        public void it_should_save_new_user()
        {
            Run();

            Assert.Equal(1, allUsers.Count);
        }

        [Fact]
        public void it_should_save_given_data()
        {
            Run();

            Assert.Equal(_email, allUsers[0].Email);
            Assert.Equal(_firstName, allUsers[0].FirstName);
            Assert.Equal(_lastName, allUsers[0].LastName);
        }

        [Fact]
        public void it_should_publish_event()
        {
            Run();

            A.CallTo(() => EventPublisher
                .Publish(A<UserWasAdded>.That.Matches(x => x.Id == 1 && x.Email == _email && x.FirstName == _firstName && x.LastName == _lastName)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}