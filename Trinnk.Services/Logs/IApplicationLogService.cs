using Trinnk.Core;
using Trinnk.Entities.Tables.Logs;

namespace Trinnk.Services.Logs
{
    public interface IApplicationLogService
    {
        IPagedList<ApplicationLog> GetApplicationLogs(int page, int pageDimension);

        void InsertApplicationLog(ApplicationLog applicationLog);

    }
}
