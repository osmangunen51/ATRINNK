using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface ICurrencyService : ICachingSupported
    {
        Currency GetCurrencyByCurrencyId(int currencyId);

        IList<Currency> GetAllCurrencies(bool showHidden = false);
    }
}
