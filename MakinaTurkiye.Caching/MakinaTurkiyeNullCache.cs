using System;
using System.Threading.Tasks;

namespace MakinaTurkiye.Caching
{
    /// <summary>
    /// Represents a MakinaTurkiyeNullCache
    /// </summary>
    public partial class MakinaTurkiyeNullCache  : ICacheManager
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return default(T);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            return false;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {

        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {

        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {

        }

        public async Task<T> GetAsync<T>(string key)
        {
            var result = default(T);
            return await Task.FromResult(result);
        }

        public async Task SetAsync(string key, object data, int cacheTime)
        {
            await Task.CompletedTask;
        }

        public async Task<bool> IsSetAsync(string key)
        {
            return await Task.FromResult(false);
        }
    }
}
