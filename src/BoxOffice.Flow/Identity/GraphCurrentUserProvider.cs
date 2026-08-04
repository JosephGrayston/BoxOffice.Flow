using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace BoxOffice.Flow.Identity;

public sealed class GraphCurrentUserProvider(GraphServiceClient graphServiceClient, CurrentPrincipalAccessor currentUserAccessor, ILogger<GraphCurrentUserProvider> logger) : ICurrentUserProvider
{
    private readonly GraphServiceClient _graphClient = graphServiceClient;
    private readonly CurrentPrincipalAccessor _currentUserAccessor = currentUserAccessor;
    private readonly ILogger<GraphCurrentUserProvider> _logger = logger;

    public async Task<UserProfile?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        try
        {
            var principal = await _currentUserAccessor.GetPrincipalAsync();

            if (!principal.Identity?.IsAuthenticated ?? false)
            {
                return null;
            }

            var currentUser = await _graphClient.Me.GetAsync(cancellationToken: cancellationToken);

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
            UserLogs.FailedToRetrieveUser(_logger, ex);
        }

        return null;
    }

    public async Task<string?> GetCurrentUserPhotoAsync(CancellationToken cancellationToken)
    {
        try
        {
            var principal = await _currentUserAccessor.GetPrincipalAsync();

            if (!principal.Identity?.IsAuthenticated ?? false)
            {
                return null;
            }

            using var stream =
                await _graphClient.Me.Photo.Content.GetAsync(cancellationToken: cancellationToken);

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
            UserLogs.FailedToRetrievePhoto(_logger, ex);
        }

        return null;
    }
}
