using MakinaTurkiye.Entities.Tables.Payments;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Payments
{
    public interface IBankAccountService
    {
        IList<BankAccount> GetAllAccounts(bool showHidden = false);
        BankAccount GetBankAccountByBankAccountId(int bankAccountId);
    }
}
