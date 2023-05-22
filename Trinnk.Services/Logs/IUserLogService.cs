using Trinnk.Entities.Tables.Logs;

namespace Trinnk.Services.Logs
{
    public interface IUserLogService
    {
        void InsertUserLog(UserLog userLog);
    }
}
