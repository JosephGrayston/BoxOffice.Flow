namespace BoxOffice.Flow.Identity;

public record UserProfile
{
    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string Initials => string.Join("",
        (DisplayName ?? string.Empty)
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Take(2)
        .Select(x => char.ToUpperInvariant(x[0])));
}
