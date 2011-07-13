using System;
using System.Reflection;
using Autofac;

namespace ProcentCqrs.Web.Mvc.MvcExtensions
{
    /// <summary>
    /// Registers all controllers into container,
    /// ensuring that their dependencies are injection not only via ctor,
    /// but also via properties (useful for common functionality shared across all controllers)
    /// </summary>
    public class WebComponentsRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<CqrsController>()
                .Where(t => t.CanBeInstantiated())
                .PropertiesAutowired()
                .AsSelf();
        }
    }
}