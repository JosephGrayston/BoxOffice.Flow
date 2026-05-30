namespace BoxOffice.Flow.Identity;

public record UserProfile
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string Initials => string.Join("", (Name ?? string.Empty).Take(1)).ToUpperInvariant();
}
