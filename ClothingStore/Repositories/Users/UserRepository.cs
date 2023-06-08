using ClothingStore.Data;
using ClothingStore.Models.Domain;
using ClothingStore.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ClothingStore.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserDTO>> GetAllUser()
        {
            var AllUserDomain = _context.User.AsQueryable();
            var UserDomainList = await AllUserDomain.Select(User => new UserDTO
            {
                ID_User = User.ID_User,
                Account = User.Account,
                Password = User.Password,
                Username = User.Username,
                Birthday = User.Birthday,
                Address = User.Address,
                DateCreate = User.DateCreate,
                ID_Authorize = User.ID_Authorize

            }).ToListAsync();
            return UserDomainList;
        }

        public async Task<GetUserAPDTO> GetUserByAccPass(string account, string password)
        {
            var ProductAP = await _context.User!.SingleOrDefaultAsync(u => u.Account == account && u.Password == password);
            if (ProductAP == null)
            {
                return null!;
            }
            var UserList = new GetUserAPDTO
            {

                Username = ProductAP.Username,
                Birthday = ProductAP.Birthday,
                Address = ProductAP.Address,
            };
            return UserList;
        }


        public async Task<RegisterUserDTO> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var userAccount = await _context.User!.SingleOrDefaultAsync(u => u.Account == registerUserDTO.Account);
            if (userAccount != null)
                return null!;
            var UserDomain = new User
            {
                Account = registerUserDTO.Account,
                Password = registerUserDTO.Password,
                DateCreate = DateTime.Now,
                ID_Authorize = 2
            };
            await _context.AddAsync(UserDomain);
            await _context.SaveChangesAsync();
            return registerUserDTO;
        }

        public LoginUser LoginUser(LoginUser loginUser)
        {
            var UserDTO = _context.User!.SingleOrDefault(u => u.Account == loginUser.Account && u.Password == loginUser.Password);
            if (UserDTO == null)
                return null!;
            return loginUser;
        }

        public async Task<UpdateUserDTO> UpdateUser(string Account, string Password, UpdateUserDTO updateUserDTO)
        {
            var UserDomain = await _context.User!.SingleOrDefaultAsync(u => u.Account == Account && u.Password == Password);
            if (UserDomain != null)
            {
                if (!string.IsNullOrEmpty(updateUserDTO.Password))
                    UserDomain.Password = updateUserDTO.Password;
                if (!string.IsNullOrEmpty(updateUserDTO.Username))
                    UserDomain.Username = updateUserDTO.Username;
                if (!string.IsNullOrEmpty(updateUserDTO.Birthday.ToShortDateString()))
                    UserDomain.Birthday = updateUserDTO.Birthday.Date;
                if (!string.IsNullOrEmpty(updateUserDTO.Address))
                    UserDomain.Address = updateUserDTO.Address;
                await _context.SaveChangesAsync();
            }
            else return null!;

            return updateUserDTO;
        }

        public async Task<AddAuthorizeUserDTO> AddAuthorizeUser(int id, AddAuthorizeUserDTO addAuthorizeUserDTO)
        {
            var UserAuthorize = await _context.User!.SingleOrDefaultAsync(u => u.ID_User == id);
            if (UserAuthorize != null)
            {
                UserAuthorize.ID_Authorize = addAuthorizeUserDTO.ID_Authorize;
                await _context.SaveChangesAsync();
            }
            else return null!;
            return addAuthorizeUserDTO;
        }

        public async Task<User> DeleteUser(int id)
        {
            var UserDomain = await _context.User!.SingleOrDefaultAsync(User => User.ID_User == id);
            if (UserDomain != null)
            {
                _context.User!.Remove(UserDomain);
                await _context.SaveChangesAsync();
            }
            else return null!;
            return UserDomain;
        }
    }
}
