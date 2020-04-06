using MakinaTurkiye.Entities.Tables.Searchs;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Searchs
{
    public class SearchScoreMap:EntityTypeConfiguration<SearchScore>
    {
        public SearchScoreMap()
        {
            this.ToTable("SearchScore");
        }

    }
}
