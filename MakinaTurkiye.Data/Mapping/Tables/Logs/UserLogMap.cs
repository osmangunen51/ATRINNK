using MakinaTurkiye.Entities.Tables.Logs;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Logs
{
    public class UserLogMap : EntityTypeConfiguration<UserLog>
    {
        public UserLogMap()
        {

            this.ToTable("UserLog");

            this.Property(ccl => ccl.CreatedDate).HasColumnName("LogDate");
            this.Property(ccl => ccl.UserLogId).HasColumnName("LogıD");

            this.Ignore(ccl => ccl.Id);
            this.HasKey(ccl => ccl.UserLogId);
        }
    }
}
