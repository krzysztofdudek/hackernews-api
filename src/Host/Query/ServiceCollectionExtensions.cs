namespace HackerNews.Api.Host.Query;

public static class ServiceCollectionExtensions
{
    public static void AddQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBestStoriesQuery, BestStoriesQuery>();
    }
}