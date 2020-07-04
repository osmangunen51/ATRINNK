using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class ConstantMap: EntityTypeConfiguration<Constant>
    {
        public ConstantMap()
        {
            this.ToTable("Constant");
            this.Ignore(c => c.Id);
            this.HasKey(c => c.ConstantId);
        }
    }
}
