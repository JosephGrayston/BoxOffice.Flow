using Microsoft.Extensions.Caching.Memory;

namespace BoxOffice.Flow.Identity;

public sealed class CachedCurrentUserProvider(ICurrentUserProvider userDirectory, IMemoryCache cache, CurrentPrincipalAccessor principalAccessor) : ICurrentUserProvider
{
    private readonly ICurrentUserProvider _userDirectory = userDirectory;
    private readonly IMemoryCache _cache = cache;
    private readonly CurrentPrincipalAccessor _principalAccessor = principalAccessor;

    public async Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var key = await GetCacheKeyAsync("user");

        if (key is not null && _cache.TryGetValue(key, out UserProfile? cachedUser))
        {
            return cachedUser;
        }

        var user = await _userDirectory.GetCurrentUserAsync(cancellationToken);

        if (key is not null && user is not null)
        {
            _cache.Set(key, user, TimeSpan.FromMinutes(15));
        }

        return user;
    }

    public async Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken)
    {
        var key = await GetCacheKeyAsync("photo");

        if (key is not null && _cache.TryGetValue(key, out string? cachedUserPhoto))
        {
            return cachedUserPhoto;
        }

        var userPhoto = await _userDirectory.GetCurrentUserPhotoAsync(cancellationToken);

        if (key is not null && userPhoto is not null)
        {
            _cache.Set(key, userPhoto, TimeSpan.FromMinutes(15));
        }

        return userPhoto;
    }

    private async Task<string?> GetCacheKeyAsync(string resource)
    {
        var principal = await _principalAccessor.GetPrincipalAsync();

        var objectId = principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

        return objectId is null
            ? null
            : $"{resource}:{objectId}";
    }
}
