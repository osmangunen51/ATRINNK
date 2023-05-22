using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
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
