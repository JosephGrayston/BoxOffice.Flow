namespace BoxOffice.Flow.Identity;

public static partial class UserLogs
{
    private const int FailedToRetrieveUserEventId = 1;
    private const int FailedToRetrievePhotoEventId = 2;

    [LoggerMessage(
        EventId = FailedToRetrieveUserEventId, 
        Level = LogLevel.Error,
        Message = "Failed to retrieve current user from Microsoft Graph.")]
    public static partial void FailedToRetrieveUser(this ILogger logger, Exception ex);

    [LoggerMessage(
        EventId = FailedToRetrievePhotoEventId,
        Level = LogLevel.Error,
        Message = "Failed to retrieve the current users photo from Microsoft Graph.")]
    public static partial void FailedToRetrievePhoto(this ILogger logger, Exception ex);
}
