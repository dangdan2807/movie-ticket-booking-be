using Newtonsoft.Json;
using StackExchange.Redis;

namespace ShortUrlBachEnd.Redis
{
    public class RedisDb : IRedisDb
    {
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;
        private readonly Serilog.ILogger _logger;

        public RedisDb(IConfiguration configuration, Serilog.ILogger logger)
        {
            _configuration = configuration;
            var redisConfiguration = new ConfigurationOptions
            {
                EndPoints = { _configuration.GetConnectionString("Redis") },
                Password = _configuration.GetConnectionString("RedisPassword"),
                AbortOnConnectFail = false,
            };
            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(redisConfiguration);
            _database = multiplexer.GetDatabase();
            _logger = logger;
        }

        public async Task<T> GetDataByKey<T>(string key)
        {
            T data = default(T);
            if (string.IsNullOrEmpty(key))
            {
                return data;
            }
            string serializedData = await _database.StringGetAsync(key);

            if (!string.IsNullOrEmpty(serializedData))
            {
                T result = JsonConvert.DeserializeObject<T>(serializedData);
                return result;
            }
            return data;
        }

        public async Task<bool> RemoveDataByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("key is null or empty");
            }

            object data = await _database.StringGetAsync(key);
            if (data == null || string.IsNullOrEmpty(data?.ToString()))
            {
                return false;
            }

            await _database.StringGetDeleteAsync(key);
            return true;
        }

        public async Task<bool> SetDataByKey<T>(string key, T value, TimeSpan expirationTime)
        {
            if (string.IsNullOrEmpty(key)) return false;
            else
            {
                string serializedData = JsonConvert.SerializeObject(value);

                await _database.StringSetAsync(key, serializedData, expirationTime);
                return true;
            }
        }

        public async Task<bool> SetDataByKey<T>(string key, T value)
        {
            TimeSpan expirationTime = TimeSpan.FromDays(7);
            return await SetDataByKey(key, value, expirationTime);
        }
    }
}
