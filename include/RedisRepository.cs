using StackExchange.Redis;

public class RedisRepository : IRedisRepository
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _db = connectionMultiplexer.GetDatabase();
        _connectionMultiplexer = connectionMultiplexer;
    }

    private void ValidateKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty", nameof(key));
        }
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        ValidateKey(key);
        return await _db.KeyExistsAsync(key);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        ValidateKey(key);
        return await _db.StringGetAsync(key);
    }

    public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiration = null)
    {
        ValidateKey(key);
        return await _db.StringSetAsync(key, value, expiration);
    }

    public async Task<bool> DeleteKeyAsync(string key)
    {
        ValidateKey(key);
        return await _db.KeyDeleteAsync(key);
    }

    public async Task<long> DeleteKeysByPatternAsync(string pattern)
    {
        var endpoints = _connectionMultiplexer.GetEndPoints();
        long deleted = 0;

        foreach (var endpoint in endpoints)
        {
            var server = _connectionMultiplexer.GetServer(endpoint);
            var keys = server.Keys(pattern: pattern);

            foreach (var key in keys)
            {
                await _db.KeyDeleteAsync(key);
                deleted++;
            }
        }

        return deleted;
    }
}
