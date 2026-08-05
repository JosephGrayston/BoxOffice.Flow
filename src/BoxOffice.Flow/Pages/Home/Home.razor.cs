using BoxOffice.Flow.Identity;
using BoxOffice.Flow.Pages.Components.Theme;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Pages.Home;

public partial class Home
{
    [Inject]
    private ThemeService ThemeService { get; set; } = default!;

    [CascadingParameter]
    private CurrentUserState UserState { get; set; } = default!;
}
