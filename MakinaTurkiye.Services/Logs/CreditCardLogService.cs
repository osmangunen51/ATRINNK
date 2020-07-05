using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Logs;
using System;

namespace MakinaTurkiye.Services.Logs
{
    public class CreditCardLogService :ICreditCardLogService
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
