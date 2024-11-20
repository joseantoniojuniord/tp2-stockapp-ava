using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using Xunit;
using System.Threading.Tasks;


namespace StockApp.API.Testes.Controllers
{
    public class TokenControllerTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            var authServiceMock = new Mock<IAuthService>();
            var tokenController = new TokenController(authServiceMock.Object);

            var expectedToken = "token";
            var expectedExpiration = DateTime.UtcNow.AddMinutes(60);

            authServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new TokenResponseDto
            {
                Token = expectedToken,
                Expiration = expectedExpiration
            });

            var userLoginDto = new UserLoginDto
            {
                Username = "testuser",
                Password = "password"
            };

            var result = await tokenController.Login(userLoginDto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var tokenResponse = result.Value as TokenResponseDto;
            Assert.NotNull(tokenResponse);
            Assert.Equal(expectedToken, tokenResponse.Token);
            Assert.Equal(expectedExpiration, tokenResponse.Expiration);
        }
    }
}
