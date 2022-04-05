using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreCatologFileMap : EntityTypeConfiguration<StoreCatologFile>
    {
        public StoreCatologFileMap()
        {
            this.ToTable("StoreCatologFile");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreCatologFileId);
        }
    }
}
