using MakinaTurkiye.Entities.Tables.Members;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Members
{
    public class MemberDescriptionLogMap : EntityTypeConfiguration<MemberDescriptionLog>
    {
        public MemberDescriptionLogMap()
        {
            this.ToTable("MemberDescription_log");
            this.HasKey(x => x.MemberDescription_logId);
            this.Ignore(x => x.Id);

        }
    }
}
