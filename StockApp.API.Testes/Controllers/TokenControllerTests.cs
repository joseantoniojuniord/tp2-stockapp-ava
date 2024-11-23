using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;

namespace StockApp.API.Testes.Controllers
{
    public class TokenControllerTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
         
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["Jwt:SecretKey"]).Returns("super-secret-key");
            configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            configurationMock.Setup(config => config["Jwt:Audience"]).Returns("TestAudience");


            var tokenController = new TokenController(Mock.Of<ILogger<TokenController>>(), configurationMock.Object);

            var userLoginDto = new UserLoginDto
            {
                Username = "admin",
                Password = "password" 
            };

            var result = await tokenController.Login(userLoginDto) as OkObjectResult;


            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

 
            var response = JObject.FromObject(result.Value);
            Assert.NotNull(response["Token"]);
            Assert.NotEmpty(response["Token"].ToString());
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["Jwt:SecretKey"]).Returns("super-secret-key");
            configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            configurationMock.Setup(config => config["Jwt:Audience"]).Returns("TestAudience");

          
            var tokenController = new TokenController(Mock.Of<ILogger<TokenController>>(), configurationMock.Object);

            var userLoginDto = new UserLoginDto
            {
                Username = "invaliduser",
                Password = "wrongpassword"
            };

            var result = await tokenController.Login(userLoginDto) as UnauthorizedObjectResult;

            
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);

            
            var response = JObject.FromObject(result.Value);
            Assert.Equal("Credenciais inválidas.", response["Message"].ToString());
        }
    }
}
