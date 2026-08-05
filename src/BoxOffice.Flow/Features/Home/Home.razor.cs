using BoxOffice.Flow.Components.Theme;
using BoxOffice.Flow.Identity;
using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Features.Home;

public partial class Home
{
    [Inject]
    private ThemeService ThemeService { get; set; } = default!;

    [CascadingParameter]
    private CurrentUserState UserState { get; set; } = default!;
}
