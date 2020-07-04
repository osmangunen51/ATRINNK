using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Logs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Logs
{
    public class LoginLogService:ILoginLogService
    {
        #region Fields

        private readonly IRepository<LoginLog> _loginLogRepository;

        #endregion

        #region Ctor

        public LoginLogService(IRepository<LoginLog> loginLogRepository)
        {
            this._loginLogRepository = loginLogRepository; 
        }

        #endregion

        #region Methods

        public IList<LoginLog> GetAllLoginLog()
        {
            var query = _loginLogRepository.Table;
            return query.ToList();
        }

        public LoginLog GetLoginLogByLoginLogId(int loginLogId)
        {
            if (loginLogId == 0)
                throw new ArgumentNullException("loginLogId");
            var query = _loginLogRepository.Table;
            return query.FirstOrDefault(x=>x.LoginLogId==loginLogId);
        }

        public void AddLoginLog(LoginLog loginLog)
        {
            if (loginLog == null)
                throw new ArgumentNullException("loginLog");
            _loginLogRepository.Insert(loginLog);
        }

        public void DeleteLoginLog(LoginLog loginLog)
        {
            if (loginLog == null)
                throw new ArgumentNullException("loginLog");
            _loginLogRepository.Delete(loginLog);
        }

        public void InsertLoginLog(LoginLog loginLog)
        {
            if (loginLog == null)
                throw new ArgumentNullException("loginLog");

            _loginLogRepository.Insert(loginLog);
        }

        #endregion
    }
}
