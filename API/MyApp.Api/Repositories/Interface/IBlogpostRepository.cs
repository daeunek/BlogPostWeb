using MyApp.Api.Models.Domain;


namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogpostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetById(Guid id);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?>DeleteAsync(Guid id);

        Task<BlogPost?> GetByUrlHandle(string urlHandle);
       
    };
}