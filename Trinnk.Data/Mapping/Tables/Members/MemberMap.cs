using Trinnk.Entities.Tables.Members;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Members
{
    public class MemberMap : EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            this.ToTable("Member");

            this.Ignore(m => m.Id);
            this.HasKey(m => m.MainPartyId);
        }
    }
}
