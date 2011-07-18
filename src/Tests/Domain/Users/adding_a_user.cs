using FakeItEasy;
using Machine.Specifications;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Users;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;

namespace ProcentCqrs.Tests.Domain.Users
{
    [Subject("Adding a user")]
    public abstract class adding_a_user : command_test<AddUserCommand>
    {
        Establish context = () =>
            {
                Email = Randomizer.Email();
                FirstName = Randomizer.String();
                LastName = Randomizer.String();

                Command = new AddUserCommand(Email, FirstName, LastName);
            };

        protected static string Email;
        protected static string FirstName;
        protected static string LastName;

        protected static dynamic AllUsers()
        {
            return DbRead.Users.All().ToList();
        }

        protected static dynamic User()
        {
            return AllUsers()[0];
        }
    }

    public class when_adding_a_user : adding_a_user
    {
        It should_persist_the_new_user = () => ((int) AllUsers().Count).ShouldEqual(1);

        It should_save_given_email = () => ((string) User().Email).ShouldEqual(Email);

        It should_save_given_first_name = () => ((string) User().FirstName).ShouldEqual(FirstName);

        It should_save_given_last_name = () => ((string) User().LastName).ShouldEqual(LastName);

        It should_publish_event = () => A.CallTo(() => EventPublisher
                .Publish(A<UserWasAdded>.That.Matches(x => x.Id == 1 && x.Email == Email && x.FirstName == FirstName && x.LastName == LastName)))
                .MustHaveHappened(Repeated.Exactly.Once)
                ;
    }
}