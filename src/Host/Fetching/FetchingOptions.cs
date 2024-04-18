using JetBrains.Annotations;

namespace HackerNews.Api.Host.Fetching;

public sealed class FetchingOptions
{
    public bool IsJobEnabled { get; [UsedImplicitly] set; }
    public int IntervalSeconds { get; [UsedImplicitly] set; } = 60;
}