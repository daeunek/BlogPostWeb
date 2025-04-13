using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyApp.Api.Models.Domain.DTO;
using CodePulse.API.Repositories.Interface;
using MyApp.Api.Models.Domain;
using Microsoft.VisualBasic;


namespace MyApp.Api.Controllers
{
    //https://localhost:xxxx/api/blogposts   //BlogpostController is changed to Categories by [conroller]
    [Route("api/[controller]")]
    [ApiController]

    public class BlogpostsController : ControllerBase
    {
        private readonly IBlogpostRepository blogpostRepository;
        private readonly ICategoryRepository categoryRepository;
        public BlogpostsController(IBlogpostRepository blogpostRepository, ICategoryRepository categoryRepository)
        {
            this.blogpostRepository = blogpostRepository;
            this.categoryRepository = categoryRepository;
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
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }


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
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
           
        }

        //GET: https://localhost:7274/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogposts = await blogpostRepository.GetAllAsync();

            //Convert Domain Model to DTO
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogposts)
            {
                response.Add(new BlogPostDto
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
                });
            }
            return Ok(response);
        }
    }
}