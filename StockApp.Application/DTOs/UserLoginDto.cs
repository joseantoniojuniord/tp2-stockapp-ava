using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockApp.Application.DTOs
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username é obrigatório.")]
        [JsonPropertyName("username")] 
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password é obrigatório.")]
        [JsonPropertyName("password")] 
        public string? Password { get; set; }
    }
}
