using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BoxOffice.Flow.Components.Common;

public partial class CurrentUserStateProvider
{
    [Inject]
    private CurrentUserState UserState { get; set; } = default!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthStateTask { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (AuthStateTask is not null)
        {
            var authState = await AuthStateTask;
            await UserState.EnsureLoadedAsync(authState.User, CancellationToken.None);
        }
    }
}
