using System.Collections.Generic;
using NLog;

namespace ProcentCqrs.Infrastructure.Startup
{
    public interface IAppStarter
    {
        void Start();
    }

    public class StartupExecutor : IAppStarter
    {
        private readonly IEnumerable<IStartupTask> _tasks;

        public StartupExecutor(IEnumerable<IStartupTask> tasks)
        {
            _tasks = tasks;
        }

        public void Start()
        {
            _log.Debug("Executing application startup tasks...");

            foreach (var task in _tasks)
            {
                string taskType = task.GetType().Name;
                _log.Debug("Starting execution of task {0}", taskType);

                task.Execute();

                _log.Debug("Execution of task {0} finished", taskType);
            }

            _log.Debug("Startup tasks executed.");
        }

        private readonly Logger _log = LogManager.GetCurrentClassLogger();
    }
}