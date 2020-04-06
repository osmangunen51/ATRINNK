using MakinaTurkiye.Entities.Tables.Logs;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Logs
{
    public class ApplicationMap:EntityTypeConfiguration<ApplicationLog>
    {
        public ApplicationMap()
        {
            this.ToTable("ApplicationLog");
        }

    }
}
