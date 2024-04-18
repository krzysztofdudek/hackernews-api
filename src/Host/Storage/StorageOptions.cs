using JetBrains.Annotations;

namespace HackerNews.Api.Host.Storage;

public sealed class StorageOptions
{
    public int SavedEntriesExpirationSeconds { get; [UsedImplicitly] set; } = 120;
}