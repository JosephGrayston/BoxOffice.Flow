using BoxOffice.Flow.Features.Auth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using MudBlazor.Services;

namespace BoxOffice.Flow.Extensions;
public static class StartupExtensions
{
    public static void ConfigureMudBlazor(this IServiceCollection services) => services.AddMudServices();

    public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(configuration.GetRequiredSection("Identity"));

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
    }

    public static void MapEndpoints(this WebApplication app)
    {
        app.MapLogin();
        app.MapLogout();
    }
}
