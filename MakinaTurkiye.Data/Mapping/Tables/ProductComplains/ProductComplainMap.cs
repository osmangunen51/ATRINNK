using MakinaTurkiye.Entities.Tables.ProductComplain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace MakinaTurkiye.Data.Mapping.Tables.ProductComplains
{
    public class ProductComplainMap : EntityTypeConfiguration<ProductComplain>
    {
        public ProductComplainMap()
        {
            this.ToTable("ProductComplain");
            this.Ignore(v => v.Id);
            this.HasKey(v => v.ID);
        }
    }
}
