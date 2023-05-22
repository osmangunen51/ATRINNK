using Trinnk.Entities.Tables.Bullettins;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Bulletins
{
    public class BulletinMemberMap : EntityTypeConfiguration<BulletinMember>
    {
        public BulletinMemberMap()
        {
            this.ToTable("BulletinMember");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.BulletinMemberId);

        }

    }
}
