using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyApp.Api.Models.Domain.DTO;
using CodePulse.API.Repositories.Interface;
using MyApp.Api.Models.Domain;


namespace MyApp.Api.Controllers
{
    //https://localhost:xxxx/api/blogposts   //BlogpostController is changed to Categories by [conroller]
    [Route("api/[controller]")]
    [ApiController]

    public class BlogpostsController : ControllerBase
    {
        private readonly IBlogpostRepository blogpostRepository;
        public BlogpostsController(IBlogpostRepository blogpostRepository)
        {
            this.blogpostRepository = blogpostRepository;
        }

        //POST: https://localhost:7274/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
        {
            //Convert DTO to Domain Model
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible
            };

            blogPost = await blogpostRepository.CreateAsync(blogPost);

            //Convert Domain Model to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible
            };
            return Ok(response);
           
        }
    }
}