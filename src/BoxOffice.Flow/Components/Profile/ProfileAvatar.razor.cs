using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Components.Profile;

public partial class ProfileAvatar
{
    [CascadingParameter]
    private CurrentUserState UserState { get; set; } = default!;

    [Inject]
    private ICurrentUserProvider CurrentUserProvider { get; set; } = default!;

    private string? AvatarUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (UserState.UserProfile is not null)
        {
            _ = LoadAvatarPhotoAsync();
        }
    }

    private async Task LoadAvatarPhotoAsync()
    {
        AvatarUrl = await CurrentUserProvider.GetCurrentUserPhotoAsync(CancellationToken);

        await InvokeAsync(StateHasChanged);
    }
}
