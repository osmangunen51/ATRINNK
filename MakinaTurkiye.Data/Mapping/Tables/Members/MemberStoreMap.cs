using MakinaTurkiye.Entities.Tables.Members;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Members
{
    public class MemberStoreMap : EntityTypeConfiguration<MemberStore>
    {
        public MemberStoreMap()
        {
            this.ToTable("MemberStore");
            this.Ignore(ms => ms.Id);
            this.HasKey(ms => ms.MemberStoreId);
        }
    }
}
