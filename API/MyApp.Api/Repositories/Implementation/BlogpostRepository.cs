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

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await dbContext.BlogPosts.Include(x=> x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogpost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogpost == null){
                return null;
            }
            //Update Blogpost
            dbContext.Entry(existingBlogpost).CurrentValues.SetValues(blogPost);
            //Update Categories
            existingBlogpost.Categories = blogPost.Categories;

            await dbContext.SaveChangesAsync();
            return blogPost;

        }
    }
}