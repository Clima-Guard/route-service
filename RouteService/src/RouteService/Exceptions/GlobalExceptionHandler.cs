using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RouteService.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred at {Time}: {Message}", DateTime.UtcNow, exception.Message);

        int statusCode = StatusCodes.Status500InternalServerError;
        string title = "Server error";
        string message = "An unexpected error occurred. Please try again later.";

        if (exception is ArgumentException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            title = "Invalid input";
            message = exception.Message;
        }
        else if (exception is TimeoutException)
        {
            statusCode = StatusCodes.Status504GatewayTimeout;
            title = "Timeout";
            message = "The request took too long to complete.";
        }

        ProblemDetails problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}