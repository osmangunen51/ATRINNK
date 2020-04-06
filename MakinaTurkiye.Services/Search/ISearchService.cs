using MakinaTurkiye.Entities.StoredProcedures.Search;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Search
{
    public partial interface ISearchService
    {
        IList<SearchResult> SearchSuggest(string SearchText);
        IList<SearchResult> SearchCategory(string SearchText);
        void CreateAndYukleSuggestSearchIndex();
        void CreateAndYukleSearchGenelIndex();
    }
}
