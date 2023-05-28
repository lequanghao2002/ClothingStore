using ClothingStore.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClothingStore.Repositories;
using ClothingStore.Models.DTO.User;
using ClothingStore.CustomActionFilter;
using Microsoft.Build.Framework;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserController(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpGet("Get-All-User")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var UserList = await _userRepository.GetAllUser();
                return Ok(UserList);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Get-User-By-Account-Password")]
        public async Task<IActionResult> GetUserAccountPassword([System.ComponentModel.DataAnnotations.Required] string account, [System.ComponentModel.DataAnnotations.Required] string password)
        {
            try
            {
                var UserByAP = await _userRepository.GetUserByAccPass(account, password);
                if(UserByAP != null) 
                    return Ok(UserByAP);
                return BadRequest("Không có user");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Register-User")]
        [ValidateModel]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserDTO RegisterUserDTO)
        {
            try
            {
                var RegisterUser = await _userRepository.RegisterUser(RegisterUserDTO);
                if (RegisterUser != null)
                {
                    return Ok(RegisterUser);
                }
                else return BadRequest("Đăng kí không thành công");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Login-User")]
        public  IActionResult LoginUser([FromForm]LoginUser loginUser)
        {
            try
            {
                var LoginUser = _userRepository.LoginUser(loginUser);
                if (LoginUser != null)
                {
                    return Ok("Đăng nhập thành công");
                }
                else return BadRequest("Đăng nhập không thành công");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Update-User")]
        public async Task<IActionResult> UpdateUser([System.ComponentModel.DataAnnotations.Required] string Account, [System.ComponentModel.DataAnnotations.Required] string Password,  [FromBody]UpdateUserDTO updateUserDTO)
        {
            try
            {
                var UpdateUser = await _userRepository.UpdateUser(Account, Password, updateUserDTO);
                if (UpdateUser != null)
                {
                    return Ok(UpdateUser);
                }
                else return BadRequest($"Không tìm thấy user có Account : {Account} và Password: {Password}");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Add-Authorize")]
        public async Task<IActionResult> AddAuthorize([System.ComponentModel.DataAnnotations.Required] int id, [FromForm] AddAuthorizeUserDTO addAuthorizeUserDTO)
        {
            try
            {
                var AddAuthorize = await _userRepository.AddAuthorizeUser(id, addAuthorizeUserDTO);
                if(AddAuthorize != null)
                    return Ok(AddAuthorize);
                return BadRequest($"Không tìm thấy user có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete-Authorize")]
        public async Task<IActionResult> DeleteUser ([System.ComponentModel.DataAnnotations.Required] int id)
        {
            try
            {
                var DeleteUser = await _userRepository.DeleteUser(id);
                if(DeleteUser != null)
                    return Ok (DeleteUser);
                return BadRequest($"Không tìm thấy user có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
