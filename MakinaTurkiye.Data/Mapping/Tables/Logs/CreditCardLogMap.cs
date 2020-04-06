using MakinaTurkiye.Entities.Tables.Logs;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Logs
{
    public class CreditCardLogMap : EntityTypeConfiguration<CreditCardLog>
    {
        public CreditCardLogMap()
        {
            this.ToTable("CreditCardLog");

            this.Property(ccl => ccl.MainPartyId).HasColumnName("MainPartyID");
            this.Property(ccl => ccl.OrderType).HasColumnName("Ordertype");
            this.Property(ccl => ccl.Status).HasColumnName("status");
            this.Property(ccl => ccl.CreatedDate).HasColumnName("Date");
            this.Property(ccl => ccl.IPAddress).HasColumnName("IP");
            this.Property(ccl => ccl.CreditCardLogId).HasColumnName("kklogid");

            this.Ignore(ccl => ccl.Id);
            this.HasKey(ccl => ccl.CreditCardLogId);
        }
    }
}
