using Microsoft.Extensions.Options;

namespace HackerNews.Api.Host.Adapter;

internal sealed class ValidateAdapterOptions : IValidateOptions<AdapterOptions>
{
    public ValidateOptionsResult Validate(string? name, AdapterOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            return ValidateOptionsResult.Fail("Adapter base url is not configured.");

        return ValidateOptionsResult.Success;
    }
}