using MakinaTurkiye.Entities.Tables.Logs;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Logs
{
    public interface ISearchEngineLogService
    {
        SearchEngineLog GetSearchEngineLogById(int searchEngineLogId);
        void InsertSearchEngineLog(SearchEngineLog searchEngineLog);
        void InsertSearchEngineLogs(IList<SearchEngineLog> searchEngineLogs);
        void UpdateSearchEngineLog(SearchEngineLog searchEngineLog);
        void UpdateSearchEngineLogs(IList<SearchEngineLog> searchEngineLogs);
        void DeleteSearchEngineLog(SearchEngineLog searchEngineLog);
    }
}
