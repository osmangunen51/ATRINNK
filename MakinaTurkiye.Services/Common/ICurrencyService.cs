using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakinaTurkiye.Entities.Tables.Common;

namespace MakinaTurkiye.Services.Common
{
    public interface ICurrencyService : ICachingSupported
    {
        Currency GetCurrencyByCurrencyId(int currencyId);

        IList<Currency> GetAllCurrencies(bool showHidden = false);
    }
}
