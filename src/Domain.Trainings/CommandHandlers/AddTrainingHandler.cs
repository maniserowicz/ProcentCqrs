using NHibernate;
using ProcentCqrs.Domain.Core;
using ProcentCqrs.Domain.Core.Commands.Trainings;
using ProcentCqrs.Domain.Core.Events.Trainings;
using ProcentCqrs.Infrastructure.NHibernate;

namespace ProcentCqrs.Domain.Trainings.CommandHandlers
{
    public class AddTrainingHandler : Handles<AddTrainingCommand>
    {
        private readonly ISession _session;
        private readonly IEventPublisher _eventPublisher;

        public AddTrainingHandler(ISession session, IEventPublisher eventPublisher)
        {
            _session = session;
            _eventPublisher = eventPublisher;
        }

        public void Handle(AddTrainingCommand message)
        {
            int newTrainingId = 0;

            _session.InTransaction(
                () =>
                    {
                        var training = new Training(message.Name);

                        newTrainingId = (int) _session.Save(training);
                    }
                );

            _eventPublisher.Publish(new TrainingWasAdded(newTrainingId, message.Name));
        }
    }
}