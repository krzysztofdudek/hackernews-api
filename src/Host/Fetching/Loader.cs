namespace HackerNews.Api.Host.Fetching;

internal sealed class Loader(IBestStoriesLoader bestStoriesLoader, IStoryLoader storyLoader) : ILoader
{
    public async Task Load(CancellationToken cancellationToken = default)
    {
        var bestStoriesIds = await bestStoriesLoader.Load(cancellationToken);

        var storiesTasks = new List<Task>();

        foreach (var id in bestStoriesIds)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            var task = storyLoader.Load(id, cancellationToken);

            storiesTasks.Add(task);
        }

        await Task.WhenAll(storiesTasks);
    }
}