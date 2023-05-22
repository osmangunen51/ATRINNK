using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Searchs;
using System;
using System.Linq;

namespace Trinnk.Services.Search
{
    public class SearchScoreService : ISearchScoreService
    {
        private IRepository<SearchScore> _searchScoreRepository;

        public SearchScoreService(IRepository<SearchScore> searchScoreRepository)
        {
            this._searchScoreRepository = searchScoreRepository;
        }
        public SearchScore GetSearchScoreByKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                throw new ArgumentNullException("keyword");

            var query = _searchScoreRepository.Table;
            return query.FirstOrDefault(x => x.Keyword == keyword);
        }

        public void InsertSearchScore(SearchScore searchScore)
        {
            if (searchScore == null)
                throw new ArgumentNullException("searchScore");
            _searchScoreRepository.Insert(searchScore);
        }

        public void UpdateSearchScore(SearchScore searchScore)
        {
            if (searchScore == null)
                throw new ArgumentNullException("searchScore");
            _searchScoreRepository.Update(searchScore);
        }
    }
}
