using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldMessages.CommonFramework.InversionOfControl;

namespace WorldMessages.DomainModelFramework.Command
{
    [AutoRegister(UseTypeForName = true)]
    public interface IHandles<T>
    {
        /// <summary>
        /// Implements what happen when <see cref="appliedEvent"/> is applied
        /// </summary>
        /// <param name="appliedEvent">Applied event</param>
        void Handle(T appliedEvent);
    }
}
