using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace NeoSistem.EnterpriseEntity.DataCache
{
    public class RefreshCache : ICacheItemRefreshAction
  {
    public void Refresh(string removedCacheKey, object data, CacheItemRemovedReason removalReason)
    {
      if(removalReason != CacheItemRemovedReason.Removed)
      {
        DataCaching.AddCache(removedCacheKey, data);
      }
    }
  }
}