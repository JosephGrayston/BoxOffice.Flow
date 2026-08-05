namespace BoxOffice.Flow.Identity;

public interface ICurrentUserProvider
{
    Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken);

    Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken);
}
