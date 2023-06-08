﻿using ClothingStore.Models.Categories;
using ClothingStore.Repositories.Categories;
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
        [AuthorizeRoles("Read", "Write", "Admin")]
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
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> CreateCategory( [FromForm] CreateCategoryDTO createCategoryDTO)
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
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> UpdateCategory([FromForm]CreateCategoryDTO createCategoryDTO, int id)
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
                        return BadRequest($"Không tìm thấy id: {id}");
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
        [AuthorizeRoles("Write", "Admin")]
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
                    return BadRequest($"Không tìm thấy id: {id}");
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
