namespace HackerNews.Api.Host.Adapter;

internal sealed class ApiClient(HttpClient httpClient) : IApiClient
{
    public async Task<IApiClient.IdsDto> GetBestStories(CancellationToken cancellationToken = default)
    {
        return (await httpClient.GetFromJsonAsync<IApiClient.IdsDto>("/v0/beststories.json", cancellationToken))!;
    }

    public async Task<IApiClient.ItemDto> GetItem(int id, CancellationToken cancellationToken = default)
    {
        return (await httpClient.GetFromJsonAsync<IApiClient.ItemDto>($"/v0/item/{id}.json", cancellationToken))!;
    }
}