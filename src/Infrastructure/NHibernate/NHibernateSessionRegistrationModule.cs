using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;

namespace ProcentCqrs.Infrastructure.NHibernate
{
    public class NHibernateSessionRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
                c.Resolve<INHibernateSessionProvider>().OpenSession()
            )
            .As<ISession>()

            // i don't really like having this here, but i'd rather do this
            // than register NH in IoC in MVC app
            .InstancePerHttpRequest();
        }
    }
}