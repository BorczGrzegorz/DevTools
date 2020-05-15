namespace DevTools.Models
{
    public enum ErrorCode
    {
        UNKNOWN = 0,

        // code 4xxx means bad request almost like in http
        BOARD_ID_UNSPECIFIED = 4001,
        BAD_BOARD_ID = 4002,

        // code 5xxx means some problem that server could not resolve
        NO_ACTIVE_SPRINT = 5001
    }
}
