using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class PhoneMap: EntityTypeConfiguration<Phone>
    {
        public PhoneMap()
        {
            this.ToTable("Phone");
            this.Ignore(p => p.Id);
            this.HasKey(p => p.PhoneId);

            //this.HasRequired(p => p.Member).WithMany(m => m.Phones).HasForeignKey(p => p.MainPartyId);
            this.HasRequired(p => p.Address).WithMany(a => a.Phones).HasForeignKey(p => p.AddressId);

        }
    }
}
