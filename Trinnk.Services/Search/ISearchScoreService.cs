using Trinnk.Entities.Tables.Searchs;

namespace Trinnk.Services.Search
{
    public interface ISearchScoreService
    {
        SearchScore GetSearchScoreByKeyword(string keyword);
        void InsertSearchScore(SearchScore searchScore);
        void UpdateSearchScore(SearchScore searchScore);

    }
}
