namespace HackerNews.Api.Host.Storage;

public static class ServiceCollectionExtensions
{
    public static void AddStorage(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IStoriesStore, StoriesStore>();
        serviceCollection.AddTransient<IBestStoriesStore, BestStoriesStore>();
    }
}