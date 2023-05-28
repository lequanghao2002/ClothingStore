using ClothingStore.CustomActionFilter;
using ClothingStore.Data;
using ClothingStore.Models.DTO;
using ClothingStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoritiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuthoritiesRepository _authoritiesRepository;
        public AuthoritiesController(AppDbContext context, IAuthoritiesRepository authoritiesRepository)
        {
            _context = context;
            _authoritiesRepository = authoritiesRepository;
        }

        [HttpGet("Get-All-Authorize")]
        public async Task<IActionResult> GetAllAuthorize()
        {
            try
            {
                var AllAuthorize = await _authoritiesRepository.GetAllAuthorize();
                return Ok(AllAuthorize);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("Create-Authorize")]
        [ValidateModel]
        public async Task<IActionResult> CreateAuthorize([FromForm] CreateAuthorizeDTO createAuthorizeDTO)
        {
            try
            {
                var AddAuthorize = await _authoritiesRepository.CreateAuthorize(createAuthorizeDTO);
                return Ok(AddAuthorize);
            }
            catch 
            {
                return BadRequest("Thêm authorize không thành công");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthorize(int id, [FromForm] AuthorizeNoIdDTO authorizeNoIdDTO)
        {
            try
            {
                if (authorizeNoIdDTO.Authorize == null)
                {
                    return Ok("Authorize không thay đổi");
                }
                var AuthorizeUpdate = await _authoritiesRepository.UpdateAuthorize(id, authorizeNoIdDTO);
                return Ok(AuthorizeUpdate);
            }
            catch
            {
                return BadRequest($"Không tìm thấy authorize có id : {id}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthorize (int id)
        {
            try
            {
                var deleteAuthorize = await _authoritiesRepository.DeleteAuthorize(id);
                if (deleteAuthorize != null)
                    return Ok(deleteAuthorize);
                 return BadRequest($"Không tìm thấy authorize có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
