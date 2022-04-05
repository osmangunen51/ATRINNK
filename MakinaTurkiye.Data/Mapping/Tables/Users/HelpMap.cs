using MakinaTurkiye.Entities.Tables.Users;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Users
{
    public class HelpMap : EntityTypeConfiguration<Help>
    {
        public HelpMap()
        {
            this.ToTable("Help");
            this.Ignore(h => h.Id);
            this.HasKey(h => h.HelpId);
        }

    }
}
