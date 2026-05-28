namespace BoxOffice.Flow.Identity;

public static partial class UserLogs
{
    private const int FailedToRetrieveUserEventId = 1;
    private const int FailedToRetrievePhotoEventId = 2;

    [LoggerMessage(
        EventId = FailedToRetrieveUserEventId, 
        Level = LogLevel.Error,
        Message = "Failed to retrieve user with ID {UserId} from Microsoft Graph.")]
    public static partial void FailedToRetrieveUser(this ILogger logger, string userId, Exception ex);

    [LoggerMessage(
        EventId = FailedToRetrievePhotoEventId,
        Level = LogLevel.Error,
        Message = "Failed to retrieve photo for user with ID {UserId} from Microsoft Graph.")]
    public static partial void FailedToRetrievePhoto(this ILogger logger, string userId, Exception ex);
}
