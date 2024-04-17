using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace HackerNews.Api.Host.Adapter;

public interface IApiClient
{
    Task<IdsDto> GetBestStories(CancellationToken cancellationToken = default);

    Task<ItemDto> GetItem(int id, CancellationToken cancellationToken = default);

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class IdsDto : List<int>;

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
    public sealed class ItemDto
    {
        [JsonPropertyName("id")] public int? Id { get; set; } //The item's unique id.

        [JsonPropertyName("deleted")] public bool? Deleted { get; set; } //true if the item is deleted.

        [JsonPropertyName("type")] public string? Type { get; set; } //The type of item. One of "job", "story", "comment", "poll", or "pollopt".

        [JsonPropertyName("by")] public string? By { get; set; } //The username of the item's author.

        [JsonPropertyName("time")] public long? Time { get; set; } //Creation date of the item, in Unix Time.

        [JsonPropertyName("text")] public string? Text { get; set; } //The comment, story or poll text. HTML.

        [JsonPropertyName("dead")] public bool? Dead { get; set; } //true if the item is dead.

        [JsonPropertyName("parent")] public int? Parent { get; set; } //The comment's parent: either another comment or the relevant story.

        [JsonPropertyName("poll")] public int? Poll { get; set; } //The pollopt's associated poll.

        [JsonPropertyName("kids")] public List<int>? Kids { get; set; } //The ids of the item's comments, in ranked display order.

        [JsonPropertyName("url")] public string? Url { get; set; } //The URL of the story.

        [JsonPropertyName("score")] public int? Score { get; set; } //The story's score, or the votes for a pollopt.

        [JsonPropertyName("title")] public string? Title { get; set; } //The title of the story, poll or job. HTML.

        [JsonPropertyName("parts")] public List<string>? Parts { get; set; } //A list of related pollopts, in display order.

        [JsonPropertyName("descendants")] public int? Descendants { get; set; } //In the case of stories or polls, the total comment count.
    }
}