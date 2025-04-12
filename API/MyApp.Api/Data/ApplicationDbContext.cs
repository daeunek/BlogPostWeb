using Microsoft.EntityFrameworkCore;
using MyApp.Api.Models.Domain;


namespace CodePulse.API.Data
{
    public class ApplicationDbContext : DbContext  //db context is an entiry frame work core
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }   //Table in DB
        public DbSet<Category> Categories { get; set; }
    }
}