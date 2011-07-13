using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ProcentCqrs.Infrastructure.IoC.Autofac;
using ProcentCqrs.Infrastructure.Startup;

namespace ProcentCqrs.Web.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ConfigureIoC();

            PerformStartup();
        }

        private void PerformStartup()
        {
            var appStarter = IoC.Container.Resolve<IAppStarter>();
            appStarter.Start();
        }

        private void ConfigureIoC()
        {
            string binDirectory = HttpRuntime.BinDirectory;

            IoC.Configure(binDirectory,
                builder => builder.RegisterModule(new AutofacWebTypesModule()),
                () =>
                    {
                        ILifetimeScope externalScope = null;
                        // return context-scoped container if runinng in MVC application
                        var resolver = DependencyResolver.Current as AutofacDependencyResolver;
                        if (resolver != null && HttpContext.Current != null)
                        {
                            externalScope = resolver.RequestLifetimeScope;
                        }
                        return externalScope;
                    }
                );

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
        }
    }
}