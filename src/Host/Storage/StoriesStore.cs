using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HackerNews.Api.Host.Storage;

internal sealed class StoriesStore(IDistributedCache distributedCache, ILogger<StoriesStore> logger) : IStoriesStore
{
    public async ValueTask<Story?> TryGet(int id, CancellationToken cancellationToken = default)
    {
        var key = CreateKey(id);
        string? value;

        try
        {
            value = await distributedCache.GetStringAsync(key, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error during getting story with id {StoryId}", id);

            throw new StorageException();
        }

        return value is null ? null : JsonSerializer.Deserialize<Story>(value);
    }

    public async ValueTask Save(Story story, CancellationToken cancellationToken = default)
    {
        var key = CreateKey(story.Id);
        var value = JsonSerializer.Serialize(story);

        try
        {
            await distributedCache.SetStringAsync(key, value, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error during saving story with id {StoryId}", story.Id);

            throw new StorageException();
        }
    }

    private static string CreateKey(int id)
    {
        return $"Story_{id}";
    }
}