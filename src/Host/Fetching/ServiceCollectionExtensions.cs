namespace HackerNews.Api.Host.Fetching;

public static class ServiceCollectionExtensions
{
    public static void AddFetching(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var section = configuration.GetSection("Fetching");
        serviceCollection.Configure<FetchingOptions>(section);

        var options = section.Get<FetchingOptions>()!;
        if (options.IsJobEnabled)
            serviceCollection.AddHostedService<Worker>();

        serviceCollection.AddTransient<ILoader, Loader>();
        serviceCollection.AddTransient<IBestStoriesLoader, BestStoriesLoader>();
        serviceCollection.AddTransient<IStoryLoader, StoryLoader>();
    }
}