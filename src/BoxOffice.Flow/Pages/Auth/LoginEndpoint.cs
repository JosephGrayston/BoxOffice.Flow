using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace BoxOffice.Flow.Pages.Auth;

public static class LoginEndpoint
{
    public static IEndpointRouteBuilder MapLogin(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/login", async context =>
        {
            await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme);
        });

        return endpoints;
    }
}
