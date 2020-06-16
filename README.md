# ZenTime

Time tracker and reporting tool. Simple project to explore features in dotnet core and React.

## Frontend

- React
- Typescript
- Redux
- Middelware error handling

## Backend

- AspNetCore API
- EF Core
- MiniProfiler for DotNet
- Logging (Serilog and Seq)
- HealthChecks
- Mediator CQRS
- Middleware error handling
- C# 8 nullable enabled

### AspNetCore API

https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1

Automatic Problem Details compatibility for 400 responses -

https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1#problem-details-for-error-status-codes

### Logging

- Serilog
- Seq

With AspNetCore

https://github.com/serilog/serilog-aspnetcore

Configure via appsettings and early init

https://github.com/serilog/serilog-aspnetcore/blob/dev/samples/EarlyInitializationSample/Program.cs

### ORM - EF Core

https://docs.microsoft.com/en-us/ef/core/

C# 8 Nullability considerations

https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt#required-and-optional-properties

EF migrations setup commands

https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli#create-the-database

Logging in AspNetCore + EF Core + Serilog has some gotchas ... must set MS level to Debug if you want to see output.

### MiniProfiler for DotNet Core

https://miniprofiler.com/dotnet/AspDotNetCore

/profiler/results
