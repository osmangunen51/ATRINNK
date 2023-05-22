using Trinnk.Caching;

namespace Trinnk.Services
{
    public abstract class BaseService : ICachingSupported
    {
        private readonly ICacheManager _cacheManager;

        private bool _cachingGetOrSetOperationEnabled;
        private bool _cahingRemoveOperationEnabled;

        public BaseService(ICacheManager cacheManager)
        {
            this._cacheManager = cacheManager;

            this._cachingGetOrSetOperationEnabled = true;
            this._cahingRemoveOperationEnabled = true;
        }

        public bool CachingGetOrSetOperationEnabled
        {
            get { return _cachingGetOrSetOperationEnabled; }
            set
            {
                _cachingGetOrSetOperationEnabled = value;
                ConfirmCacheOperation();
            }
        }

        public bool CachingRemoveOperationEnabled
        {
            get { return _cahingRemoveOperationEnabled; }
            set
            {
                _cahingRemoveOperationEnabled = value;
                ConfirmCacheOperation();
            }
        }

        private void ConfirmCacheOperation()
        {
            if (_cacheManager is RedisCacheManager redisCacheManager)
            {
                if (redisCacheManager.AllOperationEnabled)
                {
                    redisCacheManager.GetOperationEnabled = this.CachingGetOrSetOperationEnabled;
                    redisCacheManager.SetOperationEnabled = this.CachingGetOrSetOperationEnabled;
                    redisCacheManager.RemoveOperationEnabled = this.CachingRemoveOperationEnabled;
                }
            }
            if (_cacheManager is MemoryCacheManager memoryCacheManager)
            {
                if (memoryCacheManager.AllOperationEnabled)
                {
                    memoryCacheManager.GetOperationEnabled = this.CachingGetOrSetOperationEnabled;
                    memoryCacheManager.SetOperationEnabled = this.CachingGetOrSetOperationEnabled;
                    memoryCacheManager.RemoveOperationEnabled = this.CachingRemoveOperationEnabled;
                }
            }
        }
    }
}
