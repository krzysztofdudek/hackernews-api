# Hacker News API - easy access to the best stories!

## Required tools

- Docker
- Dotnet
- Powershell

## How to run it?

### Docker compose

- run PowerShell command line
- clone repository
- navigate to repository
- execute script `./scripts/run-all.ps1`
- navigate to [http://localhost:8080/swagger](http://localhost:8080/swagger)
- test the endpoint that is presented

### Local

- run PowerShell command line
- clone repository
- navigate to repository
- execute script `./scripts/run-infrastructure.ps1`
- execute `$env:ASPNETCORE_ENVIRONMENT="Development"`
- execute `cd .\src\Host\`
- execute `dotnet run`
- navigate to [http://localhost:8080/swagger](http://localhost:8080/swagger)
- test the endpoint that is presented

## Assumptions

- usage of this API can be big, so I assumed that can be scaled horizontally (that's why I added Redis as a storage)
- only one background worker is fetching data per single time interval (haven't implemented it sadly)
- api supports only one list: "best stories"
- calls to Hacker News API can be limited by configuration (configurable rate limiting when loading data)

## What can be done more?

- handling errors of http client gathering data from Hacker News API (timeouts, other...)
- some E2E tests with IApiClient returning mock data (Wiremock or replaced implementation)
- unit tests covering:
  - storage
  - fetching mechanics
- better logging (without such a spam from http clients)
- potentially query cached?
- background job running only on one instance of the service if there are many (so that only one sync is executed)
- more flexible query if required in terms of filters / text search / don't know
- add dashboard with metrics (but it really depends on the platform, where it could be deployed)
- rearrange storage if needed, so that it can be browsed more flexibly, currently there is only one list that can be browsed
- any security if needed
- CI/CD is missing
- assuming that the destination could be an Azure App Service then some deployment files (I wanted to use Terraform or Azure templates)
- SSL?