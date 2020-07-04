using MakinaTurkiye.Entities.Tables.Bullettins;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Bulletins
{
    public class BulletinMemberCategoryMap:EntityTypeConfiguration<BulletinMemberCategory>
    {
        public BulletinMemberCategoryMap()
        {
            this.ToTable("BulletinMemberCategory");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.BulletinMemberCategoryId);


            this.HasRequired(x => x.BulletinMember).WithMany(c => c.BulletinMemberCategories).HasForeignKey(x => x.BulletinMemberId);
        }

    }
}
