using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HackerNews.Api.Host.Storage;

internal sealed class StoriesStore(IDistributedCache distributedCache) : IStoriesStore
{
    public async ValueTask<Story?> TryGet(int id, CancellationToken cancellationToken = default)
    {
        var key = CreateKey(id);
        var value = await distributedCache.GetStringAsync(key, cancellationToken);

        return value is null ? null : JsonSerializer.Deserialize<Story>(value);
    }

    public async ValueTask Save(Story story, CancellationToken cancellationToken = default)
    {
        var key = CreateKey(story.Id);
        var value = JsonSerializer.Serialize(story);

        await distributedCache.SetStringAsync(key, value, cancellationToken);
    }

    private static string CreateKey(int id)
    {
        return $"Story_{id}";
    }
}