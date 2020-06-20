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
- DbUP (Database migrations)
- MiniProfiler for DotNet
- Logging (Serilog and Seq)
- HealthChecks
- Mediator CQRS
- Middleware error handling
- C# 8 nullable enabled

### AspNetCore API

https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1

ControllerBase vs Controller

https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1#controllerbase-class

FTA ^ CreatedAt action response

```c#
return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
```

Automatic Problem Details compatibility for 400 responses -

https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1#problem-details-for-error-status-codes

### OpenApi

https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.1

OpenAPI (Swagger) and NSwag for generating client libraries

Using NSwag (NSwagGen and NSwag.MSBuild) for the auto-generation of swagger.json (for testing and API contracts).

https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-3.1&tabs=visual-studio

NSwag.MSBuild

https://github.com/RicoSuter/NSwag/wiki/NSwag.MSBuild

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

One way to do automatic auditing (check out Shadow Parameters as well)

https://www.ryansouthgate.com/2019/03/18/ef-core-databse-auditing/

Alternative

If you're not sold on EF Core, then there is always Dapper. Combined with DbUp for migrations, you can build a very flexible, tailored solution (albeit more complex) solution.

https://dapper-tutorial.net/dapper

### Database Migrations (DbUp)

https://dbup.github.io/

Command line parsing, incremental and seed scripts, DB drop and create for local dev

### MiniProfiler for DotNet Core

https://miniprofiler.com/dotnet/AspDotNetCore

/profiler/results

You may also need

```c#
services.AddMemoryCache();
```

For the in memory profiling results.
