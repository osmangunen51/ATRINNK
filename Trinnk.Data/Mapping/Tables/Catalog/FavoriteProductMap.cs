using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class FavoriteProductMap : EntityTypeConfiguration<FavoriteProduct>
    {
        public FavoriteProductMap()
        {
            this.ToTable("FavoriteProduct");
            this.Ignore(fp => fp.Id);
            this.HasKey(fp => fp.FavoriteProductId);

            this.Property(fp => fp.CreatedDate).HasColumnName("RecordDate");
        }
    }
}
