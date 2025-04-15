using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Api.Models.Domain.DTO
{
    public class ImageUploadDto
    {
        [Required]
        [SwaggerSchema(Format = "binary")]
        public IFormFile file { get; set; }
        
        [Required]
        public string fileName { get; set; }
        
        [Required]
        public string title { get; set; }
    }
}