using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class ActivityTypeMap : EntityTypeConfiguration<ActivityType>
    {
        public ActivityTypeMap()
        {
            this.ToTable("ActivityType");
            this.Ignore(a => a.Id);
            this.HasKey(a => a.ActivityTypeId);

        }
    }
}
