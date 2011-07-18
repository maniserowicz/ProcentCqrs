using System;
using FakeItEasy;
using Machine.Specifications;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Infrastructure.System;
using ProcentCqrs.Tests.Structure;
using ProcentCqrs.Tests._Utils;

namespace ProcentCqrs.Tests.Domain.Trainings
{
    [Subject("Assigning trainee to training")]
    public abstract class assigning_trainee_to_training : command_test<AssignTraineeToTrainingCommand>
    {
        private Establish context = () =>
            {
                Now = Randomizer.DateTime();
                ApplicationTime._replaceCurrentTimeLogic(() => Now);

                FireCommand(new AddUserCommand(Randomizer.Email(), Randomizer.String(), Randomizer.String()));
                FireCommand(new AddTrainingCommand(Randomizer.String()));

                Command = new AssignTraineeToTrainingCommand(1, 1);
            };

        private Cleanup after_test = () =>
            {
                ApplicationTime._revertToDefaultLogic();
            };

        protected static DateTime Now;

        protected static dynamic AllAssignemnts()
        {
            return DbRead.UserTrainings.All().ToList();
        }

        protected static dynamic Assignment()
        {
            return AllAssignemnts()[0];
        }
    }

    public class when_assigning_trainee_to_training : assigning_trainee_to_training
    {
        It should_persist_new_assignment = () => ((int) AllAssignemnts().Count).ShouldEqual(1);

        It should_save_given_training_id = () => ((int)Assignment().TrainingId).ShouldEqual(1);

        It should_save_given_trainee_id = () => ((int) Assignment().UserId).ShouldEqual(1);

        It should_save_current_date = () => ((DateTime) Assignment().AssignmentDate).ShouldEqual(Now);

        It should_publish_event = () => A.CallTo(() => EventPublisher.Publish(A<TraineeWasAssignedToTraining>.That
                .Matches(x => x.TrainingId == 1 && x.UserId == 1)))
                .MustHaveHappened(Repeated.Exactly.Once)
            ;
    }

    public class when_assigning_trainee_to_the_same_training_more_than_once : assigning_trainee_to_training
    {
        Establish context = () =>
            {
                // simulate trainee already assigned to training
                FireCommand(new AssignTraineeToTrainingCommand(1, 1));
            };

        It should_raise_error = () => Error.ShouldNotBeNull();

        It should_not_publish_event = () => A.CallTo(() => 
            EventPublisher.Publish(A<TraineeWasAssignedToTraining>._))
            .MustNotHaveHappened();
    }
}