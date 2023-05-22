using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IActivityTypeService : ICachingSupported
    {
        IList<ActivityType> GetAllActivityTypes();
        ActivityType GetActivityTypeByActivityTypeId(byte activityTypeId);
        void UpdateActivityType(ActivityType activityType);
        void AddActivityType(ActivityType activityType);
    }
}
