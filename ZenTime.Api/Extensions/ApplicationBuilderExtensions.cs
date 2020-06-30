using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ZenTime.Api.Extensions
{
    public static class ProblemTypes
    {
        public const string ValidationFailed = "https://zentime.api/app-errors/validation-failed";
    }
    
    public static class ApplicationBuilderExtensions
    {
        private static string _problemJson = "application/problem+json";

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    object responseBody = null;
                    switch (contextFeature?.Error)
                    {
                        case null:
                            logger.LogError("Something went wrong");
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            break;

                        case ValidationException ex:
                            logger.LogError(ex, "Validation error occurred");
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            var details = new ProblemDetails
                            {
                                Title = "Validation Error",
                                Type = ProblemTypes.ValidationFailed,
                                Status = StatusCodes.Status400BadRequest,
                                Detail = ex.Message,
                                Instance = context.Request.Path,
                            };
                            foreach (var err in ex.Errors)
                            {
                                details.Extensions.Add(err.PropertyName, err.ToString());
                            }

                            responseBody = details;
                            context.Response.ContentType = _problemJson;
                            break;

                        default:
                            logger.LogError(contextFeature.Error, "An unexpected error occurred");
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            break;
                    }

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(responseBody));
                });
            });
        }
    }
}