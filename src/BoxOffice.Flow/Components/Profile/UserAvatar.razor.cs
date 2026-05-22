using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Components.Profile;

public partial class UserAvatar
{
    [Inject]
    private CachedUserDirectory UserDirectory { get; set; } = default!;

    private User? CurrentUser { get; set; }

    private string? AvatarUrl { get; set; }    

    protected override async Task OnInitializedAsync()
    {
        // TO DO: Replace "123" with the actual user ID of the currently logged-in user, which can be obtained from the authentication context or a similar mechanism.
        CurrentUser = await UserDirectory.GetUserAsync("123");

        if (CurrentUser is not null)
        {
            _ = LoadAvatarPhotoAsync();
        }
    }

    private async Task LoadAvatarPhotoAsync()
    {
        AvatarUrl = await UserDirectory.GetUserPhotoAsync("123");

        await InvokeAsync(StateHasChanged);
    }
}
