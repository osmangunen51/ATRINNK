using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class DeletedProductRedirectMap:EntityTypeConfiguration<DeletedProductRedirect>
    {
        public DeletedProductRedirectMap()
        {
            this.ToTable("DeletedProductRedirect");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.DeletedProductRedirectId);
        }
    }
}
