using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MyApp.Api.Models.Domain.DTO
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}