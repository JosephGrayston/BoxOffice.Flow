using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Components.UserProfile;

public partial class UserAvatar
{
    [Inject]
    private CurrentUserService CurrentUserService { get; set; } = default!;

    private CurrentUser? CurrentUser { get; set; }

    private string? AvatarUrl { get; set; }    

    protected override async Task OnInitializedAsync()
    {
        CurrentUser = await CurrentUserService.GetUserAsync();

        if (CurrentUser is not null)
        {
            _ = LoadAvatarPhotoAsync();
        }
    }

    private async Task LoadAvatarPhotoAsync()
    {
        AvatarUrl = await CurrentUserService.GetPhotoAsync();

        await InvokeAsync(StateHasChanged);
    }
}
