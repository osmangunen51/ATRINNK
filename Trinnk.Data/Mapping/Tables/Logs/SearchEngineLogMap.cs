using Trinnk.Entities.Tables.Logs;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Logs
{
    public class SearchEngineLogMap : EntityTypeConfiguration<SearchEngineLog>
    {
        public SearchEngineLogMap()
        {
            this.ToTable("SearchEngineLog");
            this.HasKey(sel => sel.Id);
            this.Ignore(sel => sel.SearchEngineType);

        }
    }
}
