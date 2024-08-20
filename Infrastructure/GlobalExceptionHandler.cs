using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OmniWorks.Infrastructure;

public class GlobalExceptionHandler : IExceptionHandler
{
    readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception message: {Message}", exception.Message);
        
        ProblemDetails problem = new()
        {
            Title = "Server error",
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = "There was a server error. View logs for more details."
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        
        return true;
    }
}