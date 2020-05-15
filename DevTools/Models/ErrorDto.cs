namespace DevTools.Models
{
    public class ErrorDto
    {
        public int? Code { get; }

        public ErrorDto(ErrorCode errorCode)
        {
            Code = (int)errorCode;
        }
    }
}
