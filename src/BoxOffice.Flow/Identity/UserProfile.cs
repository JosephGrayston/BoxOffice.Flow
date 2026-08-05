namespace BoxOffice.Flow.Identity;

public record UserProfile
{
    public string? Name { get; init; }

    public string? Email { get; init; }

    public string Initials => Name?[..1].ToUpperInvariant() ?? string.Empty;
}
