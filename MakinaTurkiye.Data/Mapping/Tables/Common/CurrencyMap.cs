using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public  class CurrencyMap : EntityTypeConfiguration<Currency>
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
