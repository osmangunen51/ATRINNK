using System;
using System.Collections.Generic;

namespace Trinnk.Payment
{
    public interface IPaymentProviderFactory
    {
        IPaymentProvider Create(BankNames bankName);
        string CreatePaymentFormHtml(IDictionary<string, object> parameters, Uri actionUrl, bool appendFormSubmitScript = true);
    }
}