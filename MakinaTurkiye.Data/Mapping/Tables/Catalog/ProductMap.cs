using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductMap:EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Product");
            this.Ignore(p => p.Id);
            this.HasKey(p => p.ProductId);

            //this.HasRequired(p => p.Brand).WithOptional().Map(p =>
            //{
            //    p.ToTable("Category");
            //    p.MapKey("CategoryId");
            //});


            //this.HasRequired(p => p.Model).WithOptional().Map(p =>
            //{
            //    p.ToTable("Category");
            //    p.MapKey("CategoryId");

            //});

            //this.HasRequired(p => p.Category).WithOptional().Map(p =>
            //{
            //    p.ToTable("Category");
            //    p.MapKey("CategoryId");
            //});

            this.HasRequired(p => p.Model).WithMany().HasForeignKey(p => p.ModelId);
            this.HasRequired(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId);
            this.HasRequired(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);

            //this.HasRequired(p => p.Model).WithMany(c => c.BrandProducts).HasForeignKey(p => p.ModelId);
            //this.HasRequired(p => p.Model).WithMany(c => c.ModelProducts).HasForeignKey(p => p.ModelId);
            //this.HasRequired(p => p.Category).WithMany(c => c.CategoryProducts).HasForeignKey(p => p.CategoryId);

            //this.HasRequired(p => p.ConstantMensei).WithMany(c => c.MenseiProducts).HasForeignKey(p => p.MenseiId);
            //this.HasRequired(p => p.ConstantOrderStatus).WithMany(c => c.OrderStatusProducts).HasForeignKey(p => p.OrderStatus);
        }
    }
}
