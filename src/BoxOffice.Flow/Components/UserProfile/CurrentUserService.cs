using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Graph;

namespace BoxOffice.Flow.Components.UserProfile;

public class CurrentUserService(AuthenticationStateProvider authenticationStateProvider, GraphServiceClient graphServiceClient)
{
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
    private readonly GraphServiceClient _graphClient = graphServiceClient;

    private Task<CurrentUser?>? _loadUserTask;
    private Task<string?>? _loadUserImageTask;


    public Task<CurrentUser?> GetUserAsync()
    {
        _loadUserTask ??= LoadUserAsync();

        return _loadUserTask;
    }

    public Task<string?> GetPhotoAsync()
    {
        _loadUserImageTask ??= LoadUserImageAsync();

        return _loadUserImageTask;
    }

    public Task RefreshAsync()
    {
        _loadUserTask = null;

        _loadUserImageTask = null;

        return Task.CompletedTask;
    }

    private async Task<CurrentUser?> LoadUserAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        var principal = authState.User;

        if (principal.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        try
        {
            var currentUser = await _graphClient.Me.GetAsync();

            if (currentUser is null)
            {
                return null;
            }

            return new CurrentUser
            {
                DisplayName = currentUser?.DisplayName,
                Email = currentUser?.Mail,
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task<string?> LoadUserImageAsync()
    {
        var user = await GetUserAsync();

        if (user is null)
            return null;

        try
        {
            using var stream =
                await _graphClient.Me.Photo.Content.GetAsync();

            if (stream is null)
                return null;

            using var memoryStream = new MemoryStream();

            await stream.CopyToAsync(memoryStream);

            var imageBytes = memoryStream.ToArray();

            return
                $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
        }
        catch
        {
            return null;
        }
    }
}
