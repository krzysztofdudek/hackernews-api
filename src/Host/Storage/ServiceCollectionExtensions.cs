namespace HackerNews.Api.Host.Storage;

public static class ServiceCollectionExtensions
{
    public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var section = configuration.GetSection("Storage");
        serviceCollection.Configure<StorageOptions>(section);

        serviceCollection.AddTransient<IStoriesStore, StoriesStore>();
        serviceCollection.AddTransient<IBestStoriesStore, BestStoriesStore>();
    }
}