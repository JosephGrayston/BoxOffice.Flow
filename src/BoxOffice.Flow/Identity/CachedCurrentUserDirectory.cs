using Microsoft.Extensions.Caching.Memory;

namespace BoxOffice.Flow.Identity;

public sealed class CachedCurrentUserDirectory(ICurrentUserDirectory userDirectory, IMemoryCache cache) : ICurrentUserDirectory
{
    private readonly ICurrentUserDirectory _userDirectory = userDirectory;
    private readonly IMemoryCache _cache = cache;

    public async Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var cacheKey = $"current-user";

        if (_cache.TryGetValue(cacheKey, out UserProfile? cachedUser))
        {
            return cachedUser;
        }

        var user = await _userDirectory.GetCurrentUserAsync(cancellationToken);

        if (user is not null)
        {
            _cache.Set(cacheKey, user, TimeSpan.FromMinutes(15));
        }

        return user;
    }

    public async Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken )
    {
        var cacheKey = $"current-user-photo";

        if (_cache.TryGetValue(cacheKey, out string? cachedUserPhoto))
        {
            return cachedUserPhoto;
        }

        var userPhoto = await _userDirectory.GetCurrentUserPhotoAsync(cancellationToken);

        if (userPhoto is not null)
        {
            _cache.Set(cacheKey, userPhoto, TimeSpan.FromMinutes(15));
        }

        return userPhoto;
    }
} 
