namespace LiveScore.Utils
{
    public enum ErrorCode
    {
        NoError = 0,
        Unknown = 1,
        InternalServerError = 2,
        ClientError = 3,
        InvalidGroupId = 4,
        MissingScores = 5,
        InvalidScores = 6
    }
}
