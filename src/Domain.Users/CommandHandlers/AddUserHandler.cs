using NHibernate;
using ProcentCqrs.Domain.Core;
using ProcentCqrs.Domain.Core.Commands.Users;
using ProcentCqrs.Domain.Core.Events.Users;
using ProcentCqrs.Infrastructure.NHibernate;

namespace ProcentCqrs.Domain.Users.CommandHandlers
{
    public class AddUserHandler : Handles<AddUserCommand>
    {
        private readonly ISession _session;
        private readonly IEventPublisher _eventPublisher;

        public AddUserHandler(ISession session, IEventPublisher eventPublisher)
        {
            _session = session;
            _eventPublisher = eventPublisher;
        }

        public void Handle(AddUserCommand message)
        {
            int newUserId = 0;

            _session.InTransaction(
                () =>
                    {
                        var user = new User(message.Email, message.FirstName, message.LastName);

                        newUserId = (int)_session.Save(user);
                    }
                );

            _eventPublisher.Publish(new UserWasAdded(newUserId, message.Email, message.FirstName, message.LastName));
        }
    }
}