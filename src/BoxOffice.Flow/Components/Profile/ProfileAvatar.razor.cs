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

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && UserState.UserProfile is not null)
        {
            _ = InvokeAsync(async () =>
            {
                AvatarUrl = await CurrentUserProvider.GetCurrentUserPhotoAsync(CancellationToken);
                StateHasChanged();
            });
        }
    }
}
