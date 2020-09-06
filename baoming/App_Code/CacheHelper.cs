using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CacheHelper 的摘要说明
/// </summary>
public static class CacheHelper
{
    //public CacheHelper()
    private static System.Web.Caching.Cache cache = HttpRuntime.Cache;

    /// <summary>
    /// 读取缓存
    /// </summary>
    /// <param name="cacheKey">键</param>
    /// <returns></returns>
    public static T GetCache<T>(string cacheKey) where T : class
    {
        if (cache[cacheKey] != null)
        {
            return (T)cache[cacheKey];
        }
        return default(T);
    }
    /// <summary>
    /// 写入缓存
    /// </summary>
    /// <param name="value">对象数据</param>
    /// <param name="cacheKey">键</param>
    public static void WriteCache<T>(T value, string cacheKey) where T : class
    {
        cache.Insert(cacheKey, value, null, DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
    }
    /// <summary>
    /// 写入缓存
    /// </summary>
    /// <param name="value">对象数据</param>
    /// <param name="cacheKey">键</param>
    /// <param name="expireTime">到期时间</param>
    public static void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
    {
        cache.Insert(cacheKey, value, null, expireTime, System.Web.Caching.Cache.NoSlidingExpiration);
    }
    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="cacheKey">键</param>
    /// <returns></returns>
    public static bool isExist(string cacheKey)
    {
        return cache[cacheKey] != null;
    }
    /// <summary>
    /// 移除指定数据缓存
    /// </summary>
    /// <param name="cacheKey">键</param>
    public static void RemoveCache(string cacheKey)
    {
        cache.Remove(cacheKey);
    }
    /// <summary>
    /// 移除全部缓存
    /// </summary>
    public static void RemoveCache()
    {
        IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
        while (CacheEnum.MoveNext())
        {
            cache.Remove(CacheEnum.Key.ToString());
        }
    }
}