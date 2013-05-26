using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldMessages.CommonFramework.InversionOfControl;

namespace WorldMessages.DataAccessFramework
{
    [AutoRegister(UseTypeForName = true)]
    public interface IEntityConfiguration
    {
        /// <summary>
        /// Add the EF mapping to <see cref="registrar"/>.
        /// Typically implements with <code>registrar.Add(this)</code>
        /// </summary>
        /// <param name="registrar">EF configuration registrar</param>
        void Add(ConfigurationRegistrar registrar);
    }
}
