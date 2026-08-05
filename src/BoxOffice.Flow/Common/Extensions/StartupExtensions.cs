using BoxOffice.Flow.Identity;
using BoxOffice.Flow.Pages.Auth;
using BoxOffice.Flow.Pages.Components.Theme;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using MudBlazor.Services;

namespace BoxOffice.Flow.Common.Extensions;

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
        services.AddScoped<GraphCurrentUserProvider>();
        services.AddScoped<CurrentPrincipalAccessor>();
        services.AddScoped<CurrentUserState>();
        services.AddScoped<ICurrentUserProvider>(sp =>
        {
            var graph = sp.GetRequiredService<GraphCurrentUserProvider>();
            var cache = sp.GetRequiredService<IMemoryCache>();
            var principalAccessor = sp.GetRequiredService<CurrentPrincipalAccessor>();


            return new CachedCurrentUserProvider(graph, cache, principalAccessor);
        });
        
    }

    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        builder.Logging.AddConsole();
    }
}
