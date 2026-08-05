using Microsoft.Extensions.Caching.Memory;

namespace BoxOffice.Flow.Identity;

public sealed class CachedCurrentUserProvider(ICurrentUserProvider userProvider, IMemoryCache cache, CurrentPrincipalAccessor principalAccessor) : ICurrentUserProvider
{

    public async Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var key = await GetCacheKeyAsync("user");

        if (key is not null && cache.TryGetValue(key, out UserProfile? cachedUser))
        {
            return cachedUser;
        }

        var user = await userProvider.GetCurrentUserAsync(cancellationToken);

        if (key is not null && user is not null)
        {
            cache.Set(key, user, TimeSpan.FromMinutes(15));
        }

        return user;
    }

    public async Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken)
    {
        var key = await GetCacheKeyAsync("photo");

        if (key is not null && cache.TryGetValue(key, out string? cachedUserPhoto))
        {
            return cachedUserPhoto;
        }

        var userPhoto = await userProvider.GetCurrentUserPhotoAsync(cancellationToken);

        if (key is not null && userPhoto is not null)
        {
            cache.Set(key, userPhoto, TimeSpan.FromMinutes(15));
        }

        return userPhoto;
    }

    private async Task<string?> GetCacheKeyAsync(string resource)
    {
        var principal = await principalAccessor.GetPrincipalAsync();

        var objectId = principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

        return objectId is null
            ? null
            : $"{resource}:{objectId}";
    }
}
