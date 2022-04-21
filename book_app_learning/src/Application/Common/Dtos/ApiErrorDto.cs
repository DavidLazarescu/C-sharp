using System.Text.Json;

namespace Application.Common.Dtos
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


        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });
    }
}