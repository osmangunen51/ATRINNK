using Trinnk.Entities.Tables.Members;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Members
{
    public class CompanyDemandMemberShipMap : EntityTypeConfiguration<CompanyDemandMembership>
    {
        public CompanyDemandMemberShipMap()
        {
            this.ToTable("CompanyDemandMembership");
            this.Ignore(c => c.Id);
            this.HasKey(c => c.CompanyDemandMembershipId);
        }
    }
}
