namespace BoxOffice.Flow.Identity;

public interface IUserDirectory
{
    Task<UserProfile?> GetUserAsync(string userId, CancellationToken cancellationToken);

    Task<string?> GetUserPhotoAsync(string userId, CancellationToken cancellationToken);
}
