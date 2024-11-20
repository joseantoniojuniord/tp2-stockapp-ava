using System.Net;

namespace StockApp.Domain.Exceptions
{
    public class AuthenticationException : Exception
    {
        public HttpStatusCode StatusCode {  get; }
        public string ErrorCode {  get; }

        public AuthenticationException(string message, string errorCode = "Erro de autenticação", HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
            : base(message) 
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public AuthenticationException(string message, HttpStatusCode statusCode = HttpStatusCode.Unauthorized) 
            : this(message, "Erro de autenticação", statusCode) { }
    }
}
