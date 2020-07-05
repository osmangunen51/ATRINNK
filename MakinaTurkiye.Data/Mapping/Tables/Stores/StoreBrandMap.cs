using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreBrandMap:EntityTypeConfiguration<StoreBrand>
    {
        public StoreBrandMap()
        {
            this.ToTable("StoreBrand");
            this.Ignore(sb => sb.Id);
            this.HasKey(sb=>sb.StoreBrandId);
        }
    }
}
