using Microsoft.Extensions.Options;
using Polly;

namespace HackerNews.Api.Host.Adapter;

public static class ServiceCollectionExtensions
{
    public static void AddAdapter(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var section = configuration.GetSection("Adapter");
        serviceCollection.Configure<AdapterOptions>(section);
        serviceCollection.AddTransient<IValidateOptions<AdapterOptions>, ValidateAdapterOptions>();

        serviceCollection.AddHttpClient<IApiClient, ApiClient>((serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AdapterOptions>>().Value;

            httpClient.BaseAddress = new Uri(options.BaseUrl);
        }).AddPolicyHandler((serviceProvider, _) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AdapterOptions>>().Value;

            var bulkheadPolicy = Policy.BulkheadAsync(options.Bulkhead.NumberOfRequests).AsAsyncPolicy<HttpResponseMessage>();

            return bulkheadPolicy;
        });
    }
}