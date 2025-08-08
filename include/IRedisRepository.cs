public interface IRedisRepository
{
    public Task<bool> KeyExistsAsync(string key);
    public Task<string?> GetStringAsync(string key);
    public Task<bool> SetStringAsync(string key, string value, TimeSpan? expiration = null);
    public Task<bool> DeleteKeyAsync(string key);
    public Task<long> DeleteKeysByPatternAsync(string pattern);
}
