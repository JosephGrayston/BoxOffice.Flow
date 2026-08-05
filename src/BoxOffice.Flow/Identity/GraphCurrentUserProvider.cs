using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace BoxOffice.Flow.Identity;

public sealed class GraphCurrentUserProvider(GraphServiceClient graphServiceClient, CurrentPrincipalAccessor currentUserAccessor, ILogger<GraphCurrentUserProvider> logger) : ICurrentUserProvider
{
    public async Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        try
        {
            var principal = await currentUserAccessor.GetPrincipalAsync();

            if (principal.Identity?.IsAuthenticated is not true)
            {
                return null;
            }

            var currentUser = await graphServiceClient.Me.GetAsync(cancellationToken: cancellationToken);

            if (currentUser is null)
            {
                return null;
            }

            return new UserProfile
            {
                Name = currentUser.GivenName,
                Email = currentUser.Mail,
            };
        }
        catch (ODataError ex)
        {
            UserLogs.FailedToRetrieveUser(logger, ex);
        }

        return null;
    }

    public async Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken)
    {
        try
        {
            var principal = await currentUserAccessor.GetPrincipalAsync();

            if (principal.Identity?.IsAuthenticated is not true)
            {
                return null;
            }

            using var stream =
                await graphServiceClient.Me.Photo.Content.GetAsync(cancellationToken: cancellationToken);

            if (stream is null)
                return null;

            using var memoryStream = new MemoryStream();

            await stream.CopyToAsync(memoryStream, cancellationToken);

            var imageBytes = memoryStream.ToArray();

            return
                $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
        }
        catch (ODataError)
        {
            // Swallow the exception and return null if the user does not have a photo or if there is an error retrieving it.
        }
        catch (Exception ex)
        {
            UserLogs.FailedToRetrievePhoto(logger, ex);
        }

        return null;
    }
}
