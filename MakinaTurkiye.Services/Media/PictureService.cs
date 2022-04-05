using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Media
{
    public class PictureService : BaseService, IPictureService
    {
        #region Constants

        private const string PICTURES_BY_PRODUCT_ID_KEY = "makinaturkiye.picture.byproductId-{0}";
        private const string PICTURES_BY_FIRST_PICTURE_PRODUCT_ID_KEY = "makinaturkiye.picture.firstpicture.byproductId-{0}";
        private const string PICTURES_BY_MAIN_PARTY_ID_KEY = "makinaturkiye.picture.bymainpartyid-{0}";
        private const string PICTURES_BY_STORE_CERTIFICATE_ID_KEY = "makinaturkiye.picture.bystorecertificateid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<Picture> _pictureService;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public PictureService(IRepository<Picture> pictureService, ICacheManager cacheManager) : base(cacheManager)
        {
            this._pictureService = pictureService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        private void RemoveCache(Picture picture)
        {
            string mainPartyIdKey = string.Format(PICTURES_BY_MAIN_PARTY_ID_KEY, picture.MainPartyId);
            string productIdKey = string.Format(PICTURES_BY_PRODUCT_ID_KEY, picture.ProductId);
            string storeCertificateIdkey = string.Format(PICTURES_BY_STORE_CERTIFICATE_ID_KEY, picture.StoreCertificateId);
            string fistPictureProductIdKey = string.Format(PICTURES_BY_FIRST_PICTURE_PRODUCT_ID_KEY, picture.ProductId);

            _cacheManager.Remove(mainPartyIdKey);
            _cacheManager.Remove(productIdKey);
            _cacheManager.Remove(storeCertificateIdkey);
            _cacheManager.Remove(fistPictureProductIdKey);
        }

        #endregion

        #region Methods

        public Picture GetFirstPictureByProductId(int productId)
        {
            if (productId == 0)
                return null;

            string key = string.Format(PICTURES_BY_FIRST_PICTURE_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () =>
            {
                var query = _pictureService.Table;
                query = query.Where(p => p.ProductId == productId).OrderBy(p => p.PictureOrder).ThenBy(x => x.PictureId);
                return query.FirstOrDefault();
            });
        }

        public IList<Picture> GetPicturesByProductId(int productId, bool isCache = true)
        {
            if (productId == 0)
                return new List<Picture>();

            string key = string.Format(PICTURES_BY_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () =>
            {
                var query = _pictureService.Table;
                query = query.Where(p => p.ProductId == productId);
                query = query.OrderBy(p => p.PictureOrder);
                return query.ToList();
            });
        }

        public IList<Picture> GetPictureByMainPartyId(int mainPartyId)
        {
            if (mainPartyId <= 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(PICTURES_BY_MAIN_PARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _pictureService.Table;
                query = query.Where(x => x.MainPartyId == mainPartyId);

                var pictures = query.ToList();
                return pictures;
            });
        }

        public IList<Picture> GetPictureByMainPartyIdWithStoreImageType(int mainPartyId, StoreImageTypeEnum storeImageType)
        {
            var query = _pictureService.Table;

            query = query.Where(x => x.MainPartyId == mainPartyId && x.StoreImageType == (byte)storeImageType);
            query = query.OrderByDescending(x => x.PictureId);

            var pictures = query.ToList();
            return pictures;
        }

        public Picture GetPictureByPictureId(int pictureId)
        {
            if (pictureId == 0)
                throw new ArgumentNullException("pictureId");

            var query = _pictureService.Table;
            return query.FirstOrDefault(x => x.PictureId == pictureId);
        }

        public IList<Picture> GetPictureByStoreDealerIds(List<int> storeDealerIds)
        {
            var query = _pictureService.Table;
            return query.Where(x => storeDealerIds.Contains(x.StoreDealerId.Value)).ToList();
        }

        public IList<Picture> GetPictureByStoreCertificateId(int storeCertificateId)
        {
            if (storeCertificateId == 0)
                throw new ArgumentNullException("storeCertificateId");

            string key = string.Format(PICTURES_BY_STORE_CERTIFICATE_ID_KEY, storeCertificateId);
            return _cacheManager.Get(key, () =>
            {
                var query = _pictureService.Table;
                query = query.Where(x => x.StoreCertificateId == storeCertificateId);

                var pictures = query.ToList();
                return pictures;
            });
        }

        public void DeletePicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");


            _pictureService.Delete(picture);

            this.RemoveCache(picture);

        }

        public void UpdatePicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            _pictureService.Update(picture);

            this.RemoveCache(picture);
        }

        public void InsertPicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            _pictureService.Insert(picture);

            this.RemoveCache(picture);
        }

        #endregion

    }
}
