using StackExchange.Redis;

namespace ShortUrlBachEnd.Redis
{
    public interface IRedisDb
    {
        Task<T> GetDataByKey<T>(string key);
        Task<bool> SetDataByKey<T>(string key, T value, TimeSpan expirationTime);
        Task<bool> SetDataByKey<T>(string key, T value);
        Task<bool> RemoveDataByKey(string key);
    }
}
