using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BoxOffice.Flow.Identity;

public sealed class CurrentPrincipalAccessor(AuthenticationStateProvider authenticationStateProvider)
{
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

    public async Task<ClaimsPrincipal> GetPrincipalAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        return authState.User;
    }
}
