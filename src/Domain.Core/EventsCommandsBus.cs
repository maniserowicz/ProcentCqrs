using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProcentCqrs.Infrastructure.IoC.Autofac;

namespace ProcentCqrs.Domain.Core
{
    /// <summary>
    /// Executes domain command handlers
    /// and domain event handlers
    /// </summary>
    /// <remarks>
    /// Mostly stolen from Greg Young's project
    /// https://github.com/gregoryyoung/m-r
    /// 
    /// Not sure if it really belongs here (in .Domain), but it needs domain-related components (ICommand and IEvent)
    /// </remarks>
    public class EventsCommandsBus : ICommandSender, IEventPublisher
    {
        public void Send<T>(T command) where T : ICommand
        {
            var handlers = IoC.Container.Resolve<IEnumerable<Handles<T>>>()
                .ToList();

            if (handlers.Count == 1)
            {
                handlers[0].Handle(command);
            }
            else if (handlers.Count == 0)
            {
                throw new InvalidOperationException("no handler registered for command {0}".FormatWith(command.GetType().Name));
            }
            else
            {
                throw new InvalidOperationException("too many ({0}) handlers registered for command {1}, only one handler is allowed".FormatWith(handlers.Count, command.GetType().Name));
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var handlers = IoC.Container.Resolve<IEnumerable<Handles<T>>>()
                .ToList();

            Parallel.ForEach(handlers, h => h.Handle(@event));
        }
    }
}