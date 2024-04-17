using HackerNews.Api.Host.Adapter;
using HackerNews.Api.Host.Storage;

namespace HackerNews.Api.Host.Fetching;

internal sealed class BestStoriesLoader(IBestStoriesStore store, IApiClient apiClient) : IBestStoriesLoader
{
    public async Task<IReadOnlyCollection<int>> Load(CancellationToken cancellationToken = default)
    {
        var ids = await apiClient.GetBestStories(cancellationToken);

        await store.Save(ids, cancellationToken);

        return ids.Select(x => x).ToArray();
    }
}