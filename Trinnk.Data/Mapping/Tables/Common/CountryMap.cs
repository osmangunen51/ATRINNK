using Trinnk.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Common
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            this.ToTable("Country");
            this.Ignore(c => c.Id);
        }
    }
}
