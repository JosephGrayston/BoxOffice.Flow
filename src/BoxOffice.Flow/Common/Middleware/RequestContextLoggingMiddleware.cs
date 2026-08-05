namespace BoxOffice.Flow.Common.Middleware;

public sealed class RequestContextLoggingMiddleware(RequestDelegate next, ILogger<RequestContextLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestContextLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.TraceIdentifier;

        var userId = context.User?.Identity?.IsAuthenticated == true
            ? context.User.Identity.Name
            : "Anonymous";

        using (_logger.BeginScope(new Dictionary<string, string>()
        {
            ["CorrelationId"] = correlationId,
            ["UserId"] = userId ??= "Unknown"
        }))
        {
            await _next(context);
        }
    }
}
