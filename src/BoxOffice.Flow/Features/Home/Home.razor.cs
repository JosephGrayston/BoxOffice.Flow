using BoxOffice.Flow.Components.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BoxOffice.Flow.Features.Home;

public partial class Home
{
    [Inject]
    private ThemeService ThemeService { get; set; } = default!;

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    private bool IsAuthenticated { get; set; }

    private string Username { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    { 
        if (AuthenticationState == null)
        {
            throw new InvalidOperationException("AuthenticationState is not provided. Ensure that the component is wrapped in a CascadingAuthenticationState.");
        }

        var authState = await AuthenticationState;
        var identity = authState.User.Identity;

        IsAuthenticated = identity?.IsAuthenticated ?? false;
        Username = identity?.Name ?? string.Empty;
    }
}
