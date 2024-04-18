using HackerNews.Api.Host.Storage;

namespace HackerNews.Api.Host.Middlewares;

public sealed class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException)
        {
        }
        catch (StorageException)
        {
            logger.LogError("During processing the request a storage error occur");

            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unhandled exception has occurred while executing the request");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}