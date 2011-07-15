using FakeItEasy;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Domain.Trainings.CommandHandlers;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;
using Xunit;

namespace ProcentCqrs.Tests.Domain.Trainings
{
    public class when_adding_a_training : CommandHandlerTest<AddTrainingHandler, AddTrainingCommand>
    {
        private string _name;

        protected override AddTrainingCommand CreateCommand()
        {
            _name = Randomizer.String();
            return new AddTrainingCommand(_name);
        }

        private dynamic allTrainings;

        protected override void Before_assertions()
        {
            allTrainings = DbRead.Trainings.All().ToList();
        }

        [Fact]
        public void it_should_save_new_training()
        {
            Run();

            Assert.Equal(1, allTrainings.Count);
        }

        [Fact]
        public void it_should_save_given_name()
        {
            Run();

            Assert.Equal(_name, allTrainings[0].Name);
        }

        [Fact]
        public void it_should_publish_event()
        {
            Run();

            A.CallTo(() => EventPublisher
                .Publish(A<TrainingWasAdded>.That.Matches(x => x.Id == 1 && x.Name == _name)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}