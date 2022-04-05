using MakinaTurkiye.Entities.Tables.Members;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Members
{
    public class MainPartyMap : EntityTypeConfiguration<MainParty>
    {
        public MainPartyMap()
        {
            this.ToTable("MainParty");
            this.Ignore(m => m.Id);
            this.HasKey(m => m.MainPartyId);
        }


    }
}
