using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BoxOffice.Flow.Identity;

public sealed class CurrentUserAccessor(AuthenticationStateProvider authenticationStateProvider)
{
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

    public async Task<CurrentUserContext> GetCurrentUserAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return new CurrentUserContext
        {
            IsAuthenticated = user.Identity?.IsAuthenticated ?? false,
            UserId = user.FindFirst("oid")?.Value ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };
    }
}
