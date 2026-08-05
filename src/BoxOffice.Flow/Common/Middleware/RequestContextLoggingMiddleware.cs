namespace BoxOffice.Flow.Common.Middleware;

public sealed class RequestContextLoggingMiddleware(RequestDelegate next, ILogger<RequestContextLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.TraceIdentifier;

        var userId = context.User?.Identity?.IsAuthenticated == true
            ? context.User.Identity.Name
            : "Anonymous";

        using (logger.BeginScope(new Dictionary<string, string>()
        {
            ["CorrelationId"] = correlationId,
            ["UserId"] = userId ?? "Unknown"
        }))
        {
            await next(context);
        }
    }
}
