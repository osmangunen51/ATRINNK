using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class PreRegistrationStoreMap : EntityTypeConfiguration<PreRegistrationStore>
    {
        public PreRegistrationStoreMap()
        {
            this.ToTable("PreRegistrationStore");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.PreRegistrationStoreId);
        }
    }
}
