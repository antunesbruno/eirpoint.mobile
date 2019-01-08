using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Ioc.Injection
{
    public static class Injector
    {
        #region Fields

        private static IContainer _container;
        private static ContainerBuilder builder;

        #endregion

        #region Properties

        public static IContainer Container
        {
            get
            {
                return _container;
            }
        }

        #endregion

        #region Methods        

        /// <summary>
        /// Create a new container
        /// </summary>
        public static void CreateContainer()
        {
            builder = new ContainerBuilder();
        }

        /// <summary>
        /// Register type references
        /// </summary>
        /// <typeparam name="T">Interface</typeparam>
        /// <typeparam name="Y">Class</typeparam>
        public static void RegisterType<T, Y>(bool newInstance = false)
        {
            if(newInstance)
                builder.RegisterType<T>().As<Y>();
            else
                builder.RegisterType<T>().As<Y>().SingleInstance();
        }

        /// <summary>
        /// Resolve dependencies by interface
        /// </summary>
        /// <typeparam name="T">Interface</typeparam>
        /// <returns></returns>
        public static T Resolver<T>()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return _container.Resolve<T>();
            }
        }

        /// <summary>
        /// Build a container
        /// Has be invoked only one time
        /// </summary>
        public static void BuildContainer()
        {
            _container = builder.Build();
        }

        #endregion
    }
}
