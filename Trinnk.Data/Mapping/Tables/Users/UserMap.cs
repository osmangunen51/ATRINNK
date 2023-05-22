using Trinnk.Entities.Tables.Users;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Users
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.UserId);
        }
    }
}
