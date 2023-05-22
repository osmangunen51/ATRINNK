using Trinnk.Entities.StoredProcedures.Search;
using System.Collections.Generic;

namespace Trinnk.Services.Search
{
    public partial interface ISearchService
    {
        IList<SearchResult> SearchSuggest(string SearchText);
        IList<SearchResult> SearchCategory(string SearchText);
        void CreateAndYukleSuggestSearchIndex();
        void CreateAndYukleSearchGenelIndex();
    }
}
