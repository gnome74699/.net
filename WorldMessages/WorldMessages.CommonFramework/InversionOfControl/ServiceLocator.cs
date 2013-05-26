
using System.Collections.Generic;

using Microsoft.Practices.Unity;

namespace WorldMessages.CommonFramework.InversionOfControl
{
    public static class ServiceLocator
    {
        /// Set Container, used at container initialisation only.
        /// </summary>
        public static IUnityContainer Container { private get; set; }

        /// <summary>
        /// Locate services
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>All services registered for type <see cref="T"/></returns>
        public static IEnumerable<T> GetServices<T>()
        {
            return Container.ResolveAll<T>();
        }
    }
}
