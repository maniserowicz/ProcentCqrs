using Autofac;
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
            // without CQRS InstancePerHttpRequest() was needed here
            // but now only a single command uses NH session
            // so there is no need to register per request...
            // which makes everything clearer - no need to reference MVC-related stuff in Infrastructure project anymore
            .InstancePerDependency();
        }
    }
}