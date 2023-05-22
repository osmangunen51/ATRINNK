using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class StoreProductCreateSettingMap : EntityTypeConfiguration<StoreProductCreateSetting>
    {
        public StoreProductCreateSettingMap()
        {
            this.ToTable("StoreProductCreateSetting");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreProductCreateSettingId);

        }
    }
}
