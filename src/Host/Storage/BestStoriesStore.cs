using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HackerNews.Api.Host.Storage;

internal sealed class BestStoriesStore(IDistributedCache distributedCache) : IBestStoriesStore
{
    private const string Key = "BestStories";

    public async ValueTask<IReadOnlyCollection<int>?> TryGet(CancellationToken cancellationToken = default)
    {
        var value = await distributedCache.GetStringAsync(Key, cancellationToken);

        return value is null ? null : JsonSerializer.Deserialize<int[]>(value);
    }

    public async ValueTask Save(IReadOnlyCollection<int> identifiers, CancellationToken cancellationToken = default)
    {
        var value = JsonSerializer.Serialize(identifiers);

        await distributedCache.SetStringAsync(Key, value, cancellationToken);
    }
}