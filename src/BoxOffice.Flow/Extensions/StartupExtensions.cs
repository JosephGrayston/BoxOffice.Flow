using Azure.Core;
using BoxOffice.Flow.Components.Theme;
using BoxOffice.Flow.Components.UserProfile;
using BoxOffice.Flow.Features.Auth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Graph;
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
            .AddMicrosoftIdentityWebApp(configuration.GetRequiredSection("Identity"))
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches();

        services.AddMicrosoftIdentityAzureTokenCredential();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
    }

    public static void ConfigureMicrosoftGraphClient(this IServiceCollection services, IConfiguration configuration)
    {
        var graphConfiguration = configuration
            .GetRequiredSection("MicrosoftGraph");

        var scopes = graphConfiguration.GetSection("Scopes").Get<string[]>();

        services.AddScoped(sp =>
        {
            var credential = sp.GetRequiredService<MicrosoftIdentityTokenCredential>();

            return new GraphServiceClient(credential, scopes);
        });
    }

    public static void MapEndpoints(this WebApplication app)
    {
        app.MapLogin();
        app.MapLogout();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ThemeService>();
        services.AddScoped<CurrentUserService>();
    }
}
