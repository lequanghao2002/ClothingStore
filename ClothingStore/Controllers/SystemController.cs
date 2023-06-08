using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClothingStore.Repositories.Token;
using ClothingStore.Models.System;
using System.Security.Claims;
using ClothingStore.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public SystemController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        // Get all user with roles
        [HttpGet("Get-all-user-in-the-system")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserWithRole()
        {
            try
            {
               var users =await _userManager.Users.ToListAsync();
                if (users != null)
                {
                    var userDTOs = new List<UserWithRoleDTO>();
                    foreach (var user in users)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var userDTO = new UserWithRoleDTO
                        {
                            Id = user.Id,
                            Username = user.UserName,
                            Roles = roles.SingleOrDefault()
                        };
                        userDTOs.Add(userDTO);
                    }
                    return Ok(userDTOs);
                }               
                return BadRequest("User not found");
            }
            catch
            {
                return BadRequest();
            }
        }

        // Register in the system
        [HttpPost ("Register-in-the-System")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerRequestDTO.UserName,
                    Email = registerRequestDTO.UserName
                };
                var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
                if (identityResult.Succeeded)
                {
                    // add role: default is read, id = 2
                    var role = "Read";
                    identityResult = await _userManager.AddToRoleAsync(identityUser, role);
                    if (identityResult.Succeeded)
                    {
                        return Ok("Register successful! Let login!");
                    }
                }
                return BadRequest("Something wrong! Can Account is valid");
            }
            catch
            {
                return BadRequest();
            }
        }

        // Login to the system
        [HttpPost("Login-to-the-System")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
                if (user != null)
                {
                    var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                    if (checkPasswordResult)
                    {
                        var role = await _userManager.GetRolesAsync(user);
                        if (role != null)
                        {
                            var jwtToken =  _tokenRepository.CreateJWTToken(user, role.SingleOrDefault()!);
                            var response = new LoginResponseDTO
                            {
                                JwtToken = jwtToken
                            };
                            return Ok(response);
                        }
                    }
                }
                return BadRequest("Username or Password incorrect!");
            }
            catch
            {
                return BadRequest();
            }
        }

        // update permission for user in system
        [HttpPut("Update-permission")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePermisstion ([Required]string Id, [Required]string roles)
        {
            try
            { 
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                var existingrole = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRoleAsync(user, existingrole.SingleOrDefault());
                if (!result.Succeeded)
                    return BadRequest("Failed to remove existing role");
                result = await _userManager.AddToRoleAsync(user, roles);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to add new role");
                }
                return Ok("User roles updated successfully");
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
