﻿using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class DealarBrandService : BaseService, IDealarBrandService
    {

        #region Constants

        private const string DEALARBRANS_BY_MAIN_PARTY_ID_KEY = "makinaturkiye.dealerbrand.bymainpartyid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<DealerBrand> _dealarBrandRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public DealarBrandService(IRepository<DealerBrand> dealarBrandRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            _dealarBrandRepository = dealarBrandRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<DealerBrand> GetDealarBrandsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(DEALARBRANS_BY_MAIN_PARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _dealarBrandRepository.Table;
                query = query.Where(x => x.MainPartyId == mainPartyId);

                var dealerBrans = query.ToList();
                return dealerBrans;
            });
        }


        public void InsertDealerBrand(DealerBrand dealerBrand)
        {
            if (dealerBrand == null)
                throw new ArgumentException("dealerBrand");

            _dealarBrandRepository.Insert(dealerBrand);
        }

        public void DeleteDealerBrand(DealerBrand dealerBrand)
        {
            if (dealerBrand == null)
                throw new ArgumentException("dealerBrand");

            _dealarBrandRepository.Delete(dealerBrand);
        }

        public DealerBrand GetDealerBrandByDealerBrandId(int dealerBrandId)
        {
            if (dealerBrandId <= 0)
                throw new ArgumentNullException("dealerBrandId");

            var query = _dealarBrandRepository.Table;

            var dealerBrand = query.FirstOrDefault(d => d.DealerBrandId == dealerBrandId);
            return dealerBrand;
        }

        #endregion

    }
}
