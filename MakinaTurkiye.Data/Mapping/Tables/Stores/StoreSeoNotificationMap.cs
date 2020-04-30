using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreSeoNotificationMap:EntityTypeConfiguration<StoreSeoNotification>
    {
        public StoreSeoNotificationMap()
        {
            this.ToTable("StoreSeoNotification");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreSeoNotificationId);

        }
    }
}
