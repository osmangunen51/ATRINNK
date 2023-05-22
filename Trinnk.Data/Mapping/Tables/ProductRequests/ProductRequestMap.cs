using Trinnk.Entities.Tables.ProductRequests;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.ProductRequests
{
    public class ProductRequestMap : EntityTypeConfiguration<ProductRequest>
    {
        public ProductRequestMap()
        {
            this.ToTable("ProductRequest");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ProductRequestId);


        }
    }
}
