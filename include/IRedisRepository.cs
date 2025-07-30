using StackExchange.Redis;

// // Redis
// builder.Services.AddScoped<IRedisRepository, RedisRepository>();
// builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
// {
//     string connectionString = "localhost:6379";
//     return ConnectionMultiplexer.Connect(connectionString);
// });

// builder.Services.AddControllers();

public interface IRedisRepository
{
    public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    public Task<T?> GetAsync<T>(string key);
    public Task<bool> DeleteAsync(string key);
    public Task<bool> ExistsAsync(string key);
}