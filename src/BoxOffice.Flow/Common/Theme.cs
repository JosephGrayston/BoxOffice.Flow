using MudBlazor;

namespace BoxOffice.Flow.Common;

public static class Theme
{
    public static MudTheme Instance { get; } = new()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#364D61",
            PrimaryContrastText = "#FFFFFF",

            Secondary = "#304455",               
            SecondaryContrastText = "#FFFFFF",

            Background = "#FFFFFF",
            Surface = "#F0F0F0",

            AppbarBackground = "#F0F0F0",
            AppbarText = "#000000",
            DrawerBackground = "#F0F0F0",

            TextPrimary = "#000000",
            TextSecondary = "#000000",
            TextDisabled = "#000000",

            ActionDefault = "#364D61",
            ActionDisabled = "#8A8A8A",

            Divider = "#E1E1E1",
            TableLines = "#E1E1E1",
            LinesDefault = "#E1E1E1",

            Success = "#28BE8A",
            Warning = "#DFAE44",
            Error = "#D06262",
            Info = "#347ADA",
        },


        PaletteDark = new PaletteDark()
        {
            Primary = "#87B6D9",
            PrimaryContrastText = "#000000",

            Secondary = "#97BFDE",
            SecondaryContrastText = "#000000",

            Background = "#121212",
            Surface = "#282828",

            AppbarBackground = "#282828",
            AppbarText = "#FFFFFF",
            DrawerBackground = "#282828",

            TextPrimary = "#FFFFFF",
            TextSecondary = "#FFFFFF",
            TextDisabled = "#FFFFFF",

            // Actions
            ActionDefault = "#87B6D9",
            ActionDisabled = "#8E9091",

            // Lines / borders
            Divider = "#474A4C",
            TableLines = "#474A4C",
            LinesDefault = "#474A4C",

            Success = "#47D5A6",
            Warning = "#D7AC61",
            Error = "#D94A4A",
            Info = "#4077D1"
        },


        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "6px",
            DrawerWidthLeft = "260px",
            DrawerMiniWidthLeft = "60px"
        },

        Typography = new Typography()
        {
            Default = new DefaultTypography
            {
                FontFamily = ["DM Sans", "Inter", "sans-serif"],
                FontSize = "0.95rem",
                FontWeight = "400"
            },
        }
    };
}
