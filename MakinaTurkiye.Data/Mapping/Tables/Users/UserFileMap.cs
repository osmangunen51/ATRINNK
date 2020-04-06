using MakinaTurkiye.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
