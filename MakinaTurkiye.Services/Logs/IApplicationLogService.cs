using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Logs;

namespace MakinaTurkiye.Services.Logs
{
    public interface IApplicationLogService
    {
        IPagedList<ApplicationLog> GetApplicationLogs(int page, int pageDimension);

    }
}
