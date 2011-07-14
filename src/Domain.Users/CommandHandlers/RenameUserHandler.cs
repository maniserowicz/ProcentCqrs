using NHibernate;
using ProcentCqrs.Domain.Core;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Users;
using ProcentCqrs.Infrastructure.NHibernate;

namespace ProcentCqrs.Domain.Users.CommandHandlers
{
    public class RenameUserHandler : Handles<RenameUserCommand>
    {
        private readonly ISession _session;
        private readonly IEventPublisher _eventPublisher;

        public RenameUserHandler(ISession session, IEventPublisher eventPublisher)
        {
            _session = session;
            _eventPublisher = eventPublisher;
        }

        public void Handle(RenameUserCommand message)
        {
            _session.InTransaction(
                () =>
                    {
                        var user = _session.Get<User>(message.UserId);
                        user.Rename(message.FirstName, message.LastName);
                    }
                );

            _eventPublisher.Publish(new UserWasRenamed(message.UserId, message.FirstName, message.LastName));
        }
    }
}