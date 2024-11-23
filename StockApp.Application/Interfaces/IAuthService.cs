using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IAuthService
    {
        void AuthenticateAsync(string v1, string v2);
        Task<TokenResponseDto> AuthenticationAsync(string username, string password);
    }
}
