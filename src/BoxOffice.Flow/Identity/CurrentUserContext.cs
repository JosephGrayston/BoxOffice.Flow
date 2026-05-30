namespace BoxOffice.Flow.Identity;

public sealed record CurrentUserContext
{
    public required bool IsAuthenticated { get; init; }
}
