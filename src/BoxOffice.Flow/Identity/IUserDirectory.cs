namespace BoxOffice.Flow.Identity;

public interface IUserDirectory
{
    Task<UserProfile?> GetUserAsync(string userId);

    Task<string?> GetUserPhotoAsync(string userId);
}
