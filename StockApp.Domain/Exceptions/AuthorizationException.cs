using System.Net;

namespace StockApp.Domain.Exceptions
{
    public class AuthorizationException : Exception
    {
       public HttpStatusCode StatusCode { get; }
       public string ErrorCode { get; }

        public AuthorizationException(string message, string errorCode = "Erro de autenticação", HttpStatusCode statusCode = HttpStatusCode.Forbidden)
             : base(message)
        { 
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public AuthorizationException(string message, HttpStatusCode statusCode = HttpStatusCode.Forbidden)
            : this(message, "Erro de autenticação", statusCode) { }
    }
}
