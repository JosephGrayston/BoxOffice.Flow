using Microsoft.Extensions.Caching.Memory;

namespace BoxOffice.Flow.Identity;

public sealed class CachedUserDirectory(IUserDirectory userDirectory, IMemoryCache cache) : IUserDirectory
{
    private readonly IUserDirectory _userDirectory = userDirectory;
    private readonly IMemoryCache _cache = cache;

    public async Task<UserProfile?> GetUserAsync(string userId)
    {
        var cacheKey = $"user:{userId}";

        if (_cache.TryGetValue(cacheKey, out UserProfile? cachedUser))
        {
            return cachedUser;
        }

        var user = await _userDirectory.GetUserAsync(userId);

        if (user is not null)
        {
            _cache.Set(cacheKey, user, TimeSpan.FromMinutes(15));
        }

        return user;
    }

    public async Task<string?> GetUserPhotoAsync(string userId)
    {
        var cacheKey = $"userPhoto:{userId}";

        if (_cache.TryGetValue(cacheKey, out string? cachedUserPhoto))
        {
            return cachedUserPhoto;
        }

        var userPhoto = await _userDirectory.GetUserPhotoAsync(userId);

        if (userPhoto is not null)
        {
            _cache.Set(cacheKey, userPhoto, TimeSpan.FromMinutes(15));
        }

        return userPhoto;
    }
} 
