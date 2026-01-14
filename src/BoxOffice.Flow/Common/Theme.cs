using MudBlazor;

namespace BoxOffice.Flow.Common;

public static class Theme
{
    public static MudTheme Instance { get; } = new()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#026AB7",
            PrimaryContrastText = "#FFFFFF",

            Secondary = "#69BD3B",
            SecondaryContrastText = "#FFFFFF",

            AppbarBackground = "#FFFFFF",
            AppbarText = "#0F172A",

            Background = "#F8FAFC",
            Surface = "#FFFFFF",

            TextPrimary = "#0F172A",
            TextSecondary = "#6B7280",
            TextDisabled = "#9CA3AF",

            ActionDefault = "#3B82F6",
            ActionDisabled = "#D1D5DB",

            Divider = "#E5E7EB",
            TableLines = "#E5E7EB",
            LinesDefault = "#E5E7EB",
        },

        PaletteDark = new PaletteDark()
        {
            Primary = "#026AB7",
            PrimaryContrastText = "#FFFFFF",

            Secondary = "#69BD3B",
            SecondaryContrastText = "#FFFFFF",

            Background = "#0F172A",
            Surface = "#111827",

            AppbarBackground = "#0B1220",
            DrawerBackground = "#0B1220",

            TextPrimary = "#E5E7EB",
            TextSecondary = "#9CA3AF",
            TextDisabled = "#6B7280",

            Divider = "#1F2937",
            TableLines = "#1F2937",
            LinesDefault = "#1F2937",

            ActionDefault = "#60A5FA",
            ActionDisabled = "#374151",
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "8px",
            DrawerWidthLeft = "260px",
            DrawerMiniWidthLeft = "60px"
        },

        Typography = new Typography()
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Poppins", "Inter", "sans-serif"],
            },
        }
    };
}
