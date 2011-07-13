using NHibernate;
using ProcentCqrs.Domain.Core;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Infrastructure.NHibernate;

namespace ProcentCqrs.Domain.Trainings.CommandHandlers
{
    public class AssignTraineeToTrainingHandler : Handles<AssignTraineeToTrainingCommand>
    {
        private readonly ISession _session;
        private readonly IEventPublisher _eventPublisher;

        public AssignTraineeToTrainingHandler(ISession session, IEventPublisher eventPublisher)
        {
            _session = session;
            _eventPublisher = eventPublisher;
        }

        public void Handle(AssignTraineeToTrainingCommand message)
        {
            _session.InTransaction(
                () =>
                {
                    var training = _session.Get<Training>(message.TrainingId);
                    var trainee = _session.Get<Trainee>(message.UserId);

                    var newAssignment = training.AssignTrainee(trainee);

                    _session.Save(newAssignment);
                }
                );

            _eventPublisher.Publish(new TraineeWasAssignedToTraining(message.TrainingId, message.UserId));
        }
    }
}