using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Logs;
using System;

namespace MakinaTurkiye.Services.Logs
{
    public class UserLogService : IUserLogService
    {
        #region Fileds

        private readonly IRepository<UserLog> _userLogRepository;

        #endregion

        #region Ctor

        public UserLogService(IRepository<UserLog> userLogRepository)
        {
            _userLogRepository = userLogRepository;
        }

        #endregion

        public void InsertUserLog(UserLog userLog)
        {
            if (userLog == null)
                throw new ArgumentNullException("userLog");

            _userLogRepository.Insert(userLog);
        }
    }
}
