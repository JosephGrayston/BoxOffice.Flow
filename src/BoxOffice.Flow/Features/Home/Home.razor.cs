using BoxOffice.Flow.Components.Theme;
using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Features.Home;

public partial class Home
{
    [Inject]
    private ThemeService ThemeService { get; set; } = default!;

    [Inject]
    private UserFacade UserFacade { get; set; } = default!;

    private UserProfile? UserProfile { get; set; }

    private CurrentUserContext? UserContext { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        UserProfile = await UserFacade.GetUserAsync();

        UserContext = await UserFacade.GetUserContextAsync();
    }
}
