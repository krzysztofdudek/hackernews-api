namespace HackerNews.Api.Host.Fetching;

public interface IBestStoriesLoader
{
    Task<IReadOnlyCollection<int>> Load(CancellationToken cancellationToken = default);
}