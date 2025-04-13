using MyApp.Api.Models.Domain;


namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogpostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();
       
    };
}