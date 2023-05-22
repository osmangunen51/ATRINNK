using Trinnk.Entities.Tables.Settings;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Settings
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.ToTable("Setting");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.SettingId);
        }

    }
}
