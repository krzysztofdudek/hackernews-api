using System.Text.Json;
using HackerNews.Api.Host.Fetching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace HackerNews.Api.Host.Storage;

internal sealed class BestStoriesStore(IDistributedCache distributedCache, IOptions<StorageOptions> options, ILogger<BestStoriesLoader> logger)
    : IBestStoriesStore
{
    private const string Key = "BestStories";

    public async ValueTask<IReadOnlyCollection<int>?> TryGet(CancellationToken cancellationToken = default)
    {
        string? value;

        try
        {
            value = await distributedCache.GetStringAsync(Key, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error during getting best stories");

            throw new StorageException();
        }

        return value is null ? null : JsonSerializer.Deserialize<int[]>(value);
    }

    public async ValueTask Save(IReadOnlyCollection<int> identifiers, CancellationToken cancellationToken = default)
    {
        var value = JsonSerializer.Serialize(identifiers);

        try
        {
            await distributedCache.SetStringAsync(Key, value, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(options.Value.SavedEntriesExpirationSeconds)
            }, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error during saving best stories");

            throw new StorageException();
        }
    }
}