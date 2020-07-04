using MakinaTurkiye.Entities.Tables.Logs;

namespace MakinaTurkiye.Services.Logs
{
    public interface IUserLogService
    {
        void InsertUserLog(UserLog userLog);
    }
}
