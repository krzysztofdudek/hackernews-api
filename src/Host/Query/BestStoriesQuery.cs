using HackerNews.Api.Host.Storage;

namespace HackerNews.Api.Host.Query;

internal sealed class BestStoriesQuery(IBestStoriesStore bestStoriesStore, IStoriesStore storiesStore) : IBestStoriesQuery
{
    public async ValueTask<IReadOnlyCollection<Story>> GetBestStories(int count, CancellationToken cancellationToken = default)
    {
        var bestStoriesIds = await bestStoriesStore.TryGet(cancellationToken);

        if (bestStoriesIds is null)
            return [];

        var bestStories = new List<Story>();

        foreach (var id in bestStoriesIds)
        {
            var story = await storiesStore.TryGet(id, cancellationToken);

            if (story is null)
                continue;

            bestStories.Add(story);
        }

        return bestStories.OrderByDescending(x => x.Score).Take(count).ToArray();
    }
}