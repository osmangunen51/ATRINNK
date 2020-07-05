using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class AddressTypeMap:EntityTypeConfiguration<AddressType>
    {
        public AddressTypeMap()
        {
            this.ToTable("AddressType");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.AddressTypeId);

        }
    }
}
