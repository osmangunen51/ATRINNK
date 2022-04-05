

namespace NeoSistem.EnterpriseEntity.DataCache
{
    using EnterpriseEntity.Extensions;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
    using System;

    public enum TimeOfLayer
    {
        /// <summary>
        /// Gün
        /// </summary>
        Day,
        /// <summary>
        /// Saat
        /// </summary>
        Hour,
        /// <summary>
        /// Dakika
        /// </summary>
        Minute,
        /// <summary>
        /// Saniye
        /// </summary>
        Second,
        /// <summary>
        /// Birim Saniye
        /// </summary>
        Tick,
    }

    public class DataCaching
    {

        private static ICacheManager cacheManager = CacheFactory.GetCacheManager("Cache Manager");

        /// <summary>
        /// TimeOut Süresi Bittiğinde Cache'deki Veri Tamamen Silinir.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="Data"></param>
        /// <param name="timeType"></param>
        /// <param name="time"></param>
        public static void AddCache(string cacheKey, object Data, TimeOfLayer timeType, double time)
        {

            AbsoluteTime Abstime = null;

            switch (timeType)
            {
                case TimeOfLayer.Day:
                    Abstime = new AbsoluteTime(TimeSpan.FromDays(time));
                    break;
                case TimeOfLayer.Hour:
                    Abstime = new AbsoluteTime(TimeSpan.FromHours(time));
                    break;
                case TimeOfLayer.Minute:
                    Abstime = new AbsoluteTime(TimeSpan.FromMinutes(time));
                    break;
                case TimeOfLayer.Second:
                    Abstime = new AbsoluteTime(TimeSpan.FromSeconds(time));
                    break;
                case TimeOfLayer.Tick:
                    Abstime = new AbsoluteTime(TimeSpan.FromTicks(Convert.ToInt64(time)));
                    break;
                default:
                    break;
            }
            cacheManager.Add(cacheKey, Data, CacheItemPriority.Normal, null, Abstime);
        }

        /// <summary>
        /// TimeOut Süresi Olağan Bir Şekilde Bittiğinde RefreshCache Metodu Devreye Girer ve Silinen Cache'deki Veri Tekrar Cache'e Eklenir.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="Data"></param>
        /// <param name="timeType"></param>
        /// <param name="time"></param>
        public static void AddCache(string cacheKey, object Data)
        {
            short timeOut = System.Configuration.ConfigurationManager.AppSettings["EnterpriseDataCacheTimeOut"].ToInt16();
            if (timeOut == 0)
                timeOut = 2000;

            cacheManager.Add(cacheKey, Data, CacheItemPriority.Normal, new RefreshCache(), new AbsoluteTime(TimeSpan.FromMinutes(timeOut)));
        }

        public static void RemoveCache(string cacheKey)
        {
            cacheManager.Remove(cacheKey);
        }

        public static bool IsCache(string cacheKey)
        {
            return cacheManager.Contains(cacheKey);
        }

        public static T GetCache<T>(string cacheKey)
        {
            return (T)cacheManager.GetData(cacheKey);
        }

        public static void ClearCache()
        {
            cacheManager.Flush();
        }

    }

}
