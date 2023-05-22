using Trinnk.Entities.Tables.Payments;
using System.Collections.Generic;

namespace Trinnk.Services.Payments
{
    public interface IBankAccountService
    {
        IList<BankAccount> GetAllAccounts(bool showHidden = false);
        BankAccount GetBankAccountByBankAccountId(int bankAccountId);
    }
}
