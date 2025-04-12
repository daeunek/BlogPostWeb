using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyApp.Api.Models.Domain.DTO;
using MyApp.Api.Models.Domain;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Data;

namespace CodePulse.API.Controllers
{   

    //https://localhost:xxxx/api/categories   //CategoriesController is changed to Categories by [conroller]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase {
        
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {   
            this.categoryRepository = categoryRepository;
        }

        //

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request){

            //Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            
            await categoryRepository.CreateAsync(category);

            //Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        //GET: https://localhost:7274/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //Map Domain Model to DTO
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }

        //GET: https://localhost:7274/api/Categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)  //not doing anything by adding fromRoute is the same
        {
            var existingCategory = await categoryRepository.GetById(id);

            if (existingCategory == null)
            {
                return NotFound(); //HTTP 404
            }

            //Domain Model to DTO
            var response = new CategoryDto{
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }   

        //PUT: https://localhost:7274/api/Categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id,UpdateCategoryRequestDto request)
        {
            //Convert DTO to Domain
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            var updatedCategory = await categoryRepository.UpdateAsync(category);
            if (updatedCategory == null)
            {
                return NotFound(); //HTTP 404
            }

            //Domain Model to DTO
            var response = new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                UrlHandle = updatedCategory.UrlHandle
            };
            return Ok(response);
        }

        //DELETE: https://localhost:7274/api/Categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);
            if (category == null)
            {
                return NotFound(); //HTTP 404
            }

            //Convert Domain model to DTO
            var response = new CategoryDto{
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response); //HTTP 200
        }

    }



}