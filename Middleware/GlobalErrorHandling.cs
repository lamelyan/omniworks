using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace OmniWorks.Middleware;

public class GlobalErrorHandling : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            //todo: log
            Console.WriteLine($"Exception occured - {e.Message}");

            ProblemDetails problem = new()
            {
                Type = "Server error",
                Title = "Server error",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = "There was an error.",
            };

            var json = JsonSerializer.Serialize(problem);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "applicaiton/json";

            await context.Response.WriteAsync(json);
        }
    }
}