using ClothingStore.Models.Categories;
using ClothingStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("get-all-category")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var categoriesList = await _categoryRepository.GetAll();
                return Ok(categoriesList);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory( CreateCategoryDTO createCategoryDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var categoryCreate = await _categoryRepository.Create(createCategoryDTO);
                    if (categoryCreate != null)
                    {
                        return Ok(categoryCreate);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> UpdateCategory(CreateCategoryDTO createCategoryDTO, int id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var categoryUpdate = await _categoryRepository.Update(createCategoryDTO, id);
                    if (categoryUpdate != null)
                    {
                        return Ok(categoryUpdate);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var categoryDelete = await _categoryRepository.Delete(id);
                if (categoryDelete != null)
                {
                    return Ok(categoryDelete);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
