using Microsoft.Extensions.Options;

namespace HackerNews.Api.Host.Fetching;

internal sealed class Worker(IOptions<FetchingOptions> options, ILoader loader) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            if (stoppingToken.IsCancellationRequested)
                return;

            try
            {
                await loader.Load(stoppingToken);

                await Task.Delay(options.Value.IntervalSeconds, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}