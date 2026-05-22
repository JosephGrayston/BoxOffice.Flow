namespace BoxOffice.Flow.Identity;

public interface IUserDirectory
{
    Task<User?> GetUserAsync(string userId);

    Task<string?> GetUserPhotoAsync(string userId);
}
