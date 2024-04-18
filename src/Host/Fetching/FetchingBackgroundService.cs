using HackerNews.Api.Host.Storage;
using Microsoft.Extensions.Options;

namespace HackerNews.Api.Host.Fetching;

internal sealed class FetchingBackgroundService(IOptions<FetchingOptions> options, ILoader loader, ILogger<FetchingBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            if (stoppingToken.IsCancellationRequested)
                return;

            try
            {
                logger.LogInformation("Best stories loading started");

                await loader.Load(stoppingToken);

                logger.LogInformation("Best stories loading finished");
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Best stories loading stopped because of application shutdown");

                return;
            }
            catch (StorageException)
            {
                logger.LogWarning("Best stories loading failed because of storage error");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unexpected error during loading best stories");
            }

            await Task.Delay(options.Value.IntervalSeconds * 1000, stoppingToken);
        }
    }
}