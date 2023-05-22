using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Common
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        #region Fields

        private readonly IRepository<Currency> _currencyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public CurrencyService(IRepository<Currency> currencyRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._currencyRepository = currencyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<Currency> GetAllCurrencies(bool showHidden = false)
        {
            var query = _currencyRepository.Table;

            if (!showHidden)
                query = query.Where(c => c.Active);

            var currencies = query.ToList();
            return currencies;
        }

        public Currency GetCurrencyByCurrencyId(int currencyId)
        {
            if (currencyId <= 0)
                throw new ArgumentNullException("currencyId");

            var query = _currencyRepository.Table;

            var currency = query.FirstOrDefault(c => c.CurrencyId == currencyId);
            return currency;
        }

        #endregion

    }
}
