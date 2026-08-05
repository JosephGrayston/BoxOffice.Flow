using System.Security.Claims;

namespace BoxOffice.Flow.Identity;

public sealed class CurrentUserState(ICurrentUserProvider currentUserProvider)
{
    private Task? _loadTask;

    public UserProfile? UserProfile { get; private set; }
    public bool IsAuthenticated { get; private set; }
    public IReadOnlyList<string> Roles { get; private set; } = [];

    public Task EnsureLoadedAsync(ClaimsPrincipal principal, CancellationToken cancellationToken) => _loadTask ??= LoadAsync(principal, cancellationToken);

    private async Task LoadAsync(ClaimsPrincipal principal, CancellationToken cancellationToken)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        UserProfile = IsAuthenticated
            ? await currentUserProvider.GetCurrentUserAsync(cancellationToken)
            : null;
    }
}
