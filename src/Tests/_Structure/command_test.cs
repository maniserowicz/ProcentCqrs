using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy;
using Machine.Specifications;
using NHibernate;
using ProcentCqrs.Domain.Core;
using ProcentCqrs.Infrastructure.NHibernate;
using ProcentCqrs.Tests._Utils;
using Simple.Data;
using System.Linq;

namespace ProcentCqrs.Tests.Structure
{
    public abstract class command_test<TCommand>
        where TCommand : class, ICommand
    {
        protected static ISession Session;
        protected static dynamic DbRead;

        protected static Exception Error;

        protected static TCommand Command;
        protected static HandlerCreationResult<TCommand> HandlerInfo;

        protected static IEventPublisher EventPublisher
        {
            get { return HandlerInfo.Dependency<IEventPublisher>(); }
        }

        private static NHibernateSessionProvider _sessionProvider;

        Establish context = () =>
            {
                PrepareDatabase();

                HandlerInfo = TryCreateHandler<TCommand>();
            };

        Because of = () =>
            {
                if (Command == null)
                {
                    throw new InvalidOperationException(
                        "You must initialize a command in Establish section of your test");
                }
                Error = Catch.Exception(
                    () => HandlerInfo.Handler.Handle(Command)
                );
            };

        Cleanup after_test = () =>
            {
                Session.Dispose();
            };

        protected static void FireCommand<T>(T command) where T : ICommand
        {
            TryCreateHandler<T>().Handler.Handle(command);
        }

        #region Database-related

        private static void PrepareDatabase()
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

        private static IDictionary<string, string> PrepareNHProperties()
        {
            return new Dictionary<string, string>
                {
                    {"connection.driver_class", "NHibernate.Driver.SqlServerCeDriver"}
                    ,
                    {"dialect", "NHibernate.Dialect.MsSqlCe40Dialect"}
                    ,
                    {"connection.connection_string", DatabaseHelper.ConnectionString}
                    ,
                    {"connection.release_mode", "on_close"}
                    ,
                    {"show_sql", "true"}
                };
        }

        #endregion

        #region Handler creation

        protected static HandlerCreationResult<T> TryCreateHandler<T>() where T : ICommand
        {
            var result = new HandlerCreationResult<T>();

            var handlerTypes = AppDomain.CurrentDomain.GetAppTypes()
                .Where(x => x.AssignableTo<Handles<T>>())
                .ToList();

            if (handlerTypes.Count == 1)
            {
                var t = handlerTypes.Single();
                var publicCtors = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                if (publicCtors.Length != 1)
                {
                    throw new InvalidOperationException(
                        "Handler {0} should have 1 public ctor, but has {1}".FormatWith(t.Name, publicCtors.Length)
                        );
                }

                foreach (var ctorParam in publicCtors.Single().GetParameters())
                {
                    if (ctorParam.ParameterType == typeof(ISession))
                    {
                        result.Dependencies.Add(ctorParam.ParameterType, Session);
                    }
                    else
                    {
                        object fakeParam = A.Fake<object>(b => b.Implements(ctorParam.ParameterType));

                        result.Dependencies.Add(ctorParam.ParameterType, fakeParam);
                    }
                }
                
                var handler = publicCtors[0].Invoke(result.Dependencies.Select(x => x.Value).ToArray());

                result.Handler = (Handles<T>)handler;
            }
            else
            {
                throw new InvalidOperationException(
                    "Cannot create a single handler for command {0}".FormatWith(typeof(T).Name)
                    );
            }

            return result;
        }

        public class HandlerCreationResult<TCommand> where TCommand : ICommand
        {
            public virtual Handles<TCommand> Handler { get; set; }
            public IDictionary<Type, object> Dependencies { get; set; }

            public HandlerCreationResult()
            {
                Dependencies = new Dictionary<Type, object>();
            }

            public TDependency Dependency<TDependency>()
            {
                return (TDependency)Dependencies
                    .Where(x => x.Key == typeof(TDependency))
                    .Single()
                    .Value;
            }
        }

        #endregion
    }
}