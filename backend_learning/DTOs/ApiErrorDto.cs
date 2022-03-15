namespace backend_learning.DTOs
{
    public class ApiErrorDto
    {
        public int StatusCode { get; set; }
        
        public string Message { get; set; }
        
        public string StackTrace { get; set; }
        
        
        public ApiErrorDto(int statusCode, string message, string stackTrace = null)
        {
            StatusCode = statusCode;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}