using HackerNews.Api.Host.Storage;

namespace HackerNews.Api.Host.Query;

public interface IBestStoriesQuery
{
    ValueTask<IReadOnlyCollection<Story>> GetBestStories(int count, CancellationToken cancellationToken = default);
}