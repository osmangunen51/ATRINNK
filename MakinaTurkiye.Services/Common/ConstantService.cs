using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Common
{
    public class ConstantService : BaseService, IConstantService
    {
        #region Constants

        private const string CONSTANTS_BY_CONSTANT_ID_KEY = "constant.byconstantId-{0}";
        private const string CONSTANTS_BY_CONSTANT_IDS_KEY = "constant.byconstantIds-{0}";
        private const string CONSTANTS_BY_CONSTANT_TYPE_KEY = "constant.byconstantType-{0}";

        #endregion

        #region Fields

        private readonly IRepository<Constant> _constantRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public ConstantService(IRepository<Constant> constantRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            _constantRepository = constantRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<Constant> GetAllConstants()
        {
            var query = _constantRepository.Table;
            query = query.OrderBy(c => c.Order);
            var constants = query.ToList();
            return constants;
        }

        public Constant GetConstantByConstantId(short constantId)
        {
            if (constantId == 0)
                return null;

            string key = string.Format(CONSTANTS_BY_CONSTANT_ID_KEY, constantId);
            return _cacheManager.Get(key, () =>
             {
                 var query = _constantRepository.Table;
                 return query.FirstOrDefault(c => c.ConstantId == constantId);
             });
        }

        public IList<Constant> GetConstantsByConstantIds(List<short> constantIds)
        {
            if (constantIds == null || constantIds.Count == 0)
                return new List<Constant>();

            string constantIdsText = string.Empty;
            foreach (var item in constantIds)
            {
                constantIdsText += string.Format("{0},", item.ToString());
            }
            constantIdsText = constantIdsText.Substring(0, constantIdsText.Length - 1);

            string key = string.Format(CONSTANTS_BY_CONSTANT_IDS_KEY, constantIdsText);
            return _cacheManager.Get(key, () =>
            {
                var query = _constantRepository.Table;
                query = query.Where(c => constantIds.Contains(c.ConstantId));
                return query.ToList();
            });
        }

        public List<Constant> GetConstantByConstantType(ConstantTypeEnum constantType)
        {
            if (constantType == 0)
                return new List<Constant>();

            string key = string.Format(CONSTANTS_BY_CONSTANT_TYPE_KEY, constantType);
            return _cacheManager.Get(key, () =>
            {
                var query = _constantRepository.Table;
                query = query.Where(c => c.ConstantType == (byte)constantType);
                return query.ToList();
            });
        }

        public void InsertConstant(Constant constant)
        {
            if (constant == null)
                throw new ArgumentNullException("consnta");
            _constantRepository.Insert(constant);
        }

        #endregion


    }
}
