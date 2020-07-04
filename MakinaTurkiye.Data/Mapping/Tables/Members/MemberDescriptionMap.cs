using MakinaTurkiye.Entities.Tables.Members;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Members
{
    public class MemberDescriptionMap : EntityTypeConfiguration<MemberDescription>
    {
        public MemberDescriptionMap()
        {
            this.ToTable("MemberDescription");
            this.Ignore(md => md.Id);
            this.HasKey(md => md.descId);

            
        }
    }
}
