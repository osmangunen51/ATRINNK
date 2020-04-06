using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Seos;
using System.Collections.Generic;
using System.Linq;
namespace MakinaTurkiye.Services.Seos
{
    public class SeoDefinitionService : ISeoDefinitionService
    {
        #region Constants

        private const string SEOS_BY_All_SEO_KEY = "makinaturkiye.seo.all-seo";
        private const string SEODEFINITIONS_BY_ENTITY_ID_KEY = "makinaturkiye.seodefinition.byentityId-{0}-{1}";
        
        #endregion
       
        #region Fields

        private readonly IRepository<SeoDefinition> _seoDefinitionRepository;
        private readonly IRepository<Seo> _seoRepository;
        private readonly ICacheManager _cacheManager;
        
        #endregion

        #region Ctor

        public SeoDefinitionService(IRepository<SeoDefinition> seoDefinitionRepository, 
            IRepository<Seo> seoRepository, 
            ICacheManager cacheManager)
        {
            _seoDefinitionRepository = seoDefinitionRepository;
            _seoRepository = seoRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public SeoDefinition GetSeoDefinitionByEntityIdWithEntityType(int entityId, EntityTypeEnum entityType)
        {
            if (entityId == 0)
                return null;

            if (entityType == 0)
                return null;

            string key = string.Format(SEODEFINITIONS_BY_ENTITY_ID_KEY, entityId, entityType);
            return _cacheManager.Get(key, () =>
            {
                var query = _seoDefinitionRepository.Table;

                var seoDefinition = query.FirstOrDefault(sd => sd.EntityId == entityId && sd.EntityTypeId == (int)entityType);
                return seoDefinition;

            });
        }

        public IList<Seo> GetAllSeos()
        {
            string key = string.Format(SEOS_BY_All_SEO_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = _seoRepository.Table;

                var seos = query.ToList();
                return seos;
                
            });
        }

        #endregion

    }
}
