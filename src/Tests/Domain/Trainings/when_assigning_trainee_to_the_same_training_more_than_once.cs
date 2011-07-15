using FakeItEasy;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Domain.Trainings.CommandHandlers;
using ProcentCqrs.Domain.Users.CommandHandlers;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;
using Xunit;

namespace ProcentCqrs.Tests.Domain.Trainings
{
    public class when_assigning_trainee_to_the_same_training_more_than_once :
        CommandHandlerTest<AssignTraineeToTrainingHandler, AssignTraineeToTrainingCommand>
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            TempHandler<AddUserHandler>().Handle(new AddUserCommand(Randomizer.Email(), Randomizer.String(), Randomizer.String()));
            TempHandler<AddTrainingHandler>().Handle(new AddTrainingCommand(Randomizer.String()));
            TempHandler<AssignTraineeToTrainingHandler>().Handle(new AssignTraineeToTrainingCommand(1, 1));
        }

        private dynamic allAssignemnts;

        protected override void Before_assertions()
        {
            allAssignemnts = DbRead.UserTrainings.All().ToList();
        }

        protected override AssignTraineeToTrainingCommand CreateCommand()
        {
            return new AssignTraineeToTrainingCommand(1, 1);
        }

        [Fact]
        public void it_should_throw()
        {
            Run();

            Assert.NotNull(Error);
        }

        [Fact]
        public void it_should_not_create_new_assignment()
        {
            Run();

            Assert.Equal(1, allAssignemnts.Count);
        }

        [Fact]
        public void it_should_not_publish_new_events()
        {
            Run();

            A.CallTo(() => EventPublisher.Publish(A<TraineeWasAssignedToTraining>._))
                .MustNotHaveHappened();
        }
    }
}