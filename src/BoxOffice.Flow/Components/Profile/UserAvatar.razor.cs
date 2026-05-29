using BoxOffice.Flow.Components.Common;
using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Components.Profile;

public partial class UserAvatar
{
    [Inject]
    private UserFacade UserFacade { get; set; } = null!;

    private UserProfile? UserProfile { get; set; }

    private string? AvatarUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserProfile = await UserFacade.GetUserAsync(CancellationToken);

        if (UserProfile is not null)
        {
            _ = LoadAvatarPhotoAsync();
        }
    }

    private async Task LoadAvatarPhotoAsync()
    {
        AvatarUrl = await UserFacade.GetUserPhotoAsync(CancellationToken);

        await InvokeAsync(StateHasChanged);
    }
}
