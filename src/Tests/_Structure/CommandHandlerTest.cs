using System;
using System.Collections.Generic;
using FakeItEasy;
using NHibernate;
using ProcentCqrs.Domain.Core;
using ProcentCqrs.Infrastructure.NHibernate;
using ProcentCqrs.Tests._Utils;
using Simple.Data;

namespace ProcentCqrs.Tests.Structure
{
    /// <summary>
    /// Currently using XUnit, but will probably move to MSpec when runner for R#6 is ready.
    /// </summary>
    public abstract class CommandHandlerTest<THandler, TCommand>: IDisposable
        where TCommand: ICommand
        where THandler : Handles<TCommand>
    {
        protected readonly ISession Session;
        protected readonly dynamic DbRead;

        protected THandler Handler { get; private set; }
        protected TCommand Command { get; private set; }
        protected IEventPublisher EventPublisher { get; private set; }

        private static NHibernateSessionProvider _sessionProvider;

        protected CommandHandlerTest()
        {
            DatabaseHelper.CreateDb();

            if (_sessionProvider == null)
            {
                NHibernateSessionProvider.OverridenProperties = PrepareNHProperties();
                _sessionProvider = new NHibernateSessionProvider();
            }

            Session = _sessionProvider.OpenSession();
            DbRead = Database.OpenConnection(DatabaseHelper.ConnectionString);
        }

        protected abstract TCommand CreateCommand();

        private IDictionary<string, string> PrepareNHProperties()
        {
            return new Dictionary<string, string>
                {
                    {"connection.driver_class", "NHibernate.Driver.SqlServerCeDriver"}
                    , {"dialect", "NHibernate.Dialect.MsSqlCe40Dialect"}
                    , {"connection.connection_string", DatabaseHelper.ConnectionString}
                    , {"connection.release_mode", "on_close"}
                    , {"show_sql", "true"}
                };
        }

        public void Dispose()
        {
            Session.Dispose();
        }

        protected void Establish_context()
        {
            EventPublisher = A.Fake<IEventPublisher>();
            Handler = (THandler)Activator.CreateInstance(typeof(THandler), Session, EventPublisher);
            Command = CreateCommand();
        }

        protected void Because_of()
        {
            Handler.Handle(Command);
        }

        protected void Run()
        {
            Establish_context();
            Because_of();

            Before_assertions();
        }

        protected abstract void Before_assertions();
    }
}