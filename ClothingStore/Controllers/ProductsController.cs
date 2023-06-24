using ClothingStore.Models.Products;
using ClothingStore.Repositories.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        [HttpGet("get-all-product")]
        [AuthorizeRoles("Read", "Write", "Admin")]
        public async Task<IActionResult> GetAllProduct(string? filter, string? sortBy, bool isAcending = true, int page = 0, int pageSize = 6) 
        {
            try
            {
                var productList = await _productRepository.GetAll(filter, sortBy, isAcending, page, pageSize);
                return Ok(productList);
            }
            catch
            {
                return BadRequest();
            }
        
        }

        [HttpGet("get-product-by-id/{id}")]
        [AuthorizeRoles("Read", "Write", "Admin")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var productById = await _productRepository.GetById(id);
                if (productById == null)
                    return BadRequest($"Không tìm thấy sản phẩm có id = {id}");
                return Ok(productById);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("get-product-by-category/{id}")]
        [AuthorizeRoles("Read", "Write", "Admin")]
        public async Task<IActionResult> GetProductByCategory([Required] int id)
        {
            try
            {
                var productList = await _productRepository.GetByCategory(id);
                return Ok(productList);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("create-product")]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var productNew = await _productRepository.Create(createProductDTO);
                    if (productNew != null)
                    {
                        return Ok(productNew);
                    }
                    else
                    {
                        return BadRequest("Tạo sản phẩm không thành công");
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

        [HttpPut("update-product/{id}")]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> UpdateProduct(CreateProductDTO createProductDTO, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productUpdate = await _productRepository.Update(createProductDTO, id);
                    if (productUpdate != null)
                    {
                        return Ok(productUpdate);
                    }
                    else
                    {
                        return BadRequest($"Không tìm thấy sản phẩm có id = {id}");
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

        [HttpDelete("delete-product/{id}")]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> DeleteProduct([Required] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productDelete = await _productRepository.Delete(id);
                    if (productDelete != null)
                    {
                        return Ok(productDelete);
                    }
                    else
                    {
                        return BadRequest($"Không tìm thấy sản phẩm có id = {id}");
                    }
                }
                catch
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
