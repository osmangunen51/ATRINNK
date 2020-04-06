using MakinaTurkiye.Entities.Tables.Logs;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Logs
{
    public interface ILoginLogService
    {
        IList<LoginLog> GetAllLoginLog();

        LoginLog GetLoginLogByLoginLogId(int loginLogId);

        void InsertLoginLog(LoginLog loginLog);

        void DeleteLoginLog(LoginLog loginLog);
    }
}
