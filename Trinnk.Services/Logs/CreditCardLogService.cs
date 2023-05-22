using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Logs;
using System;

namespace Trinnk.Services.Logs
{
    public class CreditCardLogService : ICreditCardLogService
    {

        #region Fileds

        private readonly IRepository<CreditCardLog> _creditCardLogRepository;

        #endregion

        #region Ctor

        public CreditCardLogService(IRepository<CreditCardLog> creditCardLogRepository)
        {
            _creditCardLogRepository = creditCardLogRepository;
        }

        #endregion

        #region Methods

        public void InsertCreditCardLog(CreditCardLog creditCardLog)
        {
            if (creditCardLog == null)
                throw new ArgumentNullException("creditCardLog");

            _creditCardLogRepository.Insert(creditCardLog);

        }

        #endregion
    }
}
