namespace HackerNews.Api.Host.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("Redis connection string is not configured.");

        serviceCollection.AddStackExchangeRedisCache(options => { options.Configuration = connectionString; });
        serviceCollection.AddHealthChecks().AddRedis(connectionString);
    }
}