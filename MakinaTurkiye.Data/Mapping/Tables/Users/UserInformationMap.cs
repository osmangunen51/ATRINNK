using MakinaTurkiye.Entities.Tables.Users;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Users
{
    public class UserInformationMap:EntityTypeConfiguration<UserInformation>
    {
        public UserInformationMap()
        {
            this.ToTable("UserInformation");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.UserInformationId);
            
        }
    }
}
