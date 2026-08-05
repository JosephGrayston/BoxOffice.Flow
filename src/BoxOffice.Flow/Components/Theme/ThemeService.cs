namespace BoxOffice.Flow.Components.Theme;

public sealed class ThemeService
{
    public bool IsDarkMode { get; private set; }

    public event Action? OnChange;

    public void Toggle()
    {
        IsDarkMode = !IsDarkMode;
        OnChange?.Invoke();
    }
}
