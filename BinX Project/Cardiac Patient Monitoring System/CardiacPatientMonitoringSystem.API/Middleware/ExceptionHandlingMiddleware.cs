using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception occurred while processing {Path}",
                    context.Request.Path);

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                context.Response.ContentType =
                    "application/problem+json";

                var problemDetails = new ProblemDetails
                {
                    Title = "An unexpected error occurred.",
                    Status = StatusCodes.Status500InternalServerError
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}