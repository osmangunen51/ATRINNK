
using Trinnk.Entities.Tables.Settings;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Settings
{
    public class MemberSettingMap : EntityTypeConfiguration<MemberSetting>
    {
        public MemberSettingMap()
        {
            this.ToTable("MemberSetting");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.MemberSettingId);


            //this.HasRequired(x => x.Member).WithMany(ms => ms.MemberSettings).HasForeignKey(x => x.MemberMainPartyId);
            //this.HasRequired(x => x.Store).WithMany(s=> s.MemberSettings).HasForeignKey(x => x.StoreMainPartyId);

            this.HasRequired(x => x.Setting).WithMany(ms => ms.MemberSettings).HasForeignKey(x => x.SettingId);

        }

    }
}
