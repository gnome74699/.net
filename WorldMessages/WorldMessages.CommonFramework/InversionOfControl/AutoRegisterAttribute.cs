using System;

namespace WorldMessages.CommonFramework.InversionOfControl
{
    /// <summary>
    /// Attribute used to specify that implementers of this interface must be registered in IoC container with <see cref="Name"/>.
    /// See <see cref="UnityDependencyResolver"/> for use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AutoRegisterAttribute : Attribute
    {
        /// <summary>
        /// Instanciate attribute
        /// </summary>
        public AutoRegisterAttribute()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Get or set name of named registration
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set to use Type name as registration name.
        /// No effect, if <see cref="Name"/> is also set
        /// </summary>
        public bool UseTypeForName
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set type of LifetimeManager
        /// </summary>
        public Type LifetimeManager
        {
            get;
            set;
        }
    }
}
