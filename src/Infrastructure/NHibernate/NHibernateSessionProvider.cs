using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace ProcentCqrs.Infrastructure.NHibernate
{
    public interface INHibernateSessionProvider
    {
        ISession OpenSession();
    }

    /// <summary>
    /// Opens a new NH session using preconfigured SessionFactory
    /// </summary>
    /// <remarks>
    /// Some might say that this (and everything else related to NH) belongs to another .DataAccess project.
    /// In my opinion however NHibernate IS my DataAccess. What I am doing here is simply USING it.
    /// So session management, registration in IoC, even XML mappings, are considered infrastructure.
    /// </remarks>
    public class NHibernateSessionProvider : INHibernateSessionProvider
    {
        private static ISessionFactory _sessionFactory;
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// Properties overriding those defined in config xml,
        /// useful when unit testing.
        /// </summary>
        public static IDictionary<string, string> OverridenProperties;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (_syncRoot)
                    {
                        if (_sessionFactory == null)
                        {
                            var configuration = new Configuration();

                            configuration.Configure();

                            if (OverridenProperties != null)
                            {
                                var existingProps = configuration.Properties;
                                foreach (var prop in OverridenProperties)
                                {
                                    if (existingProps.ContainsKey(prop.Key))
                                    {
                                        existingProps[prop.Key] = prop.Value;
                                    }
                                    else
                                    {
                                        existingProps.Add(prop);
                                    }
                                }
                            }

                            foreach (var assembly in AppDomain.CurrentDomain.GetAppAssemblies())
                            {
                                configuration.AddAssembly(assembly);
                            }

                            _sessionFactory = configuration.BuildSessionFactory();

#if DEBUG
                            Console.SetOut(new DebugWriter());
#endif

                        }
                    }
                }

                return _sessionFactory;
            }
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }

    class DebugWriter : TextWriter
    {
        public override void WriteLine(string value)
        {
            Debug.WriteLine(value);
            base.WriteLine(value);
        }

        public override void Write(string value)
        {
            Debug.Write(value);
            base.Write(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }
}