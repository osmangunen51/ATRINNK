using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreInfoNumberShowMap : EntityTypeConfiguration<StoreInfoNumberShow>
    {
       public StoreInfoNumberShowMap()
       {
           this.ToTable("StoreInfoNumberShow");
           this.Ignore(x => x.Id);

           this.Property(x => x.StoreInfoNumberShowId).HasColumnName("StoreInfoNumberShowID");
            this.Property(x => x.StoreMainpartyId).HasColumnName("StoreMainpartyID");

           this.HasKey(x=>x.StoreInfoNumberShowId);

          
       }
    }
}
