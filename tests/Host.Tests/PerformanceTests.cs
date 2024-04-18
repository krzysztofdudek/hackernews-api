using HackerNews.Api.Host.Fetching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NBomber.Contracts;
using NBomber.CSharp;
using Shouldly;

namespace Host.Tests;

public sealed class PerformanceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public PerformanceTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _webApplicationFactory.WithWebHostBuilder(builder => { builder.UseEnvironment("Tests"); });
    }

    [Fact]
    public async Task Handle100RpsFor10Seconds()
    {
        var httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.Services.GetRequiredService<ILoader>().Load();

        var scenario = Scenario.Create("bomb-it", async _ =>
            {
                var response = await httpClient.GetAsync("v1/best-stories?count=10");

                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            }).WithLoadSimulations(LoadSimulation.NewInject(100, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10)))
            .WithWarmUpDuration(TimeSpan.FromSeconds(5));

        var result = NBomberRunner.RegisterScenarios(scenario).Run();

        result.AllOkCount.ShouldBe(1000);
        result.AllFailCount.ShouldBe(0);
    }
}