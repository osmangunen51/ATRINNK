using Trinnk.Entities.Tables.Logs;
using System.Collections.Generic;

namespace Trinnk.Services.Logs
{
    public interface ILoginLogService
    {
        IList<LoginLog> GetAllLoginLog();

        LoginLog GetLoginLogByLoginLogId(int loginLogId);

        void InsertLoginLog(LoginLog loginLog);

        void DeleteLoginLog(LoginLog loginLog);
    }
}
