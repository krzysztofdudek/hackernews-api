namespace HackerNews.Api.Host.Fetching;

internal sealed class Loader(IBestStoriesLoader bestStoriesLoader, IStoryLoader storyLoader) : ILoader
{
    public async Task Load(CancellationToken cancellationToken = default)
    {
        var bestStoriesIds = await bestStoriesLoader.Load(cancellationToken);

        foreach (var id in bestStoriesIds)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            await storyLoader.Load(id, cancellationToken);
        }
    }
}