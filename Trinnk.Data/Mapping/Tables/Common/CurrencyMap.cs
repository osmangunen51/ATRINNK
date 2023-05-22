using Trinnk.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Common
{
    public class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            this.ToTable("Currency");
            this.Ignore(c => c.Id);
            this.HasKey(c => c.CurrencyId);

            this.Property(c => c.CreatedDate).HasColumnName("RecordDate");
        }

    }
}
