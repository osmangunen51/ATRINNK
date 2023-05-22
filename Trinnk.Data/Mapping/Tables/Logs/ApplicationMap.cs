using Trinnk.Entities.Tables.Logs;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Logs
{
    public class ApplicationMap : EntityTypeConfiguration<ApplicationLog>
    {
        public ApplicationMap()
        {
            this.ToTable("ApplicationLog");
        }

    }
}
