namespace HackerNews.Api.Host.Storage;

public interface IStoriesStore
{
    ValueTask<Story?> TryGet(int id, CancellationToken cancellationToken = default);

    ValueTask Save(Story story, CancellationToken cancellationToken = default);
}