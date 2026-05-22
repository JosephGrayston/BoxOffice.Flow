using Microsoft.Graph;

namespace BoxOffice.Flow.Identity;

public class GraphUserDirectory(GraphServiceClient graphServiceClient) : IUserDirectory
{
    private readonly GraphServiceClient _graphClient = graphServiceClient;

    public async Task<User?> GetUserAsync(string userId)
    {
        try
        {
            var currentUser = await _graphClient.Users[userId].GetAsync();

            if (currentUser is null)
            {
                return null;
            }

            return new User
            {
                DisplayName = currentUser.DisplayName,
                Email = currentUser.Mail,
            };
        }
        catch (Exception)
        {
            // TO DO: Log the exception
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
        catch (Exception)
        {
            // TO DO: Log the exception
        }

        return null;
    }
}
