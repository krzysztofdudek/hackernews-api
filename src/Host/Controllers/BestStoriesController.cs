using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using HackerNews.Api.Host.Query;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Host.Controllers;

[ApiController]
[Route("/v1/best-stories")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class BestStoriesController(IBestStoriesQuery bestStoriesQuery) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(StoryDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetBestStories([FromQuery] [Range(1, int.MaxValue)] [Required] int count = 10,
        CancellationToken cancellationToken = default)
    {
        var stories = await bestStoriesQuery.GetBestStories(count, cancellationToken);

        return Ok(stories.Select(x => new StoryDto
        {
            Title = x.Title,
            Uri = x.Uri,
            PostedBy = x.PostedBy,
            Time = x.Time,
            Score = x.Score,
            CommentCount = x.CommentCount
        }));
    }

    public sealed class StoryDto
    {
        public required string Title { get; init; }

        public required string Uri { get; init; }

        public required string PostedBy { get; init; }

        [JsonConverter(typeof(DateTimeConverter))]
        public required DateTime Time { get; init; }

        public required int Score { get; init; }

        public required int CommentCount { get; init; }
    }

    internal sealed class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Not needed.
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value:s}+00:00");
        }
    }
}