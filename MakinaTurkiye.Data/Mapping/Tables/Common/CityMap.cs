using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class CityMap: EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            this.ToTable("City");
            this.Ignore(c => c.Id);
        }
    }
}
