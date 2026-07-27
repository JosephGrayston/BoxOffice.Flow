using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BoxOffice.Flow.UnitTests.Identity
{
    internal sealed class AuthenticationStateProviderFake(ClaimsPrincipal principal) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _principal = principal;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_principal));
        }
    }
}
