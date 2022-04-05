using MakinaTurkiye.Entities.Tables.Users;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Users
{
    public class UserFileMap : EntityTypeConfiguration<UserFile>
    {
        public UserFileMap()
        {
            this.ToTable("UserFile");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.UserFileId);
        }
    }
}
