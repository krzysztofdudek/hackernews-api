using System.Net;
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
        var options = section.Get<AdapterOptions>()!;

        var rateLimitPolicy = Policy.RateLimitAsync(options.RateLimiting.NumberOfRequests, TimeSpan.FromSeconds(options.RateLimiting.TimePeriodSeconds),
            (_, _) => new HttpResponseMessage(HttpStatusCode.TooManyRequests));
        var retryPolicy = Policy.HandleResult<HttpResponseMessage>(x => x.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(1));
        var policy = Policy.WrapAsync(retryPolicy, rateLimitPolicy);

        serviceCollection.AddHttpClient<IApiClient, ApiClient>(httpClient => { httpClient.BaseAddress = new Uri(options.BaseUrl); })
            .AddPolicyHandler(policy);
    }
}