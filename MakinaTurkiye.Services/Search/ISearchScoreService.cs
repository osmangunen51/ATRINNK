using MakinaTurkiye.Entities.Tables.Searchs;

namespace MakinaTurkiye.Services.Search
{
    public interface ISearchScoreService
    {
        SearchScore GetSearchScoreByKeyword(string keyword);
        void InsertSearchScore(SearchScore searchScore);
        void UpdateSearchScore(SearchScore searchScore);

    }
}
