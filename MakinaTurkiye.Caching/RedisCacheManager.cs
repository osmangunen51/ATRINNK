using MakinaTurkiye.Caching.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Caching
{
    /// <summary>
    /// Represents a manager for caching in Redis store (http://redis.io/).
    /// Mostly it'll be used when running in a web farm or Azure.
    /// But of course it can be also used on any server or environment
    /// </summary>
    public partial class RedisCacheManager : ICacheManager, ICachingOperation
    {
        #region Fields

        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;
        //private readonly ICacheManager _perRequestCacheManager;

        #endregion

        #region Ctor

        public RedisCacheManager(IRedisConnectionWrapper connectionWrapper)
        {
            // ConnectionMultiplexer.Connect should only be called once and shared between callers
            this._connectionWrapper = connectionWrapper;

            this._db = _connectionWrapper.GetDatabase();

            //HttpContextBase httpContext = (new HttpContextWrapper(HttpContext.Current) as HttpContextBase);
            //this._perRequestCacheManager = perRequestCacheManager;
        }

        #endregion

        #region Utilities

        private byte[] SerializeAndCompress(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(zs, obj);
                return ms.ToArray();
            }
        }

        private T DecompressAndDeserialize<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(zs);
            }
        }

        private string Compress(string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        private string Decompress(string value)
        {
            var bytes = Convert.FromBase64String(value);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }


        protected byte[] Serialize(object item)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented,
                //NullValueHandling = NullValueHandling.Ignore,
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //ContractResolver = ShouldSerializeContractResolver.Instance

            };
            var jsonString = JsonConvert.SerializeObject(item, Formatting.Indented, serializerSettings);
            //jsonString = Compress(jsonString);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        protected T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(serializedObject);
            //jsonString = Decompress(jsonString);
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented,
                //NullValueHandling = NullValueHandling.Ignore,
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //ContractResolver = ShouldSerializeContractResolver.Instance

            };
            return JsonConvert.DeserializeObject<T>(jsonString, serializerSettings);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key)
        {
            if (!AllOperationEnabled)
                return default(T);

            if (!GetOperationEnabled)
                return default(T);

            //little performance workaround here:
            //we use "PerRequestCacheManager" to cache a loaded object in memory for the current HTTP request.
            //this way we won't connect to Redis server 500 times per HTTP request (e.g. each time to load a locale or setting)
            //if (_perRequestCacheManager.IsSet(key))
            //    return _perRequestCacheManager.Get<T>(key);

            var rValue = _db.StringGet(key);
            if (!rValue.HasValue)
                return default(T);

            var result = Deserialize<T>(rValue);

            //_perRequestCacheManager.Set(key, result, 0);
            return result;
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public void Set(string key, object data, int cacheTime)
        {
            if (!AllOperationEnabled)
                return;

            if (!SetOperationEnabled)
                return;

            if (data == null)
                return;

            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            _db.StringSet(key, entryBytes, expiresIn);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        public async Task<T> GetAsync<T>(string key)
        {
            if (!AllOperationEnabled)
                return default(T);

            if (!GetOperationEnabled)
                return default(T);

            //little performance workaround here:
            //we use "PerRequestCacheManager" to cache a loaded object in memory for the current HTTP request.
            //this way we won't connect to Redis server many times per HTTP request (e.g. each time to load a locale or setting)

            //if (_perRequestCacheManager.IsSet(key))
            //    return _perRequestCacheManager.Get<T>(key);

            //get serialized item from cache
            var serializedItem = await _db.StringGetAsync(key);
            if (!serializedItem.HasValue)
                return default(T);

            //deserialize item
            var item = Deserialize<T>(serializedItem);
            if (item == null)
                return default(T);

            //set item in the per-request cache
            //_perRequestCacheManager.Set(key, item, 0);

            return item;
        }

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        public async Task SetAsync(string key, object data, int cacheTime)
        {
            if (!AllOperationEnabled)
                return;

            if (!SetOperationEnabled)
                return;

            if (data == null)
                return;

            //set cache time
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            //serialize item
            var serializedItem = Serialize(data);

            //and set it to cache
            await _db.StringSetAsync(key, serializedItem, expiresIn);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            if (!AllOperationEnabled)
                return false;

            if (!SetOperationEnabled)
                return false;

            //little performance workaround here:
            //we use "PerRequestCacheManager" to cache a loaded object in memory for the current HTTP request.
            //this way we won't connect to Redis server 500 times per HTTP request (e.g. each time to load a locale or setting)

            //if (_perRequestCacheManager.IsSet(key))
            //    return true;

            return _db.KeyExists(key);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public async Task<bool> IsSetAsync(string key)
        {
            if (!AllOperationEnabled)
                return false;

            if (!SetOperationEnabled)
                return false;

            //little performance workaround here:
            //we use "PerRequestCacheManager" to cache a loaded object in memory for the current HTTP request.
            //this way we won't connect to Redis server many times per HTTP request (e.g. each time to load a locale or setting)

            //if (_perRequestCacheManager.IsSet(key))
            //    return true;

            return await _db.KeyExistsAsync(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove(string key)
        {
            if (!AllOperationEnabled)
                return;

            if (!RemoveOperationEnabled)
                return;

            _db.KeyDelete(key);
            //_perRequestCacheManager.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveByPattern(string pattern)
        {
            if (!AllOperationEnabled)
                return;

            if (!RemoveOperationEnabled)
                return;

            foreach (var ep in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(ep);
                var keys = server.Keys(database: _db.Database, pattern: "*" + pattern + "*");
                foreach (var key in keys)
                    Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            foreach (var ep in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(ep);
                //we can use the code below (commented)
                //but it requires administration permission - ",allowAdmin=true"
                //server.FlushDatabase();

                //that's why we simply interate through all elements now
                var keys = server.Keys(database: _db.Database);
                foreach (var key in keys)
                    Remove(key);
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            //if (_connectionWrapper != null)
            //    _connectionWrapper.Dispose();
        }

        public bool SetOperationEnabled { get; set; }

        public bool GetOperationEnabled { get; set; }

        public bool RemoveOperationEnabled { get; set; }

        public bool AllOperationEnabled { get; set; }

        #endregion

    }
}
