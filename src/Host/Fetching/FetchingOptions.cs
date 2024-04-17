using JetBrains.Annotations;

namespace HackerNews.Api.Host.Fetching;

public sealed class FetchingOptions
{
    public int IntervalSeconds { get; [UsedImplicitly] set; } = 60;
    public int CompleteUpdateMaxIntervalSeconds { get; [UsedImplicitly] set; } = 30;
}