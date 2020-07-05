using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Common
{
    public class BannerService : IBannerService
    {
        #region Constants

        private const string BANNERS_BY_BANNER_TYPE_KEY = "makinaturkiye.banner.bybannertype-{0}";
        private const string BANNERS_BY_CATEGORY_ID_KEY = "makinaturkiye.banner.bycategoryId-{0}";

        #endregion

        #region Fileds

        private readonly IRepository<Banner> _bannerRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public BannerService(IRepository<Banner> bannerRepository, ICacheManager cacheManager)
        {
            this._bannerRepository = bannerRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<Banner> GetBannersByCategoryId(int categoryId)
        {
            if (categoryId <= 0)
                return new List<Banner>();

            string key = string.Format(BANNERS_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () =>
            {
                var result = _bannerRepository.Table.Where(b => b.CategoryId == categoryId).ToList();
                return result;
            });
        }

        public IList<Banner> GetBannersByBannerType(byte bannerType)
        {
            if (bannerType <= 0)
                return new List<Banner>();

            string key = string.Format(BANNERS_BY_BANNER_TYPE_KEY, bannerType);
            return _cacheManager.Get(key, () =>
            {
                var result = _bannerRepository.Table.Where(b => b.BannerType == bannerType);
                result = result.OrderBy(b => b.BannerOrder);
                return result.ToList();
            });
        }

        public Banner GetBannerByBannerId(int bannerId)
        {
            if (bannerId == 0)
                throw new ArgumentNullException("bannerId");

            var query = _bannerRepository.Table;
            return query.FirstOrDefault(x => x.BannerId == bannerId);
        }

        public void InsertBanner(Banner banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            _bannerRepository.Insert(banner);
        }

        public void DeleteBanner(Banner banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            _bannerRepository.Delete(banner);

        }
        #endregion
    }
}
