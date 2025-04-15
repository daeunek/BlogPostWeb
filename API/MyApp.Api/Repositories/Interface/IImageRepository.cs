using System.Net;
using MyApp.Api.Models.Domain;

namespace MyApp.Api.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAll();

    }
}