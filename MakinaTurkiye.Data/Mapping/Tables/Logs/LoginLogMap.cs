using MakinaTurkiye.Entities.Tables.Logs;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Logs
{
    public class LoginLogMap:EntityTypeConfiguration<LoginLog>
    {
        public LoginLogMap()
        {
            this.ToTable("LoginLog");
            this.Ignore(l=>l.Id);
            this.HasKey(l=>l.LoginLogId);

            //this.HasRequired(l => l.Store).WithMany(s => s.LoginLogs).HasForeignKey(l=>l.StoreMainPartyId);
        }
    }
}
