using MakinaTurkiye.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Members
{
    public class UserMailTemplateMap:EntityTypeConfiguration<UserMailTemplate>
    {
        public UserMailTemplateMap()
        {
            this.ToTable("UserMailTemplate");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.UserMailTemplateId);
        }

    }
}
