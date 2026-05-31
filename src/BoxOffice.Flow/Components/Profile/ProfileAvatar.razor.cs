using BoxOffice.Flow.Components.Common;
using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Components.Profile;

public partial class ProfileAvatar
{
    [Inject]
    private ICurrentUserDirectory CurrentUserDirectory { get; set; } = null!;

    private UserProfile? UserProfile { get; set; }

    private string? AvatarUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserProfile = await CurrentUserDirectory.GetCurrentUserAsync(CancellationToken);

        if (UserProfile is not null)
        {
            _ = LoadAvatarPhotoAsync();
        }
    }

    private async Task LoadAvatarPhotoAsync()
    {
        AvatarUrl = await CurrentUserDirectory.GetCurrentUserPhotoAsync(CancellationToken);

        await InvokeAsync(StateHasChanged);
    }
}
