namespace BoxOffice.Flow.Identity;

public interface ICurrentUserDirectory
{
    Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken);

    Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken);
}
