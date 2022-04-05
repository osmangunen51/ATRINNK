using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class AddressChangeHistoryMap : EntityTypeConfiguration<AddressChangeHistory>
    {
        public AddressChangeHistoryMap()
        {
            this.ToTable("AddressChangeHistory");
            this.Ignore(a => a.Id);
            this.HasKey(a => a.AddressChangeHistoryId);
        }

    }
}
