using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.AutoRegistration;

namespace WorldMessages.CommonFramework.InversionOfControl
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer container;

        public UnityDependencyResolver()
        {
            ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            container = new UnityContainer();
            ServiceLocator.Container = container;

            // Ensure loading of assemblies
            IEnumerable<string> assemblies = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory, "WorldMessages.*.dll");

            var autoRegistration = container.ConfigureAutoRegistration()
                .LoadAssembliesFrom(assemblies)
                .ExcludeAssemblies(x => !x.FullName.Contains("WorldMessages."));

            // Register implementations of interfaces flagged with AutoRegisterAttribute
            autoRegistration.Include(
                t => t.GetInterfaces().Any(i => i.GetCustomAttributes(typeof(AutoRegisterAttribute), true).Any()),
                (t, c) => AutoRegisterWithTaggedInterfaces(c, t));

            // Register classes flagged with AutoRegisterAttribute
            autoRegistration.Include(
                    t => t.IsClass && t.GetCustomAttributes(typeof(AutoRegisterAttribute), true).Any(),
                    (t, c) => RegisterAutoRegisteredType(c, t, t));

            autoRegistration.ApplyAutoRegistration();
        }

        private void RegisterAutoRegisteredType(IUnityContainer container, Type to, Type @from)
        {
            var attribute =
                @from.GetCustomAttributes(typeof(AutoRegisterAttribute), true).OfType<AutoRegisterAttribute>().First();
            var name = attribute.Name;

            // Set name as specified in AutoRegisterAttribute.UseTypeForName contract
            if (attribute.UseTypeForName && string.IsNullOrEmpty(name))
            {
                name = to.Name;
            }

            var lifetimeManagerType = attribute.LifetimeManager ?? typeof(ContainerControlledLifetimeManager);
            var lifetimeManager = Activator.CreateInstance(lifetimeManagerType) as LifetimeManager;

            container.RegisterType(@from, to, name, lifetimeManager);
        }

        private void AutoRegisterWithTaggedInterfaces(IUnityContainer c, Type t)
        {
            foreach (var interfaceType in t.GetInterfaces().Where(i => i.GetCustomAttributes(typeof(AutoRegisterAttribute), true).Any()))
            {
                RegisterAutoRegisteredType(c, t, interfaceType);
            }
        }

        public object GetService(Type serviceType)
        {
            return container.IsRegistered(serviceType) || (serviceType.IsClass && !serviceType.IsAbstract) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }
    }
}
