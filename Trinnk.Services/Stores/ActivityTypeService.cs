using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Stores
{
    public class ActivityTypeService : BaseService, IActivityTypeService
    {

        #region Constants

        private const string ACTIVITYTYPES_BY_ALL_KEY = "activitiytype.all";

        #endregion

        #region Fields

        private readonly IRepository<ActivityType> _activityTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public ActivityTypeService(IRepository<ActivityType> activityTypeRepo, ICacheManager cacheManager) : base(cacheManager)
        {
            _activityTypeRepository = activityTypeRepo;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<ActivityType> GetAllActivityTypes()
        {
            string key = string.Format(ACTIVITYTYPES_BY_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = _activityTypeRepository.Table;
                query = query.OrderBy(at => at.Order);

                var activityTypes = query.ToList();
                return activityTypes;
            });
        }

        public ActivityType GetActivityTypeByActivityTypeId(byte activityTypeId)
        {
            if (activityTypeId == 0)
                throw new ArgumentNullException("activityTypeId");
            var query = _activityTypeRepository.Table;
            return query.FirstOrDefault(x => x.ActivityTypeId == activityTypeId);

        }

        public void UpdateActivityType(ActivityType activityType)
        {
            if (activityType == null)
                throw new ArgumentNullException("activityType");
            _activityTypeRepository.Update(activityType);
        }

        public void AddActivityType(ActivityType activityType)
        {
            if (activityType == null)
                throw new ArgumentNullException("activityType");
            _activityTypeRepository.Insert(activityType);
        }

        #endregion

    }
}
