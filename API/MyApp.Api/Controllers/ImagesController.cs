using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.PortableExecutable;
using MyApp.Api.Models.Domain;
using MyApp.Api.Repositories.Interface;
using MyApp.Api.Models.Domain.DTO;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImagesController: ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST: {apibaseurl}/api/images
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDto uploadDto)
        {
            ValidateFileUpload(uploadDto.file);

            if (ModelState.IsValid)
            {
                //File upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(uploadDto.file.FileName),
                    FileName = uploadDto.fileName,
                    Title = uploadDto.title,
                    DateCreated = DateTime.Now,
                };

                blogImage = await imageRepository.Upload(uploadDto.file, blogImage);

                // Convert Domain model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                };
                return Ok(response);                
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file) {
            var allowedExtensions = new string[] {".jpg",".jpeg",".png"};

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Invalid file format");

            }

            if (file.Length > 10485760) //10 MB
            {
                ModelState.AddModelError("file", "File size exceeds 10 MB");
            }


        }

        // GET: {apibaseurl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImges()
        {
            //call image repostitory to get all images
            var images = await imageRepository.GetAll();

            //convert domain model to DTO
            var response = new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    Url = image.Url,
                    DateCreated = image.DateCreated
                });
            }

            return Ok(response);
        }
    }
}