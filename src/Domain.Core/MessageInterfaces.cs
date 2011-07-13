namespace ProcentCqrs.Domain.Core
{
    public interface IMessage
    {
    }

    public interface ICommand : IMessage
    {

    }

    public interface IEvent : IMessage
    {

    }

    public interface Handles<T> where T : IMessage
    {
        void Handle(T message);
    }

    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;

    }
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}