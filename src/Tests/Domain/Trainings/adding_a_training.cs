using FakeItEasy;
using Machine.Specifications;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;

namespace ProcentCqrs.Tests.Domain.Trainings
{
    [Subject("Adding a training")]
    public abstract class adding_a_training : command_test<AddTrainingCommand>
    {
        Establish context = () =>
        {
            Name = Randomizer.String();

            Command = new AddTrainingCommand(Name);
        };

        protected static string Name;

        protected static dynamic AllTrainings()
        {
            return DbRead.Trainings.All().ToList();
        }

        protected static dynamic Training()
        {
            return AllTrainings()[0];
        }
    }

    public class when_adding_a_training : adding_a_training
    {
        It should_persist_the_new_training = () => ((int) AllTrainings().Count).ShouldEqual(1);

        It should_save_given_name = () => ((string) Training().Name).ShouldEqual(Name);

        It should_publish_event = () => A.CallTo(() => EventPublisher
                .Publish(A<TrainingWasAdded>.That.Matches(x => x.Id == 1 && x.Name == Name)))
                .MustHaveHappened(Repeated.Exactly.Once)
                ;
    }
}