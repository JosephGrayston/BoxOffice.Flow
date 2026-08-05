namespace BoxOffice.Flow.Pages.Components.Theme;

public class ThemeService
{
    public bool IsDarkMode { get; private set; }

    public event Action? OnChange;

    public void Toggle()
    {
        IsDarkMode = !IsDarkMode;
        OnChange?.Invoke();
    }
}
