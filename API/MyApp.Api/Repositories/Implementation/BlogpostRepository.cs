using CodePulse.API.Repositories.Interface;
using MyApp.Api.Models.Domain;
using CodePulse.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogpostRepository : IBlogpostRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BlogpostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
 

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}