using JetBrains.Annotations;

namespace HackerNews.Api.Host.Adapter;

public sealed class AdapterOptions
{
    public string BaseUrl { get; [UsedImplicitly] set; } = null!;
}