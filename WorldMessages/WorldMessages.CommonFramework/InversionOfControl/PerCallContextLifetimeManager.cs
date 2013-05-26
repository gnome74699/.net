using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace WorldMessages.CommonFramework.InversionOfControl
{
    public class PerCallContextLifeTimeManager : LifetimeManager
    {
        private const string Key = "SingletonPerCallContext";

        public override object GetValue()
        {
            return CallContext.GetData(Key + this.GetHashCode());
        }

        public override void SetValue(object newValue)
        {
            CallContext.SetData(Key + this.GetHashCode(), newValue);
        }

        public override void RemoveValue()
        {
        }
    }
}
