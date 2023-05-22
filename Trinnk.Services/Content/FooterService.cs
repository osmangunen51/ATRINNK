using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Content;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Trinnk.Services.Content
{
    public class FooterService : IFooterService
    {
        #region Constants

        private const string FOOTERPARENT_ALL_KEY = "footerparent.byall";
        private const string FOOTERPARENT_PATTERN_KEY = "footerparent.";

        #endregion

        #region Fields

        private readonly IRepository<FooterContent> _footerContentRepository;
        private readonly IRepository<FooterParent> _footerParentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public FooterService(IRepository<FooterContent> footerContentRepository, IRepository<FooterParent> footerParentRepository, ICacheManager cacheManager)
        {
            this._footerParentRepository = footerParentRepository;
            this._footerContentRepository = footerContentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<FooterContent> GetAllFooterContent()
        {
            var query = _footerContentRepository.Table;
            return query.ToList();
        }

        public IList<FooterParent> GetAllFooterParent()
        {
            string key = string.Format(FOOTERPARENT_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = _footerParentRepository.Table;
                query = query.OrderBy(f => f.DisplayOrder);

                query = query.Include(f => f.FooterContents);

                var footerParents = query.ToList();
                return footerParents;
            });
        }

        public FooterParent GetFooterParentByFooterParentId(int footerParentId)
        {
            if (footerParentId == 0)
                throw new ArgumentException("footerParentId");

            var query = _footerParentRepository.Table;
            return query.FirstOrDefault(fp => fp.FooterParentId == footerParentId);
        }

        public FooterContent GetFooterContentByFooterContentId(int footerContentId)
        {
            if (footerContentId == 0)
                throw new ArgumentNullException("footerContentId");

            var query = _footerContentRepository.Table;
            return query.FirstOrDefault(fc => fc.FooterContentId == footerContentId);
        }

        public IList<FooterContent> GetFooterContentsByFooterParentId(int footerParentId)
        {
            if (footerParentId == 0)
                throw new ArgumentNullException("footerParentId");

            var query = _footerContentRepository.Table;
            return query.Where(fc => fc.FooterParentId == footerParentId).ToList();
        }

        public void InsertFooterParent(FooterParent footerParent)
        {
            if (footerParent == null)
                throw new ArgumentNullException("footerParent");

            _footerParentRepository.Insert(footerParent);
            _cacheManager.RemoveByPattern(FOOTERPARENT_PATTERN_KEY);
        }

        public void InsertFooterContent(FooterContent footerContent)
        {
            if (footerContent == null)
                throw new ArgumentNullException("footerContent");

            _footerContentRepository.Insert(footerContent);
            _cacheManager.RemoveByPattern(FOOTERPARENT_PATTERN_KEY);
        }

        public void UpdateFooterContent(FooterContent footerContent)
        {
            if (footerContent == null)
                throw new ArgumentNullException("footerContent");

            _footerContentRepository.Update(footerContent);
            _cacheManager.RemoveByPattern(FOOTERPARENT_PATTERN_KEY);

        }

        public void UpdateFooterParent(FooterParent footerParent)
        {
            if (footerParent == null)
                throw new ArgumentNullException("footerParent");
            _footerParentRepository.Update(footerParent);

            //_cacheManager.RemoveByPattern(FOOTERPARENT_PATTERN_KEY);
        }

        public void DeleteFooterParent(FooterParent footerParent)
        {
            if (footerParent == null)
                throw new ArgumentNullException("footerParent");

            _footerParentRepository.Delete(footerParent);
            _cacheManager.RemoveByPattern(FOOTERPARENT_PATTERN_KEY);
        }

        public void DeleteFooterContent(FooterContent footerContent)
        {
            if (footerContent == null)
                throw new ArgumentNullException("footerContent");

            _footerContentRepository.Delete(footerContent);
            _cacheManager.RemoveByPattern(FOOTERPARENT_PATTERN_KEY);
        }

        #endregion 
    }
}
