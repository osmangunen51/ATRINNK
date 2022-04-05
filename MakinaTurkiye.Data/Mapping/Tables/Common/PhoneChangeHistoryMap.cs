using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class PhoneChangeHistoryMap : EntityTypeConfiguration<PhoneChangeHistory>
    {
        public PhoneChangeHistoryMap()
        {
            this.ToTable("PhoneChangeHistory");
            this.Ignore(p => p.Id);
            this.HasKey(p => p.PhoneChangeHistoryId);
        }
    }
}
