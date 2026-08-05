using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BoxOffice.Flow.Features.Home.Components;

public partial class FeatureCard : ComponentBase
{
    [Parameter]
    public required string Icon { get; set; }

    [Parameter]
    public required Color Colour { get; set; }

    [Parameter]
    public required string Title { get; set; }

    [Parameter]
    public required string Description { get; set; }
}
