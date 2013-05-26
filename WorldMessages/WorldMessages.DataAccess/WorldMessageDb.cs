
using System.Collections.Generic;
using System.Data.Entity;
using WorldMessages.CommonFramework.InversionOfControl;
using WorldMessages.DataAccessFramework;

namespace WorldMessages.DataAccess
{
    [AutoRegister(LifetimeManager = typeof(PerCallContextLifeTimeManager))]
    public class WorldMessageDb: DbContext 
    {
        private const string ConnectionStringName = "WorldMessagesDb";
        
        /// <summary>
        /// Used for unit tests purpose
        /// </summary>
        private List<IEntityConfiguration> registeredConfigurations;

        public WorldMessageDb()
            : base(ConnectionStringName)
        {
            Database.SetInitializer<WorldMessageDb>(null);
        }
        
        private IEnumerable<IEntityConfiguration> RegisteredConfigurations
        {
            get
            {
                return registeredConfigurations ?? ServiceLocator.GetServices<IEntityConfiguration>();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            foreach (var entityConfiguration in RegisteredConfigurations)
            {
                entityConfiguration.Add(modelBuilder.Configurations);
            }
        }
    }
}
