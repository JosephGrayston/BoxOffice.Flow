using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace BoxOffice.Flow.Identity;

public sealed class GraphUserDirectory(GraphServiceClient graphServiceClient, ILogger<GraphUserDirectory> logger) : IUserDirectory
{
    private readonly GraphServiceClient _graphClient = graphServiceClient;
    private readonly ILogger<GraphUserDirectory> _logger = logger;

    public async Task<UserProfile?> GetUserAsync(string userId)
    {
        try
        {
            // TO - DO: Remove hardcoded user id and use the userId parameter instead.
            // Temp to test logging
            var currentUser = await _graphClient.Users["100"].GetAsync();

            if (currentUser is null)
            {
                return null;
            }

            return new UserProfile
            {
                DisplayName = currentUser.DisplayName,
                Email = currentUser.Mail,
            };
        }
        catch (Exception ex)
        {
            UserLogs.FailedToRetrieveUser(_logger, userId, ex);
        }

        return null;
    }

    public async Task<string?> GetUserPhotoAsync(string userId)
    {
        var user = await GetUserAsync(userId);

        if (user is null)
            return null;

        try
        {
            using var stream =
                await _graphClient.Users[userId].Photo.Content.GetAsync();

            if (stream is null)
                return null;

            using var memoryStream = new MemoryStream();

            await stream.CopyToAsync(memoryStream);

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
            UserLogs.FailedToRetrievePhoto(_logger, userId, ex);
        }

        return null;
    }
}
