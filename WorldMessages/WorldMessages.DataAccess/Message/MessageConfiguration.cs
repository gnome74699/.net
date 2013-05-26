using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldMessages.DataAccessFramework;
using WorldMessages.DomainModel.Message;
namespace WorldMessages.DataAccess.Message
{
    internal class MessageConfiguration : EntityTypeConfiguration<MessageModel>, IEntityConfiguration
    {
        public MessageConfiguration()
        {
            ToTable("MessagesSet");
            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Identifier");

            Property(x => x.Text)
                .HasColumnName("Code");

        }

        public void Add(ConfigurationRegistrar registrar)
        {
            registrar.Add(this);
        }
    }
}
