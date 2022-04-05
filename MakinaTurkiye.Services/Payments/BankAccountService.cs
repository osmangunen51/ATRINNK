using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Payments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Payments
{
    public class BankAccountService : IBankAccountService
    {
        #region Fileds

        private readonly IRepository<BankAccount> _bankAccountRepository;

        #endregion

        #region Ctor

        public BankAccountService(IRepository<BankAccount> bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        #endregion


        public IList<BankAccount> GetAllAccounts(bool showHidden = false)
        {
            var query = _bankAccountRepository.Table;
            if (!showHidden)
                query = query.Where(ba => ba.Active);

            var bankAccounts = query.ToList();
            return bankAccounts;
        }

        public BankAccount GetBankAccountByBankAccountId(int bankAccountId)
        {
            if (bankAccountId == 0)
                throw new ArgumentNullException("bankAccountId");

            var query = _bankAccountRepository.Table;
            var bankAccount = query.FirstOrDefault(ba => ba.AccountId == bankAccountId);
            return bankAccount;
        }
    }
}
