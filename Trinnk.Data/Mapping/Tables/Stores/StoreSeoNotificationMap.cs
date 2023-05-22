using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class StoreSeoNotificationMap : EntityTypeConfiguration<StoreSeoNotification>
    {
        public StoreSeoNotificationMap()
        {
            this.ToTable("StoreSeoNotification");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreSeoNotificationId);

        }
    }
}
