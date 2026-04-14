using MudBlazor;

namespace BoxOffice.Flow.Common;

public static class Theme
{
    public static MudTheme Instance { get; } = new()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#4F63D2",
            PrimaryContrastText = "#FFFFFF",
            PrimaryDarken = "#3A4DB8",
            PrimaryLighten = "#E8EBFB",

            Secondary = "#7C8DB5",
            SecondaryContrastText = "#FFFFFF",

            Background = "#F4F6FA",
            Surface = "#FFFFFF",

            AppbarBackground = "#FFFFFF",
            AppbarText = "#1A1F36",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1A1F36",
            DrawerIcon = "#6B7A99",

            TextPrimary = "#1A1F36",
            TextSecondary = "#6B7A99",
            TextDisabled = "#B0BBCF",

            ActionDefault = "#4F63D2",
            ActionDisabled = "#B0BBCF",
            ActionDisabledBackground = "#F0F2F8",

            Divider = "#E8ECF4",
            TableLines = "#E8ECF4",
            LinesDefault = "#E8ECF4",
            LinesInputs = "#C8D0E0",

            Success = "#22C58B",
            Warning = "#F5A623",
            Error = "#E05252",
            Info = "#3A8EF6",

            HoverOpacity = 0.06,
            RippleOpacity = 0.08,
        },

        PaletteDark = new PaletteDark()
        {
            Primary = "#7B8EF8",
            PrimaryContrastText = "#0F1120",
            PrimaryDarken = "#5C70F2",
            PrimaryLighten = "#1D2245",

            Secondary = "#8F9DBF",
            SecondaryContrastText = "#0F1120",

            Background = "#0E1120",
            Surface = "#161B30",

            AppbarBackground = "#161B30",
            AppbarText = "#E2E7F5",
            DrawerBackground = "#161B30",
            DrawerText = "#E2E7F5",
            DrawerIcon = "#7B87A8",

            TextPrimary = "#E2E7F5",
            TextSecondary = "#7B87A8",
            TextDisabled = "#3D4562",

            ActionDefault = "#7B8EF8",
            ActionDisabled = "#3D4562",
            ActionDisabledBackground = "#1D2245",

            Divider = "#232842",
            TableLines = "#232842",
            LinesDefault = "#232842",
            LinesInputs = "#2F3658",

            Success = "#2ED59A",
            Warning = "#F5B84A",
            Error = "#EF5B5B",
            Info = "#5AA9F8",

            HoverOpacity = 0.08,
            RippleOpacity = 0.10,
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "10px",
            DrawerWidthLeft = "240px",
            DrawerMiniWidthLeft = "64px",
            AppbarHeight = "60px",
        },

        Typography = new Typography()
        {
            Default = new DefaultTypography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.9rem",
                FontWeight = "400",
                LineHeight = "1.6",
                LetterSpacing = "-0.01em",
            },
            H1 = new H1Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "2rem",
                FontWeight = "700",
                LineHeight = "1.2",
                LetterSpacing = "-0.03em",
            },
            H2 = new H2Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "1.5rem",
                FontWeight = "700",
                LineHeight = "1.25",
                LetterSpacing = "-0.02em",
            },
            H3 = new H3Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "1.2rem",
                FontWeight = "600",
                LineHeight = "1.3",
                LetterSpacing = "-0.015em",
            },
            H4 = new H4Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "1rem",
                FontWeight = "600",
                LineHeight = "1.4",
                LetterSpacing = "-0.01em",
            },
            H5 = new H5Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.875rem",
                FontWeight = "600",
                LineHeight = "1.4",
            },
            H6 = new H6Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.8rem",
                FontWeight = "600",
                LineHeight = "1.4",
                LetterSpacing = "0.02em",
            },
            Body1 = new Body1Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.9rem",
                FontWeight = "400",
                LineHeight = "1.6",
            },
            Body2 = new Body2Typography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.8rem",
                FontWeight = "400",
                LineHeight = "1.5",
            },
            Caption = new CaptionTypography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.72rem",
                FontWeight = "500",
                LineHeight = "1.4",
                LetterSpacing = "0.025em",
            },
            Button = new ButtonTypography
            {
                FontFamily = ["DM Sans", "sans-serif"],
                FontSize = "0.85rem",
                FontWeight = "600",
                LetterSpacing = "0",
                TextTransform = "none",
            },
        },

        ZIndex = new ZIndex()
        {
            Drawer = 1200,
            AppBar = 1100,
            Dialog = 1300,
            Popover = 1400,
            Snackbar = 1500,
        }
    };
}
