using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class DeletedProductRedirectMap : EntityTypeConfiguration<DeletedProductRedirect>
    {
        public DeletedProductRedirectMap()
        {
            this.ToTable("DeletedProductRedirect");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.DeletedProductRedirectId);
        }
    }
}
