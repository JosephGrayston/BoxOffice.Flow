using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BoxOffice.Flow.Identity;

public sealed class CurrentPrincipalAccessor(AuthenticationStateProvider authenticationStateProvider)
{
    public async Task<ClaimsPrincipal> GetPrincipalAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();

        return authState.User;
    }
}
