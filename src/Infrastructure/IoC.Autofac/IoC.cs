using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace ProcentCqrs.Infrastructure.IoC.Autofac
{
    public static class IoC
    {
        private static ILifetimeScope _container;

        private static Func<ILifetimeScope> _externalScopeDiscovery;

        /// <summary>
        /// Returns <see cref="ILifetimeScope"/> applicable to current context.
        /// </summary>
        public static ILifetimeScope Container
        {
            get
            {
                ILifetimeScope externalScope = null;

                if (_externalScopeDiscovery != null)
                {
                    externalScope = _externalScopeDiscovery();
                }

                return externalScope ?? _container;
            }
        }

        /// <summary>
        /// Sets another instance of <see cref="ILifetimeScope"/> to be used by the application.
        /// Can be useful in scenarios when default container configuration is not appropriate, like unit tests.
        /// </summary>
        /// <remarks>Be sure you know what you are doing when calling this methods.</remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static void _replaceContainer(ILifetimeScope newContainer)
        {
            _container = newContainer;
        }

        /// <summary>
        /// Configures Autofac.
        /// </summary>
        /// <param name="binDirectory">Current application's directory with all used dlls to scan in search of components.</param>
        /// <param name="additionalConfig">Additional (probably runtime-dependent) activities to perform on builder before actually creating a container.</param>
        /// <param name="externalScopeDiscovery">Runtime-dependent routine for finding current lifetime scope.</param>
        public static void Configure(string binDirectory,
            Action<ContainerBuilder> additionalConfig = null,
            Func<ILifetimeScope> externalScopeDiscovery = null
        )
        {
            if (_container != null)
            {
                throw new InvalidOperationException("IoC already configured");
            }

            foreach (string filePath in Directory.GetFiles(binDirectory, "*.dll"))
            {
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAppAssemblies())
                .AsImplementedInterfaces()
                .AsSelf();

            foreach (var moduleType in AppDomain.CurrentDomain.GetAppTypes()
                .Where(x => x.AssignableTo<IModule>() && x.CanBeInstantiated())
                )
            {
                IModule instance = Activator.CreateInstance(moduleType) as IModule;
                builder.RegisterModule(instance);
            }

            if (additionalConfig != null)
            {
                additionalConfig(builder);
            }

            _externalScopeDiscovery = externalScopeDiscovery;

            _container = builder.Build();
        }
    }
}