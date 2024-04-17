namespace HackerNews.Api.Host.Storage;

public interface IBestStoriesStore
{
    ValueTask<IReadOnlyCollection<int>?> TryGet(CancellationToken cancellationToken = default);

    ValueTask Save(IReadOnlyCollection<int> identifiers, CancellationToken cancellationToken = default);
}