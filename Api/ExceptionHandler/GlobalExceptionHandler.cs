using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace Api;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }   
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier; // Pull the id which we can use to tie the front end trace to the back end logged error

        _logger.LogError(exception, "An unhandled exception has occurred with traceId: {traceId}", traceId);

        var (statusCode, title) = GetStatusCodeAndTitle(exception);

        await Results.Problem(title: title, statusCode: statusCode, extensions: new Dictionary<string, object?> { { "traceId", traceId }}).ExecuteAsync(httpContext); // Return a generic error message

        return true; // Stop from anymore middleware running
    }

    private void TryLogToDatabase(Exception exception, string traceId)
    {
        // Log to database
    }

    private (int statusCode, string title) GetStatusCodeAndTitle(Exception exception)
    {
        // This is where I can handle other exceptions.
        // E.g. ArgumentOutOfRangeException and give another status code and title
        // I think I can use this for validation exceptions
        return exception switch
        {
            _ => (StatusCodes.Status500InternalServerError, "An unhandled exception has occurred")
        };
    }
}
