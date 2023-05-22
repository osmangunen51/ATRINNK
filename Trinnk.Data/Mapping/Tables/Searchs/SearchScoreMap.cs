using Trinnk.Entities.Tables.Searchs;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Searchs
{
    public class SearchScoreMap : EntityTypeConfiguration<SearchScore>
    {
        public SearchScoreMap()
        {
            this.ToTable("SearchScore");
        }

    }
}
