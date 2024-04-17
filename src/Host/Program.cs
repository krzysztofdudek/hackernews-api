using HackerNews.Api.Host.Adapter;
using HackerNews.Api.Host.Fetching;
using HackerNews.Api.Host.Infrastructure;
using HackerNews.Api.Host.Query;
using HackerNews.Api.Host.Storage;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("hacker-news-api", new OpenApiInfo
    {
        Title = "Hacker News API",
        Description = "API allowing to access all the best stories from Hacker News!"
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAdapter(builder.Configuration);
builder.Services.AddStorage();
builder.Services.AddFetching(builder.Configuration);
builder.Services.AddQueries();

var app = builder.Build();

app.UseSwagger(options => { options.RouteTemplate = "/api/{documentName}.openapi.json"; });

app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "Hacker News API documentation";
    options.SwaggerEndpoint("/api/hacker-news-api.openapi.json", "Hacker News API");
});

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();