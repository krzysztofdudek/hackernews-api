using JetBrains.Annotations;

namespace HackerNews.Api.Host.Adapter;

public sealed class AdapterOptions
{
    public string BaseUrl { get; [UsedImplicitly] set; } = null!;

    public RateLimitingOptions RateLimiting { get; [UsedImplicitly] set; } = new();

    public sealed class RateLimitingOptions
    {
        public int NumberOfRequests { get; [UsedImplicitly] set; } = 20;

        public int TimePeriodSeconds { get; [UsedImplicitly] set; } = 5;
    }
}