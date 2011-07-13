using NLog;
using ProcentCqrs.Domain.Core.Events.Trainings;
using System;

namespace ProcentCqrs.Domain.Core.Notifications
{
    /// <summary>
    /// Fake class showing how system can notify users about some events
    /// </summary>
    /// <remarks>
    /// Here I'm only logging info to a file, but this could be anything:
    /// sending email, sending SMS, writing info to DB...
    /// Remember that event handlers execute in background threads, outside of command transation,
    /// so this does not affect user experienece.
    /// </remarks>
    public class FakeNotifier : Handles<TrainingWasAdded>
    {
        public void Handle(TrainingWasAdded message)
        {
            _log.Info("New training was added: {0}", message.ToJSON());
        }

        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
    }
}