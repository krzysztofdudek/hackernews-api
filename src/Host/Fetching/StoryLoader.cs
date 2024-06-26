using HackerNews.Api.Host.Adapter;
using HackerNews.Api.Host.Storage;

namespace HackerNews.Api.Host.Fetching;

internal sealed class StoryLoader(IStoriesStore store, IApiClient apiClient) : IStoryLoader
{
    public async Task Load(int id, CancellationToken cancellationToken = default)
    {
        var item = await apiClient.GetItem(id, cancellationToken);

        if (item.Id is null)
            return;

        if (item.Title is null)
            return;

        if (item.Url is null)
            return;

        if (item.Time is null)
            return;

        var story = new Story
        {
            Id = item.Id.Value,
            Title = item.Title,
            Uri = item.Url,
            PostedBy = item.By ?? string.Empty,
            Score = item.Score ?? 0,
            Time = DateTime.UnixEpoch.AddSeconds(item.Time.Value),
            CommentCount = await CountComments(item.Kids ?? [], cancellationToken)
        };

        await store.Save(story, cancellationToken);
    }

    private async Task<int> CountComments(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        var tasks = ids.Select(id => CountComments(id, cancellationToken)).ToList();

        await Task.WhenAll(tasks);

        return tasks.Select(x => x.Result).Sum();
    }

    private async Task<int> CountComments(int id, CancellationToken cancellationToken = default)
    {
        var itemDto = await apiClient.GetItem(id, cancellationToken);

        if (itemDto.Type == "comment")
            return 1 + await CountComments(itemDto.Kids ?? [], cancellationToken);

        return 0;
    }
}