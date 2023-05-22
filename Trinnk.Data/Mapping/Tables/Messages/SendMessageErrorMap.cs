using Trinnk.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Messages
{
    public class SendMessageErrorMap : EntityTypeConfiguration<SendMessageError>
    {
        public SendMessageErrorMap()
        {
            this.ToTable("SendMessageError");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.MessageID);
        }
    }
}
