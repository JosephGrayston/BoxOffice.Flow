namespace BoxOffice.Flow.Identity;

public sealed class UserFacade(CurrentUserAccessor currentUserAccessor, IUserDirectory userDirectory)
{
    private readonly CurrentUserAccessor _currentUserAccessor = currentUserAccessor;
    private readonly IUserDirectory _userDirectory = userDirectory;

    public async Task<CurrentUserContext> GetUserContextAsync() => await _currentUserAccessor.GetCurrentUserAsync();

    public async Task<UserProfile?> GetUserAsync()
    {
        var user = await _currentUserAccessor.GetCurrentUserAsync();

        if (!user.IsAuthenticated || string.IsNullOrWhiteSpace(user.UserId))
        {
            return null;
        }

        return await _userDirectory.GetUserAsync(user.UserId);
    }

    public async Task<string?> GetUserPhotoAsync()
    {
        var user = await _currentUserAccessor.GetCurrentUserAsync();

        if (!user.IsAuthenticated || string.IsNullOrWhiteSpace(user.UserId))
        {
            return null;
        }

        return await _userDirectory.GetUserPhotoAsync(user.UserId);
    }
}
