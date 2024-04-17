namespace HackerNews.Api.Host.Fetching;

public interface IStoryLoader
{
    Task Load(int id, CancellationToken cancellationToken = default);
}