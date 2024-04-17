namespace HackerNews.Api.Host.Fetching;

public interface ILoader
{
    Task Load(CancellationToken cancellationToken = default);
}