using System.Text.Json;

namespace LMS.API.CustomActionFilter;

public class ExceptionHandleMiddleware
{
    private readonly ILogger<ExceptionHandleMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, RequestDelegate next)
    {
        this._logger = logger;
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //first thing to do is to log the exception
            _logger.LogError(ex, ex.Message);
            //second thing to do is to send a custom error response
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var error = new
            {
                ErrorMessage = "An error occurred while processing your request.",
                ExceptionMessage = ex.Message
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
