using System;
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

    public class NHibernateSessionProvider : INHibernateSessionProvider
    {
        private static ISessionFactory _sessionFactory;
        private static readonly object _syncRoot = new object();

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