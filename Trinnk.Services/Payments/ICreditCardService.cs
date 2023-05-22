using Trinnk.Entities.Tables.Payments;
using System.Collections.Generic;

namespace Trinnk.Services.Payments
{
    public interface ICreditCardService
    {
        CreditCard GetCreditCardByCreditCardId(int creditCardId);
        IList<CreditCard> GetAllCreditCards(bool showHidden = false);

        IList<CreditCardInstallment> GetCreditCardInstallmentsByCreditCardId(int creditCardId);
        CreditCardInstallment GetCreditCardInstallmentByCreditCardInstallmentId(int creditCardInstallmentId);

    }
}
