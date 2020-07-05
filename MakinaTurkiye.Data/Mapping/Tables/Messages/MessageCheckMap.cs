using MakinaTurkiye.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Messages
{
    public class MessageCheckMap : EntityTypeConfiguration<MessageCheck>
    {
        public MessageCheckMap()
        {
            this.ToTable("MessageCheck");
           
            this.Property(x => x.Id).HasColumnName("id");
            this.Property(x => x.Check).HasColumnName("kontrolcheck");

            this.HasKey(x => x.Id);

        }
    }
}
