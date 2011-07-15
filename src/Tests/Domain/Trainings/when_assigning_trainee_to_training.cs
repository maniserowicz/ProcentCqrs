using System;
using FakeItEasy;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Domain.Trainings.CommandHandlers;
using ProcentCqrs.Domain.Users.CommandHandlers;
using ProcentCqrs.Infrastructure.System;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;
using Xunit;

namespace ProcentCqrs.Tests.Domain.Trainings
{
    public class when_assigning_trainee_to_training : CommandHandlerTest<AssignTraineeToTrainingHandler, AssignTraineeToTrainingCommand>
    {
        private DateTime _now;

        protected override void Establish_context()
        {
            base.Establish_context();

            _now = Randomizer.DateTime();
            ApplicationTime._replaceCurrentTimeLogic(() => _now);

            TempHandler<AddUserHandler>().Handle(new AddUserCommand(Randomizer.Email(), Randomizer.String(), Randomizer.String()));
            TempHandler<AddTrainingHandler>().Handle(new AddTrainingCommand(Randomizer.String()));
        }

        public override void Dispose()
        {
            base.Dispose();

            ApplicationTime._revertToDefaultLogic();
        }

        protected override AssignTraineeToTrainingCommand CreateCommand()
        {
            return new AssignTraineeToTrainingCommand(1, 1);
        }

        private dynamic allAssignemnts;

        protected override void Before_assertions()
        {
            allAssignemnts = DbRead.UserTrainings.All().ToList();
        }

        [Fact]
        public void it_should_save_new_assignment()
        {
            Run();

            Assert.Equal(1, allAssignemnts.Count);
        }

        [Fact]
        public void it_should_save_given_ids()
        {
            Run();

            Assert.Equal(1, allAssignemnts[0].TrainingId);
            Assert.Equal(1, allAssignemnts[0].UserId);
        }

        [Fact]
        public void it_should_save_current_date()
        {
            Run();

            Assert.Equal(_now, allAssignemnts[0].AssignmentDate);
        }

        [Fact]
        public void it_should_publish_event()
        {
            Run();

            A.CallTo(() => EventPublisher.Publish(A<TraineeWasAssignedToTraining>.That
                .Matches(x => x.TrainingId == 1 && x.UserId == 1)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}